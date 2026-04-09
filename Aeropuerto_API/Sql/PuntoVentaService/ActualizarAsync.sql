UPDATE AER_PUNTOVENTA
                SET
                    PUV_CODIGO_PUNTO = :codigoPunto,
                    PUV_NOMBRE = :nombre,
                    PUV_ID_AEROPUERTO = :idAeropuerto,
                    PUV_UBICACION = :ubicacion,
                    PUV_ESTADO = :estado
                WHERE PUV_ID_PUNTO_VENTA = :id
