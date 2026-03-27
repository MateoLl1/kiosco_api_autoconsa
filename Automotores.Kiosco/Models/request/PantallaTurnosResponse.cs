using Automotores.Kiosco.Models.dto;

namespace Automotores.Kiosco.Models.request
{
    public class PantallaTurnosResponse
    {
        public List<PantallaTurnoDto> Turnos { get; set; } = new();
        public PantallaTurnoDto? TurnoActual { get; set; }
        public List<PantallaTurnoDto> TurnosRecienLlamados { get; set; } = new();
        public List<PantallaTurnoDto> TurnosPendientes { get; set; } = new();
        
    }
}