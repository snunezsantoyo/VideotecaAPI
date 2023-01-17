using API_Videoteca_Placas.Models;
using Newtonsoft.Json;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;

namespace API_Videoteca_Placas.Logic
{
    public class VideotecaLogic
    {

        public Directorio Directorio(string directory, SftpClient sftp_cliente)
        {
            try
            {
                Directorio directorio = new();
                List<FileModel> archivos = new();

                //using SftpClient cliente = new (new PasswordConnectionInfo (CredencialesModel.Get_Host(), CredencialesModel.Get_Username(), CredencialesModel.Get_Password()));

                if (!sftp_cliente.Exists(directory) || !sftp_cliente.Get(directory).IsDirectory) return new Directorio();

                directorio.Nombre = sftp_cliente.Get(directory).FullName;

                IEnumerable<SftpFile> paths = sftp_cliente.ListDirectory(directory); //  Directory.GetFiles("/");


                foreach (var path in paths)
                {
                    FileModel archivo = new();

                    if (path.IsDirectory)
                    {
                        archivo.Name = path.Name;
                        archivo.Ruta = path.FullName;
                        archivo.IsDirectory = true;
                    }
                    else
                    {
                        archivo.Name = path.Name;

                        archivo.IsDirectory = false;
                        archivo.Ruta = path.FullName;
                    }

                    if (!path.Name.Equals("..") && !path.Name.Equals("."))
                    {
                        archivos.Add(archivo);
                    }

                }
                directorio.Archivos = archivos;
                return directorio;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public FileModel GetVideo(string path)
        {
            FileModel video = new();


            try
            {
                using SftpClient cliente = new(new PasswordConnectionInfo(CredencialesModel.Get_Host(), CredencialesModel.Get_Username(), CredencialesModel.Get_Password()));
                cliente.Connect();

                if (!cliente.Exists(path))
                    throw new Exception("Archivo no encontrado.");

                SftpFileStream ftpFileStream = cliente.OpenRead(path);

                video.Name = ftpFileStream.Name;

                using (ftpFileStream)
                {
                    byte[] buffer = new byte[ftpFileStream.Length];
                    ftpFileStream.Read(buffer, 0, (int)ftpFileStream.Length);
                    video.Base64 = Convert.ToBase64String(buffer);
                }

                ftpFileStream.Close();
                cliente.Disconnect();
            }
            catch (Exception)
            {
                throw;
            }

            return video;
        }
    }
}