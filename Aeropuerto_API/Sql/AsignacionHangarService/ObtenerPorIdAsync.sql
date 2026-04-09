SELECT
                    ASH_ID_ASIGNACION,
                    ASH_ID_HANGAR,
                    ASH_ID_AVION,
                    ASH_FECHA_ENTRADA,
                    ASH_FECHA_SALIDA_PROGRAMADA,
                    ASH_FECHA_SALIDA_REAL,
                    ASH_MOTIVO,
                    ASH_COSTO_HORA,
                    ASH_COSTO_TOTAL,
                    ASH_ESTADO
                FROM AER_ASIGNACIONHANGAR
                WHERE ASH_ID_ASIGNACION = :id
