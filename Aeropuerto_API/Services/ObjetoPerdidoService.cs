using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class ObjetoPerdidoService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public ObjetoPerdidoService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<ObjetoPerdidoResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<ObjetoPerdidoResponseDto>();

            var query = _sqlQueryProvider.GetQuery("ObjetoPerdidoService/ObtenerTodosAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(MapearObjeto(reader));
            }

            return lista;
        }

        public async Task<ObjetoPerdidoResponseDto?> ObtenerPorIdAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("ObjetoPerdidoService/ObtenerPorIdAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return MapearObjeto(reader);

            return null;
        }

        public async Task<bool> CrearAsync(ObjetoPerdidoCreateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("ObjetoPerdidoService/CrearAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idVuelo", OracleDbType.Int32).Value = (object?)dto.IdVuelo ?? DBNull.Value;
            cmd.Parameters.Add("idAeropuerto", OracleDbType.Int32).Value = (object?)dto.IdAeropuerto ?? DBNull.Value;
            cmd.Parameters.Add("descripcion", OracleDbType.Varchar2).Value = (object?)dto.Descripcion ?? DBNull.Value;
            cmd.Parameters.Add("fechaReporte", OracleDbType.Date).Value = dto.FechaReporte ?? DateTime.Today;
            cmd.Parameters.Add("ubicacionEncontrado", OracleDbType.Varchar2).Value = (object?)dto.UbicacionEncontrado ?? DBNull.Value;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "REPORTADO";
            cmd.Parameters.Add("nombreReportante", OracleDbType.Varchar2).Value = (object?)dto.NombreReportante ?? DBNull.Value;
            cmd.Parameters.Add("contactoReportante", OracleDbType.Varchar2).Value = (object?)dto.ContactoReportante ?? DBNull.Value;
            cmd.Parameters.Add("fechaEntrega", OracleDbType.Date).Value = (object?)dto.FechaEntrega ?? DBNull.Value;
            cmd.Parameters.Add("nombreReclamante", OracleDbType.Varchar2).Value = (object?)dto.NombreReclamante ?? DBNull.Value;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, ObjetoPerdidoUpdateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("ObjetoPerdidoService/ActualizarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idVuelo", OracleDbType.Int32).Value = (object?)dto.IdVuelo ?? DBNull.Value;
            cmd.Parameters.Add("idAeropuerto", OracleDbType.Int32).Value = (object?)dto.IdAeropuerto ?? DBNull.Value;
            cmd.Parameters.Add("descripcion", OracleDbType.Varchar2).Value = (object?)dto.Descripcion ?? DBNull.Value;
            cmd.Parameters.Add("fechaReporte", OracleDbType.Date).Value = dto.FechaReporte ?? DateTime.Today;
            cmd.Parameters.Add("ubicacionEncontrado", OracleDbType.Varchar2).Value = (object?)dto.UbicacionEncontrado ?? DBNull.Value;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "REPORTADO";
            cmd.Parameters.Add("nombreReportante", OracleDbType.Varchar2).Value = (object?)dto.NombreReportante ?? DBNull.Value;
            cmd.Parameters.Add("contactoReportante", OracleDbType.Varchar2).Value = (object?)dto.ContactoReportante ?? DBNull.Value;
            cmd.Parameters.Add("fechaEntrega", OracleDbType.Date).Value = (object?)dto.FechaEntrega ?? DBNull.Value;
            cmd.Parameters.Add("nombreReclamante", OracleDbType.Varchar2).Value = (object?)dto.NombreReclamante ?? DBNull.Value;
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("ObjetoPerdidoService/EliminarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static ObjetoPerdidoResponseDto MapearObjeto(OracleDataReader reader)
        {
            return new ObjetoPerdidoResponseDto
            {
                IdObjeto = Convert.ToInt32(reader["OBJ_ID_OBJETO"]),
                IdVuelo = reader["OBJ_ID_VUELO"] == DBNull.Value ? null : Convert.ToInt32(reader["OBJ_ID_VUELO"]),
                IdAeropuerto = reader["OBJ_ID_AEROPUERTO"] == DBNull.Value ? null : Convert.ToInt32(reader["OBJ_ID_AEROPUERTO"]),
                Descripcion = reader["OBJ_DESCRIPCION"] == DBNull.Value ? null : reader["OBJ_DESCRIPCION"].ToString(),
                FechaReporte = reader["OBJ_FECHA_REPORTE"] == DBNull.Value ? null : Convert.ToDateTime(reader["OBJ_FECHA_REPORTE"]),
                UbicacionEncontrado = reader["OBJ_UBICACION_ENCONTRADO"] == DBNull.Value ? null : reader["OBJ_UBICACION_ENCONTRADO"].ToString(),
                Estado = reader["OBJ_ESTADO"] == DBNull.Value ? null : reader["OBJ_ESTADO"].ToString(),
                NombreReportante = reader["OBJ_NOMBRE_REPORTANTE"] == DBNull.Value ? null : reader["OBJ_NOMBRE_REPORTANTE"].ToString(),
                ContactoReportante = reader["OBJ_CONTACTO_REPORTANTE"] == DBNull.Value ? null : reader["OBJ_CONTACTO_REPORTANTE"].ToString(),
                FechaEntrega = reader["OBJ_FECHA_ENTREGA"] == DBNull.Value ? null : Convert.ToDateTime(reader["OBJ_FECHA_ENTREGA"]),
                NombreReclamante = reader["OBJ_NOMBRE_RECLAMANTE"] == DBNull.Value ? null : reader["OBJ_NOMBRE_RECLAMANTE"].ToString()
            };
        }
    }
}

