using Automotores.Kiosco.Services;
using Microsoft.AspNetCore.Mvc;

namespace Automotores.Kiosco.Controllers
{
    public class AgenciasController : BaseController
    {
        private readonly AgenciaService _agenciaService;

        public AgenciasController(AgenciaService agenciaService)
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