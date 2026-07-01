using Automotores.Kiosco.Data;
using Automotores.Kiosco.Models;
using Automotores.Kiosco.Modules.Mostrador.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Automotores.Kiosco.Modules.Mostrador.Services
{
    public class DisponibilidadService
    {
        private readonly DataContext _context;

        public DisponibilidadService(DataContext context)
        {
            _context = context;
        }

        public async Task<DisponibilidadDto> ToggleAsync(decimal usCodigo, decimal agenciaId, decimal gnCodigo)
        {
            var ahora = DateTime.Now;
            var hoy = DateTime.Today;
            var manana = hoy.AddDays(1);

            var activo = await _context.SI_DISP_VEND
                .Where(x =>
                    x.UsCodigo == usCodigo &&
                    x.AgCodigo == agenciaId &&
                    x.GnCodigo == gnCodigo &&
                    x.DvEstado == "A" &&
                    x.DvFechLogin != null &&
                    x.DvFechLogin >= hoy &&
                    x.DvFechLogin < manana)
                .OrderByDescending(x => x.DvFechLogin)
                .FirstOrDefaultAsync();

            if (activo != null)
            {
                activo.DvEstado = "I";
                activo.DvFechUltiAten = ahora;
                await _context.SaveChangesAsync();
                return Mapear(activo, "Módulo desactivado correctamente.");
            }

            var nuevo = new SI_DISP_VEND
            {
                DvEstado = "A",
                DvFechLogin = ahora,
                DvFecha = ahora,
                UsCodigo = usCodigo,
                UsCodiCrea = usCodigo,
                AgCodigo = agenciaId,
                GnCodigo = gnCodigo,
            };

            _context.SI_DISP_VEND.Add(nuevo);
            await _context.SaveChangesAsync();

            return Mapear(nuevo, "Módulo activado correctamente.");
        }

        public async Task<DisponibilidadDto?> GetEstadoAsync(decimal usCodigo, decimal agenciaId, decimal gnCodigo)
        {
            var hoy = DateTime.Today;
            var manana = hoy.AddDays(1);

            var registro = await _context.SI_DISP_VEND
                .AsNoTracking()
                .Where(x =>
                    x.UsCodigo == usCodigo &&
                    x.AgCodigo == agenciaId &&
                    x.GnCodigo == gnCodigo &&
                    x.DvFechLogin != null &&
                    x.DvFechLogin >= hoy &&
                    x.DvFechLogin < manana)
                .OrderByDescending(x => x.DvFechLogin)
                .FirstOrDefaultAsync();

            return registro == null ? null : Mapear(registro, string.Empty);
        }

        public async Task<DisponibilidadDto> ActivarAsync(decimal usCodigo, decimal agenciaId, decimal gnCodigo)
        {
            var hoy = DateTime.Today;
            var manana = hoy.AddDays(1);
            var ahora = DateTime.Now;

            // Cerrar registros activos de días anteriores
            var viejos = await _context.SI_DISP_VEND
                .Where(x =>
                    x.UsCodigo == usCodigo &&
                    x.AgCodigo == agenciaId &&
                    x.GnCodigo == gnCodigo &&
                    x.DvEstado == "A" &&
                    (x.DvFechLogin == null || x.DvFechLogin < hoy))
                .ToListAsync();

            foreach (var viejo in viejos)
            {
                viejo.DvEstado = "I";
                viejo.DvFechUltiAten = ahora;
            }

            // Si ya hay un registro activo hoy, no crear otro
            var activoHoy = await _context.SI_DISP_VEND
                .Where(x =>
                    x.UsCodigo == usCodigo &&
                    x.AgCodigo == agenciaId &&
                    x.GnCodigo == gnCodigo &&
                    x.DvEstado == "A" &&
                    x.DvFechLogin != null &&
                    x.DvFechLogin >= hoy &&
                    x.DvFechLogin < manana)
                .FirstOrDefaultAsync();

            if (activoHoy != null)
            {
                if (viejos.Any()) await _context.SaveChangesAsync();
                return Mapear(activoHoy, "Módulo ya activo.");
            }

            var nuevo = new SI_DISP_VEND
            {
                DvEstado = "A",
                DvFechLogin = ahora,
                DvFecha = ahora,
                UsCodigo = usCodigo,
                UsCodiCrea = usCodigo,
                AgCodigo = agenciaId,
                GnCodigo = gnCodigo,
            };

            _context.SI_DISP_VEND.Add(nuevo);
            await _context.SaveChangesAsync();

            return Mapear(nuevo, "Módulo activado correctamente.");
        }

        public async Task DesactivarAsync(decimal usCodigo, decimal agenciaId, decimal gnCodigo)
        {
            var hoy = DateTime.Today;
            var manana = hoy.AddDays(1);
            var ahora = DateTime.Now;

            var activo = await _context.SI_DISP_VEND
                .Where(x =>
                    x.UsCodigo == usCodigo &&
                    x.AgCodigo == agenciaId &&
                    x.GnCodigo == gnCodigo &&
                    x.DvEstado == "A" &&
                    x.DvFechLogin != null &&
                    x.DvFechLogin >= hoy &&
                    x.DvFechLogin < manana)
                .FirstOrDefaultAsync();

            if (activo == null) return;

            activo.DvEstado = "I";
            activo.DvFechUltiAten = ahora;
            await _context.SaveChangesAsync();
        }

        public async Task<List<DisponibilidadDto>> GetHistoricoAsync(decimal usCodigo, decimal agenciaId, decimal gnCodigo)
        {
            var registros = await _context.SI_DISP_VEND
                .AsNoTracking()
                .Where(x =>
                    x.UsCodigo == usCodigo &&
                    x.AgCodigo == agenciaId &&
                    x.GnCodigo == gnCodigo)
                .OrderByDescending(x => x.DvFecha)
                .ToListAsync();

            return registros.Select(x => Mapear(x, string.Empty)).ToList();
        }

        private static DisponibilidadDto Mapear(SI_DISP_VEND x, string mensaje) => new()
        {
            DvCodigo = x.DvCodigo,
            Estado = x.DvEstado?.Trim() ?? string.Empty,
            UsCodigo = x.UsCodigo ?? 0,
            AgCodigo = x.AgCodigo ?? 0,
            GnCodigo = x.GnCodigo ?? 0,
            FechaLogin = x.DvFechLogin,
            FechaUltimaAtencion = x.DvFechUltiAten,
            Fecha = x.DvFecha,
            Mensaje = mensaje,
        };
    }
}
