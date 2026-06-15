using Automotores.Kiosco.Data;
using Automotores.Kiosco.Models;
using Automotores.Kiosco.Modules.Minio.Dtos;
using Automotores.Kiosco.Modules.Minio.Interfaces;
using Automotores.Kiosco.Modules.TurnoMedia.Dtos;
using Automotores.Kiosco.Modules.TurnoMedia.Requests;
using Microsoft.EntityFrameworkCore;

namespace Automotores.Kiosco.Modules.TurnoMedia.Services
{
    public class TurneroMediaService
    {
        private readonly DataContext _context;
        private readonly IMinioService _minioService;

        public TurneroMediaService(
            DataContext context,
            IMinioService minioService)
        {
            _context = context;
            _minioService = minioService;
        }

        public async Task<List<ArchivoMinioDto>> ListarPorBucketAsync(string bucket)
        {
            if (string.IsNullOrWhiteSpace(bucket))
                throw new InvalidOperationException("El bucket es requerido.");

            return await _minioService.ListarObjetosAsync(bucket);
        }

        public async Task<List<TurneroMediaDto>> ListarPorAgenciaAsync(int agenciaId, string? estado)
        {
            if (agenciaId <= 0)
                throw new InvalidOperationException("La agencia es requerida.");

            var estadoFiltro = string.IsNullOrWhiteSpace(estado)
                ? "A"
                : estado.Trim().ToUpper();

            var lista = await _context.SI_TURNERO_MEDIA
                .AsNoTracking()
                .Where(x =>
                    x.AgCodigo == agenciaId &&
                    x.TmEstado == estadoFiltro)
                .OrderBy(x => x.TmOrden)
                .ThenBy(x => x.TmCodigo)
                .ToListAsync();

            return lista.Select(Mapear).ToList();
        }

        public async Task<TurneroMediaDto> CrearAsync(CrearTurneroMediaRequest request, IFormFile archivo)
        {
            ValidarCrear(request, archivo);

            ArchivoMinioDto? archivoSubido = null;

            try
            {
                archivoSubido = await _minioService.SubirObjetoAsync(
                    request.Bucket.Trim(),
                    archivo
                );

                var tipo = archivoSubido.Tipo.Trim().ToLower();

                if (tipo != "imagen" && tipo != "video")
                    throw new InvalidOperationException("Solo se permiten imágenes o videos para el turnero.");

                await ValidarReglasAsync(request.AgenciaId, tipo);

                var orden = request.Orden ?? await ObtenerSiguienteOrdenAsync(request.AgenciaId);

                var entidad = new SI_TURNERO_MEDIA
                {
                    AgCodigo = request.AgenciaId,
                    UsCodigo = request.UsuarioId,
                    TmBucket = request.Bucket.Trim(),
                    TmObjId = archivoSubido.Id,
                    TmTipo = tipo,
                    TmOrden = orden,
                    TmEstado = "A",
                    TmFecha = DateTime.Now,
                    TmModificacion = null
                };

                _context.SI_TURNERO_MEDIA.Add(entidad);
                await _context.SaveChangesAsync();

                var resultado = Mapear(entidad);
                resultado.Url = archivoSubido.Url;

                return resultado;
            }
            catch
            {
                if (archivoSubido != null && !string.IsNullOrWhiteSpace(archivoSubido.Id))
                {
                    try
                    {
                        await _minioService.EliminarObjetoAsync(archivoSubido.Id);
                    }
                    catch
                    {
                    }
                }

                throw;
            }
        }

        public async Task<bool> EliminarAsync(long id, EliminarTurneroMediaRequest request)
        {
            if (id <= 0)
                throw new InvalidOperationException("El código es requerido.");

            var entidad = await _context.SI_TURNERO_MEDIA
                .FirstOrDefaultAsync(x => x.TmCodigo == id);

            if (entidad == null || entidad.TmEstado == "E")
                return false;

            if (request.EliminarMinio && !string.IsNullOrWhiteSpace(entidad.TmObjId))
            {
                await _minioService.EliminarObjetoAsync(entidad.TmObjId);
            }

            entidad.TmEstado = "E";
            entidad.UsCodigo = request.UsuarioId ?? entidad.UsCodigo;
            entidad.TmModificacion = DateTime.Now;

            await _context.SaveChangesAsync();

            return true;
        }

        private async Task ValidarReglasAsync(int agenciaId, string tipo)
        {
            var tipoNormalizado = tipo.Trim().ToLower();

            if (tipoNormalizado == "imagen")
            {
                var cantidadImagenes = await _context.SI_TURNERO_MEDIA
                    .CountAsync(x =>
                        x.AgCodigo == agenciaId &&
                        x.TmTipo == "imagen" &&
                        x.TmEstado == "A");

                if (cantidadImagenes >= 5)
                    throw new InvalidOperationException("Solo se permiten 5 imágenes activas por agencia.");
            }

            if (tipoNormalizado == "video")
            {
                var cantidadVideos = await _context.SI_TURNERO_MEDIA
                    .CountAsync(x =>
                        x.AgCodigo == agenciaId &&
                        x.TmTipo == "video" &&
                        x.TmEstado == "A");

                if (cantidadVideos >= 2)
                    throw new InvalidOperationException("Solo se permite 2 video activo por agencia.");
            }
        }

        private async Task<int> ObtenerSiguienteOrdenAsync(int agenciaId)
        {
            var ultimoOrden = await _context.SI_TURNERO_MEDIA
                .Where(x =>
                    x.AgCodigo == agenciaId &&
                    x.TmEstado != "E")
                .MaxAsync(x => (int?)x.TmOrden);

            return (ultimoOrden ?? 0) + 1;
        }

        private static void ValidarCrear(CrearTurneroMediaRequest request, IFormFile archivo)
        {
            if (request == null)
                throw new InvalidOperationException("La solicitud es requerida.");

            if (request.AgenciaId <= 0)
                throw new InvalidOperationException("La agencia es requerida.");

            if (string.IsNullOrWhiteSpace(request.Bucket))
                throw new InvalidOperationException("El bucket es requerido.");

            if (archivo == null || archivo.Length == 0)
                throw new InvalidOperationException("El archivo es requerido.");
        }

        private static TurneroMediaDto Mapear(SI_TURNERO_MEDIA entidad)
        {
            return new TurneroMediaDto
            {
                Codigo = entidad.TmCodigo,
                AgenciaId = entidad.AgCodigo,
                UsuarioId = entidad.UsCodigo,
                Bucket = entidad.TmBucket ?? string.Empty,
                ObjetoId = entidad.TmObjId ?? string.Empty,
                Tipo = entidad.TmTipo ?? string.Empty,
                Orden = entidad.TmOrden,
                Estado = entidad.TmEstado ?? string.Empty,
                Fecha = entidad.TmFecha,
                Modificacion = entidad.TmModificacion
            };
        }



        public async Task ReordenarAsync(ReordenarTurneroMediaRequest request)
        {
            if (request.AgenciaId <= 0)
                throw new InvalidOperationException("La agencia es requerida.");

            if (request.Items == null || request.Items.Count == 0)
                throw new InvalidOperationException("No existen elementos para reordenar.");

            var codigos = request.Items
                .Select(x => x.Codigo)
                .Distinct()
                .ToList();

            var registros = await _context.SI_TURNERO_MEDIA
                .Where(x =>
                    x.AgCodigo == request.AgenciaId &&
                    x.TmEstado == "A" &&
                    codigos.Contains(x.TmCodigo))
                .ToListAsync();

            if (registros.Count != codigos.Count)
                throw new InvalidOperationException("Uno o más registros no pertenecen a la agencia seleccionada.");

            var orden = 1;

            foreach (var item in request.Items.OrderBy(x => x.Orden))
            {
                var registro = registros.FirstOrDefault(x => x.TmCodigo == item.Codigo);

                if (registro == null)
                    continue;

                registro.TmOrden = orden;
                registro.TmModificacion = DateTime.Now;

                orden++;
            }

            await _context.SaveChangesAsync();
        }

    }
}