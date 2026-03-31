using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class DetalleVentaBoletoService
    {
        private readonly string _connectionString;

        public DetalleVentaBoletoService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<DetalleVentaBoletoResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<DetalleVentaBoletoResponseDto>();

            const string query = @"
                SELECT
                    DEV_ID_DETALLE_VENTA,
                    DEV_ID_VENTA,
                    DEV_ID_RESERVA,
                    DEV_PRECIO_BASE,
                    DEV_CARGOS_ADICIONALES
                FROM AER_DETALLEVENTABOLETO
                ORDER BY DEV_ID_DETALLE_VENTA";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(Mapear(reader));
            }

            return lista;
        }

        public async Task<DetalleVentaBoletoResponseDto?> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT
                    DEV_ID_DETALLE_VENTA,
                    DEV_ID_VENTA,
                    DEV_ID_RESERVA,
                    DEV_PRECIO_BASE,
                    DEV_CARGOS_ADICIONALES
                FROM AER_DETALLEVENTABOLETO
                WHERE DEV_ID_DETALLE_VENTA = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return Mapear(reader);

            return null;
        }

        public async Task<bool> CrearAsync(DetalleVentaBoletoCreateDto dto)
        {
            const string query = @"
                INSERT INTO AER_DETALLEVENTABOLETO
                (
                    DEV_ID_VENTA,
                    DEV_ID_RESERVA,
                    DEV_PRECIO_BASE,
                    DEV_CARGOS_ADICIONALES
                )
                VALUES
                (
                    :idVenta,
                    :idReserva,
                    :precioBase,
                    :cargosAdicionales
                )";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idVenta", OracleDbType.Int32).Value = dto.IdVenta;
            cmd.Parameters.Add("idReserva", OracleDbType.Int32).Value = dto.IdReserva;
            cmd.Parameters.Add("precioBase", OracleDbType.Decimal).Value = (object?)dto.PrecioBase ?? DBNull.Value;
            cmd.Parameters.Add("cargosAdicionales", OracleDbType.Decimal).Value = (object?)dto.CargosAdicionales ?? DBNull.Value;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, DetalleVentaBoletoUpdateDto dto)
        {
            const string query = @"
                UPDATE AER_DETALLEVENTABOLETO
                SET
                    DEV_ID_VENTA = :idVenta,
                    DEV_ID_RESERVA = :idReserva,
                    DEV_PRECIO_BASE = :precioBase,
                    DEV_CARGOS_ADICIONALES = :cargosAdicionales
                WHERE DEV_ID_DETALLE_VENTA = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idVenta", OracleDbType.Int32).Value = dto.IdVenta;
            cmd.Parameters.Add("idReserva", OracleDbType.Int32).Value = dto.IdReserva;
            cmd.Parameters.Add("precioBase", OracleDbType.Decimal).Value = (object?)dto.PrecioBase ?? DBNull.Value;
            cmd.Parameters.Add("cargosAdicionales", OracleDbType.Decimal).Value = (object?)dto.CargosAdicionales ?? DBNull.Value;
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            const string query = @"
                DELETE FROM AER_DETALLEVENTABOLETO
                WHERE DEV_ID_DETALLE_VENTA = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static DetalleVentaBoletoResponseDto Mapear(OracleDataReader reader)
        {
            return new DetalleVentaBoletoResponseDto
            {
                Id = Convert.ToInt32(reader["DEV_ID_DETALLE_VENTA"]),
                IdVenta = Convert.ToInt32(reader["DEV_ID_VENTA"]),
                IdReserva = Convert.ToInt32(reader["DEV_ID_RESERVA"]),
                PrecioBase = reader["DEV_PRECIO_BASE"] == DBNull.Value
                    ? null
                    : Convert.ToDecimal(reader["DEV_PRECIO_BASE"]),
                CargosAdicionales = reader["DEV_CARGOS_ADICIONALES"] == DBNull.Value
                    ? null
                    : Convert.ToDecimal(reader["DEV_CARGOS_ADICIONALES"])
            };
        }
    }
}