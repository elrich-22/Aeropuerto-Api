SELECT
                    BUS_ID_BUSQUEDA,
                    BUS_ID_SESION,
                    BUS_ID_AEROPUERTO_ORIGEN,
                    BUS_ID_AEROPUERTO_DESTINO,
                    BUS_FECHA_IDA,
                    BUS_FECHA_VUELTA,
                    BUS_NUMERO_PASAJEROS,
                    BUS_CLASE,
                    BUS_FECHA_BUSQUEDA,
                    BUS_SE_CONVIRTIO_COMPRA
                FROM AER_BUSQUEDAVUELO
                ORDER BY BUS_ID_BUSQUEDA
