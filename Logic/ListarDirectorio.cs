using API_Videoteca_Placas.Models;
using Newtonsoft.Json;
using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace API_Videoteca_Placas.Logic
{
    public class ListarDirectorio
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

               // Console.WriteLine(paths.ElementAt(0).FullName);
                foreach (var path in paths)
                {
                    FileModel archivo = new();

                    if (path.IsDirectory)
                    {
                        //if (!path.Name.Equals("..")) directorio.Nombre = path.FullName;

                        archivo.Ruta = path.FullName;
                        archivo.IsDirectory = true;
                        //directorio.directorioHijo = Directorio(path.FullName, sftp_cliente);
                    }
                    else
                    {
                      
                            archivo.IsDirectory = false;
                            archivo.Ruta = path.FullName;
                        

                    }
                    //string json = JsonConvert.SerializeObject(listdirectorio);
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
            


            //foreach (string file in files)
            //    Console.WriteLine(Path.GetFileName(file));

        }

    }
}