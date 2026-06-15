namespace Automotores.Kiosco.Modules.TurnoMedia.Requests
{
    public class ReordenarTurneroMediaRequest
    {
        public int AgenciaId { get; set; }
        public List<ReordenarTurneroMediaItemRequest> Items { get; set; } = new();
    }

    public class ReordenarTurneroMediaItemRequest
    {
        public long Codigo { get; set; }
        public int Orden { get; set; }
    }
}