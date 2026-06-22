namespace Automotores.Kiosco.Modules.Auth.Requests;

public class LoginRequest
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public decimal AgenciaId { get; set; }
}
