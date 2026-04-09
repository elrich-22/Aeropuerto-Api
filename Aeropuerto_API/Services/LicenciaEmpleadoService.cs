using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class LicenciaEmpleadoService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public LicenciaEmpleadoService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<LicenciaEmpleadoResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<LicenciaEmpleadoResponseDto>();

            var query = _sqlQueryProvider.GetQuery("LicenciaEmpleadoService/ObtenerTodosAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(MapearLicencia(reader));
            }

            return lista;
        }

        public async Task<LicenciaEmpleadoResponseDto?> ObtenerPorIdAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("LicenciaEmpleadoService/ObtenerPorIdAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return MapearLicencia(reader);

            return null;
        }

        public async Task<bool> CrearAsync(LicenciaEmpleadoCreateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("LicenciaEmpleadoService/CrearAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idEmpleado", OracleDbType.Int32).Value = (object?)dto.IdEmpleado ?? DBNull.Value;
            cmd.Parameters.Add("tipoLicencia", OracleDbType.Varchar2).Value = (object?)dto.TipoLicencia ?? DBNull.Value;
            cmd.Parameters.Add("numeroLicencia", OracleDbType.Varchar2).Value = (object?)dto.NumeroLicencia ?? DBNull.Value;
            cmd.Parameters.Add("fechaEmision", OracleDbType.Date).Value = (object?)dto.FechaEmision ?? DBNull.Value;
            cmd.Parameters.Add("fechaVencimiento", OracleDbType.Date).Value = (object?)dto.FechaVencimiento ?? DBNull.Value;
            cmd.Parameters.Add("autoridadEmisora", OracleDbType.Varchar2).Value = (object?)dto.AutoridadEmisora ?? DBNull.Value;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = (object?)(dto.Estado ?? "VIGENTE") ?? DBNull.Value;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, LicenciaEmpleadoUpdateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("LicenciaEmpleadoService/ActualizarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idEmpleado", OracleDbType.Int32).Value = (object?)dto.IdEmpleado ?? DBNull.Value;
            cmd.Parameters.Add("tipoLicencia", OracleDbType.Varchar2).Value = (object?)dto.TipoLicencia ?? DBNull.Value;
            cmd.Parameters.Add("numeroLicencia", OracleDbType.Varchar2).Value = (object?)dto.NumeroLicencia ?? DBNull.Value;
            cmd.Parameters.Add("fechaEmision", OracleDbType.Date).Value = (object?)dto.FechaEmision ?? DBNull.Value;
            cmd.Parameters.Add("fechaVencimiento", OracleDbType.Date).Value = (object?)dto.FechaVencimiento ?? DBNull.Value;
            cmd.Parameters.Add("autoridadEmisora", OracleDbType.Varchar2).Value = (object?)dto.AutoridadEmisora ?? DBNull.Value;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = (object?)(dto.Estado ?? "VIGENTE") ?? DBNull.Value;
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("LicenciaEmpleadoService/EliminarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static LicenciaEmpleadoResponseDto MapearLicencia(OracleDataReader reader)
        {
            return new LicenciaEmpleadoResponseDto
            {
                IdLicencia = Convert.ToInt32(reader["LIC_ID_LICENCIA"]),
                IdEmpleado = reader["LIC_ID_EMPLEADO"] == DBNull.Value ? null : Convert.ToInt32(reader["LIC_ID_EMPLEADO"]),
                TipoLicencia = reader["LIC_TIPO_LICENCIA"] == DBNull.Value ? null : reader["LIC_TIPO_LICENCIA"].ToString(),
                NumeroLicencia = reader["LIC_NUMERO_LICENCIA"] == DBNull.Value ? null : reader["LIC_NUMERO_LICENCIA"].ToString(),
                FechaEmision = reader["LIC_FECHA_EMISION"] == DBNull.Value ? null : Convert.ToDateTime(reader["LIC_FECHA_EMISION"]),
                FechaVencimiento = reader["LIC_FECHA_VENCIMIENTO"] == DBNull.Value ? null : Convert.ToDateTime(reader["LIC_FECHA_VENCIMIENTO"]),
                AutoridadEmisora = reader["LIC_AUTORIDAD_EMISORA"] == DBNull.Value ? null : reader["LIC_AUTORIDAD_EMISORA"].ToString(),
                Estado = reader["LIC_ESTADO"] == DBNull.Value ? null : reader["LIC_ESTADO"].ToString()
            };
        }
    }
}

