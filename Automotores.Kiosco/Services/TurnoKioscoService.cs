using System.Data;
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

            var ahora = DateTime.Now;
            var hoy = DateTime.Today;
            var manana = hoy.AddDays(1);

            await using var transaccion = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);

            var turnoDb = await _context.SI_TURNO
                .Where(x => x.AgCodigo == request.AgenciaId)
                .FirstOrDefaultAsync();

            if (turnoDb == null)
                return null;

            var contadorActual = (int)(turnoDb.TuConCita ?? 0);
            var siguienteNumero = contadorActual + 1;

            if (siguienteNumero == 31)
                siguienteNumero = 1;

            turnoDb.TuConCita = siguienteNumero;

            var turno = $"C-{siguienteNumero}";

            var bahia = await _context.SI_BAHIA
                .AsNoTracking()
                .Where(x => x.AgCodigo == request.AgenciaId)
                .OrderBy(x => x.BhCodigo)
                .FirstOrDefaultAsync();

            var citaKiosco = new SI_AGEND_TECN
            {
                BhCodigo = bahia?.BhCodigo,
                AtFecha = hoy,
                AtHoraInicio = ahora.ToString("HH:mm"),
                AtHoraFin = ahora.AddMinutes(15).ToString("HH:mm"),
                AtTiempo = "15",
                TlCodigo = bahia?.TlCodigo ?? 17,
                AtStatus = 3,
                UsCodigo = 1,
                ClCodigo = cliente.ClCodigo,
                StCodigo = null,
                HtCodigo = 0,
                AtTaxi = false,
                KiCodigo = 0,
                AtObservacion = "Turno generado desde kiosco",
                AtCodiRela = 0,
                AtFechLleg = ahora,
                AtEstado = "L",
                AtFechCrea = ahora
            };

            _context.SI_AGEND_TECN.Add(citaKiosco);
            await _context.SaveChangesAsync();

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
                AsgFechMovi = ahora,
                AsgModulo = "N",
                CiCodigo = citaKiosco.AtCodigo,
                AgCodigo = request.AgenciaId,
                AsgEstado = "A",
                AsgFechAsig = null,
                AsgTime = 0,
                AsgTimeEspe = tiempoEstimado,
                AsgCita = 3,
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
                Fecha = nuevoTurno.AsgFechMovi ?? ahora
            };
        }

        private static string UnirNombre(string? nombres, string? apellidos)
        {
            return $"{(nombres ?? string.Empty).Trim()} {(apellidos ?? string.Empty).Trim()}".Trim();
        }
    }
}