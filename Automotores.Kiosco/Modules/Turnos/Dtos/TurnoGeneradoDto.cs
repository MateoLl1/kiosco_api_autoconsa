namespace Automotores.Kiosco.Modules.Turnos.Dtos
{
    public class TurnoGeneradoDto
    {
        public decimal AsgCodigo { get; set; }
        public decimal? TkCodigo { get; set; }
        public string Turno { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Modulo { get; set; } = string.Empty;
        public decimal AgenciaId { get; set; }
        public int PersonasPorDelante { get; set; }
        public int TiempoEstimadoMinutos { get; set; }
        public DateTime Fecha { get; set; }

        public string TelefonoCliente { get; set; } = string.Empty;
        public string NombreCliente { get; set; } = string.Empty;
    }
}