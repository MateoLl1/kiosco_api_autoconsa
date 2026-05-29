namespace Automotores.Kiosco.Models.dto
{
    public class WhatsappPlantillaDto
    {
        public string NumeroEnvio { get; set; } = string.Empty;
        public int Campania { get; set; }
        public List<string> Variables { get; set; } = new();
    }
}