INSERT INTO AER_PREFERENCIACLIENTE
                (
                    PRF_ID_PASAJERO,
                    PRF_TIPO_PREFERENCIA,
                    PRF_VALOR_PREFERENCIA,
                    PRF_FECHA_REGISTRO
                )
                VALUES
                (
                    :idPasajero,
                    :tipoPreferencia,
                    :valorPreferencia,
                    :fechaRegistro
                )
