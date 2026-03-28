using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class CategoriaRepuestoService
    {
        private readonly string _connectionString;

        public CategoriaRepuestoService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<CategoriaRepuestoResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<CategoriaRepuestoResponseDto>();

            const string query = @"
                SELECT
                    CAT_ID_CATEGORIA,
                    CAT_NOMBRE,
                    CAT_DESCRIPCION
                FROM AER_CATEGORIAREPUESTO
                ORDER BY CAT_ID_CATEGORIA";

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

        public async Task<CategoriaRepuestoResponseDto?> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT
                    CAT_ID_CATEGORIA,
                    CAT_NOMBRE,
                    CAT_DESCRIPCION
                FROM AER_CATEGORIAREPUESTO
                WHERE CAT_ID_CATEGORIA = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return Mapear(reader);

            return null;
        }

        public async Task<bool> CrearAsync(CategoriaRepuestoCreateDto dto)
        {
            const string query = @"
                INSERT INTO AER_CATEGORIAREPUESTO
                (
                    CAT_NOMBRE,
                    CAT_DESCRIPCION
                )
                VALUES
                (
                    :nombre,
                    :descripcion
                )";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("nombre", OracleDbType.Varchar2).Value = dto.Nombre;
            cmd.Parameters.Add("descripcion", OracleDbType.Varchar2).Value = (object?)dto.Descripcion ?? DBNull.Value;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, CategoriaRepuestoUpdateDto dto)
        {
            const string query = @"
                UPDATE AER_CATEGORIAREPUESTO
                SET
                    CAT_NOMBRE = :nombre,
                    CAT_DESCRIPCION = :descripcion
                WHERE CAT_ID_CATEGORIA = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("nombre", OracleDbType.Varchar2).Value = dto.Nombre;
            cmd.Parameters.Add("descripcion", OracleDbType.Varchar2).Value = (object?)dto.Descripcion ?? DBNull.Value;
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            const string query = @"
                DELETE FROM AER_CATEGORIAREPUESTO
                WHERE CAT_ID_CATEGORIA = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static CategoriaRepuestoResponseDto Mapear(OracleDataReader reader)
        {
            return new CategoriaRepuestoResponseDto
            {
                Id = Convert.ToInt32(reader["CAT_ID_CATEGORIA"]),
                Nombre = reader["CAT_NOMBRE"].ToString() ?? string.Empty,
                Descripcion = reader["CAT_DESCRIPCION"] == DBNull.Value ? null : reader["CAT_DESCRIPCION"].ToString()
            };
        }
    }
}