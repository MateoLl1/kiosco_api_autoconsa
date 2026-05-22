using System.Text.Json.Serialization;

namespace Automotores.Kiosco.Models.dto
{
    public class DatabookPerfilDto
    {
        [JsonPropertyName("dsnPrimer_apellido")]
        public string? DsnPrimerApellido { get; set; }

        [JsonPropertyName("dsnPrimer_nombre")]
        public string? DsnPrimerNombre { get; set; }

        [JsonPropertyName("dsnSegundo_apellido")]
        public string? DsnSegundoApellido { get; set; }

        [JsonPropertyName("dsnSegundo_nombre")]
        public string? DsnSegundoNombre { get; set; }

        [JsonPropertyName("sdFecha_1")]
        public string? SdFecha1 { get; set; }

        [JsonPropertyName("sdNut")]
        public string? SdNut { get; set; }
    }
}