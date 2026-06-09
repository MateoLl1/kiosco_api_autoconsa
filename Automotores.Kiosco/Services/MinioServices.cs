using Automotores.Kiosco.Models.dto;
using Automotores.KIOSCO.API.Options;
using Automotores.KIOSCO.API.Services.Interfaces;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using System.Text.RegularExpressions;

namespace Automotores.KIOSCO.API.Services;

public class MinioService : IMinioService
{
    private readonly IMinioClient _minioClient;
    private readonly MinioOptions _options;

    private readonly Dictionary<string, string[]> _extensionesPermitidas = new()
    {
        { "imagen", new[] { ".jpg", ".jpeg", ".png", ".webp" } },
        { "video", new[] { ".mp4", ".webm" } },
        { "audio", new[] { ".mp3", ".wav", ".ogg", ".m4a" } },
        { "documento", new[] { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".txt", ".csv" } }
    };

    public MinioService(IOptions<MinioOptions> options)
    {
        _options = options.Value;

        if (string.IsNullOrWhiteSpace(_options.Endpoint))
            throw new InvalidOperationException("La configuración Minio:Endpoint es obligatoria.");

        if (string.IsNullOrWhiteSpace(_options.AccessKey))
            throw new InvalidOperationException("La configuración Minio:AccessKey es obligatoria.");

        if (string.IsNullOrWhiteSpace(_options.SecretKey))
            throw new InvalidOperationException("La configuración Minio:SecretKey es obligatoria.");

        _minioClient = new MinioClient()
            .WithEndpoint(_options.Endpoint)
            .WithCredentials(_options.AccessKey, _options.SecretKey)
            .WithSSL(_options.UseSsl)
            .Build();
    }

    public async Task<List<BucketMinioDto>> ListarBucketsAsync()
    {
        var buckets = await _minioClient.ListBucketsAsync();

        return buckets.Buckets.Select(x => new BucketMinioDto
        {
            Nombre = x.Name,
            FechaCreacion = x.CreationDateDateTime
        }).ToList();
    }

    public async Task CrearBucketAsync(string bucket)
    {
        ValidarBucket(bucket);
        await CrearBucketSiNoExisteAsync(bucket);
    }

    public async Task<List<ArchivoMinioDto>> ListarObjetosAsync(string bucket)
    {
        ValidarBucket(bucket);
        await ValidarBucketExisteAsync(bucket);

        var archivos = new List<ArchivoMinioDto>();

        var args = new ListObjectsArgs()
            .WithBucket(bucket)
            .WithRecursive(true);

        await foreach (var item in _minioClient.ListObjectsEnumAsync(args))
        {
            if (!item.IsDir)
            {
                var id = Path.GetFileName(item.Key);

                archivos.Add(new ArchivoMinioDto
                {
                    Id = id,
                    Url = await ObtenerUrlPorNombreObjetoAsync(bucket, item.Key),
                    Tipo = ObtenerTipo(item.Key),
                    Extension = Path.GetExtension(item.Key).ToLower(),
                    TamanioBytes = checked((long)item.Size),
                    FechaModificacion = item.LastModifiedDateTime
                });
            }
        }

        return archivos;
    }

    public async Task<ArchivoMinioDto> SubirObjetoAsync(string bucket, IFormFile archivo)
    {
        ValidarBucket(bucket);
        await CrearBucketSiNoExisteAsync(bucket);
        ValidarArchivo(archivo);

        var extension = Path.GetExtension(archivo.FileName).ToLower();
        var id = $"{Guid.NewGuid():N}{extension}";

        await using var stream = archivo.OpenReadStream();

        var args = new PutObjectArgs()
            .WithBucket(bucket)
            .WithObject(id)
            .WithStreamData(stream)
            .WithObjectSize(archivo.Length)
            .WithContentType(archivo.ContentType);

        await _minioClient.PutObjectAsync(args);

        return new ArchivoMinioDto
        {
            Id = id,
            Url = await ObtenerUrlPorNombreObjetoAsync(bucket, id),
            Tipo = ObtenerTipo(id),
            Extension = extension,
            TamanioBytes = archivo.Length,
            FechaModificacion = DateTime.Now
        };
    }

    public async Task<string> ObtenerUrlAsync(string id)
    {
        ValidarIdObjeto(id);

        var ubicacion = await BuscarObjetoEnBucketsAsync(id);

        if (ubicacion == null)
            throw new InvalidOperationException("El objeto no existe.");

        return await ObtenerUrlPorNombreObjetoAsync(ubicacion.Bucket, ubicacion.NombreObjeto);
    }

    public async Task EliminarObjetoAsync(string id)
    {
        ValidarIdObjeto(id);

        var ubicacion = await BuscarObjetoEnBucketsAsync(id);

        if (ubicacion == null)
            throw new InvalidOperationException("El objeto no existe.");

        var args = new RemoveObjectArgs()
            .WithBucket(ubicacion.Bucket)
            .WithObject(ubicacion.NombreObjeto);

        await _minioClient.RemoveObjectAsync(args);
    }

    private async Task CrearBucketSiNoExisteAsync(string bucket)
    {
        var existsArgs = new BucketExistsArgs()
            .WithBucket(bucket);

        var existe = await _minioClient.BucketExistsAsync(existsArgs);

        if (!existe)
        {
            var makeArgs = new MakeBucketArgs()
                .WithBucket(bucket);

            await _minioClient.MakeBucketAsync(makeArgs);
        }
    }

    private async Task ValidarBucketExisteAsync(string bucket)
    {
        var existsArgs = new BucketExistsArgs()
            .WithBucket(bucket);

        var existe = await _minioClient.BucketExistsAsync(existsArgs);

        if (!existe)
            throw new InvalidOperationException("El bucket no existe.");
    }

    private async Task<UbicacionObjetoMinio?> BuscarObjetoEnBucketsAsync(string id)
    {
        var buckets = await _minioClient.ListBucketsAsync();

        foreach (var bucket in buckets.Buckets)
        {
            var args = new StatObjectArgs()
                .WithBucket(bucket.Name)
                .WithObject(id);

            try
            {
                await _minioClient.StatObjectAsync(args);

                return new UbicacionObjetoMinio
                {
                    Bucket = bucket.Name,
                    NombreObjeto = id
                };
            }
            catch
            {
            }
        }

        return null;
    }

    private async Task<string> ObtenerUrlPorNombreObjetoAsync(string bucket, string nombreObjeto)
    {
        var args = new PresignedGetObjectArgs()
            .WithBucket(bucket)
            .WithObject(nombreObjeto)
            .WithExpiry(_options.PublicUrlExpirationMinutes * 60);

        return await _minioClient.PresignedGetObjectAsync(args);
    }

    private void ValidarArchivo(IFormFile archivo)
    {
        if (archivo == null || archivo.Length == 0)
            throw new InvalidOperationException("El archivo es obligatorio.");

        var extension = Path.GetExtension(archivo.FileName).ToLower();

        var extensionesValidas = _extensionesPermitidas
            .SelectMany(x => x.Value)
            .ToArray();

        if (!extensionesValidas.Contains(extension))
            throw new InvalidOperationException("El formato del archivo no está permitido.");
    }

    private void ValidarBucket(string bucket)
    {
        if (string.IsNullOrWhiteSpace(bucket))
            throw new InvalidOperationException("El bucket es obligatorio.");

        if (bucket.Length < 3 || bucket.Length > 63)
            throw new InvalidOperationException("El bucket debe tener entre 3 y 63 caracteres.");

        if (!Regex.IsMatch(bucket, "^[a-z0-9][a-z0-9.-]*[a-z0-9]$"))
            throw new InvalidOperationException("El bucket solo puede contener minúsculas, números, puntos y guiones.");

        if (bucket.Contains("..") || bucket.Contains(".-") || bucket.Contains("-."))
            throw new InvalidOperationException("El bucket tiene un formato inválido.");
    }

    private void ValidarIdObjeto(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new InvalidOperationException("El id del objeto es obligatorio.");

        if (id.Contains("/") || id.Contains("\\"))
            throw new InvalidOperationException("El id del objeto no debe contener rutas.");
    }

    private string ObtenerTipo(string nombreObjeto)
    {
        var extension = Path.GetExtension(nombreObjeto).ToLower();

        foreach (var grupo in _extensionesPermitidas)
        {
            if (grupo.Value.Contains(extension))
                return grupo.Key;
        }

        return "desconocido";
    }

    private class UbicacionObjetoMinio
    {
        public string Bucket { get; set; } = string.Empty;
        public string NombreObjeto { get; set; } = string.Empty;
    }
}