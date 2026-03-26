
namespace Automotores.Kiosco.Models.dto;

public class PantallaTurnoDto
{
    public decimal AsgCodigo { get; set; }
    public string Turno { get; set; } = string.Empty;
    public string Modulo { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public decimal Tiempo { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public bool RequiereCambioEstado { get; set; }
}

