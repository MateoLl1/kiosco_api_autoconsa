using Automotores.Kiosco.Models.request;
using Automotores.Kiosco.Services;
using Microsoft.AspNetCore.Mvc;

namespace Automotores.Kiosco.Controllers
{
    public class PantallaTurnosController : BaseController
    {
        private readonly PantallaTurnosService _pantallaTurnosService;

        public PantallaTurnosController(PantallaTurnosService pantallaTurnosService)
        {
            _pantallaTurnosService = pantallaTurnosService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTurnosPantalla([FromQuery] decimal agenciaId)
        {
            if (agenciaId <= 0)
            {
                return BadRequest(new
                {
                    resultado = "error",
                    codigo = "AGENCIA_REQUERIDA",
                    mensaje = "La agencia es requerida."
                });
            }

            var lista = await _pantallaTurnosService.ObtenerTurnosPantallaAsync(agenciaId);
            return Ok(lista);
        }

        [HttpPost("marcar-mostrado")]
        public async Task<IActionResult> MarcarTurnoMostrado([FromBody] MarcarTurnoMostradoRequest request)
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

            var resultado = await _pantallaTurnosService.MarcarTurnoMostradoAsync(request.AsgCodigo);

            if (resultado.Resultado == "error")
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }
    }
}