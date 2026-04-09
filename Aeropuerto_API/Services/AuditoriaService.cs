using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class AuditoriaService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public AuditoriaService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<AuditoriaResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<AuditoriaResponseDto>();

            var query = _sqlQueryProvider.GetQuery("AuditoriaService/ObtenerTodosAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(MapearAuditoria(reader));
            }

            return lista;
        }

        public async Task<AuditoriaResponseDto?> ObtenerPorIdAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("AuditoriaService/ObtenerPorIdAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return MapearAuditoria(reader);

            return null;
        }

        public async Task<bool> CrearAsync(AuditoriaCreateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("AuditoriaService/CrearAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("tablaAfectada", OracleDbType.Varchar2).Value = (object?)dto.TablaAfectada ?? DBNull.Value;
            cmd.Parameters.Add("operacion", OracleDbType.Varchar2).Value = (object?)dto.Operacion ?? DBNull.Value;
            cmd.Parameters.Add("usuario", OracleDbType.Varchar2).Value = (object?)dto.Usuario ?? DBNull.Value;
            cmd.Parameters.Add("fechaHora", OracleDbType.TimeStamp).Value = dto.FechaHora ?? DateTime.Now;
            cmd.Parameters.Add("ipAddress", OracleDbType.Varchar2).Value = (object?)dto.IpAddress ?? DBNull.Value;
            cmd.Parameters.Add("datosAnteriores", OracleDbType.Clob).Value = (object?)dto.DatosAnteriores ?? DBNull.Value;
            cmd.Parameters.Add("datosNuevos", OracleDbType.Clob).Value = (object?)dto.DatosNuevos ?? DBNull.Value;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, AuditoriaUpdateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("AuditoriaService/ActualizarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("tablaAfectada", OracleDbType.Varchar2).Value = (object?)dto.TablaAfectada ?? DBNull.Value;
            cmd.Parameters.Add("operacion", OracleDbType.Varchar2).Value = (object?)dto.Operacion ?? DBNull.Value;
            cmd.Parameters.Add("usuario", OracleDbType.Varchar2).Value = (object?)dto.Usuario ?? DBNull.Value;
            cmd.Parameters.Add("fechaHora", OracleDbType.TimeStamp).Value = dto.FechaHora ?? DateTime.Now;
            cmd.Parameters.Add("ipAddress", OracleDbType.Varchar2).Value = (object?)dto.IpAddress ?? DBNull.Value;
            cmd.Parameters.Add("datosAnteriores", OracleDbType.Clob).Value = (object?)dto.DatosAnteriores ?? DBNull.Value;
            cmd.Parameters.Add("datosNuevos", OracleDbType.Clob).Value = (object?)dto.DatosNuevos ?? DBNull.Value;
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("AuditoriaService/EliminarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static AuditoriaResponseDto MapearAuditoria(OracleDataReader reader)
        {
            return new AuditoriaResponseDto
            {
                IdAuditoria = Convert.ToInt32(reader["AUD_ID_AUDITORIA"]),
                TablaAfectada = reader["AUD_TABLA_AFECTADA"] == DBNull.Value ? null : reader["AUD_TABLA_AFECTADA"].ToString(),
                Operacion = reader["AUD_OPERACION"] == DBNull.Value ? null : reader["AUD_OPERACION"].ToString(),
                Usuario = reader["AUD_USUARIO"] == DBNull.Value ? null : reader["AUD_USUARIO"].ToString(),
                FechaHora = reader["AUD_FECHA_HORA"] == DBNull.Value ? null : Convert.ToDateTime(reader["AUD_FECHA_HORA"]),
                IpAddress = reader["AUD_IP_ADDRESS"] == DBNull.Value ? null : reader["AUD_IP_ADDRESS"].ToString(),
                DatosAnteriores = reader["AUD_DATOS_ANTERIORES"] == DBNull.Value ? null : reader["AUD_DATOS_ANTERIORES"].ToString(),
                DatosNuevos = reader["AUD_DATOS_NUEVOS"] == DBNull.Value ? null : reader["AUD_DATOS_NUEVOS"].ToString()
            };
        }
    }
}

