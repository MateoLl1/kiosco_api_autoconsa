using Automotores.KIOSCO.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Automotores.KIOSCO.API.Controllers;

[ApiController]
[Route("api/v1/minio")]
public class MinioController : ControllerBase
{
    private readonly IMinioService _minioService;

    public MinioController(IMinioService minioService)
    {
        _minioService = minioService;
    }

    [HttpGet("{carpeta}")]
    public async Task<IActionResult> Listar(string carpeta)
    {
        var resultado = await _minioService.ListarAsync(carpeta);
        return Ok(resultado);
    }

    [HttpPost("{carpeta}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Subir(
        string carpeta,
        IFormFile archivo)
    {
        var resultado = await _minioService.SubirAsync(archivo, carpeta);
        return Ok(resultado);
    }

    [HttpGet("url")]
    public async Task<IActionResult> ObtenerUrl(
        [FromQuery] string nombreObjeto)
    {
        var url = await _minioService.ObtenerUrlAsync(nombreObjeto);

        return Ok(new
        {
            Url = url
        });
    }

    [HttpDelete]
    public async Task<IActionResult> Eliminar(
        [FromQuery] string nombreObjeto)
    {
        await _minioService.EliminarAsync(nombreObjeto);

        return NoContent();
    }
}