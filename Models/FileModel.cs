namespace API_Videoteca_Placas.Models
{
    public class FileModel
    {
        public string? Name { get; set; }
        public string? Extension { get; set; }
        public string? Ruta { get; set; }
        public bool? IsDirectory { get; set; }
        public string? Base64 { get; set; }

    }
}
