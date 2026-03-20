using Automotores.Kiosco.Services;
using Microsoft.AspNetCore.Mvc;

namespace Automotores.Kiosco.Controllers
{
    [ApiController]
    [Route("api/v1/agencias")]
    public class AgenciaController : ControllerBase
    {
        private readonly AgenciaService _agenciaService;

        public AgenciaController(AgenciaService agenciaService)
        {
            _agenciaService = agenciaService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            var lista = await _agenciaService.ObtenerTodasAsync();
            return Ok(lista);
        }
    }
}