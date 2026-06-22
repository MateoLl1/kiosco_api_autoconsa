using Automotores.Kiosco.Modules.Auth.Requests;
using Automotores.Kiosco.Modules.Auth.Services;
using Microsoft.AspNetCore.Mvc;

namespace Automotores.Kiosco.Controllers;

public class AuthController : BaseController
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Login) || string.IsNullOrWhiteSpace(request.Password))
            return BadRequest("Login y password son requeridos.");

        var (response, error) = await _authService.LoginAsync(request);

        if (error is not null)
            return Unauthorized(new { mensaje = error });

        return Ok(response);
    }
}
