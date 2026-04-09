using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class PromocionService
    {
        private readonly string _connectionString;

        public PromocionService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<PromocionResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<PromocionResponseDto>();

            const string query = @"
                SELECT
                    PRO_ID_PROMOCION,
                    PRO_CODIGO_PROMOCION,
                    PRO_DESCRIPCION,
                    PRO_TIPO_DESCUENTO,
                    PRO_VALOR_DESCUENTO,
                    PRO_FECHA_INICIO,
                    PRO_FECHA_FIN,
                    PRO_USOS_MAXIMOS,
                    PRO_USOS_ACTUALES,
                    PRO_ESTADO
                FROM AER_PROMOCION
                ORDER BY PRO_ID_PROMOCION";

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
            const string query = @"
                SELECT
                    PRO_ID_PROMOCION,
                    PRO_CODIGO_PROMOCION,
                    PRO_DESCRIPCION,
                    PRO_TIPO_DESCUENTO,
                    PRO_VALOR_DESCUENTO,
                    PRO_FECHA_INICIO,
                    PRO_FECHA_FIN,
                    PRO_USOS_MAXIMOS,
                    PRO_USOS_ACTUALES,
                    PRO_ESTADO
                FROM AER_PROMOCION
                WHERE PRO_ID_PROMOCION = :id";

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
            const string query = @"
                INSERT INTO AER_PROMOCION
                (
                    PRO_CODIGO_PROMOCION,
                    PRO_DESCRIPCION,
                    PRO_TIPO_DESCUENTO,
                    PRO_VALOR_DESCUENTO,
                    PRO_FECHA_INICIO,
                    PRO_FECHA_FIN,
                    PRO_USOS_MAXIMOS,
                    PRO_USOS_ACTUALES,
                    PRO_ESTADO
                )
                VALUES
                (
                    :codigoPromocion,
                    :descripcion,
                    :tipoDescuento,
                    :valorDescuento,
                    :fechaInicio,
                    :fechaFin,
                    :usosMaximos,
                    :usosActuales,
                    :estado
                )";

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
            const string query = @"
                UPDATE AER_PROMOCION
                SET
                    PRO_CODIGO_PROMOCION = :codigoPromocion,
                    PRO_DESCRIPCION = :descripcion,
                    PRO_TIPO_DESCUENTO = :tipoDescuento,
                    PRO_VALOR_DESCUENTO = :valorDescuento,
                    PRO_FECHA_INICIO = :fechaInicio,
                    PRO_FECHA_FIN = :fechaFin,
                    PRO_USOS_MAXIMOS = :usosMaximos,
                    PRO_USOS_ACTUALES = :usosActuales,
                    PRO_ESTADO = :estado
                WHERE PRO_ID_PROMOCION = :id";

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
            const string query = @"
                DELETE FROM AER_PROMOCION
                WHERE PRO_ID_PROMOCION = :id";

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
