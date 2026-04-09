using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class AsistenciaService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public AsistenciaService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<AsistenciaResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<AsistenciaResponseDto>();

            var query = _sqlQueryProvider.GetQuery("AsistenciaService/ObtenerTodosAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(MapearAsistencia(reader));
            }

            return lista;
        }

        public async Task<AsistenciaResponseDto?> ObtenerPorIdAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("AsistenciaService/ObtenerPorIdAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return MapearAsistencia(reader);

            return null;
        }

        public async Task<bool> CrearAsync(AsistenciaCreateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("AsistenciaService/CrearAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idEmpleado", OracleDbType.Int32).Value = (object?)dto.IdEmpleado ?? DBNull.Value;
            cmd.Parameters.Add("fecha", OracleDbType.Date).Value = (object?)dto.Fecha ?? DBNull.Value;
            cmd.Parameters.Add("horaEntrada", OracleDbType.TimeStamp).Value = (object?)dto.HoraEntrada ?? DBNull.Value;
            cmd.Parameters.Add("horaSalida", OracleDbType.TimeStamp).Value = (object?)dto.HoraSalida ?? DBNull.Value;
            cmd.Parameters.Add("horasTrabajadas", OracleDbType.Decimal).Value = (object?)dto.HorasTrabajadas ?? DBNull.Value;
            cmd.Parameters.Add("tipo", OracleDbType.Varchar2).Value = (object?)dto.Tipo ?? DBNull.Value;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = (object?)dto.Estado ?? DBNull.Value;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, AsistenciaUpdateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("AsistenciaService/ActualizarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idEmpleado", OracleDbType.Int32).Value = (object?)dto.IdEmpleado ?? DBNull.Value;
            cmd.Parameters.Add("fecha", OracleDbType.Date).Value = (object?)dto.Fecha ?? DBNull.Value;
            cmd.Parameters.Add("horaEntrada", OracleDbType.TimeStamp).Value = (object?)dto.HoraEntrada ?? DBNull.Value;
            cmd.Parameters.Add("horaSalida", OracleDbType.TimeStamp).Value = (object?)dto.HoraSalida ?? DBNull.Value;
            cmd.Parameters.Add("horasTrabajadas", OracleDbType.Decimal).Value = (object?)dto.HorasTrabajadas ?? DBNull.Value;
            cmd.Parameters.Add("tipo", OracleDbType.Varchar2).Value = (object?)dto.Tipo ?? DBNull.Value;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = (object?)dto.Estado ?? DBNull.Value;
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("AsistenciaService/EliminarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static AsistenciaResponseDto MapearAsistencia(OracleDataReader reader)
        {
            return new AsistenciaResponseDto
            {
                IdAsistencia = Convert.ToInt32(reader["ASI_ID_ASISTENCIA"]),
                IdEmpleado = reader["ASI_ID_EMPLEADO"] == DBNull.Value ? null : Convert.ToInt32(reader["ASI_ID_EMPLEADO"]),
                Fecha = reader["ASI_FECHA"] == DBNull.Value ? null : Convert.ToDateTime(reader["ASI_FECHA"]),
                HoraEntrada = reader["ASI_HORA_ENTRADA"] == DBNull.Value ? null : Convert.ToDateTime(reader["ASI_HORA_ENTRADA"]),
                HoraSalida = reader["ASI_HORA_SALIDA"] == DBNull.Value ? null : Convert.ToDateTime(reader["ASI_HORA_SALIDA"]),
                HorasTrabajadas = reader["ASI_HORAS_TRABAJADAS"] == DBNull.Value ? null : Convert.ToDecimal(reader["ASI_HORAS_TRABAJADAS"]),
                Tipo = reader["ASI_TIPO"] == DBNull.Value ? null : reader["ASI_TIPO"].ToString(),
                Estado = reader["ASI_ESTADO"] == DBNull.Value ? null : reader["ASI_ESTADO"].ToString()
            };
        }
    }
}

