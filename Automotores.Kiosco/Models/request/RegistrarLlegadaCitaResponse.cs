namespace Automotores.Kiosco.Models.request
{
    public class RegistrarLlegadaCitaResponse
    {
        public string Resultado { get; set; } = string.Empty;
        public string? Turno { get; set; }
        public string? Bahia { get; set; }
        public string? Tecnico { get; set; }
        public string? Codigo { get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }
}