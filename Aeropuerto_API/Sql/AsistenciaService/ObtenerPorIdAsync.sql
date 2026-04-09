SELECT
                    ASI_ID_ASISTENCIA,
                    ASI_ID_EMPLEADO,
                    ASI_FECHA,
                    ASI_HORA_ENTRADA,
                    ASI_HORA_SALIDA,
                    ASI_HORAS_TRABAJADAS,
                    ASI_TIPO,
                    ASI_ESTADO
                FROM AER_ASISTENCIA
                WHERE ASI_ID_ASISTENCIA = :id
