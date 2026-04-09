INSERT INTO AER_PUNTOVENTA
                (
                    PUV_CODIGO_PUNTO,
                    PUV_NOMBRE,
                    PUV_ID_AEROPUERTO,
                    PUV_UBICACION,
                    PUV_ESTADO
                )
                VALUES
                (
                    :codigoPunto,
                    :nombre,
                    :idAeropuerto,
                    :ubicacion,
                    :estado
                )
