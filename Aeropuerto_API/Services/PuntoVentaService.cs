using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class PuntoVentaService
    {
        private readonly string _connectionString;

        public PuntoVentaService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<PuntoVentaResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<PuntoVentaResponseDto>();

            const string query = @"
                SELECT
                    PUV_ID_PUNTO_VENTA,
                    PUV_CODIGO_PUNTO,
                    PUV_NOMBRE,
                    PUV_ID_AEROPUERTO,
                    PUV_UBICACION,
                    PUV_ESTADO
                FROM AER_PUNTOVENTA
                ORDER BY PUV_ID_PUNTO_VENTA";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new PuntoVentaResponseDto
                {
                    Id = Convert.ToInt32(reader["PUV_ID_PUNTO_VENTA"]),
                    CodigoPunto = reader["PUV_CODIGO_PUNTO"].ToString() ?? string.Empty,
                    Nombre = reader["PUV_NOMBRE"].ToString() ?? string.Empty,
                    IdAeropuerto = Convert.ToInt32(reader["PUV_ID_AEROPUERTO"]),
                    Ubicacion = reader["PUV_UBICACION"] == DBNull.Value ? null : reader["PUV_UBICACION"].ToString(),
                    Estado = reader["PUV_ESTADO"] == DBNull.Value ? string.Empty : reader["PUV_ESTADO"].ToString() ?? string.Empty
                });
            }

            return lista;
        }

        public async Task<PuntoVentaResponseDto?> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT
                    PUV_ID_PUNTO_VENTA,
                    PUV_CODIGO_PUNTO,
                    PUV_NOMBRE,
                    PUV_ID_AEROPUERTO,
                    PUV_UBICACION,
                    PUV_ESTADO
                FROM AER_PUNTOVENTA
                WHERE PUV_ID_PUNTO_VENTA = :id";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new PuntoVentaResponseDto
                {
                    Id = Convert.ToInt32(reader["PUV_ID_PUNTO_VENTA"]),
                    CodigoPunto = reader["PUV_CODIGO_PUNTO"].ToString() ?? string.Empty,
                    Nombre = reader["PUV_NOMBRE"].ToString() ?? string.Empty,
                    IdAeropuerto = Convert.ToInt32(reader["PUV_ID_AEROPUERTO"]),
                    Ubicacion = reader["PUV_UBICACION"] == DBNull.Value ? null : reader["PUV_UBICACION"].ToString(),
                    Estado = reader["PUV_ESTADO"] == DBNull.Value ? string.Empty : reader["PUV_ESTADO"].ToString() ?? string.Empty
                };
            }

            return null;
        }

        public async Task<bool> CrearAsync(PuntoVentaCreateDto dto)
        {
            const string query = @"
                INSERT INTO AER_PUNTOVENTA
                (
                    PUV_CODIGO_PUNTO,
                    PUV_NOMBRE,
                    PUV_ID_AEROPUERTO,
                    PUV_UBICACION,
                    PUV_ESTADO
                )
                VALUES
                (
                    :codigoPunto,
                    :nombre,
                    :idAeropuerto,
                    :ubicacion,
                    :estado
                )";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("codigoPunto", OracleDbType.Varchar2).Value = dto.CodigoPunto;
            command.Parameters.Add("nombre", OracleDbType.Varchar2).Value = dto.Nombre;
            command.Parameters.Add("idAeropuerto", OracleDbType.Int32).Value = dto.IdAeropuerto;
            command.Parameters.Add("ubicacion", OracleDbType.Varchar2).Value = (object?)dto.Ubicacion ?? DBNull.Value;
            command.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "ACTIVO";

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> ActualizarAsync(int id, PuntoVentaUpdateDto dto)
        {
            const string query = @"
                UPDATE AER_PUNTOVENTA
                SET
                    PUV_CODIGO_PUNTO = :codigoPunto,
                    PUV_NOMBRE = :nombre,
                    PUV_ID_AEROPUERTO = :idAeropuerto,
                    PUV_UBICACION = :ubicacion,
                    PUV_ESTADO = :estado
                WHERE PUV_ID_PUNTO_VENTA = :id";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("codigoPunto", OracleDbType.Varchar2).Value = dto.CodigoPunto;
            command.Parameters.Add("nombre", OracleDbType.Varchar2).Value = dto.Nombre;
            command.Parameters.Add("idAeropuerto", OracleDbType.Int32).Value = dto.IdAeropuerto;
            command.Parameters.Add("ubicacion", OracleDbType.Varchar2).Value = (object?)dto.Ubicacion ?? DBNull.Value;
            command.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "ACTIVO";
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            const string query = @"
                DELETE FROM AER_PUNTOVENTA
                WHERE PUV_ID_PUNTO_VENTA = :id";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}