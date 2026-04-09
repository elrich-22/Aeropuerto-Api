using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class HangarService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public HangarService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<HangarResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<HangarResponseDto>();

            var query = _sqlQueryProvider.GetQuery("HangarService/ObtenerTodosAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(MapearHangar(reader));
            }

            return lista;
        }

        public async Task<HangarResponseDto?> ObtenerPorIdAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("HangarService/ObtenerPorIdAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return MapearHangar(reader);

            return null;
        }

        public async Task<bool> CrearAsync(HangarCreateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("HangarService/CrearAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("codigoHangar", OracleDbType.Varchar2).Value = dto.CodigoHangar;
            cmd.Parameters.Add("nombre", OracleDbType.Varchar2).Value = dto.Nombre;
            cmd.Parameters.Add("idAeropuerto", OracleDbType.Int32).Value = dto.IdAeropuerto;
            cmd.Parameters.Add("capacidadAviones", OracleDbType.Int32).Value = (object?)dto.CapacidadAviones ?? DBNull.Value;
            cmd.Parameters.Add("areaM2", OracleDbType.Decimal).Value = (object?)dto.AreaM2 ?? DBNull.Value;
            cmd.Parameters.Add("alturaMaxima", OracleDbType.Decimal).Value = (object?)dto.AlturaMaxima ?? DBNull.Value;
            cmd.Parameters.Add("tipo", OracleDbType.Varchar2).Value = (object?)dto.Tipo ?? DBNull.Value;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "ACTIVO";

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, HangarUpdateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("HangarService/ActualizarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("codigoHangar", OracleDbType.Varchar2).Value = dto.CodigoHangar;
            cmd.Parameters.Add("nombre", OracleDbType.Varchar2).Value = dto.Nombre;
            cmd.Parameters.Add("idAeropuerto", OracleDbType.Int32).Value = dto.IdAeropuerto;
            cmd.Parameters.Add("capacidadAviones", OracleDbType.Int32).Value = (object?)dto.CapacidadAviones ?? DBNull.Value;
            cmd.Parameters.Add("areaM2", OracleDbType.Decimal).Value = (object?)dto.AreaM2 ?? DBNull.Value;
            cmd.Parameters.Add("alturaMaxima", OracleDbType.Decimal).Value = (object?)dto.AlturaMaxima ?? DBNull.Value;
            cmd.Parameters.Add("tipo", OracleDbType.Varchar2).Value = (object?)dto.Tipo ?? DBNull.Value;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "ACTIVO";
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("HangarService/EliminarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static HangarResponseDto MapearHangar(OracleDataReader reader)
        {
            return new HangarResponseDto
            {
                Id = Convert.ToInt32(reader["HAN_ID_HANGAR"]),
                CodigoHangar = reader["HAN_CODIGO_HANGAR"].ToString() ?? string.Empty,
                Nombre = reader["HAN_NOMBRE"].ToString() ?? string.Empty,
                IdAeropuerto = Convert.ToInt32(reader["HAN_ID_AEROPUERTO"]),
                CapacidadAviones = reader["HAN_CAPACIDAD_AVIONES"] == DBNull.Value ? null : Convert.ToInt32(reader["HAN_CAPACIDAD_AVIONES"]),
                AreaM2 = reader["HAN_AREA_M2"] == DBNull.Value ? null : Convert.ToDecimal(reader["HAN_AREA_M2"]),
                AlturaMaxima = reader["HAN_ALTURA_MAXIMA"] == DBNull.Value ? null : Convert.ToDecimal(reader["HAN_ALTURA_MAXIMA"]),
                Tipo = reader["HAN_TIPO"] == DBNull.Value ? null : reader["HAN_TIPO"].ToString(),
                Estado = reader["HAN_ESTADO"] == DBNull.Value ? string.Empty : reader["HAN_ESTADO"].ToString() ?? string.Empty
            };
        }
    }
}
