using Automotores.Kiosco.Data;
using Automotores.Kiosco.Modules.Turnos.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Automotores.Kiosco.Modules.Turnos.Services
{
    public class TurnoService
    {
        private const int MinutosPromedioPorTurno = 5;

        private readonly DataContext _context;

        public TurnoService(DataContext context)
        {
            _context = context;
        }

        public async Task<decimal?> ObtenerCodigoCitaActivaPorIdentificacionAsync(string identificacion, decimal agenciaId)
        {
            identificacion = identificacion.Trim();

            if (string.IsNullOrWhiteSpace(identificacion) || agenciaId <= 0)
                return null;

            var hoy = DateTime.Today;
            var manana = hoy.AddDays(1);

            var codigoCita = await (
                from a in _context.SI_AGEND_TECN.AsNoTracking()
                join b in _context.SI_CLIENTE.AsNoTracking() on a.ClCodigo equals b.ClCodigo
                join h in _context.SI_BAHIA.AsNoTracking() on a.BhCodigo equals h.BhCodigo
                where b.ClId == identificacion
                    && a.AtFecha >= hoy
                    && a.AtFecha < manana
                    && a.TlCodigo.HasValue
                    && new[] { 5m, 8m, 17m, 18m }.Contains(a.TlCodigo.Value)
                    && h.AgCodigo == agenciaId
                    && a.AtStatus.HasValue
                    && new[] { 1m, 2m, 6m }.Contains(a.AtStatus.Value)
                    && a.HtCodigo == 0
                    && a.AtFechLleg == null
                orderby a.AtHoraInicio, a.AtCodigo
                select (decimal?)a.AtCodigo
            ).FirstOrDefaultAsync();

            return codigoCita;
        }

        public async Task<List<CitaRecepcionDto>> ObtenerCitasRecepcionAsync(decimal agenciaId)
        {
            var hoy = DateTime.Today;
            var manana = hoy.AddDays(1);

            var activas = await (
                from a in _context.SI_AGEND_TECN.AsNoTracking()
                join b in _context.SI_CLIENTE.AsNoTracking() on a.ClCodigo equals b.ClCodigo
                join d in _context.SI_STICKER.AsNoTracking() on a.StCodigo equals d.StCodigo
                join m in _context.SI_MODELO.AsNoTracking() on d.MoCodigo equals m.MoCodigo
                join h in _context.SI_BAHIA.AsNoTracking() on a.BhCodigo equals h.BhCodigo
                where a.AtFecha >= hoy
                    && a.AtFecha < manana
                    && a.TlCodigo.HasValue
                    && new[] { 5m, 8m, 17m, 18m }.Contains(a.TlCodigo.Value)
                    && h.AgCodigo == agenciaId
                    && a.AtStatus.HasValue
                    && new[] { 1m, 2m, 6m }.Contains(a.AtStatus.Value)
                    && a.HtCodigo == 0
                select new CitaRecepcionDto
                {
                    CodigoCita = a.AtCodigo,
                    HoraCita = FormatearHora(a.AtHoraInicio),
                    Placa = d.StPlaca ?? string.Empty,
                    NombreCliente = FormatearNombreCliente(b.ClApellido, b.ClNombre, m.MoNombre),
                    ModeloVehiculo = m.MoNombre ?? string.Empty,
                    Bahia = FormatearBahia(h.BhNombre),
                    Estatus = a.AtStatus ?? 0,
                    TlCodigo = a.TlCodigo ?? 0,
                    Estado = MapearEstado(a.AtStatus ?? 0),
                    TipoLabor = MapearTipoLabor(a.TlCodigo ?? 0),
                    ClaveVisual = MapearClaveVisual(a.AtStatus ?? 0, a.TlCodigo ?? 0)
                }
            ).ToListAsync();

            var canceladas = await (
                from a in _context.SI_AGEND_TECN.AsNoTracking()
                join b in _context.SI_CLIENTE.AsNoTracking() on a.ClCodigo equals b.ClCodigo
                join d in _context.SI_STICKER.AsNoTracking() on a.StCodigo equals d.StCodigo
                join m in _context.SI_MODELO.AsNoTracking() on d.MoCodigo equals m.MoCodigo
                join h in _context.SI_BAHIA.AsNoTracking() on a.BhCodigo equals h.BhCodigo
                where a.AtFecha >= hoy
                    && a.AtFecha < manana
                    && a.TlCodigo.HasValue
                    && new[] { 5m, 8m, 17m, 18m }.Contains(a.TlCodigo.Value)
                    && h.AgCodigo == agenciaId
                    && a.AtStatus == 5
                    && a.AtFechLleg == null
                    && a.AtFechCanc.HasValue
                    && a.AtFechCanc.Value >= hoy
                    && a.AtFechCanc.Value < manana
                    && a.HtCodigo == 0
                select new CitaRecepcionDto
                {
                    CodigoCita = a.AtCodigo,
                    HoraCita = FormatearHora(a.AtHoraInicio),
                    Placa = d.StPlaca ?? string.Empty,
                    NombreCliente = FormatearNombreCliente(b.ClApellido, b.ClNombre, m.MoNombre),
                    ModeloVehiculo = m.MoNombre ?? string.Empty,
                    Bahia = FormatearBahia(h.BhNombre),
                    Estatus = a.AtStatus ?? 0,
                    TlCodigo = a.TlCodigo ?? 0,
                    Estado = MapearEstado(a.AtStatus ?? 0),
                    TipoLabor = MapearTipoLabor(a.TlCodigo ?? 0),
                    ClaveVisual = MapearClaveVisual(a.AtStatus ?? 0, a.TlCodigo ?? 0)
                }
            ).ToListAsync();

            return activas
                .Concat(canceladas)
                .OrderBy(x => x.HoraCita)
                .ThenBy(x => x.CodigoCita)
                .ToList();
        }

        public async Task<TurnoClienteDto?> ObtenerTurnoPorIdentificacionAsync(string identificacion, decimal agenciaId)
        {
            identificacion = identificacion.Trim();

            if (string.IsNullOrWhiteSpace(identificacion) || agenciaId <= 0)
                return null;

            var hoy = DateTime.Today;
            var manana = hoy.AddDays(1);

            var resultado = await (
                from cliente in _context.SI_CLIENTE.AsNoTracking()
                join cita in _context.SI_AGEND_TECN.AsNoTracking()
                    on cliente.ClCodigo equals cita.ClCodigo
                join turno in _context.SI_ASIG_TURNO.AsNoTracking()
                    on cita.AtCodigo equals turno.CiCodigo
                where cliente.ClId == identificacion
                    && turno.AgCodigo == agenciaId
                    && turno.TuId != null
                    && turno.AsgEstado != null
                    && turno.AsgEstado != "I"
                    && turno.AsgFechMovi != null
                    && turno.AsgFechMovi >= hoy
                    && turno.AsgFechMovi < manana
                orderby turno.AsgFechMovi descending, turno.AsgCodigo descending
                select new TurnoClienteDto
                {
                    AsgCodigo = turno.AsgCodigo,
                    Turno = turno.TuId == null ? string.Empty : turno.TuId.Trim(),
                    Estado = turno.AsgEstado == null ? string.Empty : turno.AsgEstado.Trim(),
                    Modulo = turno.AsgModulo == null ? string.Empty : turno.AsgModulo.Trim(),
                    AgenciaId = turno.AgCodigo ?? 0,
                    Tiempo = turno.AsgTime,
                    TiempoEspera = turno.AsgTimeEspe,
                    FechaMovimiento = turno.AsgFechMovi,
                    FechaAsignacion = turno.AsgFechAsig,
                    ClCodigo = cliente.ClCodigo,
                    Identificacion = cliente.ClId,
                    Cliente = FormatearNombreCompleto(cliente.ClNombre, cliente.ClApellido),
                    CitaId = cita.AtCodigo,
                    FechaCita = cita.AtFecha,
                    HoraCita = cita.AtHoraInicio ?? string.Empty,
                    Tipo = ObtenerTipoDesdeTurno(turno.TuId),
                    Area = ObtenerAreaDesdeTurnoYTipoLabor(turno.TuId, cita.TlCodigo),
                    TelefonoCliente = cliente.ClTelefono2 ?? string.Empty
                }
            ).FirstOrDefaultAsync();

            if (resultado == null)
                return null;

            var personasPorDelante = await CalcularPersonasPorDelanteAsync(
                agenciaId,
                resultado.AsgCodigo,
                hoy,
                manana
            );

            resultado.PersonasPorDelante = personasPorDelante;
            resultado.TiempoEstimadoMinutos = CalcularTiempoEstimado(
                resultado.Estado,
                personasPorDelante
            );

            return resultado;
        }

        private async Task<int> CalcularPersonasPorDelanteAsync(
            decimal agenciaId,
            decimal asgCodigo,
            DateTime hoy,
            DateTime manana)
        {
            return await _context.SI_ASIG_TURNO
                .AsNoTracking()
                .CountAsync(x =>
                    x.AgCodigo == agenciaId &&
                    x.AsgEstado == "A" &&
                    x.AsgModulo == "N" &&
                    x.AsgFechMovi != null &&
                    x.AsgFechMovi >= hoy &&
                    x.AsgFechMovi < manana &&
                    x.AsgCodigo < asgCodigo);
        }

        private static int CalcularTiempoEstimado(string estado, int personasPorDelante)
        {
            var estadoNormalizado = (estado ?? string.Empty).Trim().ToUpperInvariant();

            if (estadoNormalizado != "A")
                return 0;

            return (personasPorDelante + 1) * MinutosPromedioPorTurno;
        }

        private static string ObtenerAreaDesdeTurnoYTipoLabor(string? turno, decimal? tlCodigo)
        {
            var valorTurno = (turno ?? string.Empty).Trim().ToUpperInvariant();

            if (valorTurno.StartsWith("F"))
                return "Flota de Empresa";

            if (tlCodigo == 17)
                return "Taller / Servicios";

            if (tlCodigo == 18)
                return "Servicio Rápido";

            if (tlCodigo == 5)
                return "Taller / Servicios";

            if (tlCodigo == 8)
                return "Taller / Servicios";

            return "Taller / Servicios";
        }

        private static string ObtenerTipoDesdeTurno(string? turno)
        {
            var valor = (turno ?? string.Empty).Trim().ToUpperInvariant();

            if (valor.StartsWith("C")) return "con_cita";
            if (valor.StartsWith("S")) return "sin_cita";
            if (valor.StartsWith("F")) return "flota";
            if (valor.StartsWith("L")) return "latoneria";

            return "otro";
        }

        private static string FormatearHora(string? hora)
        {
            if (string.IsNullOrWhiteSpace(hora))
                return string.Empty;

            return hora.Length >= 5 ? hora[..5] : hora;
        }

        private static string FormatearNombreCliente(string? apellido, string? nombre, string? modelo)
        {
            var cliente = $"{apellido} {nombre}".Trim();
            var clienteCorto = cliente.Length > 20 ? cliente[..20] + "..." : cliente;
            var modeloLimpio = (modelo ?? string.Empty).Trim();
            var modeloCorto = modeloLimpio.Length > 20 ? modeloLimpio[..20] + "..." : modeloLimpio;

            return $"{clienteCorto} | {modeloCorto}";
        }

        private static string FormatearNombreCompleto(string? nombre, string? apellido)
        {
            return $"{(nombre ?? string.Empty).Trim()} {(apellido ?? string.Empty).Trim()}".Trim();
        }

        private static string FormatearBahia(string? bahia)
        {
            var valor = (bahia ?? string.Empty).Trim();
            return valor.Length > 10 ? valor[..10] + ".." : valor;
        }

        private static string MapearEstado(decimal estatus)
        {
            if (estatus == 1) return "agendado";
            if (estatus == 2) return "confirmado";
            if (estatus == 5) return "cancelado";
            if (estatus == 6) return "no_llego";
            return "desconocido";
        }

        private static string MapearTipoLabor(decimal tlCodigo)
        {
            if (tlCodigo == 5) return "mantenimiento";
            if (tlCodigo == 8) return "reparacion";
            if (tlCodigo == 17) return "recepcion";
            if (tlCodigo == 18) return "servicio_rapido";
            return "otro";
        }

        private static string MapearClaveVisual(decimal estatus, decimal tlCodigo)
        {
            if (estatus == 5) return "cancelado";
            if (estatus == 6) return "no_llego";

            if (estatus == 1 || estatus == 2)
            {
                if (tlCodigo == 5) return "mantenimiento";
                return "reparacion";
            }

            return "normal";
        }
    }
}