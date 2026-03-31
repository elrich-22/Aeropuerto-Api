using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class TripulacionService
    {
        private readonly string _connectionString;

        public TripulacionService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<TripulacionResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<TripulacionResponseDto>();

            const string query = @"
                SELECT
                    TRI_ID_TRIPULACION,
                    TRI_ID_VUELO,
                    TRI_ID_EMPLEADO,
                    TRI_ROL,
                    TRI_HORAS_VUELO
                FROM AER_TRIPULACION
                ORDER BY TRI_ID_TRIPULACION";

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
            const string query = @"
                SELECT
                    TRI_ID_TRIPULACION,
                    TRI_ID_VUELO,
                    TRI_ID_EMPLEADO,
                    TRI_ROL,
                    TRI_HORAS_VUELO
                FROM AER_TRIPULACION
                WHERE TRI_ID_TRIPULACION = :id";

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
            const string query = @"
                INSERT INTO AER_TRIPULACION
                (
                    TRI_ID_VUELO,
                    TRI_ID_EMPLEADO,
                    TRI_ROL,
                    TRI_HORAS_VUELO
                )
                VALUES
                (
                    :idVuelo,
                    :idEmpleado,
                    :rol,
                    :horasVuelo
                )";

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
            const string query = @"
                UPDATE AER_TRIPULACION
                SET
                    TRI_ID_VUELO = :idVuelo,
                    TRI_ID_EMPLEADO = :idEmpleado,
                    TRI_ROL = :rol,
                    TRI_HORAS_VUELO = :horasVuelo
                WHERE TRI_ID_TRIPULACION = :id";

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
            const string query = @"
                DELETE FROM AER_TRIPULACION
                WHERE TRI_ID_TRIPULACION = :id";

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