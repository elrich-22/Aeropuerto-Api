UPDATE AER_VUELO
                SET
                    VUE_ID_PROGRAMA_VUELO = :idPrograma,
                    VUE_ID_AVION = :idAvion,
                    VUE_FECHA_VUELO = :fechaVuelo,
                    VUE_HORA_SALIDA_REAL = :horaSalidaReal,
                    VUE_HORA_LLEGADA_REAL = :horaLlegadaReal,
                    VUE_PLAZAS_OCUPADAS = :plazasOcupadas,
                    VUE_PLAZAS_VACIAS = :plazasVacias,
                    VUE_ESTADO = :estado,
                    VUE_FECHA_REPROGRAMACION = :fechaReprogramacion,
                    VUE_MOTIVO_CANCELACION = :motivoCancelacion,
                    VUE_PUERTA_EMBARQUE = :puertaEmbarque,
                    VUE_TERMINAL = :terminal,
                    VUE_RETRASO_MINUTOS = :retrasoMinutos
                WHERE VUE_ID_VUELO = :id
