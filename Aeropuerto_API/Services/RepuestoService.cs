using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class RepuestoService
    {
        private readonly string _connectionString;

        public RepuestoService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<RepuestoResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<RepuestoResponseDto>();

            const string query = @"
                SELECT
                    REP_ID_REPUESTO,
                    REP_CODIGO_REPUESTO,
                    REP_NOMBRE,
                    REP_DESCRIPCION,
                    REP_ID_CATEGORIA,
                    REP_ID_MODELO_AVION,
                    REP_NUMERO_PARTE_FABRICANTE,
                    REP_STOCK_MINIMO,
                    REP_STOCK_ACTUAL,
                    REP_STOCK_MAXIMO,
                    REP_PRECIO_UNITARIO,
                    REP_UBICACION_BODEGA,
                    REP_ESTADO
                FROM AER_REPUESTO
                ORDER BY REP_ID_REPUESTO";

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
            const string query = @"
                SELECT
                    REP_ID_REPUESTO,
                    REP_CODIGO_REPUESTO,
                    REP_NOMBRE,
                    REP_DESCRIPCION,
                    REP_ID_CATEGORIA,
                    REP_ID_MODELO_AVION,
                    REP_NUMERO_PARTE_FABRICANTE,
                    REP_STOCK_MINIMO,
                    REP_STOCK_ACTUAL,
                    REP_STOCK_MAXIMO,
                    REP_PRECIO_UNITARIO,
                    REP_UBICACION_BODEGA,
                    REP_ESTADO
                FROM AER_REPUESTO
                WHERE REP_ID_REPUESTO = :id";

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
            const string query = @"
                INSERT INTO AER_REPUESTO
                (
                    REP_CODIGO_REPUESTO,
                    REP_NOMBRE,
                    REP_DESCRIPCION,
                    REP_ID_CATEGORIA,
                    REP_ID_MODELO_AVION,
                    REP_NUMERO_PARTE_FABRICANTE,
                    REP_STOCK_MINIMO,
                    REP_STOCK_ACTUAL,
                    REP_STOCK_MAXIMO,
                    REP_PRECIO_UNITARIO,
                    REP_UBICACION_BODEGA,
                    REP_ESTADO
                )
                VALUES
                (
                    :codigoRepuesto,
                    :nombre,
                    :descripcion,
                    :idCategoria,
                    :idModeloAvion,
                    :numeroParteFabricante,
                    :stockMinimo,
                    :stockActual,
                    :stockMaximo,
                    :precioUnitario,
                    :ubicacionBodega,
                    :estado
                )";

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
            const string query = @"
                UPDATE AER_REPUESTO
                SET
                    REP_CODIGO_REPUESTO = :codigoRepuesto,
                    REP_NOMBRE = :nombre,
                    REP_DESCRIPCION = :descripcion,
                    REP_ID_CATEGORIA = :idCategoria,
                    REP_ID_MODELO_AVION = :idModeloAvion,
                    REP_NUMERO_PARTE_FABRICANTE = :numeroParteFabricante,
                    REP_STOCK_MINIMO = :stockMinimo,
                    REP_STOCK_ACTUAL = :stockActual,
                    REP_STOCK_MAXIMO = :stockMaximo,
                    REP_PRECIO_UNITARIO = :precioUnitario,
                    REP_UBICACION_BODEGA = :ubicacionBodega,
                    REP_ESTADO = :estado
                WHERE REP_ID_REPUESTO = :id";

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
            const string query = @"
                DELETE FROM AER_REPUESTO
                WHERE REP_ID_REPUESTO = :id";

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