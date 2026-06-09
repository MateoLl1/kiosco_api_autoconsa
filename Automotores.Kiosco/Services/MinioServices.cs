
using Minio;
using Minio.DataModel.Args;
using Microsoft.Extensions.Options;
using Automotores.Kiosco.Models.dto;
using Automotores.KIOSCO.API.Services.Interfaces;
using Automotores.KIOSCO.API.Config;
    
namespace Automotores.KIOSCO.API.Services;

public class MinioService : IMinioService
{
    private readonly IMinioClient _minioClient;
    private readonly MinioOptions _options;

    private readonly string[] _imagenesPermitidas = { ".jpg", ".jpeg", ".png", ".webp" };
    private readonly string[] _videosPermitidos = { ".mp4", ".webm" };

    public MinioService(IOptions<MinioOptions> options)
    {
        _options = options.Value;

        _minioClient = new MinioClient()
            .WithEndpoint(_options.Endpoint)
            .WithCredentials(_options.AccessKey, _options.SecretKey)
            .WithSSL(_options.UseSsl)
            .Build();
    }

    public async Task<List<ArchivoMinioDto>> ListarAsync(string carpeta)
    {
        await CrearBucketSiNoExisteAsync();

        var archivos = new List<ArchivoMinioDto>();

        var args = new ListObjectsArgs()
            .WithBucket(_options.Bucket)
            .WithPrefix($"{carpeta}/")
            .WithRecursive(true);

        await foreach (var item in _minioClient.ListObjectsEnumAsync(args))
        {
            if (!item.IsDir)
            {
                archivos.Add(new ArchivoMinioDto
                {
                    NombreObjeto = item.Key,
                    Tipo = ObtenerTipo(item.Key),
                    TamanioBytes = checked((long)item.Size),
                    FechaModificacion = item.LastModifiedDateTime
                });
            }
        }

        foreach (var archivo in archivos)
        {
            archivo.Url = await ObtenerUrlAsync(archivo.NombreObjeto);
        }

        return archivos;
    }

    public async Task<ArchivoMinioDto> SubirAsync(IFormFile archivo, string carpeta)
    {
        await CrearBucketSiNoExisteAsync();

        ValidarArchivo(archivo, carpeta);

        var extension = Path.GetExtension(archivo.FileName).ToLower();
        var nombreObjeto = $"{carpeta}/{Guid.NewGuid():N}{extension}";

        await using var stream = archivo.OpenReadStream();

        var args = new PutObjectArgs()
            .WithBucket(_options.Bucket)
            .WithObject(nombreObjeto)
            .WithStreamData(stream)
            .WithObjectSize(archivo.Length)
            .WithContentType(archivo.ContentType);

        await _minioClient.PutObjectAsync(args);

        return new ArchivoMinioDto
        {
            NombreObjeto = nombreObjeto,
            Url = await ObtenerUrlAsync(nombreObjeto),
            Tipo = ObtenerTipo(nombreObjeto),
            TamanioBytes = archivo.Length,
            FechaModificacion = DateTime.Now
        };
    }

    public async Task<string> ObtenerUrlAsync(string nombreObjeto)
    {
        var args = new PresignedGetObjectArgs()
            .WithBucket(_options.Bucket)
            .WithObject(nombreObjeto)
            .WithExpiry(_options.PublicUrlExpirationMinutes * 60);

        return await _minioClient.PresignedGetObjectAsync(args);
    }

    public async Task EliminarAsync(string nombreObjeto)
    {
        var args = new RemoveObjectArgs()
            .WithBucket(_options.Bucket)
            .WithObject(nombreObjeto);

        await _minioClient.RemoveObjectAsync(args);
    }

    private async Task CrearBucketSiNoExisteAsync()
    {
        var existsArgs = new BucketExistsArgs()
            .WithBucket(_options.Bucket);

        var existe = await _minioClient.BucketExistsAsync(existsArgs);

        if (!existe)
        {
            var makeArgs = new MakeBucketArgs()
                .WithBucket(_options.Bucket);

            await _minioClient.MakeBucketAsync(makeArgs);
        }
    }

    private void ValidarArchivo(IFormFile archivo, string carpeta)
    {
        if (archivo == null || archivo.Length == 0)
            throw new InvalidOperationException("El archivo es obligatorio.");

        var extension = Path.GetExtension(archivo.FileName).ToLower();

        if (carpeta == "imagenes" && !_imagenesPermitidas.Contains(extension))
            throw new InvalidOperationException("Formato de imagen no permitido.");

        if (carpeta == "videos" && !_videosPermitidos.Contains(extension))
            throw new InvalidOperationException("Formato de video no permitido.");
    }

    private string ObtenerTipo(string nombreObjeto)
    {
        var extension = Path.GetExtension(nombreObjeto).ToLower();

        if (_imagenesPermitidas.Contains(extension))
            return "imagen";

        if (_videosPermitidos.Contains(extension))
            return "video";

        return "desconocido";
    }
}