using Automotores.Kiosco.Data;
using Automotores.Kiosco.Modules.PantallaTurnos.Dtos;
using Automotores.Kiosco.Modules.PantallaTurnos.Responses;
using Microsoft.EntityFrameworkCore;

namespace Automotores.Kiosco.Modules.PantallaTurnos.Services
{
    public class PantallaTurnosService
    {
        private readonly DataContext _context;

        public PantallaTurnosService(DataContext context)
        {
            _context = context;
        }

        public async Task<PantallaTurnosResponse> ObtenerTurnosPantallaAsync(decimal agenciaId, DateTime? ultimaFechaAudio = null, string? filtro = null)
        {
            var response = new PantallaTurnosResponse();

            if (agenciaId <= 0)
            {
                return response;
            }

            var hoy = DateTime.Today;
            var manana = hoy.AddDays(1);

            var visiblesBase = await _context.SI_ASIG_TURNO
                .AsNoTracking()
                .Where(x =>
                    x.AgCodigo == agenciaId &&
                    x.UsCodigo != 1 &&
                    x.TuId != null &&
                    x.AsgEstado != null &&
                    x.AsgEstado != "I" &&
                    (x.AsgEstado == "L" || x.AsgEstado == "R") &&
                    x.AsgModulo != null &&
                    x.AsgModulo != "N" &&
                    x.AsgFechMovi != null &&
                    x.AsgFechMovi >= hoy &&
                    x.AsgFechMovi < manana &&
                    (filtro == null || filtro == "todos" ||
                     (filtro == "mostrador" && x.TuId!.StartsWith("M")) ||
                     (filtro == "servicio" && !x.TuId!.StartsWith("M"))))
                .Select(x => new TurnoPantallaBase
                {
                    AsgCodigo = x.AsgCodigo,
                    TuId = x.TuId,
                    AsgModulo = x.AsgModulo,
                    AsgEstado = x.AsgEstado,
                    AsgTime = x.AsgTime,
                    CiCodigo = x.CiCodigo,
                    AsgFechMovi = x.AsgFechMovi,
                    AsgFechAsig = x.AsgFechAsig,
                    FechaReferencia = x.AsgEstado == "R"
                        ? x.AsgFechMovi
                        : x.AsgFechAsig ?? x.AsgFechMovi
                })
                .OrderByDescending(x => x.FechaReferencia)
                .ThenByDescending(x => x.AsgCodigo)
                .Take(30)
                .ToListAsync();

            var nombresVisibles = await ObtenerNombresClientesAsync(visiblesBase);
            var turnos = MapearTurnos(visiblesBase, nombresVisibles, true);

            var turnoActual = turnos
                .Where(x => x.Estado == "R" && x.EsTurnoActual)
                .OrderByDescending(x => x.FechaReferencia)
                .ThenByDescending(x => x.AsgCodigo)
                .FirstOrDefault();

            if (turnoActual == null)
            {
                turnoActual = turnos
                    .Where(x => x.Estado == "R" || x.Estado == "L")
                    .OrderByDescending(x => x.FechaReferencia)
                    .ThenByDescending(x => x.AsgCodigo)
                    .FirstOrDefault();
            }

            response.TurnoActual = turnoActual;

            response.TurnosRecienLlamados = turnos
                .Where(x => x.Estado == "R" && x.RequiereCambioEstado)
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
                    .Where(x => x.Estado != "I")
                    .ToList();
            }
            else
            {
                response.Turnos = turnos
                    .Where(x => x.Estado != "I")
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
                    x.AsgFechMovi >= hoy &&
                    x.AsgFechMovi < manana &&
                    (filtro == null || filtro == "todos" ||
                     (filtro == "mostrador" && x.TuId!.StartsWith("M")) ||
                     (filtro == "servicio" && !x.TuId!.StartsWith("M"))))
                .OrderBy(x => x.AsgFechMovi)
                .ThenBy(x => x.AsgCodigo)
                .Select(x => new TurnoPantallaBase
                {
                    AsgCodigo = x.AsgCodigo,
                    TuId = x.TuId,
                    AsgModulo = x.AsgModulo,
                    AsgEstado = x.AsgEstado,
                    AsgTime = x.AsgTime,
                    CiCodigo = x.CiCodigo,
                    AsgFechMovi = x.AsgFechMovi,
                    FechaReferencia = x.AsgFechMovi
                })
                .Take(10)
                .ToListAsync();

            var nombresPendientes = await ObtenerNombresClientesAsync(pendientesBase);
            response.TurnosPendientes = MapearTurnos(pendientesBase, nombresPendientes, false)
                .Where(x => x.Estado != "I")
                .ToList();

            return response;
        }

        private async Task<Dictionary<decimal, string>> ObtenerNombresClientesAsync(List<TurnoPantallaBase> turnos)
        {
            // Diccionario keyed por AsgCodigo (único por turno)
            var nombres = new Dictionary<decimal, string>();

            // con_cita: CiCodigo = AtCodigo → SI_AGEND_TECN → SI_CLIENTE
            var itemsConCita = turnos
                .Where(x => ObtenerTipoDesdeTurno(x.TuId ?? string.Empty) == "con_cita" && (x.CiCodigo ?? 0) > 0)
                .ToList();

            if (itemsConCita.Any())
            {
                var codigosCita = itemsConCita.Select(x => x.CiCodigo!.Value).Distinct().ToList();
                var nombresCita = await (
                    from at in _context.SI_AGEND_TECN.AsNoTracking()
                    join cl in _context.SI_CLIENTE.AsNoTracking() on at.ClCodigo equals cl.ClCodigo
                    where codigosCita.Contains(at.AtCodigo)
                    select new { at.AtCodigo, cl.ClContacto, cl.ClNombre, cl.ClApellido }
                ).ToListAsync();

                var mapCita = nombresCita.ToDictionary(x => x.AtCodigo, x => UnirNombreCliente(x.ClContacto, x.ClApellido, x.ClNombre));
                foreach (var item in itemsConCita)
                    if (mapCita.TryGetValue(item.CiCodigo!.Value, out var nombre))
                        nombres[item.AsgCodigo] = nombre;
            }

            // kiosco / sin_cita: CiCodigo = ClCodigo → SI_CLIENTE
            var itemsCliente = turnos
                .Where(x =>
                    (ObtenerTipoDesdeTurno(x.TuId ?? string.Empty) == "kiosco" ||
                     ObtenerTipoDesdeTurno(x.TuId ?? string.Empty) == "sin_cita") &&
                    (x.CiCodigo ?? 0) > 0)
                .ToList();

            if (itemsCliente.Any())
            {
                var codigosCliente = itemsCliente.Select(x => x.CiCodigo!.Value).Distinct().ToList();
                var nombresCliente = await _context.SI_CLIENTE
                    .AsNoTracking()
                    .Where(x => codigosCliente.Contains(x.ClCodigo))
                    .Select(x => new { x.ClCodigo, x.ClContacto, x.ClNombre, x.ClApellido })
                    .ToListAsync();

                var mapCliente = nombresCliente.ToDictionary(x => x.ClCodigo, x => UnirNombreCliente(x.ClContacto, x.ClApellido, x.ClNombre));
                foreach (var item in itemsCliente)
                    if (mapCliente.TryGetValue(item.CiCodigo!.Value, out var nombre))
                        nombres[item.AsgCodigo] = nombre;
            }

            // mostrador: AsgCodigo → SI_TURNO_KIOSCO → SI_CLIENTE (solo si tiene CL_CODIGO)
            var asgCodigosMostrador = turnos
                .Where(x => ObtenerTipoDesdeTurno(x.TuId ?? string.Empty) == "mostrador")
                .Select(x => x.AsgCodigo)
                .Distinct()
                .ToList();

            if (asgCodigosMostrador.Any())
            {
                var nombresMostrador = await (
                    from tk in _context.SI_TURNO_KIOSCO.AsNoTracking()
                    join cl in _context.SI_CLIENTE.AsNoTracking() on tk.ClCodigo equals cl.ClCodigo
                    where tk.AsgCodigo != null && asgCodigosMostrador.Contains(tk.AsgCodigo.Value)
                    select new { AsgCodigo = tk.AsgCodigo!.Value, cl.ClContacto, cl.ClNombre, cl.ClApellido }
                ).ToListAsync();

                foreach (var item in nombresMostrador)
                    nombres[item.AsgCodigo] = UnirNombreCliente(item.ClContacto, item.ClApellido, item.ClNombre);
            }

            return nombres;
        }

        private static List<PantallaTurnoDto> MapearTurnos(List<TurnoPantallaBase> turnosBase, Dictionary<decimal, string> nombres, bool validarActual)
        {
            var turnos = new List<PantallaTurnoDto>();

            foreach (var item in turnosBase)
            {
                var nombreCliente = nombres.TryGetValue(item.AsgCodigo, out var nombre) ? nombre : string.Empty;

                var estado = (item.AsgEstado ?? string.Empty).Trim();
                var tiempo = item.AsgTime ?? 0;

                if (estado == "I")
                    continue;

                turnos.Add(new PantallaTurnoDto
                {
                    AsgCodigo = item.AsgCodigo,
                    Turno = (item.TuId ?? string.Empty).Trim(),
                    Modulo = (item.AsgModulo ?? string.Empty).Trim(),
                    Estado = estado,
                    Tiempo = tiempo,
                    Tipo = ObtenerTipoDesdeTurno(item.TuId ?? string.Empty),
                    RequiereCambioEstado = estado == "R",
                    EsTurnoActual = validarActual && estado == "R" && tiempo > 0,
                    NombreCliente = nombreCliente,
                    FechaReferencia = item.FechaReferencia
                });
            }

            return turnos;
        }

        private static string UnirNombreCliente(string? contacto, string? apellido, string? nombre)
        {
            var nombreContacto = (contacto ?? string.Empty).Trim();
            var nombres = (nombre ?? string.Empty).Trim();
            var apellidos = (apellido ?? string.Empty).Trim();

            if (!string.IsNullOrWhiteSpace(nombreContacto))
                return nombreContacto;

            return $"{nombres} {apellidos}".Trim();
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

        private class TurnoPantallaBase
        {
            public decimal AsgCodigo { get; set; }
            public string? TuId { get; set; }
            public string? AsgModulo { get; set; }
            public string? AsgEstado { get; set; }
            public decimal? AsgTime { get; set; }
            public decimal? CiCodigo { get; set; }
            public DateTime? AsgFechMovi { get; set; }
            public DateTime? AsgFechAsig { get; set; }
            public DateTime? FechaReferencia { get; set; }
        }
    }
}