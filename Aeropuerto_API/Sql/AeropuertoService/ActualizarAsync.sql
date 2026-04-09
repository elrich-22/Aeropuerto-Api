UPDATE AER_AEROPUERTO
                SET
                    AER_CODIGO_AEROPUERTO = :codigoAeropuerto,
                    AER_NOMBRE = :nombre,
                    AER_CIUDAD = :ciudad,
                    AER_PAIS = :pais,
                    AER_ZONA_HORARIA = :zonaHoraria,
                    AER_ESTADO = :estado,
                    AER_TIPO = :tipo,
                    AER_LATITUD = :latitud,
                    AER_LONGITUD = :longitud,
                    AER_CODIGO_IATA = :codigoIata,
                    AER_CODIGO_ICAO = :codigoIcao
                WHERE AER_ID = :id
