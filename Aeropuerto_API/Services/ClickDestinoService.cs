using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class ClickDestinoService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public ClickDestinoService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<ClickDestinoResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<ClickDestinoResponseDto>();

            var query = _sqlQueryProvider.GetQuery("ClickDestinoService/ObtenerTodosAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(MapearClick(reader));
            }

            return lista;
        }

        public async Task<ClickDestinoResponseDto?> ObtenerPorIdAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("ClickDestinoService/ObtenerPorIdAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return MapearClick(reader);

            return null;
        }

        public async Task<bool> CrearAsync(ClickDestinoCreateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("ClickDestinoService/CrearAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idSesion", OracleDbType.Int32).Value = (object?)dto.IdSesion ?? DBNull.Value;
            cmd.Parameters.Add("idAeropuertoDestino", OracleDbType.Int32).Value = (object?)dto.IdAeropuertoDestino ?? DBNull.Value;
            cmd.Parameters.Add("fechaClick", OracleDbType.TimeStamp).Value = dto.FechaClick ?? DateTime.Now;
            cmd.Parameters.Add("origenBusqueda", OracleDbType.Varchar2).Value = (object?)dto.OrigenBusqueda ?? DBNull.Value;
            cmd.Parameters.Add("fechaViajeBuscada", OracleDbType.Date).Value = (object?)dto.FechaViajeBuscada ?? DBNull.Value;
            cmd.Parameters.Add("numeroPasajeros", OracleDbType.Decimal).Value = (object?)dto.NumeroPasajeros ?? DBNull.Value;
            cmd.Parameters.Add("claseBuscada", OracleDbType.Varchar2).Value = (object?)dto.ClaseBuscada ?? DBNull.Value;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, ClickDestinoUpdateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("ClickDestinoService/ActualizarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idSesion", OracleDbType.Int32).Value = (object?)dto.IdSesion ?? DBNull.Value;
            cmd.Parameters.Add("idAeropuertoDestino", OracleDbType.Int32).Value = (object?)dto.IdAeropuertoDestino ?? DBNull.Value;
            cmd.Parameters.Add("fechaClick", OracleDbType.TimeStamp).Value = dto.FechaClick ?? DateTime.Now;
            cmd.Parameters.Add("origenBusqueda", OracleDbType.Varchar2).Value = (object?)dto.OrigenBusqueda ?? DBNull.Value;
            cmd.Parameters.Add("fechaViajeBuscada", OracleDbType.Date).Value = (object?)dto.FechaViajeBuscada ?? DBNull.Value;
            cmd.Parameters.Add("numeroPasajeros", OracleDbType.Decimal).Value = (object?)dto.NumeroPasajeros ?? DBNull.Value;
            cmd.Parameters.Add("claseBuscada", OracleDbType.Varchar2).Value = (object?)dto.ClaseBuscada ?? DBNull.Value;
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("ClickDestinoService/EliminarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static ClickDestinoResponseDto MapearClick(OracleDataReader reader)
        {
            return new ClickDestinoResponseDto
            {
                IdClick = Convert.ToInt32(reader["CLI_ID_CLICK"]),
                IdSesion = reader["CLI_ID_SESION"] == DBNull.Value ? null : Convert.ToInt32(reader["CLI_ID_SESION"]),
                IdAeropuertoDestino = reader["CLI_ID_AEROPUERTO_DESTINO"] == DBNull.Value ? null : Convert.ToInt32(reader["CLI_ID_AEROPUERTO_DESTINO"]),
                FechaClick = reader["CLI_FECHA_CLICK"] == DBNull.Value ? null : Convert.ToDateTime(reader["CLI_FECHA_CLICK"]),
                OrigenBusqueda = reader["CLI_ORIGEN_BUSQUEDA"] == DBNull.Value ? null : reader["CLI_ORIGEN_BUSQUEDA"].ToString(),
                FechaViajeBuscada = reader["CLI_FECHA_VIAJE_BUSCADA"] == DBNull.Value ? null : Convert.ToDateTime(reader["CLI_FECHA_VIAJE_BUSCADA"]),
                NumeroPasajeros = reader["CLI_NUMERO_PASAJEROS"] == DBNull.Value ? null : Convert.ToDecimal(reader["CLI_NUMERO_PASAJEROS"]),
                ClaseBuscada = reader["CLI_CLASE_BUSCADA"] == DBNull.Value ? null : reader["CLI_CLASE_BUSCADA"].ToString()
            };
        }
    }
}

