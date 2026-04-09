SELECT
                    TRA_ID_TRANSACCION,
                    TRA_ID_RESERVA,
                    TRA_ID_METODO_PAGO,
                    TRA_MONTO_TOTAL,
                    TRA_MONEDA,
                    TRA_FECHA_TRANSACCION,
                    TRA_ESTADO,
                    TRA_NUMERO_AUTORIZACION,
                    TRA_REFERENCIA_EXTERNA,
                    TRA_IP_CLIENTE,
                    TRA_DETALLES_TARJETA
                FROM AER_TRANSACCIONPAGO
                WHERE TRA_ID_TRANSACCION = :id
