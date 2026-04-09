using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class UsoPromocionService
    {
        private readonly string _connectionString;

        public UsoPromocionService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<UsoPromocionResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<UsoPromocionResponseDto>();

            const string query = @"
                SELECT
                    USO_ID_USO,
                    USO_ID_PROMOCION,
                    USO_ID_RESERVA,
                    USO_FECHA_USO,
                    USO_MONTO_DESCUENTO
                FROM AER_USOPROMOCION
                ORDER BY USO_ID_USO";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(MapearUso(reader));
            }

            return lista;
        }

        public async Task<UsoPromocionResponseDto?> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT
                    USO_ID_USO,
                    USO_ID_PROMOCION,
                    USO_ID_RESERVA,
                    USO_FECHA_USO,
                    USO_MONTO_DESCUENTO
                FROM AER_USOPROMOCION
                WHERE USO_ID_USO = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return MapearUso(reader);

            return null;
        }

        public async Task<bool> CrearAsync(UsoPromocionCreateDto dto)
        {
            const string query = @"
                INSERT INTO AER_USOPROMOCION
                (
                    USO_ID_PROMOCION,
                    USO_ID_RESERVA,
                    USO_FECHA_USO,
                    USO_MONTO_DESCUENTO
                )
                VALUES
                (
                    :idPromocion,
                    :idReserva,
                    :fechaUso,
                    :montoDescuento
                )";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idPromocion", OracleDbType.Int32).Value = (object?)dto.IdPromocion ?? DBNull.Value;
            cmd.Parameters.Add("idReserva", OracleDbType.Int32).Value = (object?)dto.IdReserva ?? DBNull.Value;
            cmd.Parameters.Add("fechaUso", OracleDbType.TimeStamp).Value = dto.FechaUso ?? DateTime.Now;
            cmd.Parameters.Add("montoDescuento", OracleDbType.Decimal).Value = (object?)dto.MontoDescuento ?? DBNull.Value;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, UsoPromocionUpdateDto dto)
        {
            const string query = @"
                UPDATE AER_USOPROMOCION
                SET
                    USO_ID_PROMOCION = :idPromocion,
                    USO_ID_RESERVA = :idReserva,
                    USO_FECHA_USO = :fechaUso,
                    USO_MONTO_DESCUENTO = :montoDescuento
                WHERE USO_ID_USO = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idPromocion", OracleDbType.Int32).Value = (object?)dto.IdPromocion ?? DBNull.Value;
            cmd.Parameters.Add("idReserva", OracleDbType.Int32).Value = (object?)dto.IdReserva ?? DBNull.Value;
            cmd.Parameters.Add("fechaUso", OracleDbType.TimeStamp).Value = dto.FechaUso ?? DateTime.Now;
            cmd.Parameters.Add("montoDescuento", OracleDbType.Decimal).Value = (object?)dto.MontoDescuento ?? DBNull.Value;
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            const string query = @"
                DELETE FROM AER_USOPROMOCION
                WHERE USO_ID_USO = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static UsoPromocionResponseDto MapearUso(OracleDataReader reader)
        {
            return new UsoPromocionResponseDto
            {
                IdUso = Convert.ToInt32(reader["USO_ID_USO"]),
                IdPromocion = reader["USO_ID_PROMOCION"] == DBNull.Value ? null : Convert.ToInt32(reader["USO_ID_PROMOCION"]),
                IdReserva = reader["USO_ID_RESERVA"] == DBNull.Value ? null : Convert.ToInt32(reader["USO_ID_RESERVA"]),
                FechaUso = reader["USO_FECHA_USO"] == DBNull.Value ? null : Convert.ToDateTime(reader["USO_FECHA_USO"]),
                MontoDescuento = reader["USO_MONTO_DESCUENTO"] == DBNull.Value ? null : Convert.ToDecimal(reader["USO_MONTO_DESCUENTO"])
            };
        }
    }
}
