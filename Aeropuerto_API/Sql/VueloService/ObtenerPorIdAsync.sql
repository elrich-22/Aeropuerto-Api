SELECT
                    VUE_ID_VUELO,
                    VUE_ID_PROGRAMA_VUELO,
                    VUE_ID_AVION,
                    VUE_FECHA_VUELO,
                    VUE_HORA_SALIDA_REAL,
                    VUE_HORA_LLEGADA_REAL,
                    VUE_PLAZAS_OCUPADAS,
                    VUE_PLAZAS_VACIAS,
                    VUE_ESTADO,
                    VUE_FECHA_REPROGRAMACION,
                    VUE_MOTIVO_CANCELACION,
                    VUE_PUERTA_EMBARQUE,
                    VUE_TERMINAL,
                    VUE_RETRASO_MINUTOS
                FROM AER_VUELO
                WHERE VUE_ID_VUELO = :id
