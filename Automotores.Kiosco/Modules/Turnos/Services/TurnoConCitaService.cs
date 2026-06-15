using Automotores.Kiosco.Data;
using Automotores.Kiosco.Modules.Turnos.Responses;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace Automotores.Kiosco.Modules.Turnos.Services
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
                return new RegistrarLlegadaCitaResponse
                {
                    Resultado = "error",
                    Codigo = "AGENCIA_REQUERIDA",
                    Mensaje = "La agencia es requerida."
                };
            }

            if (citaId <= 0)
            {
                return new RegistrarLlegadaCitaResponse
                {
                    Resultado = "error",
                    Codigo = "CITA_REQUERIDA",
                    Mensaje = "La cita es requerida."
                };
            }

            var existeCita = await _context.SI_AGEND_TECN
                .AsNoTracking()
                .AnyAsync(x => x.AtCodigo == citaId);

            if (!existeCita)
            {
                return new RegistrarLlegadaCitaResponse
                {
                    Resultado = "error",
                    Codigo = "NO_EXISTE",
                    Mensaje = "La cita no existe."
                };
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
                        Codigo = "SIN_RESPUESTA",
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
                        Codigo = "RESPUESTA_INVALIDA",
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
            catch
            {
                return new RegistrarLlegadaCitaResponse
                {
                    Resultado = "error",
                    Codigo = "ERROR_INTERNO",
                    Mensaje = "Ocurrió un error interno al procesar la cita."
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