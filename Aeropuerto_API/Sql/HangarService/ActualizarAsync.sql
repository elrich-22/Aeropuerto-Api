UPDATE AER_HANGAR
                SET
                    HAN_CODIGO_HANGAR = :codigoHangar,
                    HAN_NOMBRE = :nombre,
                    HAN_ID_AEROPUERTO = :idAeropuerto,
                    HAN_CAPACIDAD_AVIONES = :capacidadAviones,
                    HAN_AREA_M2 = :areaM2,
                    HAN_ALTURA_MAXIMA = :alturaMaxima,
                    HAN_TIPO = :tipo,
                    HAN_ESTADO = :estado
                WHERE HAN_ID_HANGAR = :id
