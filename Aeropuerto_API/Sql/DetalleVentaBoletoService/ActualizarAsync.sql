UPDATE AER_DETALLEVENTABOLETO
                SET
                    DEV_ID_VENTA = :idVenta,
                    DEV_ID_RESERVA = :idReserva,
                    DEV_PRECIO_BASE = :precioBase,
                    DEV_CARGOS_ADICIONALES = :cargosAdicionales
                WHERE DEV_ID_DETALLE_VENTA = :id
