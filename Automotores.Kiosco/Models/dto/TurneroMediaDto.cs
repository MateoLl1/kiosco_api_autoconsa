namespace Automotores.Kiosco.Models.dto
{
    public class TurneroMediaDto
    {
        public long Codigo { get; set; }
        public int AgenciaId { get; set; }
        public int? UsuarioId { get; set; }
        public string Bucket { get; set; } = string.Empty;
        public string ObjetoId { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public int? Orden { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime? Fecha { get; set; }
        public DateTime? Modificacion { get; set; }
        public string Url { get; set; } = string.Empty;
    }
}