UPDATE AER_MOVIMIENTOREPUESTO
                SET
                    MOV_ID_REPUESTO = :idRepuesto,
                    MOV_TIPO_MOVIMIENTO = :tipoMovimiento,
                    MOV_CANTIDAD = :cantidad,
                    MOV_FECHA_MOVIMIENTO = :fechaMovimiento,
                    MOV_ID_EMPLEADO = :idEmpleado,
                    MOV_MOTIVO = :motivo,
                    MOV_REFERENCIA = :referencia
                WHERE MOV_ID_MOVIMIENTO = :id
