using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class BusquedaVueloService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public BusquedaVueloService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<BusquedaVueloResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<BusquedaVueloResponseDto>();

            var query = _sqlQueryProvider.GetQuery("BusquedaVueloService/ObtenerTodosAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(MapearBusqueda(reader));
            }

            return lista;
        }

        public async Task<BusquedaVueloResponseDto?> ObtenerPorIdAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("BusquedaVueloService/ObtenerPorIdAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return MapearBusqueda(reader);

            return null;
        }

        public async Task<bool> CrearAsync(BusquedaVueloCreateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("BusquedaVueloService/CrearAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idSesion", OracleDbType.Int32).Value = (object?)dto.IdSesion ?? DBNull.Value;
            cmd.Parameters.Add("idAeropuertoOrigen", OracleDbType.Int32).Value = (object?)dto.IdAeropuertoOrigen ?? DBNull.Value;
            cmd.Parameters.Add("idAeropuertoDestino", OracleDbType.Int32).Value = (object?)dto.IdAeropuertoDestino ?? DBNull.Value;
            cmd.Parameters.Add("fechaIda", OracleDbType.Date).Value = (object?)dto.FechaIda ?? DBNull.Value;
            cmd.Parameters.Add("fechaVuelta", OracleDbType.Date).Value = (object?)dto.FechaVuelta ?? DBNull.Value;
            cmd.Parameters.Add("numeroPasajeros", OracleDbType.Decimal).Value = (object?)dto.NumeroPasajeros ?? 1m;
            cmd.Parameters.Add("clase", OracleDbType.Varchar2).Value = (object?)dto.Clase ?? DBNull.Value;
            cmd.Parameters.Add("fechaBusqueda", OracleDbType.TimeStamp).Value = dto.FechaBusqueda ?? DateTime.Now;
            cmd.Parameters.Add("seConvirtioCompra", OracleDbType.Char).Value = dto.SeConvirtioCompra ?? "N";

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, BusquedaVueloUpdateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("BusquedaVueloService/ActualizarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idSesion", OracleDbType.Int32).Value = (object?)dto.IdSesion ?? DBNull.Value;
            cmd.Parameters.Add("idAeropuertoOrigen", OracleDbType.Int32).Value = (object?)dto.IdAeropuertoOrigen ?? DBNull.Value;
            cmd.Parameters.Add("idAeropuertoDestino", OracleDbType.Int32).Value = (object?)dto.IdAeropuertoDestino ?? DBNull.Value;
            cmd.Parameters.Add("fechaIda", OracleDbType.Date).Value = (object?)dto.FechaIda ?? DBNull.Value;
            cmd.Parameters.Add("fechaVuelta", OracleDbType.Date).Value = (object?)dto.FechaVuelta ?? DBNull.Value;
            cmd.Parameters.Add("numeroPasajeros", OracleDbType.Decimal).Value = (object?)dto.NumeroPasajeros ?? 1m;
            cmd.Parameters.Add("clase", OracleDbType.Varchar2).Value = (object?)dto.Clase ?? DBNull.Value;
            cmd.Parameters.Add("fechaBusqueda", OracleDbType.TimeStamp).Value = dto.FechaBusqueda ?? DateTime.Now;
            cmd.Parameters.Add("seConvirtioCompra", OracleDbType.Char).Value = dto.SeConvirtioCompra ?? "N";
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("BusquedaVueloService/EliminarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static BusquedaVueloResponseDto MapearBusqueda(OracleDataReader reader)
        {
            return new BusquedaVueloResponseDto
            {
                IdBusqueda = Convert.ToInt32(reader["BUS_ID_BUSQUEDA"]),
                IdSesion = reader["BUS_ID_SESION"] == DBNull.Value ? null : Convert.ToInt32(reader["BUS_ID_SESION"]),
                IdAeropuertoOrigen = reader["BUS_ID_AEROPUERTO_ORIGEN"] == DBNull.Value ? null : Convert.ToInt32(reader["BUS_ID_AEROPUERTO_ORIGEN"]),
                IdAeropuertoDestino = reader["BUS_ID_AEROPUERTO_DESTINO"] == DBNull.Value ? null : Convert.ToInt32(reader["BUS_ID_AEROPUERTO_DESTINO"]),
                FechaIda = reader["BUS_FECHA_IDA"] == DBNull.Value ? null : Convert.ToDateTime(reader["BUS_FECHA_IDA"]),
                FechaVuelta = reader["BUS_FECHA_VUELTA"] == DBNull.Value ? null : Convert.ToDateTime(reader["BUS_FECHA_VUELTA"]),
                NumeroPasajeros = reader["BUS_NUMERO_PASAJEROS"] == DBNull.Value ? null : Convert.ToDecimal(reader["BUS_NUMERO_PASAJEROS"]),
                Clase = reader["BUS_CLASE"] == DBNull.Value ? null : reader["BUS_CLASE"].ToString(),
                FechaBusqueda = reader["BUS_FECHA_BUSQUEDA"] == DBNull.Value ? null : Convert.ToDateTime(reader["BUS_FECHA_BUSQUEDA"]),
                SeConvirtioCompra = reader["BUS_SE_CONVIRTIO_COMPRA"] == DBNull.Value ? null : reader["BUS_SE_CONVIRTIO_COMPRA"].ToString()
            };
        }
    }
}

