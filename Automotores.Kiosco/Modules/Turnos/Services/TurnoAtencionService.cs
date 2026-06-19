using Automotores.Kiosco.Data;
using Automotores.Kiosco.Models;
using Automotores.Kiosco.Modules.Turnos.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Automotores.Kiosco.Modules.Turnos.Services
{
    public class TurnoAtencionService
    {
        private const int UsuarioMostrador = 999;
        private const string ModuloMostrador = "1";

        private readonly DataContext _context;

        public TurnoAtencionService(DataContext context)
        {
            _context = context;
        }

        public async Task<TurnoAtencionDto?> LlamarSiguienteAsync(decimal agenciaId)
        {
            if (agenciaId <= 0)
                return null;

            await using var tx = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);

            var hoy = DateTime.Today;
            var manana = hoy.AddDays(1);
            var ahora = DateTime.Now;

            var turnoEnProceso = await _context.SI_ASIG_TURNO
                .AsNoTracking()
                .Where(x =>
                    x.AgCodigo == agenciaId &&
                    x.AsgEstado == "R" &&
                    x.UsCodigo != 1 &&
                    x.AsgModulo != null &&
                    x.AsgModulo != "N" &&
                    x.AsgFechMovi != null &&
                    x.AsgFechMovi >= hoy &&
                    x.AsgFechMovi < manana)
                .OrderByDescending(x => x.AsgFechMovi)
                .ThenByDescending(x => x.AsgCodigo)
                .FirstOrDefaultAsync();

            if (turnoEnProceso != null)
                throw new InvalidOperationException("Usted tiene un turno en proceso, lo debe atender antes de llamar el siguiente.");

            var turno = await _context.SI_ASIG_TURNO
                .Where(x =>
                    x.AgCodigo == agenciaId &&
                    x.AsgEstado == "A" &&
                    x.AsgModulo == "N" &&
                    x.AsgFechMovi != null &&
                    x.AsgFechMovi >= hoy &&
                    x.AsgFechMovi < manana)
                .OrderBy(x => x.AsgFechMovi)
                .ThenBy(x => x.AsgCodigo)
                .FirstOrDefaultAsync();

            if (turno == null)
                return null;

            turno.AsgModulo = ModuloMostrador;
            turno.AsgFechAsig = ahora;
            turno.AsgFechMovi = ahora;
            turno.UsCodigo = UsuarioMostrador;
            turno.AsgEstado = "R";
            turno.AsgTime = 9;

            await _context.SaveChangesAsync();
            await tx.CommitAsync();

            return Mapear(turno, "Turno llamado correctamente.");
        }

        public async Task<TurnoAtencionDto?> RellamarAsync(decimal asgCodigo)
        {
            if (asgCodigo <= 0)
                return null;

            var turno = await _context.SI_ASIG_TURNO
                .FirstOrDefaultAsync(x => x.AsgCodigo == asgCodigo);

            if (turno == null)
                return null;

            if (turno.AsgEstado == "I" || turno.AsgEstado == "T")
                return null;

            turno.AsgEstado = "R";
            turno.AsgTime = 3;
            turno.AsgFechMovi = DateTime.Now;
            turno.UsCodigo = UsuarioMostrador;

            if (string.IsNullOrWhiteSpace(turno.AsgModulo) || turno.AsgModulo == "N")
                turno.AsgModulo = ModuloMostrador;

            await _context.SaveChangesAsync();

            return Mapear(turno, "Turno rellamado correctamente.");
        }

        public async Task<TurnoAtencionDto?> AtenderAsync(decimal asgCodigo)
        {
            if (asgCodigo <= 0)
                return null;

            var turno = await _context.SI_ASIG_TURNO
                .FirstOrDefaultAsync(x => x.AsgCodigo == asgCodigo);

            if (turno == null)
                return null;

            if (turno.AsgEstado == "I" || turno.AsgEstado == "T")
                return null;

            turno.AsgEstado = "T";
            turno.AsgTime = 0;
            turno.AsgFechMovi = DateTime.Now;

            await _context.SaveChangesAsync();

            return Mapear(turno, "Turno atendido correctamente.");
        }

        private static TurnoAtencionDto Mapear(SI_ASIG_TURNO turno, string mensaje)
        {
            return new TurnoAtencionDto
            {
                AsgCodigo = turno.AsgCodigo,
                Turno = turno.TuId?.Trim() ?? string.Empty,
                Estado = turno.AsgEstado?.Trim() ?? string.Empty,
                Modulo = turno.AsgModulo?.Trim() ?? string.Empty,
                AgenciaId = turno.AgCodigo ?? 0,
                FechaMovimiento = turno.AsgFechMovi,
                Mensaje = mensaje
            };
        }
    }
}