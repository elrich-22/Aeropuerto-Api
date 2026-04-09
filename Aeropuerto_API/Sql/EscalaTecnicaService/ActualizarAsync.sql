UPDATE AER_ESCALATECNICA
                SET
                    ESC_ID_PROGRAMA_VUELO = :idProgramaVuelo,
                    ESC_ID_AEROPUERTO = :idAeropuerto,
                    ESC_NUMERO_ORDEN = :numeroOrden,
                    ESC_HORA_LLEGADA_ESTIMADA = :horaLlegadaEstimada,
                    ESC_HORA_SALIDA_ESTIMADA = :horaSalidaEstimada,
                    ESC_DURACION_ESCALA = :duracionEscala
                WHERE ESC_ID_ESCALA = :id
