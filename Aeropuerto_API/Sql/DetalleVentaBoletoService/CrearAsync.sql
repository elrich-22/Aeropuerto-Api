INSERT INTO AER_DETALLEVENTABOLETO
                (
                    DEV_ID_VENTA,
                    DEV_ID_RESERVA,
                    DEV_PRECIO_BASE,
                    DEV_CARGOS_ADICIONALES
                )
                VALUES
                (
                    :idVenta,
                    :idReserva,
                    :precioBase,
                    :cargosAdicionales
                )
