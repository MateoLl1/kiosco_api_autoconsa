using System.Data;
using Automotores.Kiosco.Data;
using Automotores.Kiosco.Models;
using Automotores.Kiosco.Modules.Turnos.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Automotores.Kiosco.Modules.Mostrador.Services
{
    public class TurnoMostradorService
    {
        private const int MinutosPromedioPorTurno = 5;

        private readonly DataContext _context;

        public TurnoMostradorService(DataContext context)
        {
            _context = context;
        }

        public async Task<TurnoGeneradoDto> GenerarAsync(decimal agenciaId, string? identificacion)
        {
            await using var tx = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);

            var hoy = DateTime.Today;
            var manana = hoy.AddDays(1);
            var ahora = DateTime.Now;

            var turnoDb = await _context.SI_TURNO
                .Where(x => x.AgCodigo == agenciaId)
                .FirstOrDefaultAsync();

            if (turnoDb == null)
                throw new Exception("No existe configuración de turnos para la agencia");

            string nombreCliente = string.Empty;
            decimal? clCodigo = null;

            if (!string.IsNullOrWhiteSpace(identificacion))
            {
                var cliente = await _context.SI_CLIENTE
                    .AsNoTracking()
                    .Where(x => x.ClId.Trim() == identificacion.Trim())
                    .Select(x => new { x.ClCodigo, x.ClNombre, x.ClApellido, x.ClTipoId })
                    .FirstOrDefaultAsync();

                if (cliente != null)
                {
                    clCodigo = cliente.ClCodigo;
                    nombreCliente = cliente.ClTipoId == "Natural"
                        ? $"{cliente.ClApellido} {cliente.ClNombre}".Trim()
                        : $"{cliente.ClNombre}".Trim();
                }
            }

            var personasPorDelante = await _context.SI_ASIG_TURNO
                .AsNoTracking()
                .CountAsync(x =>
                    x.AgCodigo == agenciaId &&
                    x.AsgEstado == "A" &&
                    x.AsgModulo == "N" &&
                    x.AsgFechMovi != null &&
                    x.AsgFechMovi >= hoy &&
                    x.AsgFechMovi < manana);

            var tiempoEstimado = (personasPorDelante + 1) * MinutosPromedioPorTurno;

            // Reiniciar contador si no hay turnos M- creados hoy
            var hayTurnosHoy = await _context.SI_ASIG_TURNO
                .AnyAsync(x =>
                    x.AgCodigo == agenciaId &&
                    x.TuId != null &&
                    x.TuId.StartsWith("M-") &&
                    x.AsgFechMovi >= hoy &&
                    x.AsgFechMovi < manana);

            if (!hayTurnosHoy)
                turnoDb.TuMostrador = 0;

            int contador = (int)(turnoDb.TuMostrador ?? 0);
            int nuevo = contador + 1;
            if (nuevo == 31)
                nuevo = 1;

            turnoDb.TuMostrador = nuevo;
            await _context.SaveChangesAsync();

            var turnoTexto = $"M-{nuevo}";

            var nuevoTurno = new SI_ASIG_TURNO
            {
                TuId = turnoTexto,
                UsCodigo = 1,
                AsgFechMovi = ahora,
                AsgModulo = "N",
                CiCodigo = 0,
                AgCodigo = agenciaId,
                AsgEstado = "A",
                AsgFechAsig = null,
                AsgTime = 0,
                AsgTimeEspe = tiempoEstimado,
                AsgCita = 1,
                AsgLlegada = 0,
                ClCodiAseg = 0
            };

            _context.SI_ASIG_TURNO.Add(nuevoTurno);
            await _context.SaveChangesAsync();

            var turnoKiosco = new SI_TURNO_KIOSCO
            {
                AgCodigo = agenciaId,
                ClCodigo = clCodigo,
                AsgCodigo = nuevoTurno.AsgCodigo,
                TkTurno = turnoTexto,
                TkTipo = "mostrador",
                TkEstado = "A",
                TkFechCrea = ahora
            };

            _context.SI_TURNO_KIOSCO.Add(turnoKiosco);
            await _context.SaveChangesAsync();

            await tx.CommitAsync();

            return new TurnoGeneradoDto
            {
                AsgCodigo = nuevoTurno.AsgCodigo,
                TkCodigo = turnoKiosco.TkCodigo,
                Turno = turnoTexto,
                Tipo = "mostrador",
                Area = "Mostrador",
                Estado = "A",
                Modulo = "N",
                AgenciaId = agenciaId,
                PersonasPorDelante = personasPorDelante,
                TiempoEstimadoMinutos = tiempoEstimado,
                Fecha = ahora,
                NombreCliente = nombreCliente
            };
        }
    }
}
