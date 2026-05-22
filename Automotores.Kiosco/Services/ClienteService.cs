using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;
using Automotores.Kiosco.Data;
using Automotores.Kiosco.Models;
using Automotores.Kiosco.Models.dto;
using Microsoft.EntityFrameworkCore;

namespace Automotores.Kiosco.Services
{
    public class ClienteService
    {
        private readonly DataContext _db;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly KeycloakTokenService _keycloakTokenService;

        public ClienteService(
            DataContext db,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            KeycloakTokenService keycloakTokenService)
        {
            _db = db;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _keycloakTokenService = keycloakTokenService;
        }

        public async Task<ClienteSiacDto?> ObtenerPorIdentificacionAsync(string identificacion, int empresa)
        {
            identificacion = identificacion.Trim();

            var clienteExistente = await BuscarClienteSiacAsync(identificacion);

            if (clienteExistente != null)
                return MapearClienteSiac(clienteExistente);

            var perfilDatabook = await ConsultarDatabookAsync(identificacion, empresa);

            if (perfilDatabook == null)
                return null;

            var nombres = UnirTexto(perfilDatabook.DsnPrimerNombre, perfilDatabook.DsnSegundoNombre);
            var apellidos = UnirTexto(perfilDatabook.DsnPrimerApellido, perfilDatabook.DsnSegundoApellido);
            var fechaNacimiento = ConvertirFecha(perfilDatabook.SdFecha1);

            if (string.IsNullOrWhiteSpace(nombres) && string.IsNullOrWhiteSpace(apellidos))
                return null;

            var nuevoCliente = new SI_CLIENTE
            {
                ClNombre = nombres,
                ClApellido = apellidos,
                ClId = identificacion,
                ClTipoId = "Natural",
                ClFechNac = fechaNacimiento,
                UgCodigo = 3,
                UsCodigo = 1,
                ClFechMovi = DateTime.Now,
                ClCupo = 0,
                ClValusado = 0,
                ClContEspe = false,
                ClEliminado = false,
                PlCodigo = 0,
                ClPorcDesc = 0,
                ClPorcDescServ = 0,
                ZcCodigo = 0,
                ClActualizado = 1,
                ClIva = 1,
                ClExpoHabi = false,
                ClActuClie = true,
                ClMicrEmpr = false,
                ClAgenRete = false,
                ClExpoHabiRecu = false,
                ClEcoValor = false,
                ClInstPubl = false,
                ClRimpe = false,
                ClNegoPopu = false,
                ClGranCont = false,
                ClInstPublFina = false
            };

            _db.SI_CLIENTE.Add(nuevoCliente);
            await _db.SaveChangesAsync();

            var clienteCreado = await BuscarClienteSiacAsync(identificacion);

            if (clienteCreado == null)
                return MapearClienteSiac(nuevoCliente);

            return MapearClienteSiac(clienteCreado);
        }

        private async Task<SI_CLIENTE?> BuscarClienteSiacAsync(string identificacion)
        {
            return await _db.SI_CLIENTE
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ClId == identificacion);
        }

        private async Task<DatabookPerfilDto?> ConsultarDatabookAsync(string identificacion, int empresa)
        {
            var accessToken = await _keycloakTokenService.ObtenerAccessTokenAsync();

            if (string.IsNullOrWhiteSpace(accessToken))
                return null;

            var baseUrl = _configuration["ServiciosExternos:SiacApi"];

            if (string.IsNullOrWhiteSpace(baseUrl))
                return null;

            var url = $"{baseUrl.TrimEnd('/')}/InfoCredito/perfil-databook?identificacion={Uri.EscapeDataString(identificacion)}&empresa={empresa}";

            var cliente = _httpClientFactory.CreateClient();
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var respuesta = await cliente.GetAsync(url);

            if (!respuesta.IsSuccessStatusCode)
                return null;

            var contenido = await respuesta.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(contenido))
                return null;

            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var perfiles = JsonSerializer.Deserialize<List<DatabookPerfilDto>>(contenido, opciones);

            return perfiles?.FirstOrDefault();
        }

        private static ClienteSiacDto MapearClienteSiac(SI_CLIENTE cliente)
        {
            return new ClienteSiacDto
            {
                ClCodigo = cliente.ClCodigo,
                Identificacion = cliente.ClId,
                TipoIdentificacion = cliente.ClTipoId ?? "",
                Nombres = cliente.ClNombre ?? "",
                Apellidos = cliente.ClApellido ?? "",
                NombreCompleto = UnirTexto(cliente.ClNombre, cliente.ClApellido),
                FechaNacimiento = cliente.ClFechNac,
                Telefono1 = cliente.ClTelefono1 ?? "",
                Telefono2 = cliente.ClTelefono2 ?? "",
                Correo = cliente.ClMail ?? "",
                UgCodigo = cliente.UgCodigo,
                UsCodigo = cliente.UsCodigo
            };
        }

        private static string UnirTexto(params string?[] valores)
        {
            return string.Join(" ", valores
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(x => x!.Trim()))
                .Trim();
        }

        private static DateTime? ConvertirFecha(string? fecha)
        {
            if (string.IsNullOrWhiteSpace(fecha))
                return null;

            if (DateTime.TryParseExact(fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var resultado))
                return resultado;

            return null;
        }
    }
}