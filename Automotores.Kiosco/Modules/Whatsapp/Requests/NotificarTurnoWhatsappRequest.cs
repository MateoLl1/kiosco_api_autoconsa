namespace Automotores.Kiosco.Modules.Whatsapp.Requests
{
    public class NotificarTurnoWhatsappRequest
    {
        public string NumeroEnvio { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;
        public string Turno { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
    }
}