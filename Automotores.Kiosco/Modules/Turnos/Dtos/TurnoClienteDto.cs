namespace Automotores.Kiosco.Modules.Turnos.Dtos
{
    public class TurnoClienteDto
    {
        public decimal AsgCodigo { get; set; }
        public string Turno { get; set; } = "";
        public string Estado { get; set; } = "";
        public string Modulo { get; set; } = "";
        public decimal AgenciaId { get; set; }
        public decimal? Tiempo { get; set; }
        public decimal? TiempoEspera { get; set; }
        public DateTime? FechaMovimiento { get; set; }
        public DateTime? FechaAsignacion { get; set; }
        public decimal ClCodigo { get; set; }
        public string Identificacion { get; set; } = "";
        public string Cliente { get; set; } = "";
        public decimal? CitaId { get; set; }
        public DateTime? FechaCita { get; set; }
        public string HoraCita { get; set; } = "";
        public string Tipo { get; set; } = "";

        public string Area { get; set; } = "";
        public int PersonasPorDelante { get; set; }
        public int TiempoEstimadoMinutos { get; set; }

        public string TelefonoCliente { get; set; } = string.Empty;
    }
}