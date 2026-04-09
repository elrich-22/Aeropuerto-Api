using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class ArrestoService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public ArrestoService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<ArrestoResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<ArrestoResponseDto>();

            var query = _sqlQueryProvider.GetQuery("ArrestoService/ObtenerTodosAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(MapearArresto(reader));
            }

            return lista;
        }

        public async Task<ArrestoResponseDto?> ObtenerPorIdAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("ArrestoService/ObtenerPorIdAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return MapearArresto(reader);

            return null;
        }

        public async Task<bool> CrearAsync(ArrestoCreateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("ArrestoService/CrearAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idPasajero", OracleDbType.Int32).Value = (object?)dto.IdPasajero ?? DBNull.Value;
            cmd.Parameters.Add("idVuelo", OracleDbType.Int32).Value = (object?)dto.IdVuelo ?? DBNull.Value;
            cmd.Parameters.Add("idAeropuerto", OracleDbType.Int32).Value = (object?)dto.IdAeropuerto ?? DBNull.Value;
            cmd.Parameters.Add("fechaHoraArresto", OracleDbType.TimeStamp).Value = dto.FechaHoraArresto ?? DateTime.Now;
            cmd.Parameters.Add("motivo", OracleDbType.Varchar2).Value = (object?)dto.Motivo ?? DBNull.Value;
            cmd.Parameters.Add("autoridadCargo", OracleDbType.Varchar2).Value = (object?)dto.AutoridadCargo ?? DBNull.Value;
            cmd.Parameters.Add("descripcionIncidente", OracleDbType.Clob).Value = (object?)dto.DescripcionIncidente ?? DBNull.Value;
            cmd.Parameters.Add("ubicacionArresto", OracleDbType.Varchar2).Value = (object?)dto.UbicacionArresto ?? DBNull.Value;
            cmd.Parameters.Add("estadoCaso", OracleDbType.Varchar2).Value = dto.EstadoCaso ?? "EN_PROCESO";
            cmd.Parameters.Add("numeroExpediente", OracleDbType.Varchar2).Value = (object?)dto.NumeroExpediente ?? DBNull.Value;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, ArrestoUpdateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("ArrestoService/ActualizarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idPasajero", OracleDbType.Int32).Value = (object?)dto.IdPasajero ?? DBNull.Value;
            cmd.Parameters.Add("idVuelo", OracleDbType.Int32).Value = (object?)dto.IdVuelo ?? DBNull.Value;
            cmd.Parameters.Add("idAeropuerto", OracleDbType.Int32).Value = (object?)dto.IdAeropuerto ?? DBNull.Value;
            cmd.Parameters.Add("fechaHoraArresto", OracleDbType.TimeStamp).Value = dto.FechaHoraArresto ?? DateTime.Now;
            cmd.Parameters.Add("motivo", OracleDbType.Varchar2).Value = (object?)dto.Motivo ?? DBNull.Value;
            cmd.Parameters.Add("autoridadCargo", OracleDbType.Varchar2).Value = (object?)dto.AutoridadCargo ?? DBNull.Value;
            cmd.Parameters.Add("descripcionIncidente", OracleDbType.Clob).Value = (object?)dto.DescripcionIncidente ?? DBNull.Value;
            cmd.Parameters.Add("ubicacionArresto", OracleDbType.Varchar2).Value = (object?)dto.UbicacionArresto ?? DBNull.Value;
            cmd.Parameters.Add("estadoCaso", OracleDbType.Varchar2).Value = dto.EstadoCaso ?? "EN_PROCESO";
            cmd.Parameters.Add("numeroExpediente", OracleDbType.Varchar2).Value = (object?)dto.NumeroExpediente ?? DBNull.Value;
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("ArrestoService/EliminarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static ArrestoResponseDto MapearArresto(OracleDataReader reader)
        {
            return new ArrestoResponseDto
            {
                IdArresto = Convert.ToInt32(reader["ARR_ID_ARRESTO"]),
                IdPasajero = reader["ARR_ID_PASAJERO"] == DBNull.Value ? null : Convert.ToInt32(reader["ARR_ID_PASAJERO"]),
                IdVuelo = reader["ARR_ID_VUELO"] == DBNull.Value ? null : Convert.ToInt32(reader["ARR_ID_VUELO"]),
                IdAeropuerto = reader["ARR_ID_AEROPUERTO"] == DBNull.Value ? null : Convert.ToInt32(reader["ARR_ID_AEROPUERTO"]),
                FechaHoraArresto = reader["ARR_FECHA_HORA_ARRESTO"] == DBNull.Value ? null : Convert.ToDateTime(reader["ARR_FECHA_HORA_ARRESTO"]),
                Motivo = reader["ARR_MOTIVO"] == DBNull.Value ? null : reader["ARR_MOTIVO"].ToString(),
                AutoridadCargo = reader["ARR_AUTORIDAD_CARGO"] == DBNull.Value ? null : reader["ARR_AUTORIDAD_CARGO"].ToString(),
                DescripcionIncidente = reader["ARR_DESCRIPCION_INCIDENTE"] == DBNull.Value ? null : reader["ARR_DESCRIPCION_INCIDENTE"].ToString(),
                UbicacionArresto = reader["ARR_UBICACION_ARRESTO"] == DBNull.Value ? null : reader["ARR_UBICACION_ARRESTO"].ToString(),
                EstadoCaso = reader["ARR_ESTADO_CASO"] == DBNull.Value ? null : reader["ARR_ESTADO_CASO"].ToString(),
                NumeroExpediente = reader["ARR_NUMERO_EXPEDIENTE"] == DBNull.Value ? null : reader["ARR_NUMERO_EXPEDIENTE"].ToString()
            };
        }
    }
}

