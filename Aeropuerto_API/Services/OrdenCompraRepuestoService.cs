using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class OrdenCompraRepuestoService
    {
        private readonly string _connectionString;

        public OrdenCompraRepuestoService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<OrdenCompraRepuestoResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<OrdenCompraRepuestoResponseDto>();

            const string query = @"
                SELECT
                    ORC_ID_ORDEN_COMPRA,
                    ORC_NUMERO_ORDEN,
                    ORC_ID_PROVEEDOR,
                    ORC_FECHA_ORDEN,
                    ORC_FECHA_ENTREGA_ESPERADA,
                    ORC_FECHA_ENTREGA_REAL,
                    ORC_MONTO_TOTAL,
                    ORC_ESTADO,
                    ORC_ID_EMPLEADO_SOLICITA,
                    ORC_OBSERVACIONES
                FROM AER_ORDENCOMPRAREPUESTO
                ORDER BY ORC_ID_ORDEN_COMPRA";

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

        public async Task<OrdenCompraRepuestoResponseDto?> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT
                    ORC_ID_ORDEN_COMPRA,
                    ORC_NUMERO_ORDEN,
                    ORC_ID_PROVEEDOR,
                    ORC_FECHA_ORDEN,
                    ORC_FECHA_ENTREGA_ESPERADA,
                    ORC_FECHA_ENTREGA_REAL,
                    ORC_MONTO_TOTAL,
                    ORC_ESTADO,
                    ORC_ID_EMPLEADO_SOLICITA,
                    ORC_OBSERVACIONES
                FROM AER_ORDENCOMPRAREPUESTO
                WHERE ORC_ID_ORDEN_COMPRA = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return Mapear(reader);

            return null;
        }

        public async Task<bool> CrearAsync(OrdenCompraRepuestoCreateDto dto)
        {
            const string query = @"
                INSERT INTO AER_ORDENCOMPRAREPUESTO
                (
                    ORC_NUMERO_ORDEN,
                    ORC_ID_PROVEEDOR,
                    ORC_FECHA_ORDEN,
                    ORC_FECHA_ENTREGA_ESPERADA,
                    ORC_FECHA_ENTREGA_REAL,
                    ORC_MONTO_TOTAL,
                    ORC_ESTADO,
                    ORC_ID_EMPLEADO_SOLICITA,
                    ORC_OBSERVACIONES
                )
                VALUES
                (
                    :numeroOrden,
                    :idProveedor,
                    :fechaOrden,
                    :fechaEntregaEsperada,
                    :fechaEntregaReal,
                    :montoTotal,
                    :estado,
                    :idEmpleadoSolicita,
                    :observaciones
                )";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("numeroOrden", OracleDbType.Varchar2).Value = dto.NumeroOrden;
            cmd.Parameters.Add("idProveedor", OracleDbType.Int32).Value = dto.IdProveedor;
            cmd.Parameters.Add("fechaOrden", OracleDbType.Date).Value = dto.FechaOrden;
            cmd.Parameters.Add("fechaEntregaEsperada", OracleDbType.Date).Value = (object?)dto.FechaEntregaEsperada ?? DBNull.Value;
            cmd.Parameters.Add("fechaEntregaReal", OracleDbType.Date).Value = (object?)dto.FechaEntregaReal ?? DBNull.Value;
            cmd.Parameters.Add("montoTotal", OracleDbType.Decimal).Value = (object?)dto.MontoTotal ?? DBNull.Value;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "PENDIENTE";
            cmd.Parameters.Add("idEmpleadoSolicita", OracleDbType.Int32).Value = dto.IdEmpleadoSolicita;
            cmd.Parameters.Add("observaciones", OracleDbType.Varchar2).Value = (object?)dto.Observaciones ?? DBNull.Value;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, OrdenCompraRepuestoUpdateDto dto)
        {
            const string query = @"
                UPDATE AER_ORDENCOMPRAREPUESTO
                SET
                    ORC_NUMERO_ORDEN = :numeroOrden,
                    ORC_ID_PROVEEDOR = :idProveedor,
                    ORC_FECHA_ORDEN = :fechaOrden,
                    ORC_FECHA_ENTREGA_ESPERADA = :fechaEntregaEsperada,
                    ORC_FECHA_ENTREGA_REAL = :fechaEntregaReal,
                    ORC_MONTO_TOTAL = :montoTotal,
                    ORC_ESTADO = :estado,
                    ORC_ID_EMPLEADO_SOLICITA = :idEmpleadoSolicita,
                    ORC_OBSERVACIONES = :observaciones
                WHERE ORC_ID_ORDEN_COMPRA = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("numeroOrden", OracleDbType.Varchar2).Value = dto.NumeroOrden;
            cmd.Parameters.Add("idProveedor", OracleDbType.Int32).Value = dto.IdProveedor;
            cmd.Parameters.Add("fechaOrden", OracleDbType.Date).Value = dto.FechaOrden;
            cmd.Parameters.Add("fechaEntregaEsperada", OracleDbType.Date).Value = (object?)dto.FechaEntregaEsperada ?? DBNull.Value;
            cmd.Parameters.Add("fechaEntregaReal", OracleDbType.Date).Value = (object?)dto.FechaEntregaReal ?? DBNull.Value;
            cmd.Parameters.Add("montoTotal", OracleDbType.Decimal).Value = (object?)dto.MontoTotal ?? DBNull.Value;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "PENDIENTE";
            cmd.Parameters.Add("idEmpleadoSolicita", OracleDbType.Int32).Value = dto.IdEmpleadoSolicita;
            cmd.Parameters.Add("observaciones", OracleDbType.Varchar2).Value = (object?)dto.Observaciones ?? DBNull.Value;
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            const string query = @"
                DELETE FROM AER_ORDENCOMPRAREPUESTO
                WHERE ORC_ID_ORDEN_COMPRA = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static OrdenCompraRepuestoResponseDto Mapear(OracleDataReader reader)
        {
            return new OrdenCompraRepuestoResponseDto
            {
                Id = Convert.ToInt32(reader["ORC_ID_ORDEN_COMPRA"]),
                NumeroOrden = reader["ORC_NUMERO_ORDEN"].ToString() ?? string.Empty,
                IdProveedor = Convert.ToInt32(reader["ORC_ID_PROVEEDOR"]),
                FechaOrden = Convert.ToDateTime(reader["ORC_FECHA_ORDEN"]),
                FechaEntregaEsperada = reader["ORC_FECHA_ENTREGA_ESPERADA"] == DBNull.Value
                    ? null
                    : Convert.ToDateTime(reader["ORC_FECHA_ENTREGA_ESPERADA"]),
                FechaEntregaReal = reader["ORC_FECHA_ENTREGA_REAL"] == DBNull.Value
                    ? null
                    : Convert.ToDateTime(reader["ORC_FECHA_ENTREGA_REAL"]),
                MontoTotal = reader["ORC_MONTO_TOTAL"] == DBNull.Value
                    ? null
                    : Convert.ToDecimal(reader["ORC_MONTO_TOTAL"]),
                Estado = reader["ORC_ESTADO"] == DBNull.Value ? string.Empty : reader["ORC_ESTADO"].ToString() ?? string.Empty,
                IdEmpleadoSolicita = Convert.ToInt32(reader["ORC_ID_EMPLEADO_SOLICITA"]),
                Observaciones = reader["ORC_OBSERVACIONES"] == DBNull.Value ? null : reader["ORC_OBSERVACIONES"].ToString()
            };
        }
    }
}