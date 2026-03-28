using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class DepartamentoService
    {
        private readonly string _connectionString;

        public DepartamentoService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<DepartamentoResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<DepartamentoResponseDto>();

            const string query = @"
                SELECT
                    DEP_ID_DEPARTAMENTO,
                    DEP_NOMBRE,
                    DEP_DESCRIPCION,
                    DEP_ID_AEROPUERTO,
                    DEP_ESTADO
                FROM AER_DEPARTAMENTO
                ORDER BY DEP_ID_DEPARTAMENTO";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(MapearDepartamento(reader));
            }

            return lista;
        }

        public async Task<DepartamentoResponseDto?> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT
                    DEP_ID_DEPARTAMENTO,
                    DEP_NOMBRE,
                    DEP_DESCRIPCION,
                    DEP_ID_AEROPUERTO,
                    DEP_ESTADO
                FROM AER_DEPARTAMENTO
                WHERE DEP_ID_DEPARTAMENTO = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return MapearDepartamento(reader);

            return null;
        }

        public async Task<bool> CrearAsync(DepartamentoCreateDto dto)
        {
            const string query = @"
                INSERT INTO AER_DEPARTAMENTO
                (
                    DEP_NOMBRE,
                    DEP_DESCRIPCION,
                    DEP_ID_AEROPUERTO,
                    DEP_ESTADO
                )
                VALUES
                (
                    :nombre,
                    :descripcion,
                    :idAeropuerto,
                    :estado
                )";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("nombre", OracleDbType.Varchar2).Value = dto.Nombre;
            cmd.Parameters.Add("descripcion", OracleDbType.Varchar2).Value = (object?)dto.Descripcion ?? DBNull.Value;
            cmd.Parameters.Add("idAeropuerto", OracleDbType.Int32).Value = dto.IdAeropuerto;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "ACTIVO";

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, DepartamentoUpdateDto dto)
        {
            const string query = @"
                UPDATE AER_DEPARTAMENTO
                SET
                    DEP_NOMBRE = :nombre,
                    DEP_DESCRIPCION = :descripcion,
                    DEP_ID_AEROPUERTO = :idAeropuerto,
                    DEP_ESTADO = :estado
                WHERE DEP_ID_DEPARTAMENTO = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("nombre", OracleDbType.Varchar2).Value = dto.Nombre;
            cmd.Parameters.Add("descripcion", OracleDbType.Varchar2).Value = (object?)dto.Descripcion ?? DBNull.Value;
            cmd.Parameters.Add("idAeropuerto", OracleDbType.Int32).Value = dto.IdAeropuerto;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "ACTIVO";
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            const string query = @"
                DELETE FROM AER_DEPARTAMENTO
                WHERE DEP_ID_DEPARTAMENTO = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static DepartamentoResponseDto MapearDepartamento(OracleDataReader reader)
        {
            return new DepartamentoResponseDto
            {
                Id = Convert.ToInt32(reader["DEP_ID_DEPARTAMENTO"]),
                Nombre = reader["DEP_NOMBRE"].ToString() ?? string.Empty,
                Descripcion = reader["DEP_DESCRIPCION"] == DBNull.Value ? null : reader["DEP_DESCRIPCION"].ToString(),
                IdAeropuerto = Convert.ToInt32(reader["DEP_ID_AEROPUERTO"]),
                Estado = reader["DEP_ESTADO"] == DBNull.Value ? string.Empty : reader["DEP_ESTADO"].ToString() ?? string.Empty
            };
        }
    }
}