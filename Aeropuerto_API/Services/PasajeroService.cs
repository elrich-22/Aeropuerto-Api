using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class PasajeroService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public PasajeroService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<PasajeroResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<PasajeroResponseDto>();

            var query = _sqlQueryProvider.GetQuery("PasajeroService/ObtenerTodosAsync.sql");

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
            var query = _sqlQueryProvider.GetQuery("PasajeroService/ObtenerPorIdAsync.sql");

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
            var query = _sqlQueryProvider.GetQuery("PasajeroService/CrearAsync.sql");

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
            var query = _sqlQueryProvider.GetQuery("PasajeroService/ActualizarAsync.sql");

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
            var query = _sqlQueryProvider.GetQuery("PasajeroService/EliminarAsync.sql");

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}
