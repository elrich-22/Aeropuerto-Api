UPDATE AER_PROGRAMAVUELO
                SET
                    PRV_NUMERO_VUELO = :numeroVuelo,
                    PRV_ID_AEROLINEA = :idAerolinea,
                    PRV_ID_AEROPUERTO_ORIGEN = :idAeropuertoOrigen,
                    PRV_ID_AEROPUERTO_DESTINO = :idAeropuertoDestino,
                    PRV_HORA_SALIDA_PROGRAMADA = :horaSalidaProgramada,
                    PRV_HORA_LLEGADA_PROGRAMADA = :horaLlegadaProgramada,
                    PRV_DURACION_ESTIMADA = :duracionEstimada,
                    PRV_TIPO_VUELO = :tipoVuelo,
                    PRV_ESTADO = :estado
                WHERE PRV_ID = :id
