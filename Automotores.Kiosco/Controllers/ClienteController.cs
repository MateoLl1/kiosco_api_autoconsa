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

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var lista = await _servicio.ObtenerTodosAsync();
            return Ok(lista);
        }
    }
}