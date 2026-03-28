using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class TestConnectionService
    {
        private readonly string _connectionString;

        public TestConnectionService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<string> ProbarConexionAsync()
        {
            try
            {
                await using var connection = new OracleConnection(_connectionString);
                await connection.OpenAsync();

                return "Conexión exitosa a Oracle 😎";
            }
            catch (Exception ex)
            {
                return $"Error al conectar: {ex.Message}";
            }
        }
    }
}