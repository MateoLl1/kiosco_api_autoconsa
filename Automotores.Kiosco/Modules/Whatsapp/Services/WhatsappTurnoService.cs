
using System.Text;
using System.Text.Json;
using Automotores.Kiosco.Modules.Whatsapp.Dtos;
using Automotores.Kiosco.Modules.Whatsapp.Requests;

namespace Automotores.Kiosco.Modules.Whatsapp.Services
{
    public class WhatsappTurnoService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public WhatsappTurnoService(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<bool> NotificarTurnoAsync(NotificarTurnoWhatsappRequest request)
        {
            var url = _configuration["Whatsapp:Url"];
            var apiAuth = _configuration["Whatsapp:ApiAuth"];
            var campania = int.TryParse(_configuration["Whatsapp:CampaniaTurno"], out var valorCampania)
                ? valorCampania
                : 53;

            if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(apiAuth))
                return false;

            var numero = NormalizarNumeroEcuador(request.NumeroEnvio);

            if (string.IsNullOrWhiteSpace(numero))
                return false;

            var payload = new WhatsappPlantillaDto
            {
                NumeroEnvio = numero,
                Campania = campania,
                Variables = new List<string>
                {
                    LimpiarTexto(request.Cliente),
                    LimpiarTexto(request.Turno),
                    LimpiarTexto(request.Area)
                }
            };

            var json = JsonSerializer.Serialize(payload, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            var cliente = _httpClientFactory.CreateClient();

            using var mensaje = new HttpRequestMessage(HttpMethod.Post, url);
            mensaje.Headers.Add("api-auth", apiAuth);
            mensaje.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var respuesta = await cliente.SendAsync(mensaje);

            return respuesta.IsSuccessStatusCode;
        }

        private static string NormalizarNumeroEcuador(string numero)
        {
            var valor = new string((numero ?? string.Empty)
                .Where(char.IsDigit)
                .ToArray());

            if (string.IsNullOrWhiteSpace(valor))
                return string.Empty;

            if (valor.StartsWith("593") && valor.Length >= 12)
                return valor;

            if (valor.StartsWith("0") && valor.Length == 10)
                return $"593{valor[1..]}";

            if (valor.StartsWith("9") && valor.Length == 9)
                return $"593{valor}";

            return valor;
        }

        private static string LimpiarTexto(string valor)
        {
            return (valor ?? string.Empty).Trim();
        }
    }
}