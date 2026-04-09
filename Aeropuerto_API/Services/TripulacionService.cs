using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class TripulacionService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public TripulacionService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<TripulacionResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<TripulacionResponseDto>();

            var query = _sqlQueryProvider.GetQuery("TripulacionService/ObtenerTodosAsync.sql");

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

        public async Task<TripulacionResponseDto?> ObtenerPorIdAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("TripulacionService/ObtenerPorIdAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return Mapear(reader);

            return null;
        }

        public async Task<bool> CrearAsync(TripulacionCreateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("TripulacionService/CrearAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idVuelo", OracleDbType.Int32).Value = dto.IdVuelo;
            cmd.Parameters.Add("idEmpleado", OracleDbType.Int32).Value = dto.IdEmpleado;
            cmd.Parameters.Add("rol", OracleDbType.Varchar2).Value = dto.Rol;
            cmd.Parameters.Add("horasVuelo", OracleDbType.Decimal).Value = (object?)dto.HorasVuelo ?? DBNull.Value;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, TripulacionUpdateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("TripulacionService/ActualizarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idVuelo", OracleDbType.Int32).Value = dto.IdVuelo;
            cmd.Parameters.Add("idEmpleado", OracleDbType.Int32).Value = dto.IdEmpleado;
            cmd.Parameters.Add("rol", OracleDbType.Varchar2).Value = dto.Rol;
            cmd.Parameters.Add("horasVuelo", OracleDbType.Decimal).Value = (object?)dto.HorasVuelo ?? DBNull.Value;
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("TripulacionService/EliminarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static TripulacionResponseDto Mapear(OracleDataReader reader)
        {
            return new TripulacionResponseDto
            {
                Id = Convert.ToInt32(reader["TRI_ID_TRIPULACION"]),
                IdVuelo = Convert.ToInt32(reader["TRI_ID_VUELO"]),
                IdEmpleado = Convert.ToInt32(reader["TRI_ID_EMPLEADO"]),
                Rol = reader["TRI_ROL"].ToString() ?? string.Empty,
                HorasVuelo = reader["TRI_HORAS_VUELO"] == DBNull.Value
                    ? null
                    : Convert.ToDecimal(reader["TRI_HORAS_VUELO"])
            };
        }
    }
}
