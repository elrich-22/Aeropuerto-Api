using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class VueloService
    {
        private readonly string _connectionString;

        public VueloService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<VueloResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<VueloResponseDto>();

            const string query = @"
                SELECT
                    VUE_ID_VUELO,
                    VUE_ID_PROGRAMA_VUELO,
                    VUE_ID_AVION,
                    VUE_FECHA_VUELO,
                    VUE_HORA_SALIDA_REAL,
                    VUE_HORA_LLEGADA_REAL,
                    VUE_PLAZAS_OCUPADAS,
                    VUE_PLAZAS_VACIAS,
                    VUE_ESTADO,
                    VUE_FECHA_REPROGRAMACION,
                    VUE_MOTIVO_CANCELACION,
                    VUE_PUERTA_EMBARQUE,
                    VUE_TERMINAL,
                    VUE_RETRASO_MINUTOS
                FROM AER_VUELO
                ORDER BY VUE_ID_VUELO";

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
            const string query = @"
                SELECT
                    VUE_ID_VUELO,
                    VUE_ID_PROGRAMA_VUELO,
                    VUE_ID_AVION,
                    VUE_FECHA_VUELO,
                    VUE_HORA_SALIDA_REAL,
                    VUE_HORA_LLEGADA_REAL,
                    VUE_PLAZAS_OCUPADAS,
                    VUE_PLAZAS_VACIAS,
                    VUE_ESTADO,
                    VUE_FECHA_REPROGRAMACION,
                    VUE_MOTIVO_CANCELACION,
                    VUE_PUERTA_EMBARQUE,
                    VUE_TERMINAL,
                    VUE_RETRASO_MINUTOS
                FROM AER_VUELO
                WHERE VUE_ID_VUELO = :id";

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
            const string query = @"
                INSERT INTO AER_VUELO
                (
                    VUE_ID_PROGRAMA_VUELO,
                    VUE_ID_AVION,
                    VUE_FECHA_VUELO,
                    VUE_HORA_SALIDA_REAL,
                    VUE_HORA_LLEGADA_REAL,
                    VUE_PLAZAS_OCUPADAS,
                    VUE_PLAZAS_VACIAS,
                    VUE_ESTADO,
                    VUE_FECHA_REPROGRAMACION,
                    VUE_MOTIVO_CANCELACION,
                    VUE_PUERTA_EMBARQUE,
                    VUE_TERMINAL,
                    VUE_RETRASO_MINUTOS
                )
                VALUES
                (
                    :idPrograma,
                    :idAvion,
                    :fechaVuelo,
                    :horaSalidaReal,
                    :horaLlegadaReal,
                    :plazasOcupadas,
                    :plazasVacias,
                    :estado,
                    :fechaReprogramacion,
                    :motivoCancelacion,
                    :puertaEmbarque,
                    :terminal,
                    :retrasoMinutos
                )";

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
            const string query = @"
                UPDATE AER_VUELO
                SET
                    VUE_ID_PROGRAMA_VUELO = :idPrograma,
                    VUE_ID_AVION = :idAvion,
                    VUE_FECHA_VUELO = :fechaVuelo,
                    VUE_HORA_SALIDA_REAL = :horaSalidaReal,
                    VUE_HORA_LLEGADA_REAL = :horaLlegadaReal,
                    VUE_PLAZAS_OCUPADAS = :plazasOcupadas,
                    VUE_PLAZAS_VACIAS = :plazasVacias,
                    VUE_ESTADO = :estado,
                    VUE_FECHA_REPROGRAMACION = :fechaReprogramacion,
                    VUE_MOTIVO_CANCELACION = :motivoCancelacion,
                    VUE_PUERTA_EMBARQUE = :puertaEmbarque,
                    VUE_TERMINAL = :terminal,
                    VUE_RETRASO_MINUTOS = :retrasoMinutos
                WHERE VUE_ID_VUELO = :id";

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
            const string query = @"
                DELETE FROM AER_VUELO
                WHERE VUE_ID_VUELO = :id";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}