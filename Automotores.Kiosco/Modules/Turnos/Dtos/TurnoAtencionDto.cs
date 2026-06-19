namespace Automotores.Kiosco.Modules.Turnos.Dtos
{
    public class TurnoAtencionDto
    {
        public decimal AsgCodigo { get; set; }
        public string Turno { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Modulo { get; set; } = string.Empty;
        public decimal AgenciaId { get; set; }
        public DateTime? FechaMovimiento { get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }
}