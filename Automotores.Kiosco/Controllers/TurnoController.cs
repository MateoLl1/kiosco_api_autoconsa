using Automotores.Kiosco.Models.request;
using Automotores.Kiosco.Services;
using Microsoft.AspNetCore.Mvc;

namespace Automotores.Kiosco.Controllers
{
    public class TurnosController : BaseController
    {
        private readonly TurnoService _turnoService;
        private readonly TurnoGeneradorService _turnoGeneradorService;
        private readonly TurnoConCitaService _turnoConCitaService;

        public TurnosController(
            TurnoService turnoService,
            TurnoGeneradorService turnoGeneradorService,
            TurnoConCitaService turnoConCitaService)
        {
            _turnoService = turnoService;
            _turnoGeneradorService = turnoGeneradorService;
            _turnoConCitaService = turnoConCitaService;
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

        [HttpPost("sin-cita")]
        public async Task<IActionResult> GenerarSinCita([FromQuery] decimal agenciaId)
        {
            if (agenciaId <= 0)
            {
                return BadRequest(new
                {
                    mensaje = "La agencia es requerida."
                });
            }

            var turno = await _turnoGeneradorService.GenerarSinCitaAsync(agenciaId);

            return Ok(turno);
        }

        [HttpPost("sin-cita-flotas")]
        public async Task<IActionResult> GenerarSinCitaFlotas([FromQuery] decimal agenciaId)
        {
            if (agenciaId <= 0)
            {
                return BadRequest(new
                {
                    mensaje = "La agencia es requerida."
                });
            }

            var turno = await _turnoGeneradorService.GenerarSinCitaFlotaAsync(agenciaId);

            return Ok(turno);
        }

        [HttpPost("recepcion/registrar-llegada")]
        public async Task<IActionResult> RegistrarLlegada([FromBody] RegistrarLlegadaCitaRequest request)
        {
            if (request == null)
            {
                return BadRequest(new
                {
                    resultado = "error",
                    codigo = "REQUEST_INVALIDO",
                    mensaje = "La solicitud es requerida."
                });
            }

            var resultado = await _turnoConCitaService.RegistrarLlegadaAsync(request.AgenciaId, request.CitaId);

            if (resultado.Resultado == "error")
            {
                if (resultado.Codigo == "AGENCIA_REQUERIDA" ||
                    resultado.Codigo == "CITA_REQUERIDA" ||
                    resultado.Codigo == "NO_EXISTE" ||
                    resultado.Codigo == "REQUEST_INVALIDO")
                {
                    return BadRequest(resultado);
                }

                if (resultado.Codigo == "ERR" || resultado.Codigo == "HT")
                {
                    return Conflict(resultado);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, resultado);
            }

            return Ok(resultado);
        }



        [HttpGet("por-identificacion")]
        public async Task<IActionResult> ObtenerTurnoPorIdentificacion(
            [FromQuery] string identificacion,
            [FromQuery] decimal agenciaId)
        {
            if (string.IsNullOrWhiteSpace(identificacion))
            {
                return BadRequest(new
                {
                    mensaje = "La identificación es requerida."
                });
            }

            if (agenciaId <= 0)
            {
                return BadRequest(new
                {
                    mensaje = "La agencia es requerida."
                });
            }

            var resultado = await _turnoService.ObtenerTurnoPorIdentificacionAsync(
                identificacion,
                agenciaId
            );

            if (resultado == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró un turno asociado a la identificación enviada."
                });
            }

            return Ok(resultado);
        }


    }
}