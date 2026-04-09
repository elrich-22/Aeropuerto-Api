using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class EscalaTecnicaService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public EscalaTecnicaService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<EscalaTecnicaResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<EscalaTecnicaResponseDto>();

            var query = _sqlQueryProvider.GetQuery("EscalaTecnicaService/ObtenerTodosAsync.sql");

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
            var query = _sqlQueryProvider.GetQuery("EscalaTecnicaService/ObtenerPorIdAsync.sql");

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
            var query = _sqlQueryProvider.GetQuery("EscalaTecnicaService/CrearAsync.sql");

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
            var query = _sqlQueryProvider.GetQuery("EscalaTecnicaService/ActualizarAsync.sql");

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
            var query = _sqlQueryProvider.GetQuery("EscalaTecnicaService/EliminarAsync.sql");

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
