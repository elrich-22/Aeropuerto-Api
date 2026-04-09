using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class VueloService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public VueloService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<VueloResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<VueloResponseDto>();

            var query = _sqlQueryProvider.GetQuery("VueloService/ObtenerTodosAsync.sql");

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new VueloResponseDto
                {
                    Id = Convert.ToInt32(reader["VUE_ID_VUELO"]),
                    IdPrograma = Convert.ToInt32(reader["VUE_ID_PROGRAMA_VUELO"]),
                    IdAvion = Convert.ToInt32(reader["VUE_ID_AVION"]),
                    FechaVuelo = Convert.ToDateTime(reader["VUE_FECHA_VUELO"]),
                    HoraSalidaReal = reader["VUE_HORA_SALIDA_REAL"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["VUE_HORA_SALIDA_REAL"]),
                    HoraLlegadaReal = reader["VUE_HORA_LLEGADA_REAL"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["VUE_HORA_LLEGADA_REAL"]),
                    PlazasOcupadas = reader["VUE_PLAZAS_OCUPADAS"] == DBNull.Value
                        ? 0
                        : Convert.ToInt32(reader["VUE_PLAZAS_OCUPADAS"]),
                    PlazasVacias = reader["VUE_PLAZAS_VACIAS"] == DBNull.Value
                        ? null
                        : Convert.ToInt32(reader["VUE_PLAZAS_VACIAS"]),
                    Estado = reader["VUE_ESTADO"] == DBNull.Value
                        ? string.Empty
                        : reader["VUE_ESTADO"].ToString() ?? string.Empty,
                    FechaReprogramacion = reader["VUE_FECHA_REPROGRAMACION"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["VUE_FECHA_REPROGRAMACION"]),
                    MotivoCancelacion = reader["VUE_MOTIVO_CANCELACION"] == DBNull.Value
                        ? null
                        : reader["VUE_MOTIVO_CANCELACION"].ToString(),
                    PuertaEmbarque = reader["VUE_PUERTA_EMBARQUE"] == DBNull.Value
                        ? null
                        : reader["VUE_PUERTA_EMBARQUE"].ToString(),
                    Terminal = reader["VUE_TERMINAL"] == DBNull.Value
                        ? null
                        : reader["VUE_TERMINAL"].ToString(),
                    RetrasoMinutos = reader["VUE_RETRASO_MINUTOS"] == DBNull.Value
                        ? 0
                        : Convert.ToInt32(reader["VUE_RETRASO_MINUTOS"])
                });
            }

            return lista;
        }

        public async Task<VueloResponseDto?> ObtenerPorIdAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("VueloService/ObtenerPorIdAsync.sql");

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new VueloResponseDto
                {
                    Id = Convert.ToInt32(reader["VUE_ID_VUELO"]),
                    IdPrograma = Convert.ToInt32(reader["VUE_ID_PROGRAMA_VUELO"]),
                    IdAvion = Convert.ToInt32(reader["VUE_ID_AVION"]),
                    FechaVuelo = Convert.ToDateTime(reader["VUE_FECHA_VUELO"]),
                    HoraSalidaReal = reader["VUE_HORA_SALIDA_REAL"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["VUE_HORA_SALIDA_REAL"]),
                    HoraLlegadaReal = reader["VUE_HORA_LLEGADA_REAL"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["VUE_HORA_LLEGADA_REAL"]),
                    PlazasOcupadas = reader["VUE_PLAZAS_OCUPADAS"] == DBNull.Value
                        ? 0
                        : Convert.ToInt32(reader["VUE_PLAZAS_OCUPADAS"]),
                    PlazasVacias = reader["VUE_PLAZAS_VACIAS"] == DBNull.Value
                        ? null
                        : Convert.ToInt32(reader["VUE_PLAZAS_VACIAS"]),
                    Estado = reader["VUE_ESTADO"] == DBNull.Value
                        ? string.Empty
                        : reader["VUE_ESTADO"].ToString() ?? string.Empty,
                    FechaReprogramacion = reader["VUE_FECHA_REPROGRAMACION"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["VUE_FECHA_REPROGRAMACION"]),
                    MotivoCancelacion = reader["VUE_MOTIVO_CANCELACION"] == DBNull.Value
                        ? null
                        : reader["VUE_MOTIVO_CANCELACION"].ToString(),
                    PuertaEmbarque = reader["VUE_PUERTA_EMBARQUE"] == DBNull.Value
                        ? null
                        : reader["VUE_PUERTA_EMBARQUE"].ToString(),
                    Terminal = reader["VUE_TERMINAL"] == DBNull.Value
                        ? null
                        : reader["VUE_TERMINAL"].ToString(),
                    RetrasoMinutos = reader["VUE_RETRASO_MINUTOS"] == DBNull.Value
                        ? 0
                        : Convert.ToInt32(reader["VUE_RETRASO_MINUTOS"])
                };
            }

            return null;
        }

        public async Task<bool> CrearAsync(VueloCreateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("VueloService/CrearAsync.sql");

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("idPrograma", OracleDbType.Int32).Value = dto.IdPrograma;
            command.Parameters.Add("idAvion", OracleDbType.Int32).Value = dto.IdAvion;
            command.Parameters.Add("fechaVuelo", OracleDbType.Date).Value = dto.FechaVuelo;
            command.Parameters.Add("horaSalidaReal", OracleDbType.TimeStamp).Value = (object?)dto.HoraSalidaReal ?? DBNull.Value;
            command.Parameters.Add("horaLlegadaReal", OracleDbType.TimeStamp).Value = (object?)dto.HoraLlegadaReal ?? DBNull.Value;
            command.Parameters.Add("plazasOcupadas", OracleDbType.Int32).Value = dto.PlazasOcupadas ?? 0;
            command.Parameters.Add("plazasVacias", OracleDbType.Int32).Value = (object?)dto.PlazasVacias ?? DBNull.Value;
            command.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "PROGRAMADO";
            command.Parameters.Add("fechaReprogramacion", OracleDbType.Date).Value = (object?)dto.FechaReprogramacion ?? DBNull.Value;
            command.Parameters.Add("motivoCancelacion", OracleDbType.Varchar2).Value = (object?)dto.MotivoCancelacion ?? DBNull.Value;
            command.Parameters.Add("puertaEmbarque", OracleDbType.Varchar2).Value = (object?)dto.PuertaEmbarque ?? DBNull.Value;
            command.Parameters.Add("terminal", OracleDbType.Varchar2).Value = (object?)dto.Terminal ?? DBNull.Value;
            command.Parameters.Add("retrasoMinutos", OracleDbType.Int32).Value = dto.RetrasoMinutos ?? 0;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> ActualizarAsync(int id, VueloUpdateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("VueloService/ActualizarAsync.sql");

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("idPrograma", OracleDbType.Int32).Value = dto.IdPrograma;
            command.Parameters.Add("idAvion", OracleDbType.Int32).Value = dto.IdAvion;
            command.Parameters.Add("fechaVuelo", OracleDbType.Date).Value = dto.FechaVuelo;
            command.Parameters.Add("horaSalidaReal", OracleDbType.TimeStamp).Value = (object?)dto.HoraSalidaReal ?? DBNull.Value;
            command.Parameters.Add("horaLlegadaReal", OracleDbType.TimeStamp).Value = (object?)dto.HoraLlegadaReal ?? DBNull.Value;
            command.Parameters.Add("plazasOcupadas", OracleDbType.Int32).Value = dto.PlazasOcupadas;
            command.Parameters.Add("plazasVacias", OracleDbType.Int32).Value = (object?)dto.PlazasVacias ?? DBNull.Value;
            command.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "PROGRAMADO";
            command.Parameters.Add("fechaReprogramacion", OracleDbType.Date).Value = (object?)dto.FechaReprogramacion ?? DBNull.Value;
            command.Parameters.Add("motivoCancelacion", OracleDbType.Varchar2).Value = (object?)dto.MotivoCancelacion ?? DBNull.Value;
            command.Parameters.Add("puertaEmbarque", OracleDbType.Varchar2).Value = (object?)dto.PuertaEmbarque ?? DBNull.Value;
            command.Parameters.Add("terminal", OracleDbType.Varchar2).Value = (object?)dto.Terminal ?? DBNull.Value;
            command.Parameters.Add("retrasoMinutos", OracleDbType.Int32).Value = dto.RetrasoMinutos;
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("VueloService/EliminarAsync.sql");

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}
