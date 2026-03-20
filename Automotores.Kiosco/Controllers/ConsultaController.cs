using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly ClienteService _servicio;

    public ClienteController(ClienteService servicio)
    {
        _servicio = servicio;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var lista = await _servicio.ObtenerTodosAsync();
        return Ok(lista);
    }
}