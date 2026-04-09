SELECT
                    MOV_ID_MOVIMIENTO,
                    MOV_ID_REPUESTO,
                    MOV_TIPO_MOVIMIENTO,
                    MOV_CANTIDAD,
                    MOV_FECHA_MOVIMIENTO,
                    MOV_ID_EMPLEADO,
                    MOV_MOTIVO,
                    MOV_REFERENCIA
                FROM AER_MOVIMIENTOREPUESTO
                WHERE MOV_ID_MOVIMIENTO = :id
