using Automotores.Kiosco.Data;
using Automotores.Kiosco.Models;
using Microsoft.EntityFrameworkCore;

namespace Automotores.Kiosco.Services
{
    public class TurnoService
    {
        private readonly DataContext _context;

        public TurnoService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<CitaRecepcionDto>> ObtenerCitasRecepcionAsync(decimal agenciaId)
        {
            var hoy = DateTime.Today;
            var manana = hoy.AddDays(1);

            var activas = await (
                from a in _context.SI_AGEND_TECN.AsNoTracking()
                join b in _context.SI_CLIENTE.AsNoTracking() on a.ClCodigo equals b.ClCodigo
                join d in _context.SI_STICKER.AsNoTracking() on a.StCodigo equals d.StCodigo
                join m in _context.SI_MODELO.AsNoTracking() on d.MoCodigo equals m.MoCodigo
                join h in _context.SI_BAHIA.AsNoTracking() on a.BhCodigo equals h.BhCodigo
                where a.AtFecha >= hoy
                    && a.AtFecha < manana
                    && a.TlCodigo.HasValue
                    && new[] { 5m, 8m, 17m, 18m }.Contains(a.TlCodigo.Value)
                    && h.AgCodigo == agenciaId
                    && a.AtStatus.HasValue
                    && new[] { 1m, 2m, 6m }.Contains(a.AtStatus.Value)
                    && a.HtCodigo == 0
                select new CitaRecepcionDto
                {
                    CodigoCita = a.AtCodigo,
                    HoraCita = FormatearHora(a.AtHoraInicio),
                    Placa = d.StPlaca ?? string.Empty,
                    NombreCliente = FormatearNombreCliente(b.ClApellido, b.ClNombre, m.MoNombre),
                    ModeloVehiculo = m.MoNombre ?? string.Empty,
                    Bahia = FormatearBahia(h.BhNombre),
                    Estatus = a.AtStatus ?? 0,
                    TlCodigo = a.TlCodigo ?? 0,
                    Estado = MapearEstado(a.AtStatus ?? 0),
                    TipoLabor = MapearTipoLabor(a.TlCodigo ?? 0),
                    ClaveVisual = MapearClaveVisual(a.AtStatus ?? 0, a.TlCodigo ?? 0)
                }
            ).ToListAsync();

            var canceladas = await (
                from a in _context.SI_AGEND_TECN.AsNoTracking()
                join b in _context.SI_CLIENTE.AsNoTracking() on a.ClCodigo equals b.ClCodigo
                join d in _context.SI_STICKER.AsNoTracking() on a.StCodigo equals d.StCodigo
                join m in _context.SI_MODELO.AsNoTracking() on d.MoCodigo equals m.MoCodigo
                join h in _context.SI_BAHIA.AsNoTracking() on a.BhCodigo equals h.BhCodigo
                where a.AtFecha >= hoy
                    && a.AtFecha < manana
                    && a.TlCodigo.HasValue
                    && new[] { 5m, 8m, 17m, 18m }.Contains(a.TlCodigo.Value)
                    && h.AgCodigo == agenciaId
                    && a.AtStatus == 5
                    && a.AtFechLleg == null
                    && a.AtFechCanc.HasValue
                    && a.AtFechCanc.Value >= hoy
                    && a.AtFechCanc.Value < manana
                    && a.HtCodigo == 0
                select new CitaRecepcionDto
                {
                    CodigoCita = a.AtCodigo,
                    HoraCita = FormatearHora(a.AtHoraInicio),
                    Placa = d.StPlaca ?? string.Empty,
                    NombreCliente = FormatearNombreCliente(b.ClApellido, b.ClNombre, m.MoNombre),
                    ModeloVehiculo = m.MoNombre ?? string.Empty,
                    Bahia = FormatearBahia(h.BhNombre),
                    Estatus = a.AtStatus ?? 0,
                    TlCodigo = a.TlCodigo ?? 0,
                    Estado = MapearEstado(a.AtStatus ?? 0),
                    TipoLabor = MapearTipoLabor(a.TlCodigo ?? 0),
                    ClaveVisual = MapearClaveVisual(a.AtStatus ?? 0, a.TlCodigo ?? 0)
                }
            ).ToListAsync();

            return activas
                .Concat(canceladas)
                .OrderBy(x => x.HoraCita)
                .ThenBy(x => x.CodigoCita)
                .ToList();
        }

        private static string FormatearHora(string? hora)
        {
            if (string.IsNullOrWhiteSpace(hora))
            {
                return string.Empty;
            }

            return hora.Length >= 5 ? hora[..5] : hora;
        }

        private static string FormatearNombreCliente(string? apellido, string? nombre, string? modelo)
        {
            var cliente = $"{apellido} {nombre}".Trim();
            var clienteCorto = cliente.Length > 20 ? cliente[..20] + "..." : cliente;
            var modeloLimpio = (modelo ?? string.Empty).Trim();
            var modeloCorto = modeloLimpio.Length > 20 ? modeloLimpio[..20] + "..." : modeloLimpio;

            return $"{clienteCorto} | {modeloCorto}";
        }

        private static string FormatearBahia(string? bahia)
        {
            var valor = (bahia ?? string.Empty).Trim();
            return valor.Length > 10 ? valor[..10] + ".." : valor;
        }

        private static string MapearEstado(decimal estatus)
        {
            if (estatus == 1) return "agendado";
            if (estatus == 2) return "confirmado";
            if (estatus == 5) return "cancelado";
            if (estatus == 6) return "no_llego";
            return "desconocido";
        }

        private static string MapearTipoLabor(decimal tlCodigo)
        {
            if (tlCodigo == 5) return "mantenimiento";
            if (tlCodigo == 8) return "reparacion";
            if (tlCodigo == 17) return "recepcion";
            if (tlCodigo == 18) return "servicio_rapido";
            return "otro";
        }

        private static string MapearClaveVisual(decimal estatus, decimal tlCodigo)
        {
            if (estatus == 5) return "cancelado";
            if (estatus == 6) return "no_llego";
            if (estatus == 1 || estatus == 2)
            {
                if (tlCodigo == 5) return "mantenimiento";
                return "reparacion";
            }

            return "normal";
        }
    }
}