namespace Automotores.Kiosco.Modules.Mostrador.Dtos
{
    public class DisponibilidadDto
    {
        public decimal DvCodigo { get; set; }
        public string Estado { get; set; } = string.Empty;
        public decimal UsCodigo { get; set; }
        public decimal AgCodigo { get; set; }
        public decimal GnCodigo { get; set; }
        public DateTime? FechaLogin { get; set; }
        public DateTime? FechaUltimaAtencion { get; set; }
        public DateTime? Fecha { get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }
}
