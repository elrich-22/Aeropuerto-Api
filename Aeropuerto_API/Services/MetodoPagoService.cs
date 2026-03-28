using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class MetodoPagoService
    {
        private readonly string _connectionString;

        public MetodoPagoService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<MetodoPagoResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<MetodoPagoResponseDto>();

            const string query = @"
                SELECT
                    MET_ID_METODO_PAGO,
                    MET_NOMBRE,
                    MET_TIPO,
                    MET_ESTADO,
                    MET_COMISION_PORCENTAJE
                FROM AER_METODOPAGO
                ORDER BY MET_ID_METODO_PAGO";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new MetodoPagoResponseDto
                {
                    Id = Convert.ToInt32(reader["MET_ID_METODO_PAGO"]),
                    Nombre = reader["MET_NOMBRE"].ToString() ?? string.Empty,
                    Tipo = reader["MET_TIPO"].ToString() ?? string.Empty,
                    Estado = reader["MET_ESTADO"] == DBNull.Value ? string.Empty : reader["MET_ESTADO"].ToString() ?? string.Empty,
                    ComisionPorcentaje = reader["MET_COMISION_PORCENTAJE"] == DBNull.Value
                        ? null
                        : Convert.ToDecimal(reader["MET_COMISION_PORCENTAJE"])
                });
            }

            return lista;
        }

        public async Task<MetodoPagoResponseDto?> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT
                    MET_ID_METODO_PAGO,
                    MET_NOMBRE,
                    MET_TIPO,
                    MET_ESTADO,
                    MET_COMISION_PORCENTAJE
                FROM AER_METODOPAGO
                WHERE MET_ID_METODO_PAGO = :id";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new MetodoPagoResponseDto
                {
                    Id = Convert.ToInt32(reader["MET_ID_METODO_PAGO"]),
                    Nombre = reader["MET_NOMBRE"].ToString() ?? string.Empty,
                    Tipo = reader["MET_TIPO"].ToString() ?? string.Empty,
                    Estado = reader["MET_ESTADO"] == DBNull.Value ? string.Empty : reader["MET_ESTADO"].ToString() ?? string.Empty,
                    ComisionPorcentaje = reader["MET_COMISION_PORCENTAJE"] == DBNull.Value
                        ? null
                        : Convert.ToDecimal(reader["MET_COMISION_PORCENTAJE"])
                };
            }

            return null;
        }

        public async Task<bool> CrearAsync(MetodoPagoCreateDto dto)
        {
            const string query = @"
                INSERT INTO AER_METODOPAGO
                (
                    MET_NOMBRE,
                    MET_TIPO,
                    MET_ESTADO,
                    MET_COMISION_PORCENTAJE
                )
                VALUES
                (
                    :nombre,
                    :tipo,
                    :estado,
                    :comisionPorcentaje
                )";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("nombre", OracleDbType.Varchar2).Value = dto.Nombre;
            command.Parameters.Add("tipo", OracleDbType.Varchar2).Value = dto.Tipo;
            command.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "ACTIVO";
            command.Parameters.Add("comisionPorcentaje", OracleDbType.Decimal).Value = (object?)dto.ComisionPorcentaje ?? DBNull.Value;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> ActualizarAsync(int id, MetodoPagoUpdateDto dto)
        {
            const string query = @"
                UPDATE AER_METODOPAGO
                SET
                    MET_NOMBRE = :nombre,
                    MET_TIPO = :tipo,
                    MET_ESTADO = :estado,
                    MET_COMISION_PORCENTAJE = :comisionPorcentaje
                WHERE MET_ID_METODO_PAGO = :id";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("nombre", OracleDbType.Varchar2).Value = dto.Nombre;
            command.Parameters.Add("tipo", OracleDbType.Varchar2).Value = dto.Tipo;
            command.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "ACTIVO";
            command.Parameters.Add("comisionPorcentaje", OracleDbType.Decimal).Value = (object?)dto.ComisionPorcentaje ?? DBNull.Value;
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            const string query = @"
                DELETE FROM AER_METODOPAGO
                WHERE MET_ID_METODO_PAGO = :id";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}