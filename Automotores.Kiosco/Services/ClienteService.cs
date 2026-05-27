using Automotores.Kiosco.Data;
using Automotores.Kiosco.Models;
using Automotores.Kiosco.Models.dto;
using Microsoft.EntityFrameworkCore;

namespace Automotores.Kiosco.Services
{
    public class ClienteService
    {
        private readonly DataContext _db;

        public ClienteService(DataContext db)
        {
            _db = db;
        }

        public async Task<ClienteSiacDto?> ObtenerPorIdentificacionAsync(string identificacion)
        {
            identificacion = identificacion.Trim();

            var cliente = await BuscarClienteSiacAsync(identificacion);

            if (cliente == null)
                return null;

            return MapearClienteSiac(cliente);
        }

        private async Task<SI_CLIENTE?> BuscarClienteSiacAsync(string identificacion)
        {
            return await _db.SI_CLIENTE
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ClId == identificacion);
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
    }
}