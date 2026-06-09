using Automotores.Kiosco.Models.dto;

namespace Automotores.KIOSCO.API.Services.Interfaces;
public interface IMinioService
{
    Task<List<ArchivoMinioDto>> ListarAsync(string carpeta);
    Task<ArchivoMinioDto> SubirAsync(IFormFile archivo, string carpeta);
    Task<string> ObtenerUrlAsync(string nombreObjeto);
    Task EliminarAsync(string nombreObjeto);
}