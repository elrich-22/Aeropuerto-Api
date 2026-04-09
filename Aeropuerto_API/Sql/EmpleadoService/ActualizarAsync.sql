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
                WHERE EMP_ID_EMPLEADO = :id
