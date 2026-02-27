using Automotores.Kiosco.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Automotores.Kiosco.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DataBookController : ControllerBase
    {
        
        private readonly DatabookService _databook;

        public DataBookController (DatabookService databook)
        {
            _databook = databook;
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _databook.ObtenerDatosAsync();

            if (resultado == null)
            {
                return StatusCode(500, "Error al consumir la API");
            }

            return Ok(resultado);
        }

    }
}
