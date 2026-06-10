namespace Automotores.Kiosco.Models.request
{
    public class CrearTurneroMediaRequest
    {
        public int AgenciaId { get; set; }
        public int? UsuarioId { get; set; }
        public string Bucket { get; set; } = string.Empty;
        public int? Orden { get; set; }
    }
}