using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class EscalaTecnicaService
    {
        private readonly string _connectionString;

        public EscalaTecnicaService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<EscalaTecnicaResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<EscalaTecnicaResponseDto>();

            const string query = @"
                SELECT
                    ESC_ID_ESCALA,
                    ESC_ID_PROGRAMA_VUELO,
                    ESC_ID_AEROPUERTO,
                    ESC_NUMERO_ORDEN,
                    ESC_HORA_LLEGADA_ESTIMADA,
                    ESC_HORA_SALIDA_ESTIMADA,
                    ESC_DURACION_ESCALA
                FROM AER_ESCALATECNICA
                ORDER BY ESC_ID_ESCALA";

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

        public async Task<EscalaTecnicaResponseDto?> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT
                    ESC_ID_ESCALA,
                    ESC_ID_PROGRAMA_VUELO,
                    ESC_ID_AEROPUERTO,
                    ESC_NUMERO_ORDEN,
                    ESC_HORA_LLEGADA_ESTIMADA,
                    ESC_HORA_SALIDA_ESTIMADA,
                    ESC_DURACION_ESCALA
                FROM AER_ESCALATECNICA
                WHERE ESC_ID_ESCALA = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return Mapear(reader);

            return null;
        }

        public async Task<bool> CrearAsync(EscalaTecnicaCreateDto dto)
        {
            const string query = @"
                INSERT INTO AER_ESCALATECNICA
                (
                    ESC_ID_PROGRAMA_VUELO,
                    ESC_ID_AEROPUERTO,
                    ESC_NUMERO_ORDEN,
                    ESC_HORA_LLEGADA_ESTIMADA,
                    ESC_HORA_SALIDA_ESTIMADA,
                    ESC_DURACION_ESCALA
                )
                VALUES
                (
                    :idProgramaVuelo,
                    :idAeropuerto,
                    :numeroOrden,
                    :horaLlegadaEstimada,
                    :horaSalidaEstimada,
                    :duracionEscala
                )";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idProgramaVuelo", OracleDbType.Int32).Value = dto.IdProgramaVuelo;
            cmd.Parameters.Add("idAeropuerto", OracleDbType.Int32).Value = dto.IdAeropuerto;
            cmd.Parameters.Add("numeroOrden", OracleDbType.Int32).Value = dto.NumeroOrden;
            cmd.Parameters.Add("horaLlegadaEstimada", OracleDbType.Char).Value = (object?)dto.HoraLlegadaEstimada ?? DBNull.Value;
            cmd.Parameters.Add("horaSalidaEstimada", OracleDbType.Char).Value = (object?)dto.HoraSalidaEstimada ?? DBNull.Value;
            cmd.Parameters.Add("duracionEscala", OracleDbType.Int32).Value = (object?)dto.DuracionEscala ?? DBNull.Value;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, EscalaTecnicaUpdateDto dto)
        {
            const string query = @"
                UPDATE AER_ESCALATECNICA
                SET
                    ESC_ID_PROGRAMA_VUELO = :idProgramaVuelo,
                    ESC_ID_AEROPUERTO = :idAeropuerto,
                    ESC_NUMERO_ORDEN = :numeroOrden,
                    ESC_HORA_LLEGADA_ESTIMADA = :horaLlegadaEstimada,
                    ESC_HORA_SALIDA_ESTIMADA = :horaSalidaEstimada,
                    ESC_DURACION_ESCALA = :duracionEscala
                WHERE ESC_ID_ESCALA = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("idProgramaVuelo", OracleDbType.Int32).Value = dto.IdProgramaVuelo;
            cmd.Parameters.Add("idAeropuerto", OracleDbType.Int32).Value = dto.IdAeropuerto;
            cmd.Parameters.Add("numeroOrden", OracleDbType.Int32).Value = dto.NumeroOrden;
            cmd.Parameters.Add("horaLlegadaEstimada", OracleDbType.Char).Value = (object?)dto.HoraLlegadaEstimada ?? DBNull.Value;
            cmd.Parameters.Add("horaSalidaEstimada", OracleDbType.Char).Value = (object?)dto.HoraSalidaEstimada ?? DBNull.Value;
            cmd.Parameters.Add("duracionEscala", OracleDbType.Int32).Value = (object?)dto.DuracionEscala ?? DBNull.Value;
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            const string query = @"
                DELETE FROM AER_ESCALATECNICA
                WHERE ESC_ID_ESCALA = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static EscalaTecnicaResponseDto Mapear(OracleDataReader reader)
        {
            return new EscalaTecnicaResponseDto
            {
                Id = Convert.ToInt32(reader["ESC_ID_ESCALA"]),
                IdProgramaVuelo = Convert.ToInt32(reader["ESC_ID_PROGRAMA_VUELO"]),
                IdAeropuerto = Convert.ToInt32(reader["ESC_ID_AEROPUERTO"]),
                NumeroOrden = Convert.ToInt32(reader["ESC_NUMERO_ORDEN"]),
                HoraLlegadaEstimada = reader["ESC_HORA_LLEGADA_ESTIMADA"] == DBNull.Value
                    ? null
                    : reader["ESC_HORA_LLEGADA_ESTIMADA"].ToString(),
                HoraSalidaEstimada = reader["ESC_HORA_SALIDA_ESTIMADA"] == DBNull.Value
                    ? null
                    : reader["ESC_HORA_SALIDA_ESTIMADA"].ToString(),
                DuracionEscala = reader["ESC_DURACION_ESCALA"] == DBNull.Value
                    ? null
                    : Convert.ToInt32(reader["ESC_DURACION_ESCALA"])
            };
        }
    }
}