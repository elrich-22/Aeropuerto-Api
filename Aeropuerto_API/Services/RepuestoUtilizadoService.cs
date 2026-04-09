using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class RepuestoUtilizadoService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public RepuestoUtilizadoService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<RepuestoUtilizadoResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<RepuestoUtilizadoResponseDto>();

            var query = _sqlQueryProvider.GetQuery("RepuestoUtilizadoService/ObtenerTodosAsync.sql");

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

        public async Task<RepuestoUtilizadoResponseDto?> ObtenerPorIdAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("RepuestoUtilizadoService/ObtenerPorIdAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return Mapear(reader);

            return null;
        }

        public async Task<bool> CrearAsync(RepuestoUtilizadoCreateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("RepuestoUtilizadoService/CrearAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idMantenimiento", OracleDbType.Int32).Value = dto.IdMantenimiento;
            cmd.Parameters.Add("idRepuesto", OracleDbType.Int32).Value = dto.IdRepuesto;
            cmd.Parameters.Add("cantidad", OracleDbType.Int32).Value = dto.Cantidad;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, RepuestoUtilizadoUpdateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("RepuestoUtilizadoService/ActualizarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idMantenimiento", OracleDbType.Int32).Value = dto.IdMantenimiento;
            cmd.Parameters.Add("idRepuesto", OracleDbType.Int32).Value = dto.IdRepuesto;
            cmd.Parameters.Add("cantidad", OracleDbType.Int32).Value = dto.Cantidad;
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("RepuestoUtilizadoService/EliminarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static RepuestoUtilizadoResponseDto Mapear(OracleDataReader reader)
        {
            return new RepuestoUtilizadoResponseDto
            {
                Id = Convert.ToInt32(reader["RUT_ID_REPUESTO_UTILIZADO"]),
                IdMantenimiento = Convert.ToInt32(reader["RUT_ID_MANTENIMIENTO"]),
                IdRepuesto = Convert.ToInt32(reader["RUT_ID_REPUESTO"]),
                Cantidad = Convert.ToInt32(reader["RUT_CANTIDAD"])
            };
        }
    }
}
