using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class PasajeroService
    {
        private readonly string _connectionString;

        public PasajeroService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<PasajeroResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<PasajeroResponseDto>();

            const string query = @"
                SELECT
                    PAS_ID_PASAJERO,
                    PAS_NUMERO_DOCUMENTO,
                    PAS_TIPO_DOCUMENTO,
                    PAS_NOMBRES,
                    PAS_APELLIDOS,
                    PAS_FECHA_NACIMIENTO,
                    PAS_NACIONALIDAD,
                    PAS_SEXO,
                    PAS_TELEFONO,
                    PAS_EMAIL,
                    PAS_FECHA_REGISTRO
                FROM AER_PASAJERO
                ORDER BY PAS_ID_PASAJERO";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new PasajeroResponseDto
                {
                    Id = Convert.ToInt32(reader["PAS_ID_PASAJERO"]),
                    NumeroDocumento = reader["PAS_NUMERO_DOCUMENTO"].ToString() ?? string.Empty,
                    TipoDocumento = reader["PAS_TIPO_DOCUMENTO"].ToString() ?? string.Empty,
                    Nombres = reader["PAS_NOMBRES"].ToString() ?? string.Empty,
                    Apellidos = reader["PAS_APELLIDOS"].ToString() ?? string.Empty,
                    FechaNacimiento = Convert.ToDateTime(reader["PAS_FECHA_NACIMIENTO"]),
                    Nacionalidad = reader["PAS_NACIONALIDAD"].ToString() ?? string.Empty,
                    Sexo = reader["PAS_SEXO"].ToString() ?? string.Empty,
                    Telefono = reader["PAS_TELEFONO"].ToString() ?? string.Empty,
                    Email = reader["PAS_EMAIL"].ToString() ?? string.Empty,
                    FechaRegistro = reader["PAS_FECHA_REGISTRO"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["PAS_FECHA_REGISTRO"])
                });
            }

            return lista;
        }

        public async Task<PasajeroResponseDto?> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT
                    PAS_ID_PASAJERO,
                    PAS_NUMERO_DOCUMENTO,
                    PAS_TIPO_DOCUMENTO,
                    PAS_NOMBRES,
                    PAS_APELLIDOS,
                    PAS_FECHA_NACIMIENTO,
                    PAS_NACIONALIDAD,
                    PAS_SEXO,
                    PAS_TELEFONO,
                    PAS_EMAIL,
                    PAS_FECHA_REGISTRO
                FROM AER_PASAJERO
                WHERE PAS_ID_PASAJERO = :id";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new PasajeroResponseDto
                {
                    Id = Convert.ToInt32(reader["PAS_ID_PASAJERO"]),
                    NumeroDocumento = reader["PAS_NUMERO_DOCUMENTO"].ToString() ?? string.Empty,
                    TipoDocumento = reader["PAS_TIPO_DOCUMENTO"].ToString() ?? string.Empty,
                    Nombres = reader["PAS_NOMBRES"].ToString() ?? string.Empty,
                    Apellidos = reader["PAS_APELLIDOS"].ToString() ?? string.Empty,
                    FechaNacimiento = Convert.ToDateTime(reader["PAS_FECHA_NACIMIENTO"]),
                    Nacionalidad = reader["PAS_NACIONALIDAD"].ToString() ?? string.Empty,
                    Sexo = reader["PAS_SEXO"].ToString() ?? string.Empty,
                    Telefono = reader["PAS_TELEFONO"].ToString() ?? string.Empty,
                    Email = reader["PAS_EMAIL"].ToString() ?? string.Empty,
                    FechaRegistro = reader["PAS_FECHA_REGISTRO"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["PAS_FECHA_REGISTRO"])
                };
            }

            return null;
        }

        public async Task<bool> CrearAsync(PasajeroCreateDto dto)
        {
            const string query = @"
                INSERT INTO AER_PASAJERO
                (
                    PAS_NUMERO_DOCUMENTO,
                    PAS_TIPO_DOCUMENTO,
                    PAS_NOMBRES,
                    PAS_APELLIDOS,
                    PAS_FECHA_NACIMIENTO,
                    PAS_NACIONALIDAD,
                    PAS_SEXO,
                    PAS_TELEFONO,
                    PAS_EMAIL
                )
                VALUES
                (
                    :numeroDocumento,
                    :tipoDocumento,
                    :nombres,
                    :apellidos,
                    :fechaNacimiento,
                    :nacionalidad,
                    :sexo,
                    :telefono,
                    :email
                )";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("numeroDocumento", OracleDbType.Varchar2).Value = dto.NumeroDocumento;
            command.Parameters.Add("tipoDocumento", OracleDbType.Varchar2).Value = dto.TipoDocumento;
            command.Parameters.Add("nombres", OracleDbType.Varchar2).Value = dto.Nombres;
            command.Parameters.Add("apellidos", OracleDbType.Varchar2).Value = dto.Apellidos;
            command.Parameters.Add("fechaNacimiento", OracleDbType.Date).Value = dto.FechaNacimiento;
            command.Parameters.Add("nacionalidad", OracleDbType.Varchar2).Value = dto.Nacionalidad;
            command.Parameters.Add("sexo", OracleDbType.Char).Value = dto.Sexo;
            command.Parameters.Add("telefono", OracleDbType.Varchar2).Value = dto.Telefono;
            command.Parameters.Add("email", OracleDbType.Varchar2).Value = dto.Email;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> ActualizarAsync(int id, PasajeroUpdateDto dto)
        {
            const string query = @"
                UPDATE AER_PASAJERO
                SET
                    PAS_NUMERO_DOCUMENTO = :numeroDocumento,
                    PAS_TIPO_DOCUMENTO = :tipoDocumento,
                    PAS_NOMBRES = :nombres,
                    PAS_APELLIDOS = :apellidos,
                    PAS_FECHA_NACIMIENTO = :fechaNacimiento,
                    PAS_NACIONALIDAD = :nacionalidad,
                    PAS_SEXO = :sexo,
                    PAS_TELEFONO = :telefono,
                    PAS_EMAIL = :email
                WHERE PAS_ID_PASAJERO = :id";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("numeroDocumento", OracleDbType.Varchar2).Value = dto.NumeroDocumento;
            command.Parameters.Add("tipoDocumento", OracleDbType.Varchar2).Value = dto.TipoDocumento;
            command.Parameters.Add("nombres", OracleDbType.Varchar2).Value = dto.Nombres;
            command.Parameters.Add("apellidos", OracleDbType.Varchar2).Value = dto.Apellidos;
            command.Parameters.Add("fechaNacimiento", OracleDbType.Date).Value = dto.FechaNacimiento;
            command.Parameters.Add("nacionalidad", OracleDbType.Varchar2).Value = dto.Nacionalidad;
            command.Parameters.Add("sexo", OracleDbType.Char).Value = dto.Sexo;
            command.Parameters.Add("telefono", OracleDbType.Varchar2).Value = dto.Telefono;
            command.Parameters.Add("email", OracleDbType.Varchar2).Value = dto.Email;
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            const string query = @"
                DELETE FROM AER_PASAJERO
                WHERE PAS_ID_PASAJERO = :id";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}