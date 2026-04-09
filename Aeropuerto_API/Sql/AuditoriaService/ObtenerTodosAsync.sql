SELECT
                    AUD_ID_AUDITORIA,
                    AUD_TABLA_AFECTADA,
                    AUD_OPERACION,
                    AUD_USUARIO,
                    AUD_FECHA_HORA,
                    AUD_IP_ADDRESS,
                    AUD_DATOS_ANTERIORES,
                    AUD_DATOS_NUEVOS
                FROM AER_AUDITORIA
                ORDER BY AUD_ID_AUDITORIA
