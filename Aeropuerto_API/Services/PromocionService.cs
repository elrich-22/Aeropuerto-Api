using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class PromocionService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public PromocionService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<PromocionResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<PromocionResponseDto>();

            var query = _sqlQueryProvider.GetQuery("PromocionService/ObtenerTodosAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(MapearPromocion(reader));
            }

            return lista;
        }

        public async Task<PromocionResponseDto?> ObtenerPorIdAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("PromocionService/ObtenerPorIdAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return MapearPromocion(reader);

            return null;
        }

        public async Task<bool> CrearAsync(PromocionCreateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("PromocionService/CrearAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("codigoPromocion", OracleDbType.Varchar2).Value = (object?)dto.CodigoPromocion ?? DBNull.Value;
            cmd.Parameters.Add("descripcion", OracleDbType.Varchar2).Value = (object?)dto.Descripcion ?? DBNull.Value;
            cmd.Parameters.Add("tipoDescuento", OracleDbType.Varchar2).Value = (object?)dto.TipoDescuento ?? DBNull.Value;
            cmd.Parameters.Add("valorDescuento", OracleDbType.Decimal).Value = (object?)dto.ValorDescuento ?? DBNull.Value;
            cmd.Parameters.Add("fechaInicio", OracleDbType.Date).Value = (object?)dto.FechaInicio ?? DBNull.Value;
            cmd.Parameters.Add("fechaFin", OracleDbType.Date).Value = (object?)dto.FechaFin ?? DBNull.Value;
            cmd.Parameters.Add("usosMaximos", OracleDbType.Decimal).Value = (object?)dto.UsosMaximos ?? DBNull.Value;
            cmd.Parameters.Add("usosActuales", OracleDbType.Decimal).Value = dto.UsosActuales ?? 0m;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "ACTIVA";

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, PromocionUpdateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("PromocionService/ActualizarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("codigoPromocion", OracleDbType.Varchar2).Value = (object?)dto.CodigoPromocion ?? DBNull.Value;
            cmd.Parameters.Add("descripcion", OracleDbType.Varchar2).Value = (object?)dto.Descripcion ?? DBNull.Value;
            cmd.Parameters.Add("tipoDescuento", OracleDbType.Varchar2).Value = (object?)dto.TipoDescuento ?? DBNull.Value;
            cmd.Parameters.Add("valorDescuento", OracleDbType.Decimal).Value = (object?)dto.ValorDescuento ?? DBNull.Value;
            cmd.Parameters.Add("fechaInicio", OracleDbType.Date).Value = (object?)dto.FechaInicio ?? DBNull.Value;
            cmd.Parameters.Add("fechaFin", OracleDbType.Date).Value = (object?)dto.FechaFin ?? DBNull.Value;
            cmd.Parameters.Add("usosMaximos", OracleDbType.Decimal).Value = (object?)dto.UsosMaximos ?? DBNull.Value;
            cmd.Parameters.Add("usosActuales", OracleDbType.Decimal).Value = dto.UsosActuales ?? 0m;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "ACTIVA";
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("PromocionService/EliminarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static PromocionResponseDto MapearPromocion(OracleDataReader reader)
        {
            return new PromocionResponseDto
            {
                IdPromocion = Convert.ToInt32(reader["PRO_ID_PROMOCION"]),
                CodigoPromocion = reader["PRO_CODIGO_PROMOCION"] == DBNull.Value ? null : reader["PRO_CODIGO_PROMOCION"].ToString(),
                Descripcion = reader["PRO_DESCRIPCION"] == DBNull.Value ? null : reader["PRO_DESCRIPCION"].ToString(),
                TipoDescuento = reader["PRO_TIPO_DESCUENTO"] == DBNull.Value ? null : reader["PRO_TIPO_DESCUENTO"].ToString(),
                ValorDescuento = reader["PRO_VALOR_DESCUENTO"] == DBNull.Value ? null : Convert.ToDecimal(reader["PRO_VALOR_DESCUENTO"]),
                FechaInicio = reader["PRO_FECHA_INICIO"] == DBNull.Value ? null : Convert.ToDateTime(reader["PRO_FECHA_INICIO"]),
                FechaFin = reader["PRO_FECHA_FIN"] == DBNull.Value ? null : Convert.ToDateTime(reader["PRO_FECHA_FIN"]),
                UsosMaximos = reader["PRO_USOS_MAXIMOS"] == DBNull.Value ? null : Convert.ToDecimal(reader["PRO_USOS_MAXIMOS"]),
                UsosActuales = reader["PRO_USOS_ACTUALES"] == DBNull.Value ? null : Convert.ToDecimal(reader["PRO_USOS_ACTUALES"]),
                Estado = reader["PRO_ESTADO"] == DBNull.Value ? null : reader["PRO_ESTADO"].ToString()
            };
        }
    }
}

