using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class PuestoService
    {
        private readonly string _connectionString;

        public PuestoService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<PuestoResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<PuestoResponseDto>();

            const string query = @"
                SELECT
                    PUE_ID_PUESTO,
                    PUE_NOMBRE,
                    PUE_ID_DEPARTAMENTO,
                    PUE_DESCRIPCION,
                    PUE_SALARIO_MINIMO,
                    PUE_SALARIO_MAXIMO,
                    PUE_REQUIERE_LICENCIA
                FROM AER_PUESTO
                ORDER BY PUE_ID_PUESTO";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(MapearPuesto(reader));
            }

            return lista;
        }

        public async Task<PuestoResponseDto?> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT
                    PUE_ID_PUESTO,
                    PUE_NOMBRE,
                    PUE_ID_DEPARTAMENTO,
                    PUE_DESCRIPCION,
                    PUE_SALARIO_MINIMO,
                    PUE_SALARIO_MAXIMO,
                    PUE_REQUIERE_LICENCIA
                FROM AER_PUESTO
                WHERE PUE_ID_PUESTO = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return MapearPuesto(reader);

            return null;
        }

        public async Task<bool> CrearAsync(PuestoCreateDto dto)
        {
            const string query = @"
                INSERT INTO AER_PUESTO
                (
                    PUE_NOMBRE,
                    PUE_ID_DEPARTAMENTO,
                    PUE_DESCRIPCION,
                    PUE_SALARIO_MINIMO,
                    PUE_SALARIO_MAXIMO,
                    PUE_REQUIERE_LICENCIA
                )
                VALUES
                (
                    :nombre,
                    :idDepartamento,
                    :descripcion,
                    :salarioMinimo,
                    :salarioMaximo,
                    :requiereLicencia
                )";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("nombre", OracleDbType.Varchar2).Value = dto.Nombre;
            cmd.Parameters.Add("idDepartamento", OracleDbType.Int32).Value = dto.IdDepartamento;
            cmd.Parameters.Add("descripcion", OracleDbType.Varchar2).Value = (object?)dto.Descripcion ?? DBNull.Value;
            cmd.Parameters.Add("salarioMinimo", OracleDbType.Decimal).Value = (object?)dto.SalarioMinimo ?? DBNull.Value;
            cmd.Parameters.Add("salarioMaximo", OracleDbType.Decimal).Value = (object?)dto.SalarioMaximo ?? DBNull.Value;
            cmd.Parameters.Add("requiereLicencia", OracleDbType.Varchar2).Value = (object?)dto.RequiereLicencia ?? DBNull.Value;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, PuestoUpdateDto dto)
        {
            const string query = @"
                UPDATE AER_PUESTO
                SET
                    PUE_NOMBRE = :nombre,
                    PUE_ID_DEPARTAMENTO = :idDepartamento,
                    PUE_DESCRIPCION = :descripcion,
                    PUE_SALARIO_MINIMO = :salarioMinimo,
                    PUE_SALARIO_MAXIMO = :salarioMaximo,
                    PUE_REQUIERE_LICENCIA = :requiereLicencia
                WHERE PUE_ID_PUESTO = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("nombre", OracleDbType.Varchar2).Value = dto.Nombre;
            cmd.Parameters.Add("idDepartamento", OracleDbType.Int32).Value = dto.IdDepartamento;
            cmd.Parameters.Add("descripcion", OracleDbType.Varchar2).Value = (object?)dto.Descripcion ?? DBNull.Value;
            cmd.Parameters.Add("salarioMinimo", OracleDbType.Decimal).Value = (object?)dto.SalarioMinimo ?? DBNull.Value;
            cmd.Parameters.Add("salarioMaximo", OracleDbType.Decimal).Value = (object?)dto.SalarioMaximo ?? DBNull.Value;
            cmd.Parameters.Add("requiereLicencia", OracleDbType.Varchar2).Value = (object?)dto.RequiereLicencia ?? DBNull.Value;
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            const string query = @"
                DELETE FROM AER_PUESTO
                WHERE PUE_ID_PUESTO = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static PuestoResponseDto MapearPuesto(OracleDataReader reader)
        {
            return new PuestoResponseDto
            {
                Id = Convert.ToInt32(reader["PUE_ID_PUESTO"]),
                Nombre = reader["PUE_NOMBRE"].ToString() ?? string.Empty,
                IdDepartamento = Convert.ToInt32(reader["PUE_ID_DEPARTAMENTO"]),
                Descripcion = reader["PUE_DESCRIPCION"] == DBNull.Value ? null : reader["PUE_DESCRIPCION"].ToString(),
                SalarioMinimo = reader["PUE_SALARIO_MINIMO"] == DBNull.Value ? null : Convert.ToDecimal(reader["PUE_SALARIO_MINIMO"]),
                SalarioMaximo = reader["PUE_SALARIO_MAXIMO"] == DBNull.Value ? null : Convert.ToDecimal(reader["PUE_SALARIO_MAXIMO"]),
                RequiereLicencia = reader["PUE_REQUIERE_LICENCIA"] == DBNull.Value ? null : reader["PUE_REQUIERE_LICENCIA"].ToString()
            };
        }
    }
}