using API_Videoteca_Placas.Models;
using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace API_Videoteca_Placas.Logic
{
    public class ListarDirectorio
    {

        public List<Directorio> Directorio(string directory, SftpClient sftp_cliente)
        {
            try
            {
                List<Directorio> listdirectorio = new();

                //using SftpClient cliente = new (new PasswordConnectionInfo (CredencialesModel.Get_Host(), CredencialesModel.Get_Username(), CredencialesModel.Get_Password()));

                if (!sftp_cliente.Exists(directory)) return new List<Directorio>(); 

                IEnumerable<SftpFile> paths = sftp_cliente.ListDirectory(directory); //  Directory.GetFiles("/");

                foreach (var path in paths)
                {
                    Directorio directorio = new();

                    if (path.IsDirectory && !path.Name.Equals("..") && !path.Name.Equals("."))
                    {
                        directorio.Isdirectory = true;
                        directorio.Ruta = path.FullName;
                        //directorio.directorioHijo = Directorio(path.FullName, sftp_cliente);
                    }
                    else
                    {
                        
                           directorio.Isdirectory = false;
                            directorio.Ruta = path.FullName;                    
                       
                    }
                    if (!path.Name.Equals("..") && !path.Name.Equals("."))
                    {
                        listdirectorio.Add(directorio);
                    }
                }


                return listdirectorio;
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