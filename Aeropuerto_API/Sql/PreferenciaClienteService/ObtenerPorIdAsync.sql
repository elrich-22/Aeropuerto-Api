SELECT
                    PRF_ID_PREFERENCIA,
                    PRF_ID_PASAJERO,
                    PRF_TIPO_PREFERENCIA,
                    PRF_VALOR_PREFERENCIA,
                    PRF_FECHA_REGISTRO
                FROM AER_PREFERENCIACLIENTE
                WHERE PRF_ID_PREFERENCIA = :id
