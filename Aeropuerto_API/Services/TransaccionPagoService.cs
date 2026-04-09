using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class TransaccionPagoService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public TransaccionPagoService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<TransaccionPagoResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<TransaccionPagoResponseDto>();

            var query = _sqlQueryProvider.GetQuery("TransaccionPagoService/ObtenerTodosAsync.sql");

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new TransaccionPagoResponseDto
                {
                    Id = Convert.ToInt32(reader["TRA_ID_TRANSACCION"]),
                    IdReserva = Convert.ToInt32(reader["TRA_ID_RESERVA"]),
                    IdMetodoPago = Convert.ToInt32(reader["TRA_ID_METODO_PAGO"]),
                    MontoTotal = Convert.ToDecimal(reader["TRA_MONTO_TOTAL"]),
                    Moneda = reader["TRA_MONEDA"].ToString() ?? string.Empty,
                    FechaTransaccion = Convert.ToDateTime(reader["TRA_FECHA_TRANSACCION"]),
                    Estado = reader["TRA_ESTADO"] == DBNull.Value ? string.Empty : reader["TRA_ESTADO"].ToString() ?? string.Empty,
                    NumeroAutorizacion = reader["TRA_NUMERO_AUTORIZACION"] == DBNull.Value ? null : reader["TRA_NUMERO_AUTORIZACION"].ToString(),
                    ReferenciaExterna = reader["TRA_REFERENCIA_EXTERNA"] == DBNull.Value ? null : reader["TRA_REFERENCIA_EXTERNA"].ToString(),
                    IpCliente = reader["TRA_IP_CLIENTE"] == DBNull.Value ? null : reader["TRA_IP_CLIENTE"].ToString(),
                    DetallesTarjeta = reader["TRA_DETALLES_TARJETA"] == DBNull.Value ? null : reader["TRA_DETALLES_TARJETA"].ToString()
                });
            }

            return lista;
        }

        public async Task<TransaccionPagoResponseDto?> ObtenerPorIdAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("TransaccionPagoService/ObtenerPorIdAsync.sql");

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new TransaccionPagoResponseDto
                {
                    Id = Convert.ToInt32(reader["TRA_ID_TRANSACCION"]),
                    IdReserva = Convert.ToInt32(reader["TRA_ID_RESERVA"]),
                    IdMetodoPago = Convert.ToInt32(reader["TRA_ID_METODO_PAGO"]),
                    MontoTotal = Convert.ToDecimal(reader["TRA_MONTO_TOTAL"]),
                    Moneda = reader["TRA_MONEDA"].ToString() ?? string.Empty,
                    FechaTransaccion = Convert.ToDateTime(reader["TRA_FECHA_TRANSACCION"]),
                    Estado = reader["TRA_ESTADO"] == DBNull.Value ? string.Empty : reader["TRA_ESTADO"].ToString() ?? string.Empty,
                    NumeroAutorizacion = reader["TRA_NUMERO_AUTORIZACION"] == DBNull.Value ? null : reader["TRA_NUMERO_AUTORIZACION"].ToString(),
                    ReferenciaExterna = reader["TRA_REFERENCIA_EXTERNA"] == DBNull.Value ? null : reader["TRA_REFERENCIA_EXTERNA"].ToString(),
                    IpCliente = reader["TRA_IP_CLIENTE"] == DBNull.Value ? null : reader["TRA_IP_CLIENTE"].ToString(),
                    DetallesTarjeta = reader["TRA_DETALLES_TARJETA"] == DBNull.Value ? null : reader["TRA_DETALLES_TARJETA"].ToString()
                };
            }

            return null;
        }

        public async Task<bool> CrearAsync(TransaccionPagoCreateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("TransaccionPagoService/CrearAsync.sql");

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("idReserva", OracleDbType.Int32).Value = dto.IdReserva;
            command.Parameters.Add("idMetodoPago", OracleDbType.Int32).Value = dto.IdMetodoPago;
            command.Parameters.Add("montoTotal", OracleDbType.Decimal).Value = dto.MontoTotal;
            command.Parameters.Add("moneda", OracleDbType.Varchar2).Value = dto.Moneda;
            command.Parameters.Add("fechaTransaccion", OracleDbType.TimeStamp).Value = dto.FechaTransaccion;
            command.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "APROBADA";
            command.Parameters.Add("numeroAutorizacion", OracleDbType.Varchar2).Value = (object?)dto.NumeroAutorizacion ?? DBNull.Value;
            command.Parameters.Add("referenciaExterna", OracleDbType.Varchar2).Value = (object?)dto.ReferenciaExterna ?? DBNull.Value;
            command.Parameters.Add("ipCliente", OracleDbType.Varchar2).Value = (object?)dto.IpCliente ?? DBNull.Value;
            command.Parameters.Add("detallesTarjeta", OracleDbType.Varchar2).Value = (object?)dto.DetallesTarjeta ?? DBNull.Value;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> ActualizarAsync(int id, TransaccionPagoUpdateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("TransaccionPagoService/ActualizarAsync.sql");

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("idReserva", OracleDbType.Int32).Value = dto.IdReserva;
            command.Parameters.Add("idMetodoPago", OracleDbType.Int32).Value = dto.IdMetodoPago;
            command.Parameters.Add("montoTotal", OracleDbType.Decimal).Value = dto.MontoTotal;
            command.Parameters.Add("moneda", OracleDbType.Varchar2).Value = dto.Moneda;
            command.Parameters.Add("fechaTransaccion", OracleDbType.TimeStamp).Value = dto.FechaTransaccion;
            command.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "APROBADA";
            command.Parameters.Add("numeroAutorizacion", OracleDbType.Varchar2).Value = (object?)dto.NumeroAutorizacion ?? DBNull.Value;
            command.Parameters.Add("referenciaExterna", OracleDbType.Varchar2).Value = (object?)dto.ReferenciaExterna ?? DBNull.Value;
            command.Parameters.Add("ipCliente", OracleDbType.Varchar2).Value = (object?)dto.IpCliente ?? DBNull.Value;
            command.Parameters.Add("detallesTarjeta", OracleDbType.Varchar2).Value = (object?)dto.DetallesTarjeta ?? DBNull.Value;
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("TransaccionPagoService/EliminarAsync.sql");

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}
