namespace Automotores.Kiosco.Models.dto
{
    public class TurnoKioscoDto
    {
        public decimal AsgCodigo { get; set; }
        public string Turno { get; set; } = "";
        public decimal AgenciaId { get; set; }
        public decimal ClCodigo { get; set; }
        public string Identificacion { get; set; } = "";
        public string Cliente { get; set; } = "";
        public string Estado { get; set; } = "";
        public string Modulo { get; set; } = "";
        public int PersonasPorDelante { get; set; }
        public int TiempoEstimadoMinutos { get; set; }
        public DateTime Fecha { get; set; }
    }
}