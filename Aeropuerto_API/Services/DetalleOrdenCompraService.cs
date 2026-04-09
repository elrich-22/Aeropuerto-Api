using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class DetalleOrdenCompraService
    {
        private readonly string _connectionString;

        public DetalleOrdenCompraService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<DetalleOrdenCompraResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<DetalleOrdenCompraResponseDto>();

            const string query = @"
                SELECT
                    DET_ID_DETALLE,
                    DET_ID_ORDEN_COMPRA,
                    DET_ID_REPUESTO,
                    DET_CANTIDAD,
                    DET_PRECIO_UNITARIO,
                    DET_SUBTOTAL
                FROM AER_DETALLEORDENCOMPRA
                ORDER BY DET_ID_DETALLE";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(MapearDetalle(reader));
            }

            return lista;
        }

        public async Task<DetalleOrdenCompraResponseDto?> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT
                    DET_ID_DETALLE,
                    DET_ID_ORDEN_COMPRA,
                    DET_ID_REPUESTO,
                    DET_CANTIDAD,
                    DET_PRECIO_UNITARIO,
                    DET_SUBTOTAL
                FROM AER_DETALLEORDENCOMPRA
                WHERE DET_ID_DETALLE = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return MapearDetalle(reader);

            return null;
        }

        public async Task<bool> CrearAsync(DetalleOrdenCompraCreateDto dto)
        {
            const string query = @"
                INSERT INTO AER_DETALLEORDENCOMPRA
                (
                    DET_ID_ORDEN_COMPRA,
                    DET_ID_REPUESTO,
                    DET_CANTIDAD,
                    DET_PRECIO_UNITARIO,
                    DET_SUBTOTAL
                )
                VALUES
                (
                    :idOrdenCompra,
                    :idRepuesto,
                    :cantidad,
                    :precioUnitario,
                    :subtotal
                )";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idOrdenCompra", OracleDbType.Int32).Value = (object?)dto.IdOrdenCompra ?? DBNull.Value;
            cmd.Parameters.Add("idRepuesto", OracleDbType.Int32).Value = (object?)dto.IdRepuesto ?? DBNull.Value;
            cmd.Parameters.Add("cantidad", OracleDbType.Decimal).Value = (object?)dto.Cantidad ?? DBNull.Value;
            cmd.Parameters.Add("precioUnitario", OracleDbType.Decimal).Value = (object?)dto.PrecioUnitario ?? DBNull.Value;
            cmd.Parameters.Add("subtotal", OracleDbType.Decimal).Value = (object?)dto.Subtotal ?? DBNull.Value;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, DetalleOrdenCompraUpdateDto dto)
        {
            const string query = @"
                UPDATE AER_DETALLEORDENCOMPRA
                SET
                    DET_ID_ORDEN_COMPRA = :idOrdenCompra,
                    DET_ID_REPUESTO = :idRepuesto,
                    DET_CANTIDAD = :cantidad,
                    DET_PRECIO_UNITARIO = :precioUnitario,
                    DET_SUBTOTAL = :subtotal
                WHERE DET_ID_DETALLE = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idOrdenCompra", OracleDbType.Int32).Value = (object?)dto.IdOrdenCompra ?? DBNull.Value;
            cmd.Parameters.Add("idRepuesto", OracleDbType.Int32).Value = (object?)dto.IdRepuesto ?? DBNull.Value;
            cmd.Parameters.Add("cantidad", OracleDbType.Decimal).Value = (object?)dto.Cantidad ?? DBNull.Value;
            cmd.Parameters.Add("precioUnitario", OracleDbType.Decimal).Value = (object?)dto.PrecioUnitario ?? DBNull.Value;
            cmd.Parameters.Add("subtotal", OracleDbType.Decimal).Value = (object?)dto.Subtotal ?? DBNull.Value;
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            const string query = @"
                DELETE FROM AER_DETALLEORDENCOMPRA
                WHERE DET_ID_DETALLE = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static DetalleOrdenCompraResponseDto MapearDetalle(OracleDataReader reader)
        {
            return new DetalleOrdenCompraResponseDto
            {
                IdDetalle = Convert.ToInt32(reader["DET_ID_DETALLE"]),
                IdOrdenCompra = reader["DET_ID_ORDEN_COMPRA"] == DBNull.Value ? null : Convert.ToInt32(reader["DET_ID_ORDEN_COMPRA"]),
                IdRepuesto = reader["DET_ID_REPUESTO"] == DBNull.Value ? null : Convert.ToInt32(reader["DET_ID_REPUESTO"]),
                Cantidad = reader["DET_CANTIDAD"] == DBNull.Value ? null : Convert.ToDecimal(reader["DET_CANTIDAD"]),
                PrecioUnitario = reader["DET_PRECIO_UNITARIO"] == DBNull.Value ? null : Convert.ToDecimal(reader["DET_PRECIO_UNITARIO"]),
                Subtotal = reader["DET_SUBTOTAL"] == DBNull.Value ? null : Convert.ToDecimal(reader["DET_SUBTOTAL"])
            };
        }
    }
}
