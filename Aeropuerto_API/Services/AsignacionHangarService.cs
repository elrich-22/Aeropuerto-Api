using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class AsignacionHangarService
    {
        private readonly string _connectionString;

        public AsignacionHangarService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<AsignacionHangarResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<AsignacionHangarResponseDto>();

            const string query = @"
                SELECT
                    ASH_ID_ASIGNACION,
                    ASH_ID_HANGAR,
                    ASH_ID_AVION,
                    ASH_FECHA_ENTRADA,
                    ASH_FECHA_SALIDA_PROGRAMADA,
                    ASH_FECHA_SALIDA_REAL,
                    ASH_MOTIVO,
                    ASH_COSTO_HORA,
                    ASH_COSTO_TOTAL,
                    ASH_ESTADO
                FROM AER_ASIGNACIONHANGAR
                ORDER BY ASH_ID_ASIGNACION";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(Mapear(reader));
            }

            return lista;
        }

        public async Task<AsignacionHangarResponseDto?> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT
                    ASH_ID_ASIGNACION,
                    ASH_ID_HANGAR,
                    ASH_ID_AVION,
                    ASH_FECHA_ENTRADA,
                    ASH_FECHA_SALIDA_PROGRAMADA,
                    ASH_FECHA_SALIDA_REAL,
                    ASH_MOTIVO,
                    ASH_COSTO_HORA,
                    ASH_COSTO_TOTAL,
                    ASH_ESTADO
                FROM AER_ASIGNACIONHANGAR
                WHERE ASH_ID_ASIGNACION = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return Mapear(reader);

            return null;
        }

        public async Task<bool> CrearAsync(AsignacionHangarCreateDto dto)
        {
            const string query = @"
                INSERT INTO AER_ASIGNACIONHANGAR
                (
                    ASH_ID_HANGAR,
                    ASH_ID_AVION,
                    ASH_FECHA_ENTRADA,
                    ASH_FECHA_SALIDA_PROGRAMADA,
                    ASH_FECHA_SALIDA_REAL,
                    ASH_MOTIVO,
                    ASH_COSTO_HORA,
                    ASH_COSTO_TOTAL,
                    ASH_ESTADO
                )
                VALUES
                (
                    :idHangar,
                    :idAvion,
                    :fechaEntrada,
                    :fechaSalidaProgramada,
                    :fechaSalidaReal,
                    :motivo,
                    :costoHora,
                    :costoTotal,
                    :estado
                )";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idHangar", OracleDbType.Int32).Value = dto.IdHangar;
            cmd.Parameters.Add("idAvion", OracleDbType.Int32).Value = dto.IdAvion;
            cmd.Parameters.Add("fechaEntrada", OracleDbType.TimeStamp).Value = dto.FechaEntrada;
            cmd.Parameters.Add("fechaSalidaProgramada", OracleDbType.TimeStamp).Value = (object?)dto.FechaSalidaProgramada ?? DBNull.Value;
            cmd.Parameters.Add("fechaSalidaReal", OracleDbType.TimeStamp).Value = (object?)dto.FechaSalidaReal ?? DBNull.Value;
            cmd.Parameters.Add("motivo", OracleDbType.Varchar2).Value = (object?)dto.Motivo ?? DBNull.Value;
            cmd.Parameters.Add("costoHora", OracleDbType.Decimal).Value = (object?)dto.CostoHora ?? DBNull.Value;
            cmd.Parameters.Add("costoTotal", OracleDbType.Decimal).Value = (object?)dto.CostoTotal ?? DBNull.Value;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "ACTIVA";

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, AsignacionHangarUpdateDto dto)
        {
            const string query = @"
                UPDATE AER_ASIGNACIONHANGAR
                SET
                    ASH_ID_HANGAR = :idHangar,
                    ASH_ID_AVION = :idAvion,
                    ASH_FECHA_ENTRADA = :fechaEntrada,
                    ASH_FECHA_SALIDA_PROGRAMADA = :fechaSalidaProgramada,
                    ASH_FECHA_SALIDA_REAL = :fechaSalidaReal,
                    ASH_MOTIVO = :motivo,
                    ASH_COSTO_HORA = :costoHora,
                    ASH_COSTO_TOTAL = :costoTotal,
                    ASH_ESTADO = :estado
                WHERE ASH_ID_ASIGNACION = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idHangar", OracleDbType.Int32).Value = dto.IdHangar;
            cmd.Parameters.Add("idAvion", OracleDbType.Int32).Value = dto.IdAvion;
            cmd.Parameters.Add("fechaEntrada", OracleDbType.TimeStamp).Value = dto.FechaEntrada;
            cmd.Parameters.Add("fechaSalidaProgramada", OracleDbType.TimeStamp).Value = (object?)dto.FechaSalidaProgramada ?? DBNull.Value;
            cmd.Parameters.Add("fechaSalidaReal", OracleDbType.TimeStamp).Value = (object?)dto.FechaSalidaReal ?? DBNull.Value;
            cmd.Parameters.Add("motivo", OracleDbType.Varchar2).Value = (object?)dto.Motivo ?? DBNull.Value;
            cmd.Parameters.Add("costoHora", OracleDbType.Decimal).Value = (object?)dto.CostoHora ?? DBNull.Value;
            cmd.Parameters.Add("costoTotal", OracleDbType.Decimal).Value = (object?)dto.CostoTotal ?? DBNull.Value;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "ACTIVA";
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            const string query = @"
                DELETE FROM AER_ASIGNACIONHANGAR
                WHERE ASH_ID_ASIGNACION = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static AsignacionHangarResponseDto Mapear(OracleDataReader reader)
        {
            return new AsignacionHangarResponseDto
            {
                Id = Convert.ToInt32(reader["ASH_ID_ASIGNACION"]),
                IdHangar = Convert.ToInt32(reader["ASH_ID_HANGAR"]),
                IdAvion = Convert.ToInt32(reader["ASH_ID_AVION"]),
                FechaEntrada = Convert.ToDateTime(reader["ASH_FECHA_ENTRADA"]),
                FechaSalidaProgramada = reader["ASH_FECHA_SALIDA_PROGRAMADA"] == DBNull.Value
                    ? null
                    : Convert.ToDateTime(reader["ASH_FECHA_SALIDA_PROGRAMADA"]),
                FechaSalidaReal = reader["ASH_FECHA_SALIDA_REAL"] == DBNull.Value
                    ? null
                    : Convert.ToDateTime(reader["ASH_FECHA_SALIDA_REAL"]),
                Motivo = reader["ASH_MOTIVO"] == DBNull.Value ? null : reader["ASH_MOTIVO"].ToString(),
                CostoHora = reader["ASH_COSTO_HORA"] == DBNull.Value ? null : Convert.ToDecimal(reader["ASH_COSTO_HORA"]),
                CostoTotal = reader["ASH_COSTO_TOTAL"] == DBNull.Value ? null : Convert.ToDecimal(reader["ASH_COSTO_TOTAL"]),
                Estado = reader["ASH_ESTADO"] == DBNull.Value ? string.Empty : reader["ASH_ESTADO"].ToString() ?? string.Empty
            };
        }
    }
}