using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class MetodoPagoService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public MetodoPagoService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<MetodoPagoResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<MetodoPagoResponseDto>();

            var query = _sqlQueryProvider.GetQuery("MetodoPagoService/ObtenerTodosAsync.sql");

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
            var query = _sqlQueryProvider.GetQuery("MetodoPagoService/ObtenerPorIdAsync.sql");

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
            var query = _sqlQueryProvider.GetQuery("MetodoPagoService/CrearAsync.sql");

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
            var query = _sqlQueryProvider.GetQuery("MetodoPagoService/ActualizarAsync.sql");

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
            var query = _sqlQueryProvider.GetQuery("MetodoPagoService/EliminarAsync.sql");

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}
