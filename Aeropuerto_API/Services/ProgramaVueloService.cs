using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class ProgramaVueloService
    {
        private readonly string _connectionString;

        public ProgramaVueloService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<ProgramaVueloResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<ProgramaVueloResponseDto>();

            const string query = @"
                SELECT
                    PRV_ID,
                    PRV_NUMERO_VUELO,
                    PRV_ID_AEROLINEA,
                    PRV_ID_AEROPUERTO_ORIGEN,
                    PRV_ID_AEROPUERTO_DESTINO,
                    PRV_HORA_SALIDA_PROGRAMADA,
                    PRV_HORA_LLEGADA_PROGRAMADA,
                    PRV_DURACION_ESTIMADA,
                    PRV_TIPO_VUELO,
                    PRV_ESTADO,
                    PRV_FECHA_CREACION
                FROM AER_PROGRAMAVUELO
                ORDER BY PRV_ID";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new ProgramaVueloResponseDto
                {
                    Id = Convert.ToInt32(reader["PRV_ID"]),
                    NumeroVuelo = reader["PRV_NUMERO_VUELO"].ToString() ?? string.Empty,
                    IdAerolinea = Convert.ToInt32(reader["PRV_ID_AEROLINEA"]),
                    IdAeropuertoOrigen = Convert.ToInt32(reader["PRV_ID_AEROPUERTO_ORIGEN"]),
                    IdAeropuertoDestino = Convert.ToInt32(reader["PRV_ID_AEROPUERTO_DESTINO"]),
                    HoraSalidaProgramada = reader["PRV_HORA_SALIDA_PROGRAMADA"].ToString() ?? string.Empty,
                    HoraLlegadaProgramada = reader["PRV_HORA_LLEGADA_PROGRAMADA"].ToString() ?? string.Empty,
                    DuracionEstimada = reader["PRV_DURACION_ESTIMADA"] == DBNull.Value ? null : Convert.ToInt32(reader["PRV_DURACION_ESTIMADA"]),
                    TipoVuelo = reader["PRV_TIPO_VUELO"] == DBNull.Value ? null : reader["PRV_TIPO_VUELO"].ToString(),
                    Estado = reader["PRV_ESTADO"] == DBNull.Value ? string.Empty : reader["PRV_ESTADO"].ToString() ?? string.Empty,
                    FechaCreacion = reader["PRV_FECHA_CREACION"] == DBNull.Value ? null : Convert.ToDateTime(reader["PRV_FECHA_CREACION"])
                });
            }

            return lista;
        }

        public async Task<ProgramaVueloResponseDto?> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT
                    PRV_ID,
                    PRV_NUMERO_VUELO,
                    PRV_ID_AEROLINEA,
                    PRV_ID_AEROPUERTO_ORIGEN,
                    PRV_ID_AEROPUERTO_DESTINO,
                    PRV_HORA_SALIDA_PROGRAMADA,
                    PRV_HORA_LLEGADA_PROGRAMADA,
                    PRV_DURACION_ESTIMADA,
                    PRV_TIPO_VUELO,
                    PRV_ESTADO,
                    PRV_FECHA_CREACION
                FROM AER_PROGRAMAVUELO
                WHERE PRV_ID = :id";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new ProgramaVueloResponseDto
                {
                    Id = Convert.ToInt32(reader["PRV_ID"]),
                    NumeroVuelo = reader["PRV_NUMERO_VUELO"].ToString() ?? string.Empty,
                    IdAerolinea = Convert.ToInt32(reader["PRV_ID_AEROLINEA"]),
                    IdAeropuertoOrigen = Convert.ToInt32(reader["PRV_ID_AEROPUERTO_ORIGEN"]),
                    IdAeropuertoDestino = Convert.ToInt32(reader["PRV_ID_AEROPUERTO_DESTINO"]),
                    HoraSalidaProgramada = reader["PRV_HORA_SALIDA_PROGRAMADA"].ToString() ?? string.Empty,
                    HoraLlegadaProgramada = reader["PRV_HORA_LLEGADA_PROGRAMADA"].ToString() ?? string.Empty,
                    DuracionEstimada = reader["PRV_DURACION_ESTIMADA"] == DBNull.Value ? null : Convert.ToInt32(reader["PRV_DURACION_ESTIMADA"]),
                    TipoVuelo = reader["PRV_TIPO_VUELO"] == DBNull.Value ? null : reader["PRV_TIPO_VUELO"].ToString(),
                    Estado = reader["PRV_ESTADO"] == DBNull.Value ? string.Empty : reader["PRV_ESTADO"].ToString() ?? string.Empty,
                    FechaCreacion = reader["PRV_FECHA_CREACION"] == DBNull.Value ? null : Convert.ToDateTime(reader["PRV_FECHA_CREACION"])
                };
            }

            return null;
        }

        public async Task<bool> CrearAsync(ProgramaVueloCreateDto dto)
        {
            const string query = @"
                INSERT INTO AER_PROGRAMAVUELO
                (
                    PRV_NUMERO_VUELO,
                    PRV_ID_AEROLINEA,
                    PRV_ID_AEROPUERTO_ORIGEN,
                    PRV_ID_AEROPUERTO_DESTINO,
                    PRV_HORA_SALIDA_PROGRAMADA,
                    PRV_HORA_LLEGADA_PROGRAMADA,
                    PRV_DURACION_ESTIMADA,
                    PRV_TIPO_VUELO,
                    PRV_ESTADO
                )
                VALUES
                (
                    :numeroVuelo,
                    :idAerolinea,
                    :idAeropuertoOrigen,
                    :idAeropuertoDestino,
                    :horaSalidaProgramada,
                    :horaLlegadaProgramada,
                    :duracionEstimada,
                    :tipoVuelo,
                    :estado
                )";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("numeroVuelo", OracleDbType.Varchar2).Value = dto.NumeroVuelo;
            command.Parameters.Add("idAerolinea", OracleDbType.Int32).Value = dto.IdAerolinea;
            command.Parameters.Add("idAeropuertoOrigen", OracleDbType.Int32).Value = dto.IdAeropuertoOrigen;
            command.Parameters.Add("idAeropuertoDestino", OracleDbType.Int32).Value = dto.IdAeropuertoDestino;
            command.Parameters.Add("horaSalidaProgramada", OracleDbType.Char).Value = dto.HoraSalidaProgramada;
            command.Parameters.Add("horaLlegadaProgramada", OracleDbType.Char).Value = dto.HoraLlegadaProgramada;
            command.Parameters.Add("duracionEstimada", OracleDbType.Int32).Value = (object?)dto.DuracionEstimada ?? DBNull.Value;
            command.Parameters.Add("tipoVuelo", OracleDbType.Varchar2).Value = (object?)dto.TipoVuelo ?? DBNull.Value;
            command.Parameters.Add("estado", OracleDbType.Varchar2).Value = (object?)(dto.Estado ?? "ACTIVO") ?? DBNull.Value;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> ActualizarAsync(int id, ProgramaVueloUpdateDto dto)
        {
            const string query = @"
                UPDATE AER_PROGRAMAVUELO
                SET
                    PRV_NUMERO_VUELO = :numeroVuelo,
                    PRV_ID_AEROLINEA = :idAerolinea,
                    PRV_ID_AEROPUERTO_ORIGEN = :idAeropuertoOrigen,
                    PRV_ID_AEROPUERTO_DESTINO = :idAeropuertoDestino,
                    PRV_HORA_SALIDA_PROGRAMADA = :horaSalidaProgramada,
                    PRV_HORA_LLEGADA_PROGRAMADA = :horaLlegadaProgramada,
                    PRV_DURACION_ESTIMADA = :duracionEstimada,
                    PRV_TIPO_VUELO = :tipoVuelo,
                    PRV_ESTADO = :estado
                WHERE PRV_ID = :id";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("numeroVuelo", OracleDbType.Varchar2).Value = dto.NumeroVuelo;
            command.Parameters.Add("idAerolinea", OracleDbType.Int32).Value = dto.IdAerolinea;
            command.Parameters.Add("idAeropuertoOrigen", OracleDbType.Int32).Value = dto.IdAeropuertoOrigen;
            command.Parameters.Add("idAeropuertoDestino", OracleDbType.Int32).Value = dto.IdAeropuertoDestino;
            command.Parameters.Add("horaSalidaProgramada", OracleDbType.Char).Value = dto.HoraSalidaProgramada;
            command.Parameters.Add("horaLlegadaProgramada", OracleDbType.Char).Value = dto.HoraLlegadaProgramada;
            command.Parameters.Add("duracionEstimada", OracleDbType.Int32).Value = (object?)dto.DuracionEstimada ?? DBNull.Value;
            command.Parameters.Add("tipoVuelo", OracleDbType.Varchar2).Value = (object?)dto.TipoVuelo ?? DBNull.Value;
            command.Parameters.Add("estado", OracleDbType.Varchar2).Value = (object?)(dto.Estado ?? "ACTIVO") ?? DBNull.Value;
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            const string query = @"
                DELETE FROM AER_PROGRAMAVUELO
                WHERE PRV_ID = :id";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}