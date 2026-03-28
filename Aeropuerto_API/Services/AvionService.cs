using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class AvionService
    {
        private readonly string _connectionString;

        public AvionService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<AvionResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<AvionResponseDto>();

            const string query = @"
                SELECT
                    AVI_ID,
                    AVI_MATRICULA,
                    AVI_ID_MODELO,
                    AVI_ID_AEROLINEA,
                    AVI_ANIO_FABRICACION,
                    AVI_ESTADO,
                    AVI_ULTIMA_REVISION,
                    AVI_PROXIMA_REVISION,
                    AVI_HORAS_VUELO
                FROM AER_AVION
                ORDER BY AVI_ID";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new AvionResponseDto
                {
                    Id = Convert.ToInt32(reader["AVI_ID"]),
                    Matricula = reader["AVI_MATRICULA"].ToString() ?? string.Empty,
                    IdModelo = Convert.ToInt32(reader["AVI_ID_MODELO"]),
                    IdAerolinea = Convert.ToInt32(reader["AVI_ID_AEROLINEA"]),
                    AnioFabricacion = reader["AVI_ANIO_FABRICACION"] == DBNull.Value
                        ? null
                        : Convert.ToInt32(reader["AVI_ANIO_FABRICACION"]),
                    Estado = reader["AVI_ESTADO"] == DBNull.Value
                        ? string.Empty
                        : reader["AVI_ESTADO"].ToString() ?? string.Empty,
                    UltimaRevision = reader["AVI_ULTIMA_REVISION"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["AVI_ULTIMA_REVISION"]),
                    ProximaRevision = reader["AVI_PROXIMA_REVISION"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["AVI_PROXIMA_REVISION"]),
                    HorasVuelo = reader["AVI_HORAS_VUELO"] == DBNull.Value
                        ? 0
                        : Convert.ToInt32(reader["AVI_HORAS_VUELO"])
                });
            }

            return lista;
        }

        public async Task<AvionResponseDto?> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT
                    AVI_ID,
                    AVI_MATRICULA,
                    AVI_ID_MODELO,
                    AVI_ID_AEROLINEA,
                    AVI_ANIO_FABRICACION,
                    AVI_ESTADO,
                    AVI_ULTIMA_REVISION,
                    AVI_PROXIMA_REVISION,
                    AVI_HORAS_VUELO
                FROM AER_AVION
                WHERE AVI_ID = :id";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new AvionResponseDto
                {
                    Id = Convert.ToInt32(reader["AVI_ID"]),
                    Matricula = reader["AVI_MATRICULA"].ToString() ?? string.Empty,
                    IdModelo = Convert.ToInt32(reader["AVI_ID_MODELO"]),
                    IdAerolinea = Convert.ToInt32(reader["AVI_ID_AEROLINEA"]),
                    AnioFabricacion = reader["AVI_ANIO_FABRICACION"] == DBNull.Value
                        ? null
                        : Convert.ToInt32(reader["AVI_ANIO_FABRICACION"]),
                    Estado = reader["AVI_ESTADO"] == DBNull.Value
                        ? string.Empty
                        : reader["AVI_ESTADO"].ToString() ?? string.Empty,
                    UltimaRevision = reader["AVI_ULTIMA_REVISION"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["AVI_ULTIMA_REVISION"]),
                    ProximaRevision = reader["AVI_PROXIMA_REVISION"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["AVI_PROXIMA_REVISION"]),
                    HorasVuelo = reader["AVI_HORAS_VUELO"] == DBNull.Value
                        ? 0
                        : Convert.ToInt32(reader["AVI_HORAS_VUELO"])
                };
            }

            return null;
        }

        public async Task<bool> CrearAsync(AvionCreateDto dto)
        {
            const string query = @"
                INSERT INTO AER_AVION
                (
                    AVI_MATRICULA,
                    AVI_ID_MODELO,
                    AVI_ID_AEROLINEA,
                    AVI_ANIO_FABRICACION,
                    AVI_ESTADO,
                    AVI_ULTIMA_REVISION,
                    AVI_PROXIMA_REVISION,
                    AVI_HORAS_VUELO
                )
                VALUES
                (
                    :matricula,
                    :idModelo,
                    :idAerolinea,
                    :anioFabricacion,
                    :estado,
                    :ultimaRevision,
                    :proximaRevision,
                    :horasVuelo
                )";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("matricula", OracleDbType.Varchar2).Value = dto.Matricula;
            command.Parameters.Add("idModelo", OracleDbType.Int32).Value = dto.IdModelo;
            command.Parameters.Add("idAerolinea", OracleDbType.Int32).Value = dto.IdAerolinea;
            command.Parameters.Add("anioFabricacion", OracleDbType.Int32).Value = (object?)dto.AnioFabricacion ?? DBNull.Value;
            command.Parameters.Add("estado", OracleDbType.Varchar2).Value = (object?)(dto.Estado ?? "ACTIVO") ?? DBNull.Value;
            command.Parameters.Add("ultimaRevision", OracleDbType.Date).Value = (object?)dto.UltimaRevision ?? DBNull.Value;
            command.Parameters.Add("proximaRevision", OracleDbType.Date).Value = (object?)dto.ProximaRevision ?? DBNull.Value;
            command.Parameters.Add("horasVuelo", OracleDbType.Int32).Value = (object?)(dto.HorasVuelo ?? 0) ?? DBNull.Value;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> ActualizarAsync(int id, AvionUpdateDto dto)
        {
            const string query = @"
                UPDATE AER_AVION
                SET
                    AVI_MATRICULA = :matricula,
                    AVI_ID_MODELO = :idModelo,
                    AVI_ID_AEROLINEA = :idAerolinea,
                    AVI_ANIO_FABRICACION = :anioFabricacion,
                    AVI_ESTADO = :estado,
                    AVI_ULTIMA_REVISION = :ultimaRevision,
                    AVI_PROXIMA_REVISION = :proximaRevision,
                    AVI_HORAS_VUELO = :horasVuelo
                WHERE AVI_ID = :id";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("matricula", OracleDbType.Varchar2).Value = dto.Matricula;
            command.Parameters.Add("idModelo", OracleDbType.Int32).Value = dto.IdModelo;
            command.Parameters.Add("idAerolinea", OracleDbType.Int32).Value = dto.IdAerolinea;
            command.Parameters.Add("anioFabricacion", OracleDbType.Int32).Value = (object?)dto.AnioFabricacion ?? DBNull.Value;
            command.Parameters.Add("estado", OracleDbType.Varchar2).Value = (object?)(dto.Estado ?? "ACTIVO") ?? DBNull.Value;
            command.Parameters.Add("ultimaRevision", OracleDbType.Date).Value = (object?)dto.UltimaRevision ?? DBNull.Value;
            command.Parameters.Add("proximaRevision", OracleDbType.Date).Value = (object?)dto.ProximaRevision ?? DBNull.Value;
            command.Parameters.Add("horasVuelo", OracleDbType.Int32).Value = dto.HorasVuelo;
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            const string query = @"
                DELETE FROM AER_AVION
                WHERE AVI_ID = :id";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}