using Automotores.Kiosco.Data;
using Automotores.Kiosco.Models;
using Automotores.Kiosco.Models.dto;
using Automotores.Kiosco.Models.request;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Automotores.Kiosco.Services
{
    public class PantallaTurnosService
    {
        private readonly DataContext _context;

        public PantallaTurnosService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<PantallaTurnoDto>> ObtenerTurnosPantallaAsync(decimal agenciaId)
        {
            if (agenciaId <= 0)
            {
                return new List<PantallaTurnoDto>();
            }

            var horaActual = DateTime.Now.Hour;
            if (horaActual < 6 || horaActual > 20)
            {
                return new List<PantallaTurnoDto>();
            }

            await using var tx = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            await _context.Database.ExecuteSqlRawAsync(@"
UPDATE SI_ASIG_TURNO
SET ASG_TIME = ASG_TIME - 1
WHERE ASG_TIME > 0");

            await _context.Database.ExecuteSqlRawAsync(@"
UPDATE SI_ASIG_TURNO
SET ASG_TIME_ESPE = DATEDIFF(MI, ASG_FECH_MOVI, GETDATE())
WHERE ASG_ESTADO = 'A'");

            var resultado = new List<PantallaTurnoDto>();

            var turnoS = await ObtenerTurnoPorPrefijoAsync(agenciaId, "S", "sin_cita");
            if (turnoS != null) resultado.Add(turnoS);

            var turnoC = await ObtenerTurnoPorPrefijoAsync(agenciaId, "C", "con_cita");
            if (turnoC != null) resultado.Add(turnoC);

            var turnoF = await ObtenerTurnoPorPrefijoAsync(agenciaId, "F", "flota");
            if (turnoF != null) resultado.Add(turnoF);

            var turnoL = await ObtenerTurnoPorPrefijoAsync(agenciaId, "L", "latoneria");
            if (turnoL != null) resultado.Add(turnoL);

            await tx.CommitAsync();

            return resultado
                .OrderBy(x => ObtenerOrdenVisual(x.Tipo))
                .ToList();
        }

        public async Task<MarcarTurnoMostradoResponse> MarcarTurnoMostradoAsync(decimal asgCodigo)
        {
            if (asgCodigo <= 0)
            {
                return new MarcarTurnoMostradoResponse
                {
                    Resultado = "error",
                    Codigo = "ASG_REQUERIDO",
                    Mensaje = "El código del turno es requerido."
                };
            }

            var turno = await _context.SI_ASIG_TURNO
                .FirstOrDefaultAsync(x => x.AsgCodigo == asgCodigo);

            if (turno == null)
            {
                return new MarcarTurnoMostradoResponse
                {
                    Resultado = "error",
                    Codigo = "NO_EXISTE",
                    Mensaje = "El turno no existe."
                };
            }

            turno.AsgEstado = "L";
            await _context.SaveChangesAsync();

            return new MarcarTurnoMostradoResponse
            {
                Resultado = "ok",
                Codigo = "ACTUALIZADO",
                Mensaje = "El turno fue marcado como mostrado."
            };
        }

        private async Task<PantallaTurnoDto?> ObtenerTurnoPorPrefijoAsync(decimal agenciaId, string prefijo, string tipo)
        {
            return await _context.SI_ASIG_TURNO
                .AsNoTracking()
                .Where(x =>
                    x.AgCodigo == agenciaId &&
                    x.UsCodigo != 1 &&
                    (x.AsgEstado == "L" || x.AsgEstado == "R") &&
                    x.TuId != null &&
                    x.TuId.StartsWith(prefijo))
                .OrderByDescending(x => x.AsgFechMovi)
                .Select(x => new PantallaTurnoDto
                {
                    AsgCodigo = x.AsgCodigo,
                    Turno = x.TuId ?? string.Empty,
                    Modulo = x.AsgModulo ?? string.Empty,
                    Estado = x.AsgEstado ?? string.Empty,
                    Tiempo = x.AsgTime ?? 0,
                    Tipo = tipo,
                    RequiereCambioEstado = x.AsgEstado == "R"
                })
                .FirstOrDefaultAsync();
        }

        private static int ObtenerOrdenVisual(string tipo)
        {
            if (tipo == "con_cita") return 1;
            if (tipo == "flota") return 2;
            if (tipo == "latoneria") return 2;
            if (tipo == "sin_cita") return 3;
            return 99;
        }
    }
}