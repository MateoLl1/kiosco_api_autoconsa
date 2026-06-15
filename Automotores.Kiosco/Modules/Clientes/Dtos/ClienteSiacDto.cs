namespace Automotores.Kiosco.Modules.Clientes.Dtos
{
    public class ClienteSiacDto
    {
        public decimal ClCodigo { get; set; }
        public string Identificacion { get; set; } = "";
        public string TipoIdentificacion { get; set; } = "";
        public string Nombres { get; set; } = "";
        public string Apellidos { get; set; } = "";
        public string NombreCompleto { get; set; } = "";
        public DateTime? FechaNacimiento { get; set; }
        public string Telefono1 { get; set; } = "";
        public string Telefono2 { get; set; } = "";
        public string Correo { get; set; } = "";
        public decimal? UgCodigo { get; set; }
        public decimal? UsCodigo { get; set; }
    }
}