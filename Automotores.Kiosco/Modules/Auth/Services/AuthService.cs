using Automotores.Kiosco.Data;
using Automotores.Kiosco.Modules.Auth.Requests;
using Automotores.Kiosco.Modules.Auth.Responses;
using Microsoft.EntityFrameworkCore;

namespace Automotores.Kiosco.Modules.Auth.Services;

public class AuthService
{
    private readonly DataContext _context;

    public AuthService(DataContext context)
    {
        _context = context;
    }

    public async Task<(LoginResponse? response, string? error)> LoginAsync(LoginRequest request)
    {
        var passwordEncriptado = EncriptarPassword(request.Password.Trim());

        var usuario = await _context.SEG_USUARIO
            .AsNoTracking()
            .FirstOrDefaultAsync(u =>
                u.UsLogin.ToLower() == request.Login.ToLower().Trim() &&
                u.UsPassword == passwordEncriptado);

        if (usuario is null)
            return (null, "Credenciales incorrectas.");

        if (usuario.UsStatus != 0)
        {
            var motivo = usuario.UsStatus switch
            {
                1 => "La cuenta requiere cambio de clave.",
                2 => "La cuenta está bloqueada.",
                3 => "La cuenta está inactiva.",
                _ => "La cuenta no está activa."
            };
            return (null, motivo);
        }

        var parametro = await _context.SEG_PARAMETRO_USUARIO
            .AsNoTracking()
            .FirstOrDefaultAsync(p =>
                p.UsCodigo == usuario.UsCodigo &&
                p.AgCodigo == request.AgenciaId);

        if (parametro is null)
            return (null, "El usuario no tiene acceso a la agencia seleccionada.");

        var response = new LoginResponse
        {
            UsCodigo  = usuario.UsCodigo,
            UsNombre  = usuario.UsNombre,
            UsLogin   = usuario.UsLogin,
            UsPassword = usuario.UsPassword,
            PuModulo  = parametro.PuModulo
        };

        return (response, null);
    }

    private static string EncriptarPassword(string password)
    {
        var result = new char[password.Length];
        for (int i = 0; i < password.Length; i++)
            result[i] = (char)(password[i] + (i + 1));
        return new string(result);
    }
}
