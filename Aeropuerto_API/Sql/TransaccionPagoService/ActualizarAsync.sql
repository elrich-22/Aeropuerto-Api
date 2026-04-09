UPDATE AER_TRANSACCIONPAGO
                SET
                    TRA_ID_RESERVA = :idReserva,
                    TRA_ID_METODO_PAGO = :idMetodoPago,
                    TRA_MONTO_TOTAL = :montoTotal,
                    TRA_MONEDA = :moneda,
                    TRA_FECHA_TRANSACCION = :fechaTransaccion,
                    TRA_ESTADO = :estado,
                    TRA_NUMERO_AUTORIZACION = :numeroAutorizacion,
                    TRA_REFERENCIA_EXTERNA = :referenciaExterna,
                    TRA_IP_CLIENTE = :ipCliente,
                    TRA_DETALLES_TARJETA = :detallesTarjeta
                WHERE TRA_ID_TRANSACCION = :id
