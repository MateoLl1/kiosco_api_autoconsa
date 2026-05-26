using Automotores.Kiosco.Data;
using Automotores.Kiosco.Models;
using Automotores.Kiosco.Models.dto;
using Automotores.Kiosco.Models.request;
using Microsoft.EntityFrameworkCore;

namespace Automotores.Kiosco.Services
{
    public class TurnoKioscoService
    {
        private readonly DataContext _context;

        public TurnoKioscoService(DataContext context)
        {
            _context = context;
        }

        public async Task<TurnoKioscoDto?> GenerarTurnoAsync(GenerarTurnoKioscoRequest request)
        {
            if (request.AgenciaId <= 0 || request.ClCodigo <= 0)
                return null;

            var cliente = await _context.SI_CLIENTE
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ClCodigo == request.ClCodigo);

            if (cliente == null)
                return null;

            var hoy = DateTime.Today;
            var manana = hoy.AddDays(1);

            await using var transaccion = await _context.Database.BeginTransactionAsync();

            var turnosConCita = await _context.SI_ASIG_TURNO
                .Where(x =>
                    x.AgCodigo == request.AgenciaId &&
                    x.TuId != null &&
                    x.TuId.StartsWith("C-") &&
                    x.AsgFechMovi != null &&
                    x.AsgFechMovi >= hoy &&
                    x.AsgFechMovi < manana)
                .Select(x => x.TuId)
                .ToListAsync();

            var ultimoNumero = turnosConCita
                .Select(ObtenerNumeroTurno)
                .DefaultIfEmpty(0)
                .Max();

            var siguienteNumero = ultimoNumero + 1;

            if (siguienteNumero > 30)
                siguienteNumero = 1;

            var turno = $"C-{siguienteNumero}";

            var personasPorDelante = await _context.SI_ASIG_TURNO
                .AsNoTracking()
                .CountAsync(x =>
                    x.AgCodigo == request.AgenciaId &&
                    x.AsgEstado == "A" &&
                    x.AsgModulo == "N" &&
                    x.AsgFechMovi != null &&
                    x.AsgFechMovi >= hoy &&
                    x.AsgFechMovi < manana);

            var tiempoEstimado = personasPorDelante * 5;

            var nuevoTurno = new SI_ASIG_TURNO
            {
                TuId = turno,
                UsCodigo = 1,
                AsgFechMovi = DateTime.Now,
                AsgModulo = "N",
                CiCodigo = request.ClCodigo,
                AgCodigo = request.AgenciaId,
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

            await transaccion.CommitAsync();

            return new TurnoKioscoDto
            {
                AsgCodigo = nuevoTurno.AsgCodigo,
                Turno = turno,
                AgenciaId = request.AgenciaId,
                ClCodigo = cliente.ClCodigo,
                Identificacion = cliente.ClId,
                Cliente = UnirNombre(cliente.ClNombre, cliente.ClApellido),
                Estado = "PENDIENTE",
                Modulo = "N",
                PersonasPorDelante = personasPorDelante,
                TiempoEstimadoMinutos = tiempoEstimado,
                Fecha = nuevoTurno.AsgFechMovi ?? DateTime.Now
            };
        }

        private static int ObtenerNumeroTurno(string? turno)
        {
            if (string.IsNullOrWhiteSpace(turno))
                return 0;

            var valor = turno.Trim().ToUpperInvariant().Replace("C-", "");

            if (int.TryParse(valor, out var numero))
                return numero;

            return 0;
        }

        private static string UnirNombre(string? nombres, string? apellidos)
        {
            return $"{(nombres ?? string.Empty).Trim()} {(apellidos ?? string.Empty).Trim()}".Trim();
        }
    }
}