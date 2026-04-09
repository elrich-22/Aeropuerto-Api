SELECT
                    MAN_ID_MANTENIMIENTO,
                    MAN_ID_AVION,
                    MAN_TIPO_MANTENIMIENTO,
                    MAN_FECHA_INICIO,
                    MAN_FECHA_FIN_ESTIMADA,
                    MAN_FECHA_FIN_REAL,
                    MAN_DESCRIPCION_TRABAJO,
                    MAN_ID_EMPLEADO_RESPONSABLE,
                    MAN_COSTO_MANO_OBRA,
                    MAN_COSTO_REPUESTOS,
                    MAN_COSTO_TOTAL,
                    MAN_ESTADO
                FROM AER_MANTENIMIENTOAVION
                ORDER BY MAN_ID_MANTENIMIENTO
