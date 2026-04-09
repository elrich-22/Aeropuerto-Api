using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class SesionUsuarioService
    {
        private readonly string _connectionString;

        public SesionUsuarioService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<SesionUsuarioResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<SesionUsuarioResponseDto>();

            const string query = @"
                SELECT
                    SES_ID_SESION,
                    SES_SESION_ID,
                    SES_ID_PASAJERO,
                    SES_IP_ADDRESS,
                    SES_NAVEGADOR,
                    SES_SISTEMA_OPERATIVO,
                    SES_DISPOSITIVO,
                    SES_FECHA_INICIO,
                    SES_FECHA_FIN,
                    SES_DURACION_SEGUNDOS
                FROM AER_SESIONUSUARIO
                ORDER BY SES_ID_SESION";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(MapearSesion(reader));
            }

            return lista;
        }

        public async Task<SesionUsuarioResponseDto?> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT
                    SES_ID_SESION,
                    SES_SESION_ID,
                    SES_ID_PASAJERO,
                    SES_IP_ADDRESS,
                    SES_NAVEGADOR,
                    SES_SISTEMA_OPERATIVO,
                    SES_DISPOSITIVO,
                    SES_FECHA_INICIO,
                    SES_FECHA_FIN,
                    SES_DURACION_SEGUNDOS
                FROM AER_SESIONUSUARIO
                WHERE SES_ID_SESION = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return MapearSesion(reader);

            return null;
        }

        public async Task<bool> CrearAsync(SesionUsuarioCreateDto dto)
        {
            const string query = @"
                INSERT INTO AER_SESIONUSUARIO
                (
                    SES_SESION_ID,
                    SES_ID_PASAJERO,
                    SES_IP_ADDRESS,
                    SES_NAVEGADOR,
                    SES_SISTEMA_OPERATIVO,
                    SES_DISPOSITIVO,
                    SES_FECHA_INICIO,
                    SES_FECHA_FIN,
                    SES_DURACION_SEGUNDOS
                )
                VALUES
                (
                    :sesionId,
                    :idPasajero,
                    :ipAddress,
                    :navegador,
                    :sistemaOperativo,
                    :dispositivo,
                    :fechaInicio,
                    :fechaFin,
                    :duracionSegundos
                )";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("sesionId", OracleDbType.Varchar2).Value = dto.SesionId;
            cmd.Parameters.Add("idPasajero", OracleDbType.Int32).Value = (object?)dto.IdPasajero ?? DBNull.Value;
            cmd.Parameters.Add("ipAddress", OracleDbType.Varchar2).Value = (object?)dto.IpAddress ?? DBNull.Value;
            cmd.Parameters.Add("navegador", OracleDbType.Varchar2).Value = (object?)dto.Navegador ?? DBNull.Value;
            cmd.Parameters.Add("sistemaOperativo", OracleDbType.Varchar2).Value = (object?)dto.SistemaOperativo ?? DBNull.Value;
            cmd.Parameters.Add("dispositivo", OracleDbType.Varchar2).Value = (object?)dto.Dispositivo ?? DBNull.Value;
            cmd.Parameters.Add("fechaInicio", OracleDbType.TimeStamp).Value = dto.FechaInicio ?? DateTime.Now;
            cmd.Parameters.Add("fechaFin", OracleDbType.TimeStamp).Value = (object?)dto.FechaFin ?? DBNull.Value;
            cmd.Parameters.Add("duracionSegundos", OracleDbType.Decimal).Value = (object?)dto.DuracionSegundos ?? DBNull.Value;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, SesionUsuarioUpdateDto dto)
        {
            const string query = @"
                UPDATE AER_SESIONUSUARIO
                SET
                    SES_SESION_ID = :sesionId,
                    SES_ID_PASAJERO = :idPasajero,
                    SES_IP_ADDRESS = :ipAddress,
                    SES_NAVEGADOR = :navegador,
                    SES_SISTEMA_OPERATIVO = :sistemaOperativo,
                    SES_DISPOSITIVO = :dispositivo,
                    SES_FECHA_INICIO = :fechaInicio,
                    SES_FECHA_FIN = :fechaFin,
                    SES_DURACION_SEGUNDOS = :duracionSegundos
                WHERE SES_ID_SESION = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("sesionId", OracleDbType.Varchar2).Value = dto.SesionId;
            cmd.Parameters.Add("idPasajero", OracleDbType.Int32).Value = (object?)dto.IdPasajero ?? DBNull.Value;
            cmd.Parameters.Add("ipAddress", OracleDbType.Varchar2).Value = (object?)dto.IpAddress ?? DBNull.Value;
            cmd.Parameters.Add("navegador", OracleDbType.Varchar2).Value = (object?)dto.Navegador ?? DBNull.Value;
            cmd.Parameters.Add("sistemaOperativo", OracleDbType.Varchar2).Value = (object?)dto.SistemaOperativo ?? DBNull.Value;
            cmd.Parameters.Add("dispositivo", OracleDbType.Varchar2).Value = (object?)dto.Dispositivo ?? DBNull.Value;
            cmd.Parameters.Add("fechaInicio", OracleDbType.TimeStamp).Value = dto.FechaInicio ?? DateTime.Now;
            cmd.Parameters.Add("fechaFin", OracleDbType.TimeStamp).Value = (object?)dto.FechaFin ?? DBNull.Value;
            cmd.Parameters.Add("duracionSegundos", OracleDbType.Decimal).Value = (object?)dto.DuracionSegundos ?? DBNull.Value;
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            const string query = @"
                DELETE FROM AER_SESIONUSUARIO
                WHERE SES_ID_SESION = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static SesionUsuarioResponseDto MapearSesion(OracleDataReader reader)
        {
            return new SesionUsuarioResponseDto
            {
                IdSesion = Convert.ToInt32(reader["SES_ID_SESION"]),
                SesionId = reader["SES_SESION_ID"] == DBNull.Value ? null : reader["SES_SESION_ID"].ToString(),
                IdPasajero = reader["SES_ID_PASAJERO"] == DBNull.Value ? null : Convert.ToInt32(reader["SES_ID_PASAJERO"]),
                IpAddress = reader["SES_IP_ADDRESS"] == DBNull.Value ? null : reader["SES_IP_ADDRESS"].ToString(),
                Navegador = reader["SES_NAVEGADOR"] == DBNull.Value ? null : reader["SES_NAVEGADOR"].ToString(),
                SistemaOperativo = reader["SES_SISTEMA_OPERATIVO"] == DBNull.Value ? null : reader["SES_SISTEMA_OPERATIVO"].ToString(),
                Dispositivo = reader["SES_DISPOSITIVO"] == DBNull.Value ? null : reader["SES_DISPOSITIVO"].ToString(),
                FechaInicio = reader["SES_FECHA_INICIO"] == DBNull.Value ? null : Convert.ToDateTime(reader["SES_FECHA_INICIO"]),
                FechaFin = reader["SES_FECHA_FIN"] == DBNull.Value ? null : Convert.ToDateTime(reader["SES_FECHA_FIN"]),
                DuracionSegundos = reader["SES_DURACION_SEGUNDOS"] == DBNull.Value ? null : Convert.ToDecimal(reader["SES_DURACION_SEGUNDOS"])
            };
        }
    }
}
