using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class MovimientoRepuestoService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public MovimientoRepuestoService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<MovimientoRepuestoResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<MovimientoRepuestoResponseDto>();

            var query = _sqlQueryProvider.GetQuery("MovimientoRepuestoService/ObtenerTodosAsync.sql");

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

        public async Task<MovimientoRepuestoResponseDto?> ObtenerPorIdAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("MovimientoRepuestoService/ObtenerPorIdAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return Mapear(reader);

            return null;
        }

        public async Task<bool> CrearAsync(MovimientoRepuestoCreateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("MovimientoRepuestoService/CrearAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idRepuesto", OracleDbType.Int32).Value = dto.IdRepuesto;
            cmd.Parameters.Add("tipoMovimiento", OracleDbType.Varchar2).Value = dto.TipoMovimiento;
            cmd.Parameters.Add("cantidad", OracleDbType.Int32).Value = dto.Cantidad;
            cmd.Parameters.Add("fechaMovimiento", OracleDbType.TimeStamp).Value = dto.FechaMovimiento;
            cmd.Parameters.Add("idEmpleado", OracleDbType.Int32).Value = dto.IdEmpleado;
            cmd.Parameters.Add("motivo", OracleDbType.Varchar2).Value = (object?)dto.Motivo ?? DBNull.Value;
            cmd.Parameters.Add("referencia", OracleDbType.Varchar2).Value = (object?)dto.Referencia ?? DBNull.Value;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, MovimientoRepuestoUpdateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("MovimientoRepuestoService/ActualizarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idRepuesto", OracleDbType.Int32).Value = dto.IdRepuesto;
            cmd.Parameters.Add("tipoMovimiento", OracleDbType.Varchar2).Value = dto.TipoMovimiento;
            cmd.Parameters.Add("cantidad", OracleDbType.Int32).Value = dto.Cantidad;
            cmd.Parameters.Add("fechaMovimiento", OracleDbType.TimeStamp).Value = dto.FechaMovimiento;
            cmd.Parameters.Add("idEmpleado", OracleDbType.Int32).Value = dto.IdEmpleado;
            cmd.Parameters.Add("motivo", OracleDbType.Varchar2).Value = (object?)dto.Motivo ?? DBNull.Value;
            cmd.Parameters.Add("referencia", OracleDbType.Varchar2).Value = (object?)dto.Referencia ?? DBNull.Value;
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("MovimientoRepuestoService/EliminarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static MovimientoRepuestoResponseDto Mapear(OracleDataReader reader)
        {
            return new MovimientoRepuestoResponseDto
            {
                Id = Convert.ToInt32(reader["MOV_ID_MOVIMIENTO"]),
                IdRepuesto = Convert.ToInt32(reader["MOV_ID_REPUESTO"]),
                TipoMovimiento = reader["MOV_TIPO_MOVIMIENTO"].ToString() ?? string.Empty,
                Cantidad = Convert.ToInt32(reader["MOV_CANTIDAD"]),
                FechaMovimiento = Convert.ToDateTime(reader["MOV_FECHA_MOVIMIENTO"]),
                IdEmpleado = Convert.ToInt32(reader["MOV_ID_EMPLEADO"]),
                Motivo = reader["MOV_MOTIVO"] == DBNull.Value ? null : reader["MOV_MOTIVO"].ToString(),
                Referencia = reader["MOV_REFERENCIA"] == DBNull.Value ? null : reader["MOV_REFERENCIA"].ToString()
            };
        }
    }
}
