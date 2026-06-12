using Automotores.Kiosco.Controllers;
using Automotores.Kiosco.Models.request;
using Automotores.Kiosco.Services.Turnero;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace Automotores.Kiosco.Controllers
{
    [ApiController]
    public class TurneroMediaController : BaseController
    {
        private readonly TurneroMediaService _turneroMediaService;

        public TurneroMediaController(TurneroMediaService turneroMediaService)
        {
            _turneroMediaService = turneroMediaService;
        }

        [HttpGet("bucket/{bucket}")]
        public async Task<IActionResult> ListarPorBucket(string bucket)
        {
            try
            {
                var resultado = await _turneroMediaService.ListarPorBucketAsync(bucket);
                return Ok(resultado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("agencia/{agenciaId}")]
        public async Task<IActionResult> ListarPorAgencia(
            int agenciaId,
            [FromQuery] string? estado = "A")
        {
            try
            {
                var resultado = await _turneroMediaService.ListarPorAgenciaAsync(agenciaId, estado);

                foreach (var item in resultado)
                {
                    if (!string.IsNullOrWhiteSpace(item.ObjetoId))
                    {
                        item.Url = Url.Action(
                            "ObtenerContenido",
                            "Minio",
                            new { id = item.ObjetoId },
                            Request.Scheme
                        ) ?? string.Empty;
                    }
                }

                return Ok(resultado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(524288000)]
        [RequestFormLimits(MultipartBodyLengthLimit = 524288000)]
        public async Task<IActionResult> Crear([FromForm] CrearTurneroMediaRequest request, IFormFile archivo)
        {
            try
            {
                var resultado = await _turneroMediaService.CrearAsync(request, archivo);

                if (!string.IsNullOrWhiteSpace(resultado.ObjetoId))
                {
                    resultado.Url = Url.Action(
                        "ObtenerContenido",
                        "Minio",
                        new { id = resultado.ObjetoId },
                        Request.Scheme
                    ) ?? string.Empty;
                }

                return Ok(resultado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(long id, [FromBody] EliminarTurneroMediaRequest? request)
        {
            try
            {
                var resultado = await _turneroMediaService.EliminarAsync(
                    id,
                    request ?? new EliminarTurneroMediaRequest()
                );

                if (!resultado)
                {
                    return NotFound(new
                    {
                        mensaje = "No se encontró el registro solicitado."
                    });
                }

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }


        [HttpPut("reordenar")]
        public async Task<IActionResult> Reordenar([FromBody] ReordenarTurneroMediaRequest request)
        {
            try
            {
                await _turneroMediaService.ReordenarAsync(request);

                return Ok(new
                {
                    mensaje = "Orden actualizado correctamente."
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}