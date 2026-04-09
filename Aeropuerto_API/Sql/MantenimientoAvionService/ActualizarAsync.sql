UPDATE AER_MANTENIMIENTOAVION
                SET
                    MAN_ID_AVION = :idAvion,
                    MAN_TIPO_MANTENIMIENTO = :tipoMantenimiento,
                    MAN_FECHA_INICIO = :fechaInicio,
                    MAN_FECHA_FIN_ESTIMADA = :fechaFinEstimada,
                    MAN_FECHA_FIN_REAL = :fechaFinReal,
                    MAN_DESCRIPCION_TRABAJO = :descripcionTrabajo,
                    MAN_ID_EMPLEADO_RESPONSABLE = :idEmpleadoResponsable,
                    MAN_COSTO_MANO_OBRA = :costoManoObra,
                    MAN_COSTO_REPUESTOS = :costoRepuestos,
                    MAN_COSTO_TOTAL = :costoTotal,
                    MAN_ESTADO = :estado
                WHERE MAN_ID_MANTENIMIENTO = :id
