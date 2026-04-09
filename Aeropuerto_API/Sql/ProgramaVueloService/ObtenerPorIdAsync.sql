SELECT
                    PRV_ID,
                    PRV_NUMERO_VUELO,
                    PRV_ID_AEROLINEA,
                    PRV_ID_AEROPUERTO_ORIGEN,
                    PRV_ID_AEROPUERTO_DESTINO,
                    PRV_HORA_SALIDA_PROGRAMADA,
                    PRV_HORA_LLEGADA_PROGRAMADA,
                    PRV_DURACION_ESTIMADA,
                    PRV_TIPO_VUELO,
                    PRV_ESTADO,
                    PRV_FECHA_CREACION
                FROM AER_PROGRAMAVUELO
                WHERE PRV_ID = :id
