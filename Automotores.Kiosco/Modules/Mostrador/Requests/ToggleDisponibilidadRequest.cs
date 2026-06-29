namespace Automotores.Kiosco.Modules.Mostrador.Requests
{
    public class ToggleDisponibilidadRequest
    {
        public decimal UsCodigo { get; set; }
        public decimal AgenciaId { get; set; }
        public decimal GnCodigo { get; set; }
    }
}
