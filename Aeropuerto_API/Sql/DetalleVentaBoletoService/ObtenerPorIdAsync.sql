SELECT
                    DEV_ID_DETALLE_VENTA,
                    DEV_ID_VENTA,
                    DEV_ID_RESERVA,
                    DEV_PRECIO_BASE,
                    DEV_CARGOS_ADICIONALES
                FROM AER_DETALLEVENTABOLETO
                WHERE DEV_ID_DETALLE_VENTA = :id
