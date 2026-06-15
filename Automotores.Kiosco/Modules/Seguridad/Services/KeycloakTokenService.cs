using System.Text.Json;
using Automotores.Kiosco.Modules.Seguridad.Dtos;

namespace Automotores.Kiosco.Modules.Seguridad.Services
{
    public class KeycloakTokenService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public KeycloakTokenService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<string?> ObtenerAccessTokenAsync()
        {
            var tokenUrl = _configuration["Keycloak:TokenUrl"];
            var clientId = _configuration["Keycloak:ClientId"];
            var clientSecret = _configuration["Keycloak:ClientSecret"];
            var grantType = _configuration["Keycloak:GrantType"] ?? "client_credentials";

            if (string.IsNullOrWhiteSpace(tokenUrl) ||
                string.IsNullOrWhiteSpace(clientId) ||
                string.IsNullOrWhiteSpace(clientSecret))
                return null;

            var cliente = _httpClientFactory.CreateClient();

            var parametros = new Dictionary<string, string>
            {
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "grant_type", grantType }
            };

            var contenido = new FormUrlEncodedContent(parametros);
            var respuesta = await cliente.PostAsync(tokenUrl, contenido);

            if (!respuesta.IsSuccessStatusCode)
                return null;

            var json = await respuesta.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json))
                return null;

            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var token = JsonSerializer.Deserialize<KeycloakTokenDto>(json, opciones);

            if (string.IsNullOrWhiteSpace(token?.AccessToken))
                return null;

            return token.AccessToken;
        }
    }
}