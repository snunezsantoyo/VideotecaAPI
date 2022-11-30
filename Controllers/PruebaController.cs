using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using API_Videoteca_Placas.Models;
using Renci.SshNet;
using API_Videoteca_Placas.Logic;

namespace API_Videoteca_Placas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PruebaController : ControllerBase
    {

        [HttpPost]
        [Route("Directorio")]
        public ActionResult Get_Directorio(Directorio nuevaRaiz)
        {


            using SftpClient cliente = new(new PasswordConnectionInfo(CredencialesModel.Get_Host(), CredencialesModel.Get_Username(), CredencialesModel.Get_Password()));
            cliente.Connect();

            var ruta = new ListarDirectorio().Directorio(nuevaRaiz.Nombre ?? "/media/archivos/videos", cliente);


            cliente.Disconnect();

            return Ok(ruta);
        }



    }
}
