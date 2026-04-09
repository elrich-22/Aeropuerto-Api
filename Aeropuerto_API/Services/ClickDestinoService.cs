using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class ClickDestinoService
    {
        private readonly string _connectionString;

        public ClickDestinoService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<ClickDestinoResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<ClickDestinoResponseDto>();

            const string query = @"
                SELECT
                    CLI_ID_CLICK,
                    CLI_ID_SESION,
                    CLI_ID_AEROPUERTO_DESTINO,
                    CLI_FECHA_CLICK,
                    CLI_ORIGEN_BUSQUEDA,
                    CLI_FECHA_VIAJE_BUSCADA,
                    CLI_NUMERO_PASAJEROS,
                    CLI_CLASE_BUSCADA
                FROM AER_CLICKDESTINO
                ORDER BY CLI_ID_CLICK";

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
            const string query = @"
                SELECT
                    CLI_ID_CLICK,
                    CLI_ID_SESION,
                    CLI_ID_AEROPUERTO_DESTINO,
                    CLI_FECHA_CLICK,
                    CLI_ORIGEN_BUSQUEDA,
                    CLI_FECHA_VIAJE_BUSCADA,
                    CLI_NUMERO_PASAJEROS,
                    CLI_CLASE_BUSCADA
                FROM AER_CLICKDESTINO
                WHERE CLI_ID_CLICK = :id";

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
            const string query = @"
                INSERT INTO AER_CLICKDESTINO
                (
                    CLI_ID_SESION,
                    CLI_ID_AEROPUERTO_DESTINO,
                    CLI_FECHA_CLICK,
                    CLI_ORIGEN_BUSQUEDA,
                    CLI_FECHA_VIAJE_BUSCADA,
                    CLI_NUMERO_PASAJEROS,
                    CLI_CLASE_BUSCADA
                )
                VALUES
                (
                    :idSesion,
                    :idAeropuertoDestino,
                    :fechaClick,
                    :origenBusqueda,
                    :fechaViajeBuscada,
                    :numeroPasajeros,
                    :claseBuscada
                )";

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
            const string query = @"
                UPDATE AER_CLICKDESTINO
                SET
                    CLI_ID_SESION = :idSesion,
                    CLI_ID_AEROPUERTO_DESTINO = :idAeropuertoDestino,
                    CLI_FECHA_CLICK = :fechaClick,
                    CLI_ORIGEN_BUSQUEDA = :origenBusqueda,
                    CLI_FECHA_VIAJE_BUSCADA = :fechaViajeBuscada,
                    CLI_NUMERO_PASAJEROS = :numeroPasajeros,
                    CLI_CLASE_BUSCADA = :claseBuscada
                WHERE CLI_ID_CLICK = :id";

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
            const string query = @"
                DELETE FROM AER_CLICKDESTINO
                WHERE CLI_ID_CLICK = :id";

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
