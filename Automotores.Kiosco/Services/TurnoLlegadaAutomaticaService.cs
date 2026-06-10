namespace Automotores.Kiosco.Services
{
    public class TurnoLlegadaAutomaticaService
    {
        private readonly TurnoService _turnoService;
        private readonly TurnoConCitaService _turnoConCitaService;

        public TurnoLlegadaAutomaticaService(
            TurnoService turnoService,
            TurnoConCitaService turnoConCitaService)
        {
            _turnoService = turnoService;
            _turnoConCitaService = turnoConCitaService;
        }

        public async Task MarcarLlegadaSiTieneCitaAsync(string identificacion, decimal agenciaId)
        {
            if (string.IsNullOrWhiteSpace(identificacion) || agenciaId <= 0)
                return;

            var codigoCita = await _turnoService.ObtenerCodigoCitaActivaPorIdentificacionAsync(
                identificacion,
                agenciaId
            );

            if (!codigoCita.HasValue || codigoCita.Value <= 0)
                return;

            await _turnoConCitaService.RegistrarLlegadaAsync(
                agenciaId,
                codigoCita.Value
            );
        }
    }
}