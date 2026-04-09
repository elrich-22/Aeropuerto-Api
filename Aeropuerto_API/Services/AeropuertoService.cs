using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class AeropuertoService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public AeropuertoService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<AeropuertoResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<AeropuertoResponseDto>();

            var query = _sqlQueryProvider.GetQuery("AeropuertoService/ObtenerTodosAsync.sql");

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
            var query = _sqlQueryProvider.GetQuery("AeropuertoService/ObtenerPorIdAsync.sql");

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
            var query = _sqlQueryProvider.GetQuery("AeropuertoService/CrearAsync.sql");

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
            var query = _sqlQueryProvider.GetQuery("AeropuertoService/ActualizarAsync.sql");

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
            var query = _sqlQueryProvider.GetQuery("AeropuertoService/EliminarAsync.sql");

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}
