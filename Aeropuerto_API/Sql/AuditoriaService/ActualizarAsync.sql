UPDATE AER_AUDITORIA
                SET
                    AUD_TABLA_AFECTADA = :tablaAfectada,
                    AUD_OPERACION = :operacion,
                    AUD_USUARIO = :usuario,
                    AUD_FECHA_HORA = :fechaHora,
                    AUD_IP_ADDRESS = :ipAddress,
                    AUD_DATOS_ANTERIORES = :datosAnteriores,
                    AUD_DATOS_NUEVOS = :datosNuevos
                WHERE AUD_ID_AUDITORIA = :id
