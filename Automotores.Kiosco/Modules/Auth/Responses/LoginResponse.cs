namespace Automotores.Kiosco.Modules.Auth.Responses;

public class LoginResponse
{
    public decimal UsCodigo { get; set; }
    public string UsNombre { get; set; } = null!;
    public string UsLogin { get; set; } = null!;
    public string UsPassword { get; set; } = null!;
    public string? PuModulo { get; set; }
}
