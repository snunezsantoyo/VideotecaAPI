using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using API_Videoteca_Placas.Models;
using Renci.SshNet;
using API_Videoteca_Placas.Logic;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Renci.SshNet.Sftp;
using System.IO;

namespace API_Videoteca_Placas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideotecaController : ControllerBase
    {


        [HttpPost]
        [Route("Directorio")]
        public ActionResult Get_Directorio(URLModel URL)
        {
            try
            {
                using SftpClient cliente = new(new PasswordConnectionInfo(CredencialesModel.Get_Host(), CredencialesModel.Get_Username(), CredencialesModel.Get_Password()));
                cliente.Connect();
                URL.URL = String.IsNullOrEmpty(URL.URL) ? "/media/archivos/videos" : URL.URL;
                Directorio ruta = new VideotecaLogic().Directorio(URL.URL, cliente);


                cliente.Disconnect();

                return Ok(ruta);

            }
            catch (Exception)
            {
                return BadRequest();

            }


        }



        [HttpPost]
        [Route("ObtenerVideo")]
        public ActionResult Get_Video(URLModel url)
        {
            try
            {
                FileModel video = new VideotecaLogic().GetVideo(url.URL);

                return Ok(video);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    }
}
