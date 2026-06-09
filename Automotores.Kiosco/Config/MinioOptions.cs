namespace Automotores.KIOSCO.API.Config;

public class MinioOptions
{
    public string Endpoint { get; set; } = string.Empty;
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string Bucket { get; set; } = string.Empty;
    public bool UseSsl { get; set; }
    public int PublicUrlExpirationMinutes { get; set; } = 60;
}