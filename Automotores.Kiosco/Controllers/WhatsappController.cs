using Automotores.Kiosco.Modules.Whatsapp.Requests;
using Automotores.Kiosco.Modules.Whatsapp.Services;
using Microsoft.AspNetCore.Mvc;

namespace Automotores.Kiosco.Controllers
{
    public class WhatsappController : BaseController
    {
        private readonly WhatsappTurnoService _servicio;

        public WhatsappController(WhatsappTurnoService servicio)
        {
            _servicio = servicio;
        }

        [HttpPost("turno")]
        public async Task<IActionResult> NotificarTurno([FromBody] NotificarTurnoWhatsappRequest request)
        {
            if (request == null)
                return BadRequest(new { mensaje = "La solicitud es requerida." });

            if (string.IsNullOrWhiteSpace(request.NumeroEnvio))
                return BadRequest(new { mensaje = "El número de WhatsApp es requerido." });

            if (string.IsNullOrWhiteSpace(request.Turno))
                return BadRequest(new { mensaje = "El turno es requerido." });

            var enviado = await _servicio.NotificarTurnoAsync(request);

            if (!enviado)
                return StatusCode(StatusCodes.Status502BadGateway, new
                {
                    mensaje = "No se pudo enviar la notificación por WhatsApp."
                });

            return Ok(new
            {
                mensaje = "Notificación enviada correctamente."
            });
        }
    }
}