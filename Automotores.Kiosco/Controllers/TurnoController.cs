using Automotores.Kiosco.Services;
using Microsoft.AspNetCore.Mvc;

namespace Automotores.Kiosco.Controllers
{
    public class TurnosController : BaseController
    {
        private readonly TurnoService _turnoService;

        public TurnosController(TurnoService turnoService)
        {
            _turnoService = turnoService;
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
    }
}