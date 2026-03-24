using Automotores.Kiosco.Data;
using Automotores.Kiosco.Models.request;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace Automotores.Kiosco.Services
{
    public class TurnoConCitaService
    {
        private readonly DataContext _context;

        public TurnoConCitaService(DataContext context)
        {
            _context = context;
        }

        public async Task<RegistrarLlegadaCitaResponse> RegistrarLlegadaAsync(decimal agenciaId, decimal citaId)
        {
            if (agenciaId <= 0)
            {
                throw new ArgumentException("La agencia es requerida.");
            }

            if (citaId <= 0)
            {
                throw new ArgumentException("La cita es requerida.");
            }

            var conexion = _context.Database.GetDbConnection();
            var debeCerrar = conexion.State != ConnectionState.Open;

            if (debeCerrar)
            {
                await conexion.OpenAsync();
            }

            try
            {
                await using var comando = conexion.CreateCommand();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "SP_CONS_TURNO";

                comando.Parameters.Add(CrearParametro(comando, "@NUMERO", 7));
                comando.Parameters.Add(CrearParametro(comando, "@CODIGO1", agenciaId));
                comando.Parameters.Add(CrearParametro(comando, "@CODIGO2", citaId));

                await using var reader = await comando.ExecuteReaderAsync();

                if (!await reader.ReadAsync())
                {
                    return new RegistrarLlegadaCitaResponse
                    {
                        Resultado = "error",
                        Mensaje = "No fue posible procesar la llegada de la cita."
                    };
                }

                var idCita = ObtenerTexto(reader, "ID_CITA");
                var nroBahia = ObtenerTexto(reader, "NroBahia");
                var tecnico = ObtenerTexto(reader, "TECNICO");

                if (string.IsNullOrWhiteSpace(idCita))
                {
                    return new RegistrarLlegadaCitaResponse
                    {
                        Resultado = "error",
                        Mensaje = "No fue posible procesar la llegada de la cita."
                    };
                }

                if (idCita.Equals("OK", StringComparison.OrdinalIgnoreCase))
                {
                    return new RegistrarLlegadaCitaResponse
                    {
                        Resultado = "atencion_directa",
                        Bahia = string.IsNullOrWhiteSpace(nroBahia) ? null : nroBahia,
                        Tecnico = string.IsNullOrWhiteSpace(tecnico) ? null : tecnico,
                        Mensaje = "El cliente pasa directamente a bahía."
                    };
                }

                if (idCita.Equals("ERR", StringComparison.OrdinalIgnoreCase))
                {
                    return new RegistrarLlegadaCitaResponse
                    {
                        Resultado = "error",
                        Codigo = string.IsNullOrWhiteSpace(nroBahia) ? "ERR" : nroBahia,
                        Mensaje = "La cita no puede procesarse en este flujo."
                    };
                }

                return new RegistrarLlegadaCitaResponse
                {
                    Resultado = "turno_generado",
                    Turno = idCita,
                    Mensaje = "Se generó el turno de la cita correctamente."
                };
            }
            finally
            {
                if (debeCerrar && conexion.State == ConnectionState.Open)
                {
                    await conexion.CloseAsync();
                }
            }
        }

        private static DbParameter CrearParametro(DbCommand comando, string nombre, object valor)
        {
            var parametro = comando.CreateParameter();
            parametro.ParameterName = nombre;
            parametro.Value = valor;
            return parametro;
        }

        private static string ObtenerTexto(DbDataReader reader, string columna)
        {
            var ordinal = reader.GetOrdinal(columna);
            if (reader.IsDBNull(ordinal))
            {
                return string.Empty;
            }

            return Convert.ToString(reader.GetValue(ordinal))?.Trim() ?? string.Empty;
        }
    }
}