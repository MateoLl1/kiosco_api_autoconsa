using Automotores.Kiosco.Modules.Mostrador.Requests;
using Automotores.Kiosco.Modules.Mostrador.Services;
using Microsoft.AspNetCore.Mvc;

namespace Automotores.Kiosco.Controllers
{
    public class MostradorController : BaseController
    {
        private readonly DisponibilidadService _disponibilidadService;
        private readonly TurnoMostradorService _turnoMostradorService;

        public MostradorController(
            DisponibilidadService disponibilidadService,
            TurnoMostradorService turnoMostradorService)
        {
            _disponibilidadService = disponibilidadService;
            _turnoMostradorService = turnoMostradorService;
        }

        [HttpPost("disponibilidad/toggle")]
        public async Task<IActionResult> Toggle([FromBody] ToggleDisponibilidadRequest request)
        {
            if (request.UsCodigo <= 0 || request.AgenciaId <= 0 || request.GnCodigo <= 0)
                return BadRequest(new { mensaje = "UsCodigo, AgenciaId y GnCodigo son requeridos." });

            var resultado = await _disponibilidadService.ToggleAsync(
                request.UsCodigo,
                request.AgenciaId,
                request.GnCodigo);

            return Ok(resultado);
        }

        [HttpGet("disponibilidad/estado")]
        public async Task<IActionResult> GetEstado(
            [FromQuery] decimal usCodigo,
            [FromQuery] decimal agenciaId,
            [FromQuery] decimal gnCodigo)
        {
            if (usCodigo <= 0 || agenciaId <= 0 || gnCodigo <= 0)
                return BadRequest(new { mensaje = "usCodigo, agenciaId y gnCodigo son requeridos." });

            var resultado = await _disponibilidadService.GetEstadoAsync(usCodigo, agenciaId, gnCodigo);

            if (resultado == null)
                return NotFound(new { mensaje = "No se encontró disponibilidad para los parámetros indicados." });

            return Ok(resultado);
        }

        [HttpGet("disponibilidad/historico")]
        public async Task<IActionResult> GetHistorico(
            [FromQuery] decimal usCodigo,
            [FromQuery] decimal agenciaId,
            [FromQuery] decimal gnCodigo)
        {
            if (usCodigo <= 0 || agenciaId <= 0 || gnCodigo <= 0)
                return BadRequest(new { mensaje = "usCodigo, agenciaId y gnCodigo son requeridos." });

            var resultado = await _disponibilidadService.GetHistoricoAsync(usCodigo, agenciaId, gnCodigo);

            return Ok(resultado);
        }

        [HttpPost("turno")]
        public async Task<IActionResult> GenerarTurno([FromBody] CrearTurnoMostradorRequest request)
        {
            if (request == null || request.AgenciaId <= 0)
                return BadRequest(new { mensaje = "La agencia es requerida." });

            var turno = await _turnoMostradorService.GenerarAsync(
                request.AgenciaId,
                request.Identificacion);

            return Ok(turno);
        }
    }
}
