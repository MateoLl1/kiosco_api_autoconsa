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

        public async Task<TurnoAtencionDto?> LlamarSiguienteAsync(decimal agenciaId, decimal usCodigo, string? filtro = null)
        {
            if (agenciaId <= 0)
                return null;

            await using var tx = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);

            var hoy = DateTime.Today;
            var manana = hoy.AddDays(1);
            var ahora = DateTime.Now;

            if (usCodigo > 0)
            {
                var turnoEnProceso = await _context.SI_ASIG_TURNO
                    .AsNoTracking()
                    .Where(x =>
                        x.AgCodigo == agenciaId &&
                        x.AsgEstado == "R" &&
                        x.UsCodigo == usCodigo &&
                        x.AsgModulo != null &&
                        x.AsgModulo != "N" &&
                        x.AsgFechMovi != null &&
                        x.AsgFechMovi >= hoy &&
                        x.AsgFechMovi < manana)
                    .FirstOrDefaultAsync();

                if (turnoEnProceso != null)
                    throw new InvalidOperationException("Usted tiene un turno en proceso, lo debe atender antes de llamar el siguiente.");
            }

            var turno = await _context.SI_ASIG_TURNO
                .Where(x =>
                    x.AgCodigo == agenciaId &&
                    x.AsgEstado == "A" &&
                    x.AsgModulo == "N" &&
                    x.AsgFechMovi != null &&
                    x.AsgFechMovi >= hoy &&
                    x.AsgFechMovi < manana &&
                    (filtro == null || filtro == "todos" ||
                     (filtro == "mostrador" && x.TuId!.StartsWith("M")) ||
                     (filtro == "servicio" && !x.TuId!.StartsWith("M"))))
                .OrderBy(x => x.AsgFechMovi)
                .ThenBy(x => x.AsgCodigo)
                .FirstOrDefaultAsync();

            if (turno == null)
                return null;

            // Capturar fecha de llegada antes de sobreescribir AsgFechMovi
            var fechaLlegada = turno.AsgFechMovi;

            // Módulo real del usuario (PU_MODULO); fallback al default si no tiene configurado
            var moduloUsuario = ModuloMostrador;
            if (usCodigo > 0)
            {
                var puModulo = await _context.SEG_PARAMETRO_USUARIO
                    .AsNoTracking()
                    .Where(x => x.UsCodigo == usCodigo && x.AgCodigo == agenciaId)
                    .Select(x => x.PuModulo)
                    .FirstOrDefaultAsync();

                if (!string.IsNullOrWhiteSpace(puModulo))
                    moduloUsuario = puModulo.Trim();
            }

            turno.AsgModulo = moduloUsuario;
            turno.AsgFechAsig = ahora;
            turno.AsgFechMovi = ahora;
            turno.UsCodigo = usCodigo > 0 ? usCodigo : UsuarioMostrador;
            turno.AsgEstado = "R";
            turno.AsgTime = 9;

            await _context.SaveChangesAsync();

            var turnoKiosco = await _context.SI_TURNO_KIOSCO
                .FirstOrDefaultAsync(x => x.AsgCodigo == turno.AsgCodigo);

            if (turnoKiosco != null)
            {
                turnoKiosco.TkEstado = "R";
                turnoKiosco.TkFechLlam = ahora;
                turnoKiosco.UsCodiLlamo = usCodigo > 0 ? usCodigo : null;
            }
            else
            {
                // Turno creado fuera de nuestra API — crear registro de seguimiento ahora
                var tipo = ObtenerTipoDesdeTurno(turno.TuId ?? string.Empty);
                var clCodigo = await ResolverClCodigoAsync(turno.CiCodigo, tipo);

                _context.SI_TURNO_KIOSCO.Add(new SI_TURNO_KIOSCO
                {
                    AgCodigo = turno.AgCodigo ?? agenciaId,
                    ClCodigo = clCodigo,
                    AsgCodigo = turno.AsgCodigo,
                    TkTurno = (turno.TuId ?? string.Empty).Trim(),
                    TkTipo = tipo,
                    TkEstado = "R",
                    TkFechCrea = fechaLlegada ?? ahora,
                    TkFechLlam = ahora,
                    UsCodiLlamo = usCodigo > 0 ? usCodigo : null
                });
            }

            await _context.SaveChangesAsync();
            await tx.CommitAsync();

            return Mapear(turno, "Turno llamado correctamente.");
        }

        public async Task<TurnoAtencionDto?> RellamarAsync(decimal asgCodigo)
        {
            if (asgCodigo <= 0)
                return null;

            var ahora = DateTime.Now;

            var turno = await _context.SI_ASIG_TURNO
                .FirstOrDefaultAsync(x => x.AsgCodigo == asgCodigo);

            if (turno == null)
                return null;

            if (turno.AsgEstado == "I" || turno.AsgEstado == "T")
                return null;

            turno.AsgEstado = "R";
            turno.AsgTime = 3;
            turno.AsgFechMovi = ahora;

            if (string.IsNullOrWhiteSpace(turno.AsgModulo) || turno.AsgModulo == "N")
                turno.AsgModulo = ModuloMostrador;

            await _context.SaveChangesAsync();

            var turnoKiosco = await _context.SI_TURNO_KIOSCO
                .FirstOrDefaultAsync(x => x.AsgCodigo == asgCodigo);

            if (turnoKiosco != null)
            {
                turnoKiosco.TkEstado = "R";
                turnoKiosco.TkFechLlam = ahora;
                await _context.SaveChangesAsync();
            }

            return Mapear(turno, "Turno rellamado correctamente.");
        }

        public async Task<TurnoAtencionDto?> AtenderAsync(decimal asgCodigo)
        {
            if (asgCodigo <= 0)
                return null;

            var ahora = DateTime.Now;

            var turno = await _context.SI_ASIG_TURNO
                .FirstOrDefaultAsync(x => x.AsgCodigo == asgCodigo);

            if (turno == null)
                return null;

            if (turno.AsgEstado == "I" || turno.AsgEstado == "T")
                return null;

            turno.AsgEstado = "T";
            turno.AsgTime = 0;
            turno.AsgFechMovi = ahora;

            await _context.SaveChangesAsync();

            var turnoKiosco = await _context.SI_TURNO_KIOSCO
                .FirstOrDefaultAsync(x => x.AsgCodigo == asgCodigo);

            if (turnoKiosco != null)
            {
                turnoKiosco.TkEstado = "T";
                turnoKiosco.TkFechAten = ahora;
                turnoKiosco.TkTimeEspe = (decimal)(ahora - turnoKiosco.TkFechCrea).TotalSeconds;
                await _context.SaveChangesAsync();
            }

            return Mapear(turno, "Turno atendido correctamente.");
        }

        public async Task<TurnoAtencionDto?> CancelarAsync(decimal asgCodigo)
        {
            if (asgCodigo <= 0)
                return null;

            var ahora = DateTime.Now;

            var turno = await _context.SI_ASIG_TURNO
                .FirstOrDefaultAsync(x => x.AsgCodigo == asgCodigo);

            if (turno == null)
                return null;

            if (turno.AsgEstado == "I" || turno.AsgEstado == "T")
                return null;

            turno.AsgEstado = "I";
            turno.AsgFechMovi = ahora;

            await _context.SaveChangesAsync();

            var turnoKiosco = await _context.SI_TURNO_KIOSCO
                .FirstOrDefaultAsync(x => x.AsgCodigo == asgCodigo);

            if (turnoKiosco != null)
            {
                turnoKiosco.TkEstado = "I";
                await _context.SaveChangesAsync();
            }

            return Mapear(turno, "Turno anulado correctamente.");
        }

        private async Task<decimal?> ResolverClCodigoAsync(decimal? ciCodigo, string tipo)
        {
            if ((ciCodigo ?? 0) == 0) return null;

            // kiosco/sin_cita/latoneria: CiCodigo es directamente ClCodigo
            if (tipo == "kiosco" || tipo == "sin_cita" || tipo == "latoneria")
                return ciCodigo;

            // con_cita: CiCodigo es AtCodigo → buscar ClCodigo en SI_AGEND_TECN
            if (tipo == "con_cita")
            {
                return await _context.SI_AGEND_TECN
                    .AsNoTracking()
                    .Where(x => x.AtCodigo == ciCodigo)
                    .Select(x => (decimal?)x.ClCodigo)
                    .FirstOrDefaultAsync();
            }

            return null;
        }

        private static string ObtenerTipoDesdeTurno(string turno)
        {
            var valor = turno.Trim().ToUpperInvariant();
            if (valor.StartsWith("C")) return "con_cita";
            if (valor.StartsWith("K")) return "kiosco";
            if (valor.StartsWith("S")) return "sin_cita";
            if (valor.StartsWith("F")) return "flota";
            if (valor.StartsWith("L")) return "latoneria";
            if (valor.StartsWith("M")) return "mostrador";
            return "otro";
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
