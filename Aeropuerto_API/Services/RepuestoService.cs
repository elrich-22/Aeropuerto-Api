using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class RepuestoService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public RepuestoService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<RepuestoResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<RepuestoResponseDto>();

            var query = _sqlQueryProvider.GetQuery("RepuestoService/ObtenerTodosAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(MapearRepuesto(reader));
            }

            return lista;
        }

        public async Task<RepuestoResponseDto?> ObtenerPorIdAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("RepuestoService/ObtenerPorIdAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return MapearRepuesto(reader);

            return null;
        }

        public async Task<bool> CrearAsync(RepuestoCreateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("RepuestoService/CrearAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("codigoRepuesto", OracleDbType.Varchar2).Value = dto.CodigoRepuesto;
            cmd.Parameters.Add("nombre", OracleDbType.Varchar2).Value = dto.Nombre;
            cmd.Parameters.Add("descripcion", OracleDbType.Varchar2).Value = (object?)dto.Descripcion ?? DBNull.Value;
            cmd.Parameters.Add("idCategoria", OracleDbType.Int32).Value = dto.IdCategoria;
            cmd.Parameters.Add("idModeloAvion", OracleDbType.Int32).Value = dto.IdModeloAvion;
            cmd.Parameters.Add("numeroParteFabricante", OracleDbType.Varchar2).Value = (object?)dto.NumeroParteFabricante ?? DBNull.Value;
            cmd.Parameters.Add("stockMinimo", OracleDbType.Int32).Value = (object?)dto.StockMinimo ?? DBNull.Value;
            cmd.Parameters.Add("stockActual", OracleDbType.Int32).Value = (object?)dto.StockActual ?? DBNull.Value;
            cmd.Parameters.Add("stockMaximo", OracleDbType.Int32).Value = (object?)dto.StockMaximo ?? DBNull.Value;
            cmd.Parameters.Add("precioUnitario", OracleDbType.Decimal).Value = (object?)dto.PrecioUnitario ?? DBNull.Value;
            cmd.Parameters.Add("ubicacionBodega", OracleDbType.Varchar2).Value = (object?)dto.UbicacionBodega ?? DBNull.Value;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "ACTIVO";

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, RepuestoUpdateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("RepuestoService/ActualizarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("codigoRepuesto", OracleDbType.Varchar2).Value = dto.CodigoRepuesto;
            cmd.Parameters.Add("nombre", OracleDbType.Varchar2).Value = dto.Nombre;
            cmd.Parameters.Add("descripcion", OracleDbType.Varchar2).Value = (object?)dto.Descripcion ?? DBNull.Value;
            cmd.Parameters.Add("idCategoria", OracleDbType.Int32).Value = dto.IdCategoria;
            cmd.Parameters.Add("idModeloAvion", OracleDbType.Int32).Value = dto.IdModeloAvion;
            cmd.Parameters.Add("numeroParteFabricante", OracleDbType.Varchar2).Value = (object?)dto.NumeroParteFabricante ?? DBNull.Value;
            cmd.Parameters.Add("stockMinimo", OracleDbType.Int32).Value = (object?)dto.StockMinimo ?? DBNull.Value;
            cmd.Parameters.Add("stockActual", OracleDbType.Int32).Value = (object?)dto.StockActual ?? DBNull.Value;
            cmd.Parameters.Add("stockMaximo", OracleDbType.Int32).Value = (object?)dto.StockMaximo ?? DBNull.Value;
            cmd.Parameters.Add("precioUnitario", OracleDbType.Decimal).Value = (object?)dto.PrecioUnitario ?? DBNull.Value;
            cmd.Parameters.Add("ubicacionBodega", OracleDbType.Varchar2).Value = (object?)dto.UbicacionBodega ?? DBNull.Value;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "ACTIVO";
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("RepuestoService/EliminarAsync.sql");

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static RepuestoResponseDto MapearRepuesto(OracleDataReader reader)
        {
            return new RepuestoResponseDto
            {
                Id = Convert.ToInt32(reader["REP_ID_REPUESTO"]),
                CodigoRepuesto = reader["REP_CODIGO_REPUESTO"].ToString() ?? string.Empty,
                Nombre = reader["REP_NOMBRE"].ToString() ?? string.Empty,
                Descripcion = reader["REP_DESCRIPCION"] == DBNull.Value ? null : reader["REP_DESCRIPCION"].ToString(),
                IdCategoria = Convert.ToInt32(reader["REP_ID_CATEGORIA"]),
                IdModeloAvion = Convert.ToInt32(reader["REP_ID_MODELO_AVION"]),
                NumeroParteFabricante = reader["REP_NUMERO_PARTE_FABRICANTE"] == DBNull.Value ? null : reader["REP_NUMERO_PARTE_FABRICANTE"].ToString(),
                StockMinimo = reader["REP_STOCK_MINIMO"] == DBNull.Value ? null : Convert.ToInt32(reader["REP_STOCK_MINIMO"]),
                StockActual = reader["REP_STOCK_ACTUAL"] == DBNull.Value ? null : Convert.ToInt32(reader["REP_STOCK_ACTUAL"]),
                StockMaximo = reader["REP_STOCK_MAXIMO"] == DBNull.Value ? null : Convert.ToInt32(reader["REP_STOCK_MAXIMO"]),
                PrecioUnitario = reader["REP_PRECIO_UNITARIO"] == DBNull.Value ? null : Convert.ToDecimal(reader["REP_PRECIO_UNITARIO"]),
                UbicacionBodega = reader["REP_UBICACION_BODEGA"] == DBNull.Value ? null : reader["REP_UBICACION_BODEGA"].ToString(),
                Estado = reader["REP_ESTADO"] == DBNull.Value ? string.Empty : reader["REP_ESTADO"].ToString() ?? string.Empty
            };
        }
    }
}
