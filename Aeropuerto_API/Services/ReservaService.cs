using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class ReservaService
    {
        private readonly string _connectionString;
        private readonly SqlQueryProvider _sqlQueryProvider;

        public ReservaService(DatabaseSettings settings, SqlQueryProvider sqlQueryProvider)
        {
            _connectionString = settings.ConnectionString;
            _sqlQueryProvider = sqlQueryProvider;
        }

        public async Task<List<ReservaResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<ReservaResponseDto>();

            var query = _sqlQueryProvider.GetQuery("ReservaService/ObtenerTodosAsync.sql");

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new ReservaResponseDto
                {
                    Id = Convert.ToInt32(reader["RES_ID_RESERVA"]),
                    IdVuelo = Convert.ToInt32(reader["RES_ID_VUELO"]),
                    IdPasajero = Convert.ToInt32(reader["RES_ID_PASAJERO"]),
                    NumeroAsiento = reader["RES_NUMERO_ASIENTO"].ToString() ?? string.Empty,
                    Clase = reader["RES_CLASE"].ToString() ?? string.Empty,
                    FechaReserva = Convert.ToDateTime(reader["RES_FECHA_RESERVA"]),
                    Estado = reader["RES_ESTADO"] == DBNull.Value ? string.Empty : reader["RES_ESTADO"].ToString() ?? string.Empty,
                    EquipajeFacturado = reader["RES_EQUIPAJE_FACTURADO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["RES_EQUIPAJE_FACTURADO"]),
                    PesoEquipaje = reader["RES_PESO_EQUIPAJE"] == DBNull.Value ? null : Convert.ToDecimal(reader["RES_PESO_EQUIPAJE"]),
                    TarifaPagada = reader["RES_TARIFA_PAGADA"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["RES_TARIFA_PAGADA"]),
                    CodigoReserva = reader["RES_CODIGO_RESERVA"].ToString() ?? string.Empty
                });
            }

            return lista;
        }

        public async Task<ReservaResponseDto?> ObtenerPorIdAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("ReservaService/ObtenerPorIdAsync.sql");

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new ReservaResponseDto
                {
                    Id = Convert.ToInt32(reader["RES_ID_RESERVA"]),
                    IdVuelo = Convert.ToInt32(reader["RES_ID_VUELO"]),
                    IdPasajero = Convert.ToInt32(reader["RES_ID_PASAJERO"]),
                    NumeroAsiento = reader["RES_NUMERO_ASIENTO"].ToString() ?? string.Empty,
                    Clase = reader["RES_CLASE"].ToString() ?? string.Empty,
                    FechaReserva = Convert.ToDateTime(reader["RES_FECHA_RESERVA"]),
                    Estado = reader["RES_ESTADO"] == DBNull.Value ? string.Empty : reader["RES_ESTADO"].ToString() ?? string.Empty,
                    EquipajeFacturado = reader["RES_EQUIPAJE_FACTURADO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["RES_EQUIPAJE_FACTURADO"]),
                    PesoEquipaje = reader["RES_PESO_EQUIPAJE"] == DBNull.Value ? null : Convert.ToDecimal(reader["RES_PESO_EQUIPAJE"]),
                    TarifaPagada = reader["RES_TARIFA_PAGADA"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["RES_TARIFA_PAGADA"]),
                    CodigoReserva = reader["RES_CODIGO_RESERVA"].ToString() ?? string.Empty
                };
            }

            return null;
        }

        public async Task<bool> CrearAsync(ReservaCreateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("ReservaService/CrearAsync.sql");

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("idVuelo", OracleDbType.Int32).Value = dto.IdVuelo;
            command.Parameters.Add("idPasajero", OracleDbType.Int32).Value = dto.IdPasajero;
            command.Parameters.Add("numeroAsiento", OracleDbType.Varchar2).Value = dto.NumeroAsiento;
            command.Parameters.Add("clase", OracleDbType.Varchar2).Value = dto.Clase;
            command.Parameters.Add("fechaReserva", OracleDbType.Date).Value = dto.FechaReserva;
            command.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "ACTIVA";
            command.Parameters.Add("equipajeFacturado", OracleDbType.Int32).Value = dto.EquipajeFacturado ?? 0;
            command.Parameters.Add("pesoEquipaje", OracleDbType.Decimal).Value = (object?)dto.PesoEquipaje ?? DBNull.Value;
            command.Parameters.Add("tarifaPagada", OracleDbType.Decimal).Value = dto.TarifaPagada;
            command.Parameters.Add("codigoReserva", OracleDbType.Varchar2).Value = dto.CodigoReserva;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> ActualizarAsync(int id, ReservaUpdateDto dto)
        {
            var query = _sqlQueryProvider.GetQuery("ReservaService/ActualizarAsync.sql");

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("idVuelo", OracleDbType.Int32).Value = dto.IdVuelo;
            command.Parameters.Add("idPasajero", OracleDbType.Int32).Value = dto.IdPasajero;
            command.Parameters.Add("numeroAsiento", OracleDbType.Varchar2).Value = dto.NumeroAsiento;
            command.Parameters.Add("clase", OracleDbType.Varchar2).Value = dto.Clase;
            command.Parameters.Add("fechaReserva", OracleDbType.Date).Value = dto.FechaReserva;
            command.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "ACTIVA";
            command.Parameters.Add("equipajeFacturado", OracleDbType.Int32).Value = dto.EquipajeFacturado;
            command.Parameters.Add("pesoEquipaje", OracleDbType.Decimal).Value = (object?)dto.PesoEquipaje ?? DBNull.Value;
            command.Parameters.Add("tarifaPagada", OracleDbType.Decimal).Value = dto.TarifaPagada;
            command.Parameters.Add("codigoReserva", OracleDbType.Varchar2).Value = dto.CodigoReserva;
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var query = _sqlQueryProvider.GetQuery("ReservaService/EliminarAsync.sql");

            await using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new OracleCommand(query, connection);
            command.Parameters.Add("id", OracleDbType.Int32).Value = id;

            var filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}
