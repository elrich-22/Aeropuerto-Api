using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class VentaBoletoService
    {
        private readonly string _connectionString;

        public VentaBoletoService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<VentaBoletoResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<VentaBoletoResponseDto>();

            const string query = @"
                SELECT
                    VEN_ID_VENTA,
                    VEN_NUMERO_VENTA,
                    VEN_ID_PUNTO_VENTA,
                    VEN_ID_EMPLEADO_VENDEDOR,
                    VEN_ID_PASAJERO,
                    VEN_FECHA_VENTA,
                    VEN_MONTO_SUBTOTAL,
                    VEN_IMPUESTOS,
                    VEN_DESCUENTOS,
                    VEN_MONTO_TOTAL,
                    VEN_ID_METODO_PAGO,
                    VEN_CANAL_VENTA,
                    VEN_ESTADO
                FROM AER_VENTABOLETO
                ORDER BY VEN_ID_VENTA";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new VentaBoletoResponseDto
                {
                    Id = Convert.ToInt32(reader["VEN_ID_VENTA"]),
                    NumeroVenta = reader["VEN_NUMERO_VENTA"].ToString() ?? string.Empty,
                    IdPuntoVenta = Convert.ToInt32(reader["VEN_ID_PUNTO_VENTA"]),
                    IdEmpleadoVendedor = Convert.ToInt32(reader["VEN_ID_EMPLEADO_VENDEDOR"]),
                    IdPasajero = Convert.ToInt32(reader["VEN_ID_PASAJERO"]),
                    FechaVenta = Convert.ToDateTime(reader["VEN_FECHA_VENTA"]),
                    MontoSubtotal = Convert.ToDecimal(reader["VEN_MONTO_SUBTOTAL"]),
                    Impuestos = Convert.ToDecimal(reader["VEN_IMPUESTOS"]),
                    Descuentos = Convert.ToDecimal(reader["VEN_DESCUENTOS"]),
                    MontoTotal = Convert.ToDecimal(reader["VEN_MONTO_TOTAL"]),
                    IdMetodoPago = Convert.ToInt32(reader["VEN_ID_METODO_PAGO"]),
                    CanalVenta = reader["VEN_CANAL_VENTA"].ToString() ?? string.Empty,
                    Estado = reader["VEN_ESTADO"] == DBNull.Value ? string.Empty : reader["VEN_ESTADO"].ToString() ?? string.Empty
                });
            }

            return lista;
        }

        public async Task<VentaBoletoResponseDto?> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT
                    VEN_ID_VENTA,
                    VEN_NUMERO_VENTA,
                    VEN_ID_PUNTO_VENTA,
                    VEN_ID_EMPLEADO_VENDEDOR,
                    VEN_ID_PASAJERO,
                    VEN_FECHA_VENTA,
                    VEN_MONTO_SUBTOTAL,
                    VEN_IMPUESTOS,
                    VEN_DESCUENTOS,
                    VEN_MONTO_TOTAL,
                    VEN_ID_METODO_PAGO,
                    VEN_CANAL_VENTA,
                    VEN_ESTADO
                FROM AER_VENTABOLETO
                WHERE VEN_ID_VENTA = :id";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new VentaBoletoResponseDto
                {
                    Id = Convert.ToInt32(reader["VEN_ID_VENTA"]),
                    NumeroVenta = reader["VEN_NUMERO_VENTA"].ToString() ?? string.Empty,
                    IdPuntoVenta = Convert.ToInt32(reader["VEN_ID_PUNTO_VENTA"]),
                    IdEmpleadoVendedor = Convert.ToInt32(reader["VEN_ID_EMPLEADO_VENDEDOR"]),
                    IdPasajero = Convert.ToInt32(reader["VEN_ID_PASAJERO"]),
                    FechaVenta = Convert.ToDateTime(reader["VEN_FECHA_VENTA"]),
                    MontoSubtotal = Convert.ToDecimal(reader["VEN_MONTO_SUBTOTAL"]),
                    Impuestos = Convert.ToDecimal(reader["VEN_IMPUESTOS"]),
                    Descuentos = Convert.ToDecimal(reader["VEN_DESCUENTOS"]),
                    MontoTotal = Convert.ToDecimal(reader["VEN_MONTO_TOTAL"]),
                    IdMetodoPago = Convert.ToInt32(reader["VEN_ID_METODO_PAGO"]),
                    CanalVenta = reader["VEN_CANAL_VENTA"].ToString() ?? string.Empty,
                    Estado = reader["VEN_ESTADO"] == DBNull.Value ? string.Empty : reader["VEN_ESTADO"].ToString() ?? string.Empty
                };
            }

            return null;
        }

        public async Task<bool> CrearAsync(VentaBoletoCreateDto dto)
        {
            const string query = @"
                INSERT INTO AER_VENTABOLETO
                (
                    VEN_NUMERO_VENTA,
                    VEN_ID_PUNTO_VENTA,
                    VEN_ID_EMPLEADO_VENDEDOR,
                    VEN_ID_PASAJERO,
                    VEN_FECHA_VENTA,
                    VEN_MONTO_SUBTOTAL,
                    VEN_IMPUESTOS,
                    VEN_DESCUENTOS,
                    VEN_MONTO_TOTAL,
                    VEN_ID_METODO_PAGO,
                    VEN_CANAL_VENTA,
                    VEN_ESTADO
                )
                VALUES
                (
                    :numeroVenta,
                    :idPuntoVenta,
                    :idEmpleadoVendedor,
                    :idPasajero,
                    :fechaVenta,
                    :montoSubtotal,
                    :impuestos,
                    :descuentos,
                    :montoTotal,
                    :idMetodoPago,
                    :canalVenta,
                    :estado
                )";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("numeroVenta", OracleDbType.Varchar2).Value = dto.NumeroVenta;
            command.Parameters.Add("idPuntoVenta", OracleDbType.Int32).Value = dto.IdPuntoVenta;
            command.Parameters.Add("idEmpleadoVendedor", OracleDbType.Int32).Value = dto.IdEmpleadoVendedor;
            command.Parameters.Add("idPasajero", OracleDbType.Int32).Value = dto.IdPasajero;
            command.Parameters.Add("fechaVenta", OracleDbType.TimeStamp).Value = dto.FechaVenta;
            command.Parameters.Add("montoSubtotal", OracleDbType.Decimal).Value = dto.MontoSubtotal;
            command.Parameters.Add("impuestos", OracleDbType.Decimal).Value = dto.Impuestos;
            command.Parameters.Add("descuentos", OracleDbType.Decimal).Value = dto.Descuentos;
            command.Parameters.Add("montoTotal", OracleDbType.Decimal).Value = dto.MontoTotal;
            command.Parameters.Add("idMetodoPago", OracleDbType.Int32).Value = dto.IdMetodoPago;
            command.Parameters.Add("canalVenta", OracleDbType.Varchar2).Value = dto.CanalVenta;
            command.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "ACTIVA";

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> ActualizarAsync(int id, VentaBoletoUpdateDto dto)
        {
            const string query = @"
                UPDATE AER_VENTABOLETO
                SET
                    VEN_NUMERO_VENTA = :numeroVenta,
                    VEN_ID_PUNTO_VENTA = :idPuntoVenta,
                    VEN_ID_EMPLEADO_VENDEDOR = :idEmpleadoVendedor,
                    VEN_ID_PASAJERO = :idPasajero,
                    VEN_FECHA_VENTA = :fechaVenta,
                    VEN_MONTO_SUBTOTAL = :montoSubtotal,
                    VEN_IMPUESTOS = :impuestos,
                    VEN_DESCUENTOS = :descuentos,
                    VEN_MONTO_TOTAL = :montoTotal,
                    VEN_ID_METODO_PAGO = :idMetodoPago,
                    VEN_CANAL_VENTA = :canalVenta,
                    VEN_ESTADO = :estado
                WHERE VEN_ID_VENTA = :id";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("numeroVenta", OracleDbType.Varchar2).Value = dto.NumeroVenta;
            command.Parameters.Add("idPuntoVenta", OracleDbType.Int32).Value = dto.IdPuntoVenta;
            command.Parameters.Add("idEmpleadoVendedor", OracleDbType.Int32).Value = dto.IdEmpleadoVendedor;
            command.Parameters.Add("idPasajero", OracleDbType.Int32).Value = dto.IdPasajero;
            command.Parameters.Add("fechaVenta", OracleDbType.TimeStamp).Value = dto.FechaVenta;
            command.Parameters.Add("montoSubtotal", OracleDbType.Decimal).Value = dto.MontoSubtotal;
            command.Parameters.Add("impuestos", OracleDbType.Decimal).Value = dto.Impuestos;
            command.Parameters.Add("descuentos", OracleDbType.Decimal).Value = dto.Descuentos;
            command.Parameters.Add("montoTotal", OracleDbType.Decimal).Value = dto.MontoTotal;
            command.Parameters.Add("idMetodoPago", OracleDbType.Int32).Value = dto.IdMetodoPago;
            command.Parameters.Add("canalVenta", OracleDbType.Varchar2).Value = dto.CanalVenta;
            command.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "ACTIVA";
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            const string query = @"
                DELETE FROM AER_VENTABOLETO
                WHERE VEN_ID_VENTA = :id";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}