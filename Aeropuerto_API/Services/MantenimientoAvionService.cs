using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class MantenimientoAvionService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public MantenimientoAvionService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<MantenimientoAvionResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<MantenimientoAvionResponseDto>();

            var query = _sqlQueryProvider.GetQuery("MantenimientoAvionService/ObtenerTodosAsync.sql");

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

        public async Task<MantenimientoAvionResponseDto?> ObtenerPorIdAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("MantenimientoAvionService/ObtenerPorIdAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return Mapear(reader);

            return null;
        }

        public async Task<bool> CrearAsync(MantenimientoAvionCreateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("MantenimientoAvionService/CrearAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);

            cmd.Parameters.Add("idAvion", OracleDbType.Int32).Value = dto.IdAvion;
            cmd.Parameters.Add("tipoMantenimiento", OracleDbType.Varchar2).Value = dto.TipoMantenimiento;
            cmd.Parameters.Add("fechaInicio", OracleDbType.TimeStamp).Value = dto.FechaInicio;
            cmd.Parameters.Add("fechaFinEstimada", OracleDbType.TimeStamp).Value = (object?)dto.FechaFinEstimada ?? DBNull.Value;
            cmd.Parameters.Add("fechaFinReal", OracleDbType.TimeStamp).Value = (object?)dto.FechaFinReal ?? DBNull.Value;
            cmd.Parameters.Add("descripcionTrabajo", OracleDbType.Clob).Value = (object?)dto.DescripcionTrabajo ?? DBNull.Value;
            cmd.Parameters.Add("idEmpleadoResponsable", OracleDbType.Int32).Value = dto.IdEmpleadoResponsable;
            cmd.Parameters.Add("costoManoObra", OracleDbType.Decimal).Value = (object?)dto.CostoManoObra ?? DBNull.Value;
            cmd.Parameters.Add("costoRepuestos", OracleDbType.Decimal).Value = (object?)dto.CostoRepuestos ?? DBNull.Value;
            cmd.Parameters.Add("costoTotal", OracleDbType.Decimal).Value = (object?)dto.CostoTotal ?? DBNull.Value;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "EN_PROCESO";

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, MantenimientoAvionUpdateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("MantenimientoAvionService/ActualizarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);

            cmd.Parameters.Add("idAvion", OracleDbType.Int32).Value = dto.IdAvion;
            cmd.Parameters.Add("tipoMantenimiento", OracleDbType.Varchar2).Value = dto.TipoMantenimiento;
            cmd.Parameters.Add("fechaInicio", OracleDbType.TimeStamp).Value = dto.FechaInicio;
            cmd.Parameters.Add("fechaFinEstimada", OracleDbType.TimeStamp).Value = (object?)dto.FechaFinEstimada ?? DBNull.Value;
            cmd.Parameters.Add("fechaFinReal", OracleDbType.TimeStamp).Value = (object?)dto.FechaFinReal ?? DBNull.Value;
            cmd.Parameters.Add("descripcionTrabajo", OracleDbType.Clob).Value = (object?)dto.DescripcionTrabajo ?? DBNull.Value;
            cmd.Parameters.Add("idEmpleadoResponsable", OracleDbType.Int32).Value = dto.IdEmpleadoResponsable;
            cmd.Parameters.Add("costoManoObra", OracleDbType.Decimal).Value = (object?)dto.CostoManoObra ?? DBNull.Value;
            cmd.Parameters.Add("costoRepuestos", OracleDbType.Decimal).Value = (object?)dto.CostoRepuestos ?? DBNull.Value;
            cmd.Parameters.Add("costoTotal", OracleDbType.Decimal).Value = (object?)dto.CostoTotal ?? DBNull.Value;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "EN_PROCESO";
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("MantenimientoAvionService/EliminarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static MantenimientoAvionResponseDto Mapear(OracleDataReader r)
        {
            return new MantenimientoAvionResponseDto
            {
                Id = Convert.ToInt32(r["MAN_ID_MANTENIMIENTO"]),
                IdAvion = Convert.ToInt32(r["MAN_ID_AVION"]),
                TipoMantenimiento = r["MAN_TIPO_MANTENIMIENTO"].ToString() ?? string.Empty,
                FechaInicio = Convert.ToDateTime(r["MAN_FECHA_INICIO"]),
                FechaFinEstimada = r["MAN_FECHA_FIN_ESTIMADA"] == DBNull.Value
                    ? null
                    : Convert.ToDateTime(r["MAN_FECHA_FIN_ESTIMADA"]),
                FechaFinReal = r["MAN_FECHA_FIN_REAL"] == DBNull.Value
                    ? null
                    : Convert.ToDateTime(r["MAN_FECHA_FIN_REAL"]),
                DescripcionTrabajo = r["MAN_DESCRIPCION_TRABAJO"] == DBNull.Value
                    ? null
                    : r["MAN_DESCRIPCION_TRABAJO"].ToString(),
                IdEmpleadoResponsable = Convert.ToInt32(r["MAN_ID_EMPLEADO_RESPONSABLE"]),
                CostoManoObra = r["MAN_COSTO_MANO_OBRA"] == DBNull.Value
                    ? null
                    : Convert.ToDecimal(r["MAN_COSTO_MANO_OBRA"]),
                CostoRepuestos = r["MAN_COSTO_REPUESTOS"] == DBNull.Value
                    ? null
                    : Convert.ToDecimal(r["MAN_COSTO_REPUESTOS"]),
                CostoTotal = r["MAN_COSTO_TOTAL"] == DBNull.Value
                    ? null
                    : Convert.ToDecimal(r["MAN_COSTO_TOTAL"]),
                Estado = r["MAN_ESTADO"].ToString() ?? string.Empty
            };
        }
    }
}
