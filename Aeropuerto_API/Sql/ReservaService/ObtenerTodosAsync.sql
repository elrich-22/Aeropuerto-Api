SELECT
                    RES_ID_RESERVA,
                    RES_ID_VUELO,
                    RES_ID_PASAJERO,
                    RES_NUMERO_ASIENTO,
                    RES_CLASE,
                    RES_FECHA_RESERVA,
                    RES_ESTADO,
                    RES_EQUIPAJE_FACTURADO,
                    RES_PESO_EQUIPAJE,
                    RES_TARIFA_PAGADA,
                    RES_CODIGO_RESERVA
                FROM AER_RESERVA
                ORDER BY RES_ID_RESERVA
