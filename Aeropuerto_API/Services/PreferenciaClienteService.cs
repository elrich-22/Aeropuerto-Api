using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class PreferenciaClienteService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public PreferenciaClienteService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<PreferenciaClienteResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<PreferenciaClienteResponseDto>();

            var query = _sqlQueryProvider.GetQuery("PreferenciaClienteService/ObtenerTodosAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(MapearPreferencia(reader));
            }

            return lista;
        }

        public async Task<PreferenciaClienteResponseDto?> ObtenerPorIdAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("PreferenciaClienteService/ObtenerPorIdAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return MapearPreferencia(reader);

            return null;
        }

        public async Task<bool> CrearAsync(PreferenciaClienteCreateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("PreferenciaClienteService/CrearAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idPasajero", OracleDbType.Int32).Value = (object?)dto.IdPasajero ?? DBNull.Value;
            cmd.Parameters.Add("tipoPreferencia", OracleDbType.Varchar2).Value = (object?)dto.TipoPreferencia ?? DBNull.Value;
            cmd.Parameters.Add("valorPreferencia", OracleDbType.Varchar2).Value = (object?)dto.ValorPreferencia ?? DBNull.Value;
            cmd.Parameters.Add("fechaRegistro", OracleDbType.TimeStamp).Value = dto.FechaRegistro ?? DateTime.Now;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, PreferenciaClienteUpdateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("PreferenciaClienteService/ActualizarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idPasajero", OracleDbType.Int32).Value = (object?)dto.IdPasajero ?? DBNull.Value;
            cmd.Parameters.Add("tipoPreferencia", OracleDbType.Varchar2).Value = (object?)dto.TipoPreferencia ?? DBNull.Value;
            cmd.Parameters.Add("valorPreferencia", OracleDbType.Varchar2).Value = (object?)dto.ValorPreferencia ?? DBNull.Value;
            cmd.Parameters.Add("fechaRegistro", OracleDbType.TimeStamp).Value = dto.FechaRegistro ?? DateTime.Now;
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("PreferenciaClienteService/EliminarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static PreferenciaClienteResponseDto MapearPreferencia(OracleDataReader reader)
        {
            return new PreferenciaClienteResponseDto
            {
                IdPreferencia = Convert.ToInt32(reader["PRF_ID_PREFERENCIA"]),
                IdPasajero = reader["PRF_ID_PASAJERO"] == DBNull.Value ? null : Convert.ToInt32(reader["PRF_ID_PASAJERO"]),
                TipoPreferencia = reader["PRF_TIPO_PREFERENCIA"] == DBNull.Value ? null : reader["PRF_TIPO_PREFERENCIA"].ToString(),
                ValorPreferencia = reader["PRF_VALOR_PREFERENCIA"] == DBNull.Value ? null : reader["PRF_VALOR_PREFERENCIA"].ToString(),
                FechaRegistro = reader["PRF_FECHA_REGISTRO"] == DBNull.Value ? null : Convert.ToDateTime(reader["PRF_FECHA_REGISTRO"])
            };
        }
    }
}

