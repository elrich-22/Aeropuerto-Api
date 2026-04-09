UPDATE AER_USOPROMOCION
                SET
                    USO_ID_PROMOCION = :idPromocion,
                    USO_ID_RESERVA = :idReserva,
                    USO_FECHA_USO = :fechaUso,
                    USO_MONTO_DESCUENTO = :montoDescuento
                WHERE USO_ID_USO = :id
