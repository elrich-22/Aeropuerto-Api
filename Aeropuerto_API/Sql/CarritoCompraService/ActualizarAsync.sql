UPDATE AER_CARRITOCOMPRA
                SET
                    CAR_ID_PASAJERO = :idPasajero,
                    CAR_SESION_ID = :sesionId,
                    CAR_FECHA_CREACION = :fechaCreacion,
                    CAR_FECHA_EXPIRACION = :fechaExpiracion,
                    CAR_ESTADO = :estado
                WHERE CAR_ID_CARRITO = :id
