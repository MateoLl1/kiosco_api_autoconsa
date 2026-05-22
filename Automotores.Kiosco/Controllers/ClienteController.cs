using Automotores.Kiosco.Services;
using Microsoft.AspNetCore.Mvc;

namespace Automotores.Kiosco.Controllers
{
    public class ClienteController : BaseController
    {
        private readonly ClienteService _servicio;

        public ClienteController(ClienteService servicio)
        {
            _servicio = servicio;
        }

        [HttpGet("por-identificacion")]
        public async Task<IActionResult> ObtenerPorIdentificacion([FromQuery] string identificacion, [FromQuery] int empresa = 1)
        {
            if (string.IsNullOrWhiteSpace(identificacion))
                return BadRequest("La identificación es obligatoria.");

            var resultado = await _servicio.ObtenerPorIdentificacionAsync(identificacion, empresa);

            if (resultado == null)
                return NotFound("No se encontró información del cliente.");

            return Ok(resultado);
        }
    }
}