UPDATE AER_CLICKDESTINO
                SET
                    CLI_ID_SESION = :idSesion,
                    CLI_ID_AEROPUERTO_DESTINO = :idAeropuertoDestino,
                    CLI_FECHA_CLICK = :fechaClick,
                    CLI_ORIGEN_BUSQUEDA = :origenBusqueda,
                    CLI_FECHA_VIAJE_BUSCADA = :fechaViajeBuscada,
                    CLI_NUMERO_PASAJEROS = :numeroPasajeros,
                    CLI_CLASE_BUSCADA = :claseBuscada
                WHERE CLI_ID_CLICK = :id
