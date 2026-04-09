UPDATE AER_RESERVA
                SET
                    RES_ID_VUELO = :idVuelo,
                    RES_ID_PASAJERO = :idPasajero,
                    RES_NUMERO_ASIENTO = :numeroAsiento,
                    RES_CLASE = :clase,
                    RES_FECHA_RESERVA = :fechaReserva,
                    RES_ESTADO = :estado,
                    RES_EQUIPAJE_FACTURADO = :equipajeFacturado,
                    RES_PESO_EQUIPAJE = :pesoEquipaje,
                    RES_TARIFA_PAGADA = :tarifaPagada,
                    RES_CODIGO_RESERVA = :codigoReserva
                WHERE RES_ID_RESERVA = :id
