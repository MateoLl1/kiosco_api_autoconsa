using Microsoft.AspNetCore.Mvc;

namespace Automotores.Kiosco.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class BaseController : ControllerBase
    {
    }
}