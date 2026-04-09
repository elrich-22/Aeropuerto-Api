using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class DiasVueloService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public DiasVueloService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<DiasVueloResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<DiasVueloResponseDto>();

            var query = _sqlQueryProvider.GetQuery("DiasVueloService/ObtenerTodosAsync.sql");

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

        public async Task<DiasVueloResponseDto?> ObtenerPorIdAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("DiasVueloService/ObtenerPorIdAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return Mapear(reader);

            return null;
        }

        public async Task<bool> CrearAsync(DiasVueloCreateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("DiasVueloService/CrearAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idProgramaVuelo", OracleDbType.Int32).Value = dto.IdProgramaVuelo;
            cmd.Parameters.Add("diaSemana", OracleDbType.Int32).Value = dto.DiaSemana;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, DiasVueloUpdateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("DiasVueloService/ActualizarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idProgramaVuelo", OracleDbType.Int32).Value = dto.IdProgramaVuelo;
            cmd.Parameters.Add("diaSemana", OracleDbType.Int32).Value = dto.DiaSemana;
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("DiasVueloService/EliminarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static DiasVueloResponseDto Mapear(OracleDataReader reader)
        {
            return new DiasVueloResponseDto
            {
                Id = Convert.ToInt32(reader["DIA_ID_DIA_VUELO"]),
                IdProgramaVuelo = Convert.ToInt32(reader["DIA_ID_PROGRAMA_VUELO"]),
                DiaSemana = Convert.ToInt32(reader["DIA_DIA_SEMANA"])
            };
        }
    }
}
