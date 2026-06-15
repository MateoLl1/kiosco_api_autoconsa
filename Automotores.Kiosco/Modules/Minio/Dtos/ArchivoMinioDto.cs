namespace Automotores.Kiosco.Modules.Minio.Dtos;

public class ArchivoMinioDto
{
    public string Id { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Bucket { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string Extension { get; set; } = string.Empty;
    public long TamanioBytes { get; set; }
    public DateTime? FechaModificacion { get; set; }
}