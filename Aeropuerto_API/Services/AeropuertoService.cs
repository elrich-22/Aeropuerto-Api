using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class AeropuertoService
    {
        private readonly string _connectionString;

        public AeropuertoService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<AeropuertoResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<AeropuertoResponseDto>();

            const string query = @"
                SELECT
                    AER_ID,
                    AER_CODIGO_AEROPUERTO,
                    AER_NOMBRE,
                    AER_CIUDAD,
                    AER_PAIS,
                    AER_ZONA_HORARIA,
                    AER_ESTADO,
                    AER_TIPO,
                    AER_LATITUD,
                    AER_LONGITUD,
                    AER_CODIGO_IATA,
                    AER_CODIGO_ICAO,
                    AER_FECHA_REGISTRO
                FROM AER_AEROPUERTO
                ORDER BY AER_ID";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new AeropuertoResponseDto
                {
                    Id = Convert.ToInt32(reader["AER_ID"]),
                    CodigoAeropuerto = reader["AER_CODIGO_AEROPUERTO"].ToString() ?? string.Empty,
                    Nombre = reader["AER_NOMBRE"].ToString() ?? string.Empty,
                    Ciudad = reader["AER_CIUDAD"].ToString() ?? string.Empty,
                    Pais = reader["AER_PAIS"].ToString() ?? string.Empty,
                    ZonaHoraria = reader["AER_ZONA_HORARIA"] == DBNull.Value ? null : reader["AER_ZONA_HORARIA"].ToString(),
                    Estado = reader["AER_ESTADO"] == DBNull.Value ? string.Empty : reader["AER_ESTADO"].ToString() ?? string.Empty,
                    Tipo = reader["AER_TIPO"] == DBNull.Value ? null : reader["AER_TIPO"].ToString(),
                    Latitud = reader["AER_LATITUD"] == DBNull.Value ? null : Convert.ToDecimal(reader["AER_LATITUD"]),
                    Longitud = reader["AER_LONGITUD"] == DBNull.Value ? null : Convert.ToDecimal(reader["AER_LONGITUD"]),
                    CodigoIata = reader["AER_CODIGO_IATA"] == DBNull.Value ? null : reader["AER_CODIGO_IATA"].ToString(),
                    CodigoIcao = reader["AER_CODIGO_ICAO"] == DBNull.Value ? null : reader["AER_CODIGO_ICAO"].ToString(),
                    FechaRegistro = reader["AER_FECHA_REGISTRO"] == DBNull.Value ? null : Convert.ToDateTime(reader["AER_FECHA_REGISTRO"])
                });
            }

            return lista;
        }

        public async Task<AeropuertoResponseDto?> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT
                    AER_ID,
                    AER_CODIGO_AEROPUERTO,
                    AER_NOMBRE,
                    AER_CIUDAD,
                    AER_PAIS,
                    AER_ZONA_HORARIA,
                    AER_ESTADO,
                    AER_TIPO,
                    AER_LATITUD,
                    AER_LONGITUD,
                    AER_CODIGO_IATA,
                    AER_CODIGO_ICAO,
                    AER_FECHA_REGISTRO
                FROM AER_AEROPUERTO
                WHERE AER_ID = :id";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new AeropuertoResponseDto
                {
                    Id = Convert.ToInt32(reader["AER_ID"]),
                    CodigoAeropuerto = reader["AER_CODIGO_AEROPUERTO"].ToString() ?? string.Empty,
                    Nombre = reader["AER_NOMBRE"].ToString() ?? string.Empty,
                    Ciudad = reader["AER_CIUDAD"].ToString() ?? string.Empty,
                    Pais = reader["AER_PAIS"].ToString() ?? string.Empty,
                    ZonaHoraria = reader["AER_ZONA_HORARIA"] == DBNull.Value ? null : reader["AER_ZONA_HORARIA"].ToString(),
                    Estado = reader["AER_ESTADO"] == DBNull.Value ? string.Empty : reader["AER_ESTADO"].ToString() ?? string.Empty,
                    Tipo = reader["AER_TIPO"] == DBNull.Value ? null : reader["AER_TIPO"].ToString(),
                    Latitud = reader["AER_LATITUD"] == DBNull.Value ? null : Convert.ToDecimal(reader["AER_LATITUD"]),
                    Longitud = reader["AER_LONGITUD"] == DBNull.Value ? null : Convert.ToDecimal(reader["AER_LONGITUD"]),
                    CodigoIata = reader["AER_CODIGO_IATA"] == DBNull.Value ? null : reader["AER_CODIGO_IATA"].ToString(),
                    CodigoIcao = reader["AER_CODIGO_ICAO"] == DBNull.Value ? null : reader["AER_CODIGO_ICAO"].ToString(),
                    FechaRegistro = reader["AER_FECHA_REGISTRO"] == DBNull.Value ? null : Convert.ToDateTime(reader["AER_FECHA_REGISTRO"])
                };
            }

            return null;
        }

        public async Task<bool> CrearAsync(AeropuertoCreateDto dto)
        {
            const string query = @"
                INSERT INTO AER_AEROPUERTO
                (
                    AER_CODIGO_AEROPUERTO,
                    AER_NOMBRE,
                    AER_CIUDAD,
                    AER_PAIS,
                    AER_ZONA_HORARIA,
                    AER_ESTADO,
                    AER_TIPO,
                    AER_LATITUD,
                    AER_LONGITUD,
                    AER_CODIGO_IATA,
                    AER_CODIGO_ICAO
                )
                VALUES
                (
                    :codigoAeropuerto,
                    :nombre,
                    :ciudad,
                    :pais,
                    :zonaHoraria,
                    :estado,
                    :tipo,
                    :latitud,
                    :longitud,
                    :codigoIata,
                    :codigoIcao
                )";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("codigoAeropuerto", OracleDbType.Varchar2).Value = dto.CodigoAeropuerto;
            command.Parameters.Add("nombre", OracleDbType.Varchar2).Value = dto.Nombre;
            command.Parameters.Add("ciudad", OracleDbType.Varchar2).Value = dto.Ciudad;
            command.Parameters.Add("pais", OracleDbType.Varchar2).Value = dto.Pais;
            command.Parameters.Add("zonaHoraria", OracleDbType.Varchar2).Value = (object?)dto.ZonaHoraria ?? DBNull.Value;
            command.Parameters.Add("estado", OracleDbType.Varchar2).Value = (object?)(dto.Estado ?? "ACTIVO") ?? DBNull.Value;
            command.Parameters.Add("tipo", OracleDbType.Varchar2).Value = (object?)dto.Tipo ?? DBNull.Value;
            command.Parameters.Add("latitud", OracleDbType.Decimal).Value = (object?)dto.Latitud ?? DBNull.Value;
            command.Parameters.Add("longitud", OracleDbType.Decimal).Value = (object?)dto.Longitud ?? DBNull.Value;
            command.Parameters.Add("codigoIata", OracleDbType.Varchar2).Value = (object?)dto.CodigoIata ?? DBNull.Value;
            command.Parameters.Add("codigoIcao", OracleDbType.Varchar2).Value = (object?)dto.CodigoIcao ?? DBNull.Value;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> ActualizarAsync(int id, AeropuertoUpdateDto dto)
        {
            const string query = @"
                UPDATE AER_AEROPUERTO
                SET
                    AER_CODIGO_AEROPUERTO = :codigoAeropuerto,
                    AER_NOMBRE = :nombre,
                    AER_CIUDAD = :ciudad,
                    AER_PAIS = :pais,
                    AER_ZONA_HORARIA = :zonaHoraria,
                    AER_ESTADO = :estado,
                    AER_TIPO = :tipo,
                    AER_LATITUD = :latitud,
                    AER_LONGITUD = :longitud,
                    AER_CODIGO_IATA = :codigoIata,
                    AER_CODIGO_ICAO = :codigoIcao
                WHERE AER_ID = :id";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("codigoAeropuerto", OracleDbType.Varchar2).Value = dto.CodigoAeropuerto;
            command.Parameters.Add("nombre", OracleDbType.Varchar2).Value = dto.Nombre;
            command.Parameters.Add("ciudad", OracleDbType.Varchar2).Value = dto.Ciudad;
            command.Parameters.Add("pais", OracleDbType.Varchar2).Value = dto.Pais;
            command.Parameters.Add("zonaHoraria", OracleDbType.Varchar2).Value = (object?)dto.ZonaHoraria ?? DBNull.Value;
            command.Parameters.Add("estado", OracleDbType.Varchar2).Value = (object?)(dto.Estado ?? "ACTIVO") ?? DBNull.Value;
            command.Parameters.Add("tipo", OracleDbType.Varchar2).Value = (object?)dto.Tipo ?? DBNull.Value;
            command.Parameters.Add("latitud", OracleDbType.Decimal).Value = (object?)dto.Latitud ?? DBNull.Value;
            command.Parameters.Add("longitud", OracleDbType.Decimal).Value = (object?)dto.Longitud ?? DBNull.Value;
            command.Parameters.Add("codigoIata", OracleDbType.Varchar2).Value = (object?)dto.CodigoIata ?? DBNull.Value;
            command.Parameters.Add("codigoIcao", OracleDbType.Varchar2).Value = (object?)dto.CodigoIcao ?? DBNull.Value;
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            const string query = @"
                DELETE FROM AER_AEROPUERTO
                WHERE AER_ID = :id";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}