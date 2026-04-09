UPDATE AER_ASISTENCIA
                SET
                    ASI_ID_EMPLEADO = :idEmpleado,
                    ASI_FECHA = :fecha,
                    ASI_HORA_ENTRADA = :horaEntrada,
                    ASI_HORA_SALIDA = :horaSalida,
                    ASI_HORAS_TRABAJADAS = :horasTrabajadas,
                    ASI_TIPO = :tipo,
                    ASI_ESTADO = :estado
                WHERE ASI_ID_ASISTENCIA = :id
