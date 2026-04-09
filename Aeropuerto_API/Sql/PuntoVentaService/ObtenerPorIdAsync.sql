SELECT
                    PUV_ID_PUNTO_VENTA,
                    PUV_CODIGO_PUNTO,
                    PUV_NOMBRE,
                    PUV_ID_AEROPUERTO,
                    PUV_UBICACION,
                    PUV_ESTADO
                FROM AER_PUNTOVENTA
                WHERE PUV_ID_PUNTO_VENTA = :id
