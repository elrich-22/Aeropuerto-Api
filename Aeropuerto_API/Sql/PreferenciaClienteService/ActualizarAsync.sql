UPDATE AER_PREFERENCIACLIENTE
                SET
                    PRF_ID_PASAJERO = :idPasajero,
                    PRF_TIPO_PREFERENCIA = :tipoPreferencia,
                    PRF_VALOR_PREFERENCIA = :valorPreferencia,
                    PRF_FECHA_REGISTRO = :fechaRegistro
                WHERE PRF_ID_PREFERENCIA = :id
