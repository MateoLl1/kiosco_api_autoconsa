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

        
    }
}