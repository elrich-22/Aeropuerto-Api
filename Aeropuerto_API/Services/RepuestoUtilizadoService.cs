using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class RepuestoUtilizadoService
    {
        private readonly string _connectionString;

        public RepuestoUtilizadoService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<RepuestoUtilizadoResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<RepuestoUtilizadoResponseDto>();

            const string query = @"
                SELECT
                    RUT_ID_REPUESTO_UTILIZADO,
                    RUT_ID_MANTENIMIENTO,
                    RUT_ID_REPUESTO,
                    RUT_CANTIDAD
                FROM AER_REPUESTOUTILIZADO
                ORDER BY RUT_ID_REPUESTO_UTILIZADO";

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
            const string query = @"
                SELECT
                    RUT_ID_REPUESTO_UTILIZADO,
                    RUT_ID_MANTENIMIENTO,
                    RUT_ID_REPUESTO,
                    RUT_CANTIDAD
                FROM AER_REPUESTOUTILIZADO
                WHERE RUT_ID_REPUESTO_UTILIZADO = :id";

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
            const string query = @"
                INSERT INTO AER_REPUESTOUTILIZADO
                (
                    RUT_ID_MANTENIMIENTO,
                    RUT_ID_REPUESTO,
                    RUT_CANTIDAD
                )
                VALUES
                (
                    :idMantenimiento,
                    :idRepuesto,
                    :cantidad
                )";

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
            const string query = @"
                UPDATE AER_REPUESTOUTILIZADO
                SET
                    RUT_ID_MANTENIMIENTO = :idMantenimiento,
                    RUT_ID_REPUESTO = :idRepuesto,
                    RUT_CANTIDAD = :cantidad
                WHERE RUT_ID_REPUESTO_UTILIZADO = :id";

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
            const string query = @"
                DELETE FROM AER_REPUESTOUTILIZADO
                WHERE RUT_ID_REPUESTO_UTILIZADO = :id";

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