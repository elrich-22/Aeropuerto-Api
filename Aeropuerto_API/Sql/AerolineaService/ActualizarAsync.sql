UPDATE AER_AEROLINEA
                SET
                    ARL_CODIGO_AEROLINEA = :codigo,
                    ARL_NOMBRE = :nombre,
                    ARL_PAIS_ORIGEN = :paisOrigen,
                    ARL_CODIGO_IATA = :codigoIata,
                    ARL_CODIGO_ICAO = :codigoIcao,
                    ARL_ESTADO = :estado,
                    ARL_TELEFONO = :telefono,
                    ARL_EMAIL = :email,
                    ARL_SITIO_WEB = :sitioWeb
                WHERE ARL_ID = :id
