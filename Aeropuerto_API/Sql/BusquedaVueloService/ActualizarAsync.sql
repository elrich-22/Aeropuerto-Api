UPDATE AER_BUSQUEDAVUELO
                SET
                    BUS_ID_SESION = :idSesion,
                    BUS_ID_AEROPUERTO_ORIGEN = :idAeropuertoOrigen,
                    BUS_ID_AEROPUERTO_DESTINO = :idAeropuertoDestino,
                    BUS_FECHA_IDA = :fechaIda,
                    BUS_FECHA_VUELTA = :fechaVuelta,
                    BUS_NUMERO_PASAJEROS = :numeroPasajeros,
                    BUS_CLASE = :clase,
                    BUS_FECHA_BUSQUEDA = :fechaBusqueda,
                    BUS_SE_CONVIRTIO_COMPRA = :seConvirtioCompra
                WHERE BUS_ID_BUSQUEDA = :id
