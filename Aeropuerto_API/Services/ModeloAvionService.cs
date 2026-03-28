using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class ModeloAvionService
    {
        private readonly string _connectionString;

        public ModeloAvionService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<ModeloAvionResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<ModeloAvionResponseDto>();

            const string query = @"
                SELECT
                    MOD_ID_MODELO,
                    MOD_NOMBRE_MODELO,
                    MOD_FABRICANTE,
                    MOD_CAPACIDAD_PASAJEROS,
                    MOD_CAPACIDAD_CARGA,
                    MOD_ALCANCE_KM,
                    MOD_VELOCIDAD_CRUCERO,
                    MOD_ANIO_INTRODUCCION,
                    MOD_TIPO_MOTOR
                FROM AER_MODELOAVION
                ORDER BY MOD_ID_MODELO";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new ModeloAvionResponseDto
                {
                    Id = Convert.ToInt32(reader["MOD_ID_MODELO"]),
                    NombreModelo = reader["MOD_NOMBRE_MODELO"].ToString() ?? string.Empty,
                    Fabricante = reader["MOD_FABRICANTE"].ToString() ?? string.Empty,
                    CapacidadPasajeros = Convert.ToInt32(reader["MOD_CAPACIDAD_PASAJEROS"]),
                    CapacidadCarga = reader["MOD_CAPACIDAD_CARGA"] == DBNull.Value ? null : Convert.ToInt32(reader["MOD_CAPACIDAD_CARGA"]),
                    AlcanceKm = reader["MOD_ALCANCE_KM"] == DBNull.Value ? null : Convert.ToInt32(reader["MOD_ALCANCE_KM"]),
                    VelocidadCrucero = reader["MOD_VELOCIDAD_CRUCERO"] == DBNull.Value ? null : Convert.ToInt32(reader["MOD_VELOCIDAD_CRUCERO"]),
                    AnioIntroduccion = reader["MOD_ANIO_INTRODUCCION"] == DBNull.Value ? null : Convert.ToInt32(reader["MOD_ANIO_INTRODUCCION"]),
                    TipoMotor = reader["MOD_TIPO_MOTOR"] == DBNull.Value ? null : reader["MOD_TIPO_MOTOR"].ToString()
                });
            }

            return lista;
        }

        public async Task<ModeloAvionResponseDto?> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT
                    MOD_ID_MODELO,
                    MOD_NOMBRE_MODELO,
                    MOD_FABRICANTE,
                    MOD_CAPACIDAD_PASAJEROS,
                    MOD_CAPACIDAD_CARGA,
                    MOD_ALCANCE_KM,
                    MOD_VELOCIDAD_CRUCERO,
                    MOD_ANIO_INTRODUCCION,
                    MOD_TIPO_MOTOR
                FROM AER_MODELOAVION
                WHERE MOD_ID_MODELO = :id";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new ModeloAvionResponseDto
                {
                    Id = Convert.ToInt32(reader["MOD_ID_MODELO"]),
                    NombreModelo = reader["MOD_NOMBRE_MODELO"].ToString() ?? string.Empty,
                    Fabricante = reader["MOD_FABRICANTE"].ToString() ?? string.Empty,
                    CapacidadPasajeros = Convert.ToInt32(reader["MOD_CAPACIDAD_PASAJEROS"]),
                    CapacidadCarga = reader["MOD_CAPACIDAD_CARGA"] == DBNull.Value ? null : Convert.ToInt32(reader["MOD_CAPACIDAD_CARGA"]),
                    AlcanceKm = reader["MOD_ALCANCE_KM"] == DBNull.Value ? null : Convert.ToInt32(reader["MOD_ALCANCE_KM"]),
                    VelocidadCrucero = reader["MOD_VELOCIDAD_CRUCERO"] == DBNull.Value ? null : Convert.ToInt32(reader["MOD_VELOCIDAD_CRUCERO"]),
                    AnioIntroduccion = reader["MOD_ANIO_INTRODUCCION"] == DBNull.Value ? null : Convert.ToInt32(reader["MOD_ANIO_INTRODUCCION"]),
                    TipoMotor = reader["MOD_TIPO_MOTOR"] == DBNull.Value ? null : reader["MOD_TIPO_MOTOR"].ToString()
                };
            }

            return null;
        }

        public async Task<bool> CrearAsync(ModeloAvionCreateDto dto)
        {
            const string query = @"
                INSERT INTO AER_MODELOAVION
                (
                    MOD_NOMBRE_MODELO,
                    MOD_FABRICANTE,
                    MOD_CAPACIDAD_PASAJEROS,
                    MOD_CAPACIDAD_CARGA,
                    MOD_ALCANCE_KM,
                    MOD_VELOCIDAD_CRUCERO,
                    MOD_ANIO_INTRODUCCION,
                    MOD_TIPO_MOTOR
                )
                VALUES
                (
                    :nombreModelo,
                    :fabricante,
                    :capacidadPasajeros,
                    :capacidadCarga,
                    :alcanceKm,
                    :velocidadCrucero,
                    :anioIntroduccion,
                    :tipoMotor
                )";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("nombreModelo", OracleDbType.Varchar2).Value = dto.NombreModelo;
            command.Parameters.Add("fabricante", OracleDbType.Varchar2).Value = dto.Fabricante;
            command.Parameters.Add("capacidadPasajeros", OracleDbType.Int32).Value = dto.CapacidadPasajeros;
            command.Parameters.Add("capacidadCarga", OracleDbType.Int32).Value = (object?)dto.CapacidadCarga ?? DBNull.Value;
            command.Parameters.Add("alcanceKm", OracleDbType.Int32).Value = (object?)dto.AlcanceKm ?? DBNull.Value;
            command.Parameters.Add("velocidadCrucero", OracleDbType.Int32).Value = (object?)dto.VelocidadCrucero ?? DBNull.Value;
            command.Parameters.Add("anioIntroduccion", OracleDbType.Int32).Value = (object?)dto.AnioIntroduccion ?? DBNull.Value;
            command.Parameters.Add("tipoMotor", OracleDbType.Varchar2).Value = (object?)dto.TipoMotor ?? DBNull.Value;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> ActualizarAsync(int id, ModeloAvionUpdateDto dto)
        {
            const string query = @"
                UPDATE AER_MODELOAVION
                SET
                    MOD_NOMBRE_MODELO = :nombreModelo,
                    MOD_FABRICANTE = :fabricante,
                    MOD_CAPACIDAD_PASAJEROS = :capacidadPasajeros,
                    MOD_CAPACIDAD_CARGA = :capacidadCarga,
                    MOD_ALCANCE_KM = :alcanceKm,
                    MOD_VELOCIDAD_CRUCERO = :velocidadCrucero,
                    MOD_ANIO_INTRODUCCION = :anioIntroduccion,
                    MOD_TIPO_MOTOR = :tipoMotor
                WHERE MOD_ID_MODELO = :id";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("nombreModelo", OracleDbType.Varchar2).Value = dto.NombreModelo;
            command.Parameters.Add("fabricante", OracleDbType.Varchar2).Value = dto.Fabricante;
            command.Parameters.Add("capacidadPasajeros", OracleDbType.Int32).Value = dto.CapacidadPasajeros;
            command.Parameters.Add("capacidadCarga", OracleDbType.Int32).Value = (object?)dto.CapacidadCarga ?? DBNull.Value;
            command.Parameters.Add("alcanceKm", OracleDbType.Int32).Value = (object?)dto.AlcanceKm ?? DBNull.Value;
            command.Parameters.Add("velocidadCrucero", OracleDbType.Int32).Value = (object?)dto.VelocidadCrucero ?? DBNull.Value;
            command.Parameters.Add("anioIntroduccion", OracleDbType.Int32).Value = (object?)dto.AnioIntroduccion ?? DBNull.Value;
            command.Parameters.Add("tipoMotor", OracleDbType.Varchar2).Value = (object?)dto.TipoMotor ?? DBNull.Value;
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            const string query = @"
                DELETE FROM AER_MODELOAVION
                WHERE MOD_ID_MODELO = :id";

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}