using Automotores.Kiosco.Data;
using Automotores.Kiosco.Models;
using Automotores.Kiosco.Models.dto;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Automotores.Kiosco.Services
{
    public class TurnoGeneradorService
    {
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

            var turnoDb = await _context.SI_TURNO
                .Where(x => x.AgCodigo == agenciaId)
                .FirstOrDefaultAsync();

            if (turnoDb == null)
                throw new Exception("No existe configuración de turnos para la agencia");

            int contador = tipo == "S"
                ? (int)(turnoDb.TuSinCita ?? 0)
                : (int)(turnoDb.TuFlota ?? 0);

            int nuevo = contador + 1;

            if (nuevo == 31)
                nuevo = 1;

            // actualizar contador
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
                AsgFechMovi = DateTime.Now,
                AsgModulo = "N",
                CiCodigo = 0,
                AgCodigo = agenciaId,
                AsgEstado = "A",
                AsgFechAsig = null,
                AsgTime = 0,
                AsgTimeEspe = 0,
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
                Fecha = DateTime.Now
            };
        }
    }
}