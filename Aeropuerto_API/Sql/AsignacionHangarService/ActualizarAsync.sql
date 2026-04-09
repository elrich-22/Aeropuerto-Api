UPDATE AER_ASIGNACIONHANGAR
                SET
                    ASH_ID_HANGAR = :idHangar,
                    ASH_ID_AVION = :idAvion,
                    ASH_FECHA_ENTRADA = :fechaEntrada,
                    ASH_FECHA_SALIDA_PROGRAMADA = :fechaSalidaProgramada,
                    ASH_FECHA_SALIDA_REAL = :fechaSalidaReal,
                    ASH_MOTIVO = :motivo,
                    ASH_COSTO_HORA = :costoHora,
                    ASH_COSTO_TOTAL = :costoTotal,
                    ASH_ESTADO = :estado
                WHERE ASH_ID_ASIGNACION = :id
