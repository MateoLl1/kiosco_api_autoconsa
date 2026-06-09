namespace Automotores.Kiosco.Models.dto;

public class ArchivoMinioDto
{
    public string NombreObjeto { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public long TamanioBytes { get; set; }
    public DateTime? FechaModificacion { get; set; }
}