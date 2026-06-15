using Automotores.Kiosco.Modules.Minio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Automotores.Kiosco.Controllers;

[ApiController]
public class MinioController : BaseController
{
    private readonly IMinioService _minioService;

    public MinioController(IMinioService minioService)
    {
        _minioService = minioService;
    }

    [HttpGet("buckets")]
    public async Task<IActionResult> ListarBuckets()
    {
        var resultado = await _minioService.ListarBucketsAsync();
        return Ok(resultado);
    }

    [HttpPost("buckets/{bucket}")]
    public async Task<IActionResult> CrearBucket(string bucket)
    {
        await _minioService.CrearBucketAsync(bucket);
        return Ok(new { Mensaje = "Bucket creado correctamente." });
    }

    [HttpGet("{bucket}/objetos")]
    public async Task<IActionResult> ListarObjetos(string bucket)
    {
        var resultado = await _minioService.ListarObjetosAsync(bucket);
        return Ok(resultado);
    }

    [HttpPost("{bucket}/objetos")]
    [Consumes("multipart/form-data")]
    [RequestSizeLimit(524288000)]
    [RequestFormLimits(MultipartBodyLengthLimit = 524288000)]
    public async Task<IActionResult> SubirObjeto(string bucket, IFormFile archivo)
    {
        var resultado = await _minioService.SubirObjetoAsync(bucket, archivo);
        return Ok(resultado);
    }

    [HttpGet("objetos/{id}/url")]
    public async Task<IActionResult> ObtenerUrl(string id)
    {
        var url = await _minioService.ObtenerUrlAsync(id);
        return Ok(new { Url = url });
    }

    [HttpGet("objetos/{id}/contenido")]
    public async Task<IActionResult> ObtenerContenido(string id)
    {
        try
        {
            var archivo = await _minioService.DescargarObjetoAsync(id);

            return File(
                archivo.Contenido,
                archivo.ContentType,
                enableRangeProcessing: true
            );
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { Mensaje = ex.Message });
        }
    }

    [HttpDelete("objetos/{id}")]
    public async Task<IActionResult> EliminarObjeto(string id)
    {
        await _minioService.EliminarObjetoAsync(id);
        return NoContent();
    }
}