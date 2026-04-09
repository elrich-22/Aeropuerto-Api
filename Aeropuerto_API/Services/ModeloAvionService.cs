using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class ModeloAvionService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public ModeloAvionService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<ModeloAvionResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<ModeloAvionResponseDto>();

            var query = _sqlQueryProvider.GetQuery("ModeloAvionService/ObtenerTodosAsync.sql");

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
            var query = _sqlQueryProvider.GetQuery("ModeloAvionService/ObtenerPorIdAsync.sql");

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
            var query = _sqlQueryProvider.GetQuery("ModeloAvionService/CrearAsync.sql");

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
            var query = _sqlQueryProvider.GetQuery("ModeloAvionService/ActualizarAsync.sql");

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
            var query = _sqlQueryProvider.GetQuery("ModeloAvionService/EliminarAsync.sql");

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}
