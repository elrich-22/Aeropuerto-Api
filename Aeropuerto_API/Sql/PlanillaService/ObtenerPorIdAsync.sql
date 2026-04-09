SELECT
                    PLA_ID_PLANILLA,
                    PLA_ID_EMPLEADO,
                    PLA_PERIODO_INICIO,
                    PLA_PERIODO_FIN,
                    PLA_SALARIO_BASE,
                    PLA_BONIFICACIONES,
                    PLA_HORAS_EXTRA,
                    PLA_DEDUCCIONES,
                    PLA_SALARIO_NETO,
                    PLA_FECHA_PAGO,
                    PLA_ESTADO
                FROM AER_PLANILLA
                WHERE PLA_ID_PLANILLA = :id
