namespace API_Videoteca_Placas.Models
{

    public class Directorio
    {
        public string? Nombre { get; set; }

        public List<FileModel>? Archivos  { get; set; }   

        // public List<Directorio>? directorioHijo { get; set; }
    }


}
