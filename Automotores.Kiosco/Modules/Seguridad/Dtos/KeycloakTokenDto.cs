using System.Text.Json.Serialization;

namespace Automotores.Kiosco.Modules.Seguridad.Dtos
{
    public class KeycloakTokenDto
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = "";

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = "";
    }
}