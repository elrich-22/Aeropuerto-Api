using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class AerolineaService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public AerolineaService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<AerolineaResponseDto>> ObtenerTodasAsync()
        {
            var lista = new List<AerolineaResponseDto>();

            var query = _sqlQueryProvider.GetQuery("AerolineaService/ObtenerTodasAsync.sql");

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new AerolineaResponseDto
                {
                    Id = Convert.ToInt32(reader["ARL_ID"]),
                    CodigoAerolinea = reader["ARL_CODIGO_AEROLINEA"].ToString() ?? string.Empty,
                    Nombre = reader["ARL_NOMBRE"].ToString() ?? string.Empty,
                    PaisOrigen = reader["ARL_PAIS_ORIGEN"].ToString() ?? string.Empty,
                    CodigoIata = reader["ARL_CODIGO_IATA"] == DBNull.Value ? null : reader["ARL_CODIGO_IATA"].ToString(),
                    CodigoIcao = reader["ARL_CODIGO_ICAO"] == DBNull.Value ? null : reader["ARL_CODIGO_ICAO"].ToString(),
                    Estado = reader["ARL_ESTADO"] == DBNull.Value ? string.Empty : reader["ARL_ESTADO"].ToString() ?? string.Empty,
                    Telefono = reader["ARL_TELEFONO"] == DBNull.Value ? null : reader["ARL_TELEFONO"].ToString(),
                    Email = reader["ARL_EMAIL"] == DBNull.Value ? null : reader["ARL_EMAIL"].ToString(),
                    SitioWeb = reader["ARL_SITIO_WEB"] == DBNull.Value ? null : reader["ARL_SITIO_WEB"].ToString(),
                    FechaRegistro = reader["ARL_FECHA_REGISTRO"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["ARL_FECHA_REGISTRO"])
                });
            }

            return lista;
        }

        public async Task<AerolineaResponseDto?> ObtenerPorIdAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("AerolineaService/ObtenerPorIdAsync.sql");

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new AerolineaResponseDto
                {
                    Id = Convert.ToInt32(reader["ARL_ID"]),
                    CodigoAerolinea = reader["ARL_CODIGO_AEROLINEA"].ToString() ?? string.Empty,
                    Nombre = reader["ARL_NOMBRE"].ToString() ?? string.Empty,
                    PaisOrigen = reader["ARL_PAIS_ORIGEN"].ToString() ?? string.Empty,
                    CodigoIata = reader["ARL_CODIGO_IATA"] == DBNull.Value ? null : reader["ARL_CODIGO_IATA"].ToString(),
                    CodigoIcao = reader["ARL_CODIGO_ICAO"] == DBNull.Value ? null : reader["ARL_CODIGO_ICAO"].ToString(),
                    Estado = reader["ARL_ESTADO"] == DBNull.Value ? string.Empty : reader["ARL_ESTADO"].ToString() ?? string.Empty,
                    Telefono = reader["ARL_TELEFONO"] == DBNull.Value ? null : reader["ARL_TELEFONO"].ToString(),
                    Email = reader["ARL_EMAIL"] == DBNull.Value ? null : reader["ARL_EMAIL"].ToString(),
                    SitioWeb = reader["ARL_SITIO_WEB"] == DBNull.Value ? null : reader["ARL_SITIO_WEB"].ToString(),
                    FechaRegistro = reader["ARL_FECHA_REGISTRO"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["ARL_FECHA_REGISTRO"])
                };
            }

            return null;
        }

        public async Task<bool> CrearAsync(AerolineaCreateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("AerolineaService/CrearAsync.sql");

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("codigo", OracleDbType.Varchar2).Value = dto.CodigoAerolinea;
            command.Parameters.Add("nombre", OracleDbType.Varchar2).Value = dto.Nombre;
            command.Parameters.Add("paisOrigen", OracleDbType.Varchar2).Value = dto.PaisOrigen;
            command.Parameters.Add("codigoIata", OracleDbType.Varchar2).Value = (object?)dto.CodigoIata ?? DBNull.Value;
            command.Parameters.Add("codigoIcao", OracleDbType.Varchar2).Value = (object?)dto.CodigoIcao ?? DBNull.Value;
            command.Parameters.Add("estado", OracleDbType.Varchar2).Value = (object?)(dto.Estado ?? "ACTIVA") ?? DBNull.Value;
            command.Parameters.Add("telefono", OracleDbType.Varchar2).Value = (object?)dto.Telefono ?? DBNull.Value;
            command.Parameters.Add("email", OracleDbType.Varchar2).Value = (object?)dto.Email ?? DBNull.Value;
            command.Parameters.Add("sitioWeb", OracleDbType.Varchar2).Value = (object?)dto.SitioWeb ?? DBNull.Value;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> ActualizarAsync(int id, AerolineaUpdateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("AerolineaService/ActualizarAsync.sql");

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("codigo", OracleDbType.Varchar2).Value = dto.CodigoAerolinea;
            command.Parameters.Add("nombre", OracleDbType.Varchar2).Value = dto.Nombre;
            command.Parameters.Add("paisOrigen", OracleDbType.Varchar2).Value = dto.PaisOrigen;
            command.Parameters.Add("codigoIata", OracleDbType.Varchar2).Value = (object?)dto.CodigoIata ?? DBNull.Value;
            command.Parameters.Add("codigoIcao", OracleDbType.Varchar2).Value = (object?)dto.CodigoIcao ?? DBNull.Value;
            command.Parameters.Add("estado", OracleDbType.Varchar2).Value = (object?)(dto.Estado ?? "ACTIVA") ?? DBNull.Value;
            command.Parameters.Add("telefono", OracleDbType.Varchar2).Value = (object?)dto.Telefono ?? DBNull.Value;
            command.Parameters.Add("email", OracleDbType.Varchar2).Value = (object?)dto.Email ?? DBNull.Value;
            command.Parameters.Add("sitioWeb", OracleDbType.Varchar2).Value = (object?)dto.SitioWeb ?? DBNull.Value;
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("AerolineaService/EliminarAsync.sql");

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}
