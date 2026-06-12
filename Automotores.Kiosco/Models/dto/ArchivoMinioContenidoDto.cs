namespace Automotores.Kiosco.Models.dto
{
    public class ArchivoMinioContenidoDto
    {
        public Stream Contenido { get; set; } = Stream.Null;
        public string ContentType { get; set; } = "application/octet-stream";
        public string NombreArchivo { get; set; } = string.Empty;
    }
}