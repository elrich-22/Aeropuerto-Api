using AeropuertoAPI.DTOs;
using AeropuertoAPI.Models;
using Oracle.ManagedDataAccess.Client;

namespace AeropuertoAPI.Services
{
    public class EmpleadoService
    {
        private readonly string _connectionString;

        public EmpleadoService(DatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<List<EmpleadoResponseDto>> ObtenerTodosAsync()
        {
            var lista = new List<EmpleadoResponseDto>();

            const string query = @"
                SELECT
                    EMP_ID_EMPLEADO,
                    EMP_NUMERO_EMPLEADO,
                    EMP_NOMBRES,
                    EMP_APELLIDOS,
                    EMP_FECHA_NACIMIENTO,
                    EMP_DPI,
                    EMP_NIT,
                    EMP_DIRECCION,
                    EMP_TELEFONO,
                    EMP_EMAIL,
                    EMP_FECHA_CONTRATACION,
                    EMP_ID_PUESTO,
                    EMP_ID_DEPARTAMENTO,
                    EMP_SALARIO_ACTUAL,
                    EMP_TIPO_CONTRATO,
                    EMP_ESTADO
                FROM AER_EMPLEADO
                ORDER BY EMP_ID_EMPLEADO";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(MapearEmpleado(reader));
            }

            return lista;
        }

        public async Task<EmpleadoResponseDto?> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT
                    EMP_ID_EMPLEADO,
                    EMP_NUMERO_EMPLEADO,
                    EMP_NOMBRES,
                    EMP_APELLIDOS,
                    EMP_FECHA_NACIMIENTO,
                    EMP_DPI,
                    EMP_NIT,
                    EMP_DIRECCION,
                    EMP_TELEFONO,
                    EMP_EMAIL,
                    EMP_FECHA_CONTRATACION,
                    EMP_ID_PUESTO,
                    EMP_ID_DEPARTAMENTO,
                    EMP_SALARIO_ACTUAL,
                    EMP_TIPO_CONTRATO,
                    EMP_ESTADO
                FROM AER_EMPLEADO
                WHERE EMP_ID_EMPLEADO = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return MapearEmpleado(reader);

            return null;
        }

        public async Task<bool> CrearAsync(EmpleadoCreateDto dto)
        {
            const string query = @"
                INSERT INTO AER_EMPLEADO
                (
                    EMP_NUMERO_EMPLEADO,
                    EMP_NOMBRES,
                    EMP_APELLIDOS,
                    EMP_FECHA_NACIMIENTO,
                    EMP_DPI,
                    EMP_NIT,
                    EMP_DIRECCION,
                    EMP_TELEFONO,
                    EMP_EMAIL,
                    EMP_FECHA_CONTRATACION,
                    EMP_ID_PUESTO,
                    EMP_ID_DEPARTAMENTO,
                    EMP_SALARIO_ACTUAL,
                    EMP_TIPO_CONTRATO,
                    EMP_ESTADO
                )
                VALUES
                (
                    :numeroEmpleado,
                    :nombres,
                    :apellidos,
                    :fechaNacimiento,
                    :dpi,
                    :nit,
                    :direccion,
                    :telefono,
                    :email,
                    :fechaContratacion,
                    :idPuesto,
                    :idDepartamento,
                    :salarioActual,
                    :tipoContrato,
                    :estado
                )";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);

            cmd.Parameters.Add("numeroEmpleado", OracleDbType.Varchar2).Value = dto.NumeroEmpleado;
            cmd.Parameters.Add("nombres", OracleDbType.Varchar2).Value = dto.Nombres;
            cmd.Parameters.Add("apellidos", OracleDbType.Varchar2).Value = dto.Apellidos;
            cmd.Parameters.Add("fechaNacimiento", OracleDbType.Date).Value = dto.FechaNacimiento;
            cmd.Parameters.Add("dpi", OracleDbType.Varchar2).Value = (object?)dto.DPI ?? DBNull.Value;
            cmd.Parameters.Add("nit", OracleDbType.Varchar2).Value = (object?)dto.NIT ?? DBNull.Value;
            cmd.Parameters.Add("direccion", OracleDbType.Varchar2).Value = (object?)dto.Direccion ?? DBNull.Value;
            cmd.Parameters.Add("telefono", OracleDbType.Varchar2).Value = (object?)dto.Telefono ?? DBNull.Value;
            cmd.Parameters.Add("email", OracleDbType.Varchar2).Value = (object?)dto.Email ?? DBNull.Value;
            cmd.Parameters.Add("fechaContratacion", OracleDbType.Date).Value = dto.FechaContratacion;
            cmd.Parameters.Add("idPuesto", OracleDbType.Int32).Value = dto.IdPuesto;
            cmd.Parameters.Add("idDepartamento", OracleDbType.Int32).Value = dto.IdDepartamento;
            cmd.Parameters.Add("salarioActual", OracleDbType.Decimal).Value = dto.SalarioActual;
            cmd.Parameters.Add("tipoContrato", OracleDbType.Varchar2).Value = dto.TipoContrato;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "ACTIVO";

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ActualizarAsync(int id, EmpleadoUpdateDto dto)
        {
            const string query = @"
                UPDATE AER_EMPLEADO
                SET
                    EMP_NUMERO_EMPLEADO = :numeroEmpleado,
                    EMP_NOMBRES = :nombres,
                    EMP_APELLIDOS = :apellidos,
                    EMP_FECHA_NACIMIENTO = :fechaNacimiento,
                    EMP_DPI = :dpi,
                    EMP_NIT = :nit,
                    EMP_DIRECCION = :direccion,
                    EMP_TELEFONO = :telefono,
                    EMP_EMAIL = :email,
                    EMP_FECHA_CONTRATACION = :fechaContratacion,
                    EMP_ID_PUESTO = :idPuesto,
                    EMP_ID_DEPARTAMENTO = :idDepartamento,
                    EMP_SALARIO_ACTUAL = :salarioActual,
                    EMP_TIPO_CONTRATO = :tipoContrato,
                    EMP_ESTADO = :estado
                WHERE EMP_ID_EMPLEADO = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);

            cmd.Parameters.Add("numeroEmpleado", OracleDbType.Varchar2).Value = dto.NumeroEmpleado;
            cmd.Parameters.Add("nombres", OracleDbType.Varchar2).Value = dto.Nombres;
            cmd.Parameters.Add("apellidos", OracleDbType.Varchar2).Value = dto.Apellidos;
            cmd.Parameters.Add("fechaNacimiento", OracleDbType.Date).Value = dto.FechaNacimiento;
            cmd.Parameters.Add("dpi", OracleDbType.Varchar2).Value = (object?)dto.DPI ?? DBNull.Value;
            cmd.Parameters.Add("nit", OracleDbType.Varchar2).Value = (object?)dto.NIT ?? DBNull.Value;
            cmd.Parameters.Add("direccion", OracleDbType.Varchar2).Value = (object?)dto.Direccion ?? DBNull.Value;
            cmd.Parameters.Add("telefono", OracleDbType.Varchar2).Value = (object?)dto.Telefono ?? DBNull.Value;
            cmd.Parameters.Add("email", OracleDbType.Varchar2).Value = (object?)dto.Email ?? DBNull.Value;
            cmd.Parameters.Add("fechaContratacion", OracleDbType.Date).Value = dto.FechaContratacion;
            cmd.Parameters.Add("idPuesto", OracleDbType.Int32).Value = dto.IdPuesto;
            cmd.Parameters.Add("idDepartamento", OracleDbType.Int32).Value = dto.IdDepartamento;
            cmd.Parameters.Add("salarioActual", OracleDbType.Decimal).Value = dto.SalarioActual;
            cmd.Parameters.Add("tipoContrato", OracleDbType.Varchar2).Value = dto.TipoContrato;
            cmd.Parameters.Add("estado", OracleDbType.Varchar2).Value = dto.Estado ?? "ACTIVO";
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            const string query = @"
                DELETE FROM AER_EMPLEADO
                WHERE EMP_ID_EMPLEADO = :id";

            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private static EmpleadoResponseDto MapearEmpleado(OracleDataReader reader)
        {
            return new EmpleadoResponseDto
            {
                Id = Convert.ToInt32(reader["EMP_ID_EMPLEADO"]),
                NumeroEmpleado = reader["EMP_NUMERO_EMPLEADO"].ToString() ?? string.Empty,
                Nombres = reader["EMP_NOMBRES"].ToString() ?? string.Empty,
                Apellidos = reader["EMP_APELLIDOS"].ToString() ?? string.Empty,
                FechaNacimiento = Convert.ToDateTime(reader["EMP_FECHA_NACIMIENTO"]),
                DPI = reader["EMP_DPI"] == DBNull.Value ? null : reader["EMP_DPI"].ToString(),
                NIT = reader["EMP_NIT"] == DBNull.Value ? null : reader["EMP_NIT"].ToString(),
                Direccion = reader["EMP_DIRECCION"] == DBNull.Value ? null : reader["EMP_DIRECCION"].ToString(),
                Telefono = reader["EMP_TELEFONO"] == DBNull.Value ? null : reader["EMP_TELEFONO"].ToString(),
                Email = reader["EMP_EMAIL"] == DBNull.Value ? null : reader["EMP_EMAIL"].ToString(),
                FechaContratacion = Convert.ToDateTime(reader["EMP_FECHA_CONTRATACION"]),
                IdPuesto = Convert.ToInt32(reader["EMP_ID_PUESTO"]),
                IdDepartamento = Convert.ToInt32(reader["EMP_ID_DEPARTAMENTO"]),
                SalarioActual = Convert.ToDecimal(reader["EMP_SALARIO_ACTUAL"]),
                TipoContrato = reader["EMP_TIPO_CONTRATO"].ToString() ?? string.Empty,
                Estado = reader["EMP_ESTADO"].ToString() ?? string.Empty
            };
        }
    }
}