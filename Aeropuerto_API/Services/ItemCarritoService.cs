using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class ItemCarritoService
    {
        private readonly string _connectionString;

        public ItemCarritoService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<ItemCarritoResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<ItemCarritoResponseDto>();

            const string query = @"
                SELECT
                    ITE_ID_ITEM_CARRITO,
                    ITE_ID_CARRITO,
                    ITE_ID_VUELO,
                    ITE_NUMERO_ASIENTO,
                    ITE_CLASE,
                    ITE_PRECIO_UNITARIO,
                    ITE_CANTIDAD
                FROM AER_ITEMCARRITO
                ORDER BY ITE_ID_ITEM_CARRITO";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(MapearItem(reader));
            }

            return lista;
        }

        public async Task<ItemCarritoResponseDto?> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT
                    ITE_ID_ITEM_CARRITO,
                    ITE_ID_CARRITO,
                    ITE_ID_VUELO,
                    ITE_NUMERO_ASIENTO,
                    ITE_CLASE,
                    ITE_PRECIO_UNITARIO,
                    ITE_CANTIDAD
                FROM AER_ITEMCARRITO
                WHERE ITE_ID_ITEM_CARRITO = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return MapearItem(reader);

            return null;
        }

        public async Task<bool> CrearAsync(ItemCarritoCreateDto dto)
        {
            const string query = @"
                INSERT INTO AER_ITEMCARRITO
                (
                    ITE_ID_CARRITO,
                    ITE_ID_VUELO,
                    ITE_NUMERO_ASIENTO,
                    ITE_CLASE,
                    ITE_PRECIO_UNITARIO,
                    ITE_CANTIDAD
                )
                VALUES
                (
                    :idCarrito,
                    :idVuelo,
                    :numeroAsiento,
                    :clase,
                    :precioUnitario,
                    :cantidad
                )";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idCarrito", OracleDbType.Int32).Value = (object?)dto.IdCarrito ?? DBNull.Value;
            cmd.Parameters.Add("idVuelo", OracleDbType.Int32).Value = (object?)dto.IdVuelo ?? DBNull.Value;
            cmd.Parameters.Add("numeroAsiento", OracleDbType.Varchar2).Value = (object?)dto.NumeroAsiento ?? DBNull.Value;
            cmd.Parameters.Add("clase", OracleDbType.Varchar2).Value = (object?)dto.Clase ?? DBNull.Value;
            cmd.Parameters.Add("precioUnitario", OracleDbType.Decimal).Value = (object?)dto.PrecioUnitario ?? DBNull.Value;
            cmd.Parameters.Add("cantidad", OracleDbType.Decimal).Value = dto.Cantidad ?? 1m;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, ItemCarritoUpdateDto dto)
        {
            const string query = @"
                UPDATE AER_ITEMCARRITO
                SET
                    ITE_ID_CARRITO = :idCarrito,
                    ITE_ID_VUELO = :idVuelo,
                    ITE_NUMERO_ASIENTO = :numeroAsiento,
                    ITE_CLASE = :clase,
                    ITE_PRECIO_UNITARIO = :precioUnitario,
                    ITE_CANTIDAD = :cantidad
                WHERE ITE_ID_ITEM_CARRITO = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idCarrito", OracleDbType.Int32).Value = (object?)dto.IdCarrito ?? DBNull.Value;
            cmd.Parameters.Add("idVuelo", OracleDbType.Int32).Value = (object?)dto.IdVuelo ?? DBNull.Value;
            cmd.Parameters.Add("numeroAsiento", OracleDbType.Varchar2).Value = (object?)dto.NumeroAsiento ?? DBNull.Value;
            cmd.Parameters.Add("clase", OracleDbType.Varchar2).Value = (object?)dto.Clase ?? DBNull.Value;
            cmd.Parameters.Add("precioUnitario", OracleDbType.Decimal).Value = (object?)dto.PrecioUnitario ?? DBNull.Value;
            cmd.Parameters.Add("cantidad", OracleDbType.Decimal).Value = dto.Cantidad ?? 1m;
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            const string query = @"
                DELETE FROM AER_ITEMCARRITO
                WHERE ITE_ID_ITEM_CARRITO = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static ItemCarritoResponseDto MapearItem(OracleDataReader reader)
        {
            return new ItemCarritoResponseDto
            {
                IdItemCarrito = Convert.ToInt32(reader["ITE_ID_ITEM_CARRITO"]),
                IdCarrito = reader["ITE_ID_CARRITO"] == DBNull.Value ? null : Convert.ToInt32(reader["ITE_ID_CARRITO"]),
                IdVuelo = reader["ITE_ID_VUELO"] == DBNull.Value ? null : Convert.ToInt32(reader["ITE_ID_VUELO"]),
                NumeroAsiento = reader["ITE_NUMERO_ASIENTO"] == DBNull.Value ? null : reader["ITE_NUMERO_ASIENTO"].ToString(),
                Clase = reader["ITE_CLASE"] == DBNull.Value ? null : reader["ITE_CLASE"].ToString(),
                PrecioUnitario = reader["ITE_PRECIO_UNITARIO"] == DBNull.Value ? null : Convert.ToDecimal(reader["ITE_PRECIO_UNITARIO"]),
                Cantidad = reader["ITE_CANTIDAD"] == DBNull.Value ? null : Convert.ToDecimal(reader["ITE_CANTIDAD"])
            };
        }
    }
}
