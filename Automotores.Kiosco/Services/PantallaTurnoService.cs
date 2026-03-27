using Automotores.Kiosco.Data;
using Automotores.Kiosco.Models.dto;
using Automotores.Kiosco.Models.request;
using Microsoft.EntityFrameworkCore;

namespace Automotores.Kiosco.Services
{
    public class PantallaTurnosService
    {
        private readonly DataContext _context;

        public PantallaTurnosService(DataContext context)
        {
            _context = context;
        }

        public async Task<PantallaTurnosResponse> ObtenerTurnosPantallaAsync(decimal agenciaId, DateTime? ultimaFechaAudio = null)
        {
            var response = new PantallaTurnosResponse();

            if (agenciaId <= 0)
            {
                return response;
            }

            var hoy = DateTime.Today;

            var visiblesBase = await _context.SI_ASIG_TURNO
                .AsNoTracking()
                .Where(x =>
                    x.AgCodigo == agenciaId &&
                    x.UsCodigo != 1 &&
                    x.TuId != null &&
                    (x.AsgEstado == "L" || x.AsgEstado == "R") &&
                    x.AsgModulo != null &&
                    x.AsgModulo != "N" &&
                    x.AsgFechMovi != null &&
                    x.AsgFechMovi.Value.Date == hoy)
                .Select(x => new
                {
                    x.AsgCodigo,
                    x.TuId,
                    x.AsgModulo,
                    x.AsgEstado,
                    x.AsgTime,
                    x.CiCodigo,
                    x.AsgFechMovi,
                    x.AsgFechAsig,
                    FechaReferencia = x.AsgFechAsig ?? x.AsgFechMovi
                })
                .OrderByDescending(x => x.FechaReferencia)
                .ThenByDescending(x => x.AsgCodigo)
                .Take(30)
                .ToListAsync();

            var turnos = new List<PantallaTurnoDto>();

            foreach (var item in visiblesBase)
            {
                var nombreCliente = string.Empty;

                if ((item.CiCodigo ?? 0) > 0)
                {
                    nombreCliente = await ObtenerNombreClientePorCitaAsync(item.CiCodigo ?? 0);
                }

                turnos.Add(new PantallaTurnoDto
                {
                    AsgCodigo = item.AsgCodigo,
                    Turno = (item.TuId ?? string.Empty).Trim(),
                    Modulo = (item.AsgModulo ?? string.Empty).Trim(),
                    Estado = (item.AsgEstado ?? string.Empty).Trim(),
                    Tiempo = item.AsgTime ?? 0,
                    Tipo = ObtenerTipoDesdeTurno(item.TuId ?? string.Empty),
                    RequiereCambioEstado = (item.AsgEstado ?? string.Empty).Trim() == "R",
                    EsTurnoActual = (item.AsgTime ?? 0) > 0,
                    NombreCliente = nombreCliente,
                    FechaReferencia = item.FechaReferencia
                });
            }

            var turnoActual = turnos
                .Where(x => x.EsTurnoActual)
                .OrderByDescending(x => x.FechaReferencia)
                .ThenByDescending(x => x.AsgCodigo)
                .FirstOrDefault();

            if (turnoActual == null)
            {
                turnoActual = turnos
                    .OrderByDescending(x => x.FechaReferencia)
                    .ThenByDescending(x => x.AsgCodigo)
                    .FirstOrDefault();
            }

            response.TurnoActual = turnoActual;

            response.TurnosRecienLlamados = turnos
                .Where(x => x.RequiereCambioEstado)
                .OrderByDescending(x => x.FechaReferencia)
                .ThenByDescending(x => x.AsgCodigo)
                .ToList();

            if (turnoActual != null)
            {
                var indiceActual = turnos.FindIndex(x => x.AsgCodigo == turnoActual.AsgCodigo);
                var desde = Math.Max(0, indiceActual - 5);
                var cantidad = Math.Min(11, turnos.Count - desde);

                response.Turnos = turnos
                    .Skip(desde)
                    .Take(cantidad)
                    .ToList();
            }
            else
            {
                response.Turnos = turnos
                    .Take(10)
                    .ToList();
            }

            var pendientesBase = await _context.SI_ASIG_TURNO
                .AsNoTracking()
                .Where(x =>
                    x.AgCodigo == agenciaId &&
                    x.TuId != null &&
                    x.AsgEstado == "A" &&
                    x.AsgModulo == "N" &&
                    x.AsgFechMovi != null &&
                    x.AsgFechMovi.Value.Date == hoy)
                .OrderBy(x => x.AsgFechMovi)
                .ThenBy(x => x.AsgCodigo)
                .Select(x => new
                {
                    x.AsgCodigo,
                    x.TuId,
                    x.AsgModulo,
                    x.AsgEstado,
                    x.AsgTime,
                    x.CiCodigo,
                    x.AsgFechMovi
                })
                .Take(5)
                .ToListAsync();

            var turnosPendientes = new List<PantallaTurnoDto>();

            foreach (var item in pendientesBase)
            {
                var nombreCliente = string.Empty;

                if ((item.CiCodigo ?? 0) > 0)
                {
                    nombreCliente = await ObtenerNombreClientePorCitaAsync(item.CiCodigo ?? 0);
                }

                turnosPendientes.Add(new PantallaTurnoDto
                {
                    AsgCodigo = item.AsgCodigo,
                    Turno = (item.TuId ?? string.Empty).Trim(),
                    Modulo = (item.AsgModulo ?? string.Empty).Trim(),
                    Estado = (item.AsgEstado ?? string.Empty).Trim(),
                    Tiempo = item.AsgTime ?? 0,
                    Tipo = ObtenerTipoDesdeTurno(item.TuId ?? string.Empty),
                    RequiereCambioEstado = false,
                    EsTurnoActual = false,
                    NombreCliente = nombreCliente,
                    FechaReferencia = item.AsgFechMovi
                });
            }

            response.TurnosPendientes = turnosPendientes;

            return response;
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

        private async Task<string> ObtenerNombreClientePorCitaAsync(decimal ciCodigo)
        {
            var cliente = await (
                from at in _context.SI_AGEND_TECN.AsNoTracking()
                join cl in _context.SI_CLIENTE.AsNoTracking() on at.ClCodigo equals cl.ClCodigo
                where at.AtCodigo == ciCodigo
                select new
                {
                    cl.ClNombre,
                    cl.ClApellido
                }
            ).FirstOrDefaultAsync();

            if (cliente == null)
            {
                return string.Empty;
            }

            return $"{(cliente.ClApellido ?? string.Empty).Trim()} {(cliente.ClNombre ?? string.Empty).Trim()}".Trim();
        }

        private static string ObtenerTipoDesdeTurno(string turno)
        {
            var valor = turno.Trim().ToUpperInvariant();

            if (valor.StartsWith("C")) return "con_cita";
            if (valor.StartsWith("S")) return "sin_cita";
            if (valor.StartsWith("F")) return "flota";
            if (valor.StartsWith("L")) return "latoneria";
            return "otro";
        }
    }
}