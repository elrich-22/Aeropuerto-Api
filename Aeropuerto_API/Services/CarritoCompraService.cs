using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class CarritoCompraService
    {
        private readonly string _connectionString;

        public CarritoCompraService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<CarritoCompraResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<CarritoCompraResponseDto>();

            const string query = @"
                SELECT
                    CAR_ID_CARRITO,
                    CAR_ID_PASAJERO,
                    CAR_SESION_ID,
                    CAR_FECHA_CREACION,
                    CAR_FECHA_EXPIRACION,
                    CAR_ESTADO
                FROM AER_CARRITOCOMPRA
                ORDER BY CAR_ID_CARRITO";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(MapearCarrito(reader));
            }

            return lista;
        }

        public async Task<CarritoCompraResponseDto?> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT
                    CAR_ID_CARRITO,
                    CAR_ID_PASAJERO,
                    CAR_SESION_ID,
                    CAR_FECHA_CREACION,
                    CAR_FECHA_EXPIRACION,
                    CAR_ESTADO
                FROM AER_CARRITOCOMPRA
                WHERE CAR_ID_CARRITO = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return MapearCarrito(reader);

            return null;
        }

        public async Task<bool> CrearAsync(CarritoCompraCreateDto dto)
        {
            const string query = @"
                INSERT INTO AER_CARRITOCOMPRA
                (
                    CAR_ID_PASAJERO,
                    CAR_SESION_ID,
                    CAR_FECHA_CREACION,
                    CAR_FECHA_EXPIRACION,
                    CAR_ESTADO
                )
                VALUES
                (
                    :idPasajero,
                    :sesionId,
                    :fechaCreacion,
                    :fechaExpiracion,
                    :estado
                )";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            var fechaCreacion = dto.FechaCreacion ?? DateTime.Now;
            var fechaExpiracion = dto.FechaExpiracion ?? fechaCreacion.AddHours(2);

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idPasajero", OracleDbType.Int32).Value = (object?)dto.IdPasajero ?? DBNull.Value;
            cmd.Parameters.Add("sesionId", OracleDbType.Varchar2).Value = (object?)dto.SesionId ?? DBNull.Value;
            cmd.Parameters.Add("fechaCreacion", OracleDbType.TimeStamp).Value = fechaCreacion;
            cmd.Parameters.Add("fechaExpiracion", OracleDbType.TimeStamp).Value = fechaExpiracion;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "ACTIVO";

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, CarritoCompraUpdateDto dto)
        {
            const string query = @"
                UPDATE AER_CARRITOCOMPRA
                SET
                    CAR_ID_PASAJERO = :idPasajero,
                    CAR_SESION_ID = :sesionId,
                    CAR_FECHA_CREACION = :fechaCreacion,
                    CAR_FECHA_EXPIRACION = :fechaExpiracion,
                    CAR_ESTADO = :estado
                WHERE CAR_ID_CARRITO = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            var fechaCreacion = dto.FechaCreacion ?? DateTime.Now;
            var fechaExpiracion = dto.FechaExpiracion ?? fechaCreacion.AddHours(2);

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idPasajero", OracleDbType.Int32).Value = (object?)dto.IdPasajero ?? DBNull.Value;
            cmd.Parameters.Add("sesionId", OracleDbType.Varchar2).Value = (object?)dto.SesionId ?? DBNull.Value;
            cmd.Parameters.Add("fechaCreacion", OracleDbType.TimeStamp).Value = fechaCreacion;
            cmd.Parameters.Add("fechaExpiracion", OracleDbType.TimeStamp).Value = fechaExpiracion;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "ACTIVO";
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            const string query = @"
                DELETE FROM AER_CARRITOCOMPRA
                WHERE CAR_ID_CARRITO = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static CarritoCompraResponseDto MapearCarrito(OracleDataReader reader)
        {
            return new CarritoCompraResponseDto
            {
                IdCarrito = Convert.ToInt32(reader["CAR_ID_CARRITO"]),
                IdPasajero = reader["CAR_ID_PASAJERO"] == DBNull.Value ? null : Convert.ToInt32(reader["CAR_ID_PASAJERO"]),
                SesionId = reader["CAR_SESION_ID"] == DBNull.Value ? null : reader["CAR_SESION_ID"].ToString(),
                FechaCreacion = reader["CAR_FECHA_CREACION"] == DBNull.Value ? null : Convert.ToDateTime(reader["CAR_FECHA_CREACION"]),
                FechaExpiracion = reader["CAR_FECHA_EXPIRACION"] == DBNull.Value ? null : Convert.ToDateTime(reader["CAR_FECHA_EXPIRACION"]),
                Estado = reader["CAR_ESTADO"] == DBNull.Value ? null : reader["CAR_ESTADO"].ToString()
            };
        }
    }
}
