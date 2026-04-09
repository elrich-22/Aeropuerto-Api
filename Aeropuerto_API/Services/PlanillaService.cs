using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class PlanillaService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public PlanillaService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<PlanillaResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<PlanillaResponseDto>();

            var query = _sqlQueryProvider.GetQuery("PlanillaService/ObtenerTodosAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(MapearPlanilla(reader));
            }

            return lista;
        }

        public async Task<PlanillaResponseDto?> ObtenerPorIdAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("PlanillaService/ObtenerPorIdAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return MapearPlanilla(reader);

            return null;
        }

        public async Task<bool> CrearAsync(PlanillaCreateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("PlanillaService/CrearAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idEmpleado", OracleDbType.Int32).Value = (object?)dto.IdEmpleado ?? DBNull.Value;
            cmd.Parameters.Add("periodoInicio", OracleDbType.Date).Value = (object?)dto.PeriodoInicio ?? DBNull.Value;
            cmd.Parameters.Add("periodoFin", OracleDbType.Date).Value = (object?)dto.PeriodoFin ?? DBNull.Value;
            cmd.Parameters.Add("salarioBase", OracleDbType.Decimal).Value = (object?)dto.SalarioBase ?? DBNull.Value;
            cmd.Parameters.Add("bonificaciones", OracleDbType.Decimal).Value = (object?)dto.Bonificaciones ?? 0m;
            cmd.Parameters.Add("horasExtra", OracleDbType.Decimal).Value = (object?)dto.HorasExtra ?? 0m;
            cmd.Parameters.Add("deducciones", OracleDbType.Decimal).Value = (object?)dto.Deducciones ?? 0m;
            cmd.Parameters.Add("salarioNeto", OracleDbType.Decimal).Value = (object?)dto.SalarioNeto ?? DBNull.Value;
            cmd.Parameters.Add("fechaPago", OracleDbType.Date).Value = (object?)dto.FechaPago ?? DBNull.Value;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = (object?)(dto.Estado ?? "PENDIENTE") ?? DBNull.Value;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, PlanillaUpdateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("PlanillaService/ActualizarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idEmpleado", OracleDbType.Int32).Value = (object?)dto.IdEmpleado ?? DBNull.Value;
            cmd.Parameters.Add("periodoInicio", OracleDbType.Date).Value = (object?)dto.PeriodoInicio ?? DBNull.Value;
            cmd.Parameters.Add("periodoFin", OracleDbType.Date).Value = (object?)dto.PeriodoFin ?? DBNull.Value;
            cmd.Parameters.Add("salarioBase", OracleDbType.Decimal).Value = (object?)dto.SalarioBase ?? DBNull.Value;
            cmd.Parameters.Add("bonificaciones", OracleDbType.Decimal).Value = (object?)dto.Bonificaciones ?? 0m;
            cmd.Parameters.Add("horasExtra", OracleDbType.Decimal).Value = (object?)dto.HorasExtra ?? 0m;
            cmd.Parameters.Add("deducciones", OracleDbType.Decimal).Value = (object?)dto.Deducciones ?? 0m;
            cmd.Parameters.Add("salarioNeto", OracleDbType.Decimal).Value = (object?)dto.SalarioNeto ?? DBNull.Value;
            cmd.Parameters.Add("fechaPago", OracleDbType.Date).Value = (object?)dto.FechaPago ?? DBNull.Value;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = (object?)(dto.Estado ?? "PENDIENTE") ?? DBNull.Value;
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("PlanillaService/EliminarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static PlanillaResponseDto MapearPlanilla(OracleDataReader reader)
        {
            return new PlanillaResponseDto
            {
                IdPlanilla = Convert.ToInt32(reader["PLA_ID_PLANILLA"]),
                IdEmpleado = reader["PLA_ID_EMPLEADO"] == DBNull.Value ? null : Convert.ToInt32(reader["PLA_ID_EMPLEADO"]),
                PeriodoInicio = reader["PLA_PERIODO_INICIO"] == DBNull.Value ? null : Convert.ToDateTime(reader["PLA_PERIODO_INICIO"]),
                PeriodoFin = reader["PLA_PERIODO_FIN"] == DBNull.Value ? null : Convert.ToDateTime(reader["PLA_PERIODO_FIN"]),
                SalarioBase = reader["PLA_SALARIO_BASE"] == DBNull.Value ? null : Convert.ToDecimal(reader["PLA_SALARIO_BASE"]),
                Bonificaciones = reader["PLA_BONIFICACIONES"] == DBNull.Value ? null : Convert.ToDecimal(reader["PLA_BONIFICACIONES"]),
                HorasExtra = reader["PLA_HORAS_EXTRA"] == DBNull.Value ? null : Convert.ToDecimal(reader["PLA_HORAS_EXTRA"]),
                Deducciones = reader["PLA_DEDUCCIONES"] == DBNull.Value ? null : Convert.ToDecimal(reader["PLA_DEDUCCIONES"]),
                SalarioNeto = reader["PLA_SALARIO_NETO"] == DBNull.Value ? null : Convert.ToDecimal(reader["PLA_SALARIO_NETO"]),
                FechaPago = reader["PLA_FECHA_PAGO"] == DBNull.Value ? null : Convert.ToDateTime(reader["PLA_FECHA_PAGO"]),
                Estado = reader["PLA_ESTADO"] == DBNull.Value ? null : reader["PLA_ESTADO"].ToString()
            };
        }
    }
}

