using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class DiasVueloService
    {
        private readonly string _connectionString;

        public DiasVueloService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<DiasVueloResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<DiasVueloResponseDto>();

            const string query = @"
                SELECT
                    DIA_ID_DIA_VUELO,
                    DIA_ID_PROGRAMA_VUELO,
                    DIA_DIA_SEMANA
                FROM AER_DIASVUELO
                ORDER BY DIA_ID_DIA_VUELO";

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
            const string query = @"
                SELECT
                    DIA_ID_DIA_VUELO,
                    DIA_ID_PROGRAMA_VUELO,
                    DIA_DIA_SEMANA
                FROM AER_DIASVUELO
                WHERE DIA_ID_DIA_VUELO = :id";

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
            const string query = @"
                INSERT INTO AER_DIASVUELO
                (
                    DIA_ID_PROGRAMA_VUELO,
                    DIA_DIA_SEMANA
                )
                VALUES
                (
                    :idProgramaVuelo,
                    :diaSemana
                )";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idProgramaVuelo", OracleDbType.Int32).Value = dto.IdProgramaVuelo;
            cmd.Parameters.Add("diaSemana", OracleDbType.Int32).Value = dto.DiaSemana;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, DiasVueloUpdateDto dto)
        {
            const string query = @"
                UPDATE AER_DIASVUELO
                SET
                    DIA_ID_PROGRAMA_VUELO = :idProgramaVuelo,
                    DIA_DIA_SEMANA = :diaSemana
                WHERE DIA_ID_DIA_VUELO = :id";

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
            const string query = @"
                DELETE FROM AER_DIASVUELO
                WHERE DIA_ID_DIA_VUELO = :id";

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