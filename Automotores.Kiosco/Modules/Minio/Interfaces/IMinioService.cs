using Automotores.Kiosco.Modules.Minio.Dtos;

namespace Automotores.Kiosco.Modules.Minio.Interfaces;
public interface IMinioService
{
    Task<List<BucketMinioDto>> ListarBucketsAsync();
    Task CrearBucketAsync(string bucket);
    Task<List<ArchivoMinioDto>> ListarObjetosAsync(string bucket);
    Task<ArchivoMinioDto> SubirObjetoAsync(string bucket, IFormFile archivo);
    Task<string> ObtenerUrlAsync(string id);
    Task EliminarObjetoAsync(string id);

    Task<ArchivoMinioContenidoDto> DescargarObjetoAsync(string id);
}