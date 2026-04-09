INSERT INTO AER_CARRITOCOMPRA
                (
                    CAR_ID_PASAJERO,
                    CAR_SESION_ID,
                    CAR_FECHA_CREACION,
                    CAR_FECHA_EXPIRACION,
                    CAR_ESTADO
                )
                VALUES
                (
                    :idPasajero,
                    :sesionId,
                    :fechaCreacion,
                    :fechaExpiracion,
                    :estado
                )
