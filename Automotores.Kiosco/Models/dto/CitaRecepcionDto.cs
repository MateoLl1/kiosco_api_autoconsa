namespace Automotores.Kiosco.Models
{
    public class CitaRecepcionDto
    {
        public decimal CodigoCita { get; set; }
        public string HoraCita { get; set; } = string.Empty;
        public string Placa { get; set; } = string.Empty;
        public string NombreCliente { get; set; } = string.Empty;
        public string ModeloVehiculo { get; set; } = string.Empty;
        public string Bahia { get; set; } = string.Empty;
        public decimal Estatus { get; set; }
        public decimal TlCodigo { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string TipoLabor { get; set; } = string.Empty;
        public string ClaveVisual { get; set; } = string.Empty;
    }
}