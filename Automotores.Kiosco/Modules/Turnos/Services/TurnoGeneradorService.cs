using System.Data;
using Automotores.Kiosco.Data;
using Automotores.Kiosco.Models;
using Automotores.Kiosco.Modules.Turnos.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Automotores.Kiosco.Modules.Turnos.Services
{
    public class TurnoGeneradorService
    {
        private const int MinutosPromedioPorTurno = 5;

        private readonly DataContext _context;

        public TurnoGeneradorService(DataContext context)
        {
            _context = context;
        }

        public async Task<TurnoGeneradoDto> GenerarSinCitaAsync(decimal agenciaId)
        {
            return await GenerarTurnoAsync(agenciaId, "S");
        }

        public async Task<TurnoGeneradoDto> GenerarSinCitaFlotaAsync(decimal agenciaId)
        {
            return await GenerarTurnoAsync(agenciaId, "F");
        }

        private async Task<TurnoGeneradoDto> GenerarTurnoAsync(decimal agenciaId, string tipo)
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

            var personasPorDelante = await _context.SI_ASIG_TURNO
                .AsNoTracking()
                .CountAsync(x =>
                    x.AgCodigo == agenciaId &&
                    x.AsgEstado == "A" &&
                    x.AsgModulo == "N" &&
                    x.AsgFechMovi != null &&
                    x.AsgFechMovi >= hoy &&
                    x.AsgFechMovi < manana);

            var tiempoEstimado = CalcularTiempoEstimado(personasPorDelante);

            int contador = tipo == "S"
                ? (int)(turnoDb.TuSinCita ?? 0)
                : (int)(turnoDb.TuFlota ?? 0);

            int nuevo = contador + 1;

            if (nuevo == 31)
                nuevo = 1;

            if (tipo == "S")
                turnoDb.TuSinCita = nuevo;
            else
                turnoDb.TuFlota = nuevo;

            await _context.SaveChangesAsync();

            var turnoTexto = $"{tipo}-{nuevo}";

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

            await tx.CommitAsync();

            return new TurnoGeneradoDto
            {
                AsgCodigo = nuevoTurno.AsgCodigo,
                Turno = turnoTexto,
                Tipo = tipo == "S" ? "sin_cita" : "flota",
                Area = tipo == "S" ? "Taller / Servicios" : "Flota de Empresa",
                Estado = "A",
                Modulo = "N",
                AgenciaId = agenciaId,
                PersonasPorDelante = personasPorDelante,
                TiempoEstimadoMinutos = tiempoEstimado,
                Fecha = ahora
            };
        }

        private static int CalcularTiempoEstimado(int personasPorDelante)
        {
            return (personasPorDelante + 1) * MinutosPromedioPorTurno;
        }
    }
}