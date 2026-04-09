using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class ProveedorService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public ProveedorService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<ProveedorResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<ProveedorResponseDto>();

            var query = _sqlQueryProvider.GetQuery("ProveedorService/ObtenerTodosAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(MapearProveedor(reader));
            }

            return lista;
        }

        public async Task<ProveedorResponseDto?> ObtenerPorIdAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("ProveedorService/ObtenerPorIdAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return MapearProveedor(reader);

            return null;
        }

        public async Task<bool> CrearAsync(ProveedorCreateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("ProveedorService/CrearAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("nombreEmpresa", OracleDbType.Varchar2).Value = dto.NombreEmpresa;
            cmd.Parameters.Add("nit", OracleDbType.Varchar2).Value = (object?)dto.NIT ?? DBNull.Value;
            cmd.Parameters.Add("direccion", OracleDbType.Varchar2).Value = (object?)dto.Direccion ?? DBNull.Value;
            cmd.Parameters.Add("telefono", OracleDbType.Varchar2).Value = (object?)dto.Telefono ?? DBNull.Value;
            cmd.Parameters.Add("email", OracleDbType.Varchar2).Value = (object?)dto.Email ?? DBNull.Value;
            cmd.Parameters.Add("contactoPrincipal", OracleDbType.Varchar2).Value = (object?)dto.ContactoPrincipal ?? DBNull.Value;
            cmd.Parameters.Add("pais", OracleDbType.Varchar2).Value = (object?)dto.Pais ?? DBNull.Value;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "ACTIVO";
            cmd.Parameters.Add("calificacion", OracleDbType.Decimal).Value = (object?)dto.Calificacion ?? DBNull.Value;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, ProveedorUpdateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("ProveedorService/ActualizarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("nombreEmpresa", OracleDbType.Varchar2).Value = dto.NombreEmpresa;
            cmd.Parameters.Add("nit", OracleDbType.Varchar2).Value = (object?)dto.NIT ?? DBNull.Value;
            cmd.Parameters.Add("direccion", OracleDbType.Varchar2).Value = (object?)dto.Direccion ?? DBNull.Value;
            cmd.Parameters.Add("telefono", OracleDbType.Varchar2).Value = (object?)dto.Telefono ?? DBNull.Value;
            cmd.Parameters.Add("email", OracleDbType.Varchar2).Value = (object?)dto.Email ?? DBNull.Value;
            cmd.Parameters.Add("contactoPrincipal", OracleDbType.Varchar2).Value = (object?)dto.ContactoPrincipal ?? DBNull.Value;
            cmd.Parameters.Add("pais", OracleDbType.Varchar2).Value = (object?)dto.Pais ?? DBNull.Value;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "ACTIVO";
            cmd.Parameters.Add("calificacion", OracleDbType.Decimal).Value = (object?)dto.Calificacion ?? DBNull.Value;
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("ProveedorService/EliminarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static ProveedorResponseDto MapearProveedor(OracleDataReader reader)
        {
            return new ProveedorResponseDto
            {
                Id = Convert.ToInt32(reader["PRV_ID_PROVEEDOR"]),
                NombreEmpresa = reader["PRV_NOMBRE_EMPRESA"].ToString() ?? string.Empty,
                NIT = reader["PRV_NIT"] == DBNull.Value ? null : reader["PRV_NIT"].ToString(),
                Direccion = reader["PRV_DIRECCION"] == DBNull.Value ? null : reader["PRV_DIRECCION"].ToString(),
                Telefono = reader["PRV_TELEFONO"] == DBNull.Value ? null : reader["PRV_TELEFONO"].ToString(),
                Email = reader["PRV_EMAIL"] == DBNull.Value ? null : reader["PRV_EMAIL"].ToString(),
                ContactoPrincipal = reader["PRV_CONTACTO_PRINCIPAL"] == DBNull.Value ? null : reader["PRV_CONTACTO_PRINCIPAL"].ToString(),
                Pais = reader["PRV_PAIS"] == DBNull.Value ? null : reader["PRV_PAIS"].ToString(),
                Estado = reader["PRV_ESTADO"] == DBNull.Value ? string.Empty : reader["PRV_ESTADO"].ToString() ?? string.Empty,
                Calificacion = reader["PRV_CALIFICACION"] == DBNull.Value ? null : Convert.ToDecimal(reader["PRV_CALIFICACION"])
            };
        }
    }
}
