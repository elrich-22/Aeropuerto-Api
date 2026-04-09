INSERT INTO AER_USOPROMOCION
                (
                    USO_ID_PROMOCION,
                    USO_ID_RESERVA,
                    USO_FECHA_USO,
                    USO_MONTO_DESCUENTO
                )
                VALUES
                (
                    :idPromocion,
                    :idReserva,
                    :fechaUso,
                    :montoDescuento
                )
