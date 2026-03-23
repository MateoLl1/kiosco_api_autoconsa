using Automotores.Kiosco.Services;
using Microsoft.AspNetCore.Mvc;

namespace Automotores.Kiosco.Controllers
{
    public class TurnosController : BaseController
    {
        private readonly TurnoService _turnoService;
        private readonly TurnoGeneradorService _turnoGeneradorService;

        public TurnosController(
            TurnoService turnoService,
            TurnoGeneradorService turnoGeneradorService)
        {
            _turnoService = turnoService;
            _turnoGeneradorService = turnoGeneradorService;
        }

        [HttpGet("recepcion")]
        public async Task<IActionResult> ObtenerCitasRecepcion([FromQuery] decimal agenciaId)
        {
            if (agenciaId <= 0)
            {
                return BadRequest(new { mensaje = "La agencia es requerida." });
            }

            var lista = await _turnoService.ObtenerCitasRecepcionAsync(agenciaId);
            return Ok(lista);
        }

        // 🔥 NUEVO: SIN CITA
        [HttpPost("sin-cita")]
        public async Task<IActionResult> GenerarSinCita([FromQuery] decimal agenciaId)
        {
            if (agenciaId <= 0)
            {
                return BadRequest(new { mensaje = "La agencia es requerida." });
            }

            var turno = await _turnoGeneradorService.GenerarSinCitaAsync(agenciaId);

            return Ok(new
            {
                turno = turno.Turno,
                codigo = turno.AsgCodigo,
                tipo = turno.Tipo,
                fecha = turno.Fecha
            });
        }

        // 🔥 NUEVO: SIN CITA FLOTAS
        [HttpPost("sin-cita-flotas")]
        public async Task<IActionResult> GenerarSinCitaFlotas([FromQuery] decimal agenciaId)
        {
            if (agenciaId <= 0)
            {
                return BadRequest(new { mensaje = "La agencia es requerida." });
            }

            var turno = await _turnoGeneradorService.GenerarSinCitaFlotaAsync(agenciaId);

            return Ok(new
            {
                turno = turno.Turno,
                codigo = turno.AsgCodigo,
                tipo = turno.Tipo,
                fecha = turno.Fecha
            });
        }
    }
}