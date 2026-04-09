UPDATE AER_PLANILLA
                SET
                    PLA_ID_EMPLEADO = :idEmpleado,
                    PLA_PERIODO_INICIO = :periodoInicio,
                    PLA_PERIODO_FIN = :periodoFin,
                    PLA_SALARIO_BASE = :salarioBase,
                    PLA_BONIFICACIONES = :bonificaciones,
                    PLA_HORAS_EXTRA = :horasExtra,
                    PLA_DEDUCCIONES = :deducciones,
                    PLA_SALARIO_NETO = :salarioNeto,
                    PLA_FECHA_PAGO = :fechaPago,
                    PLA_ESTADO = :estado
                WHERE PLA_ID_PLANILLA = :id
