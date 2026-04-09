SELECT
                    ARR_ID_ARRESTO,
                    ARR_ID_PASAJERO,
                    ARR_ID_VUELO,
                    ARR_ID_AEROPUERTO,
                    ARR_FECHA_HORA_ARRESTO,
                    ARR_MOTIVO,
                    ARR_AUTORIDAD_CARGO,
                    ARR_DESCRIPCION_INCIDENTE,
                    ARR_UBICACION_ARRESTO,
                    ARR_ESTADO_CASO,
                    ARR_NUMERO_EXPEDIENTE
                FROM AER_ARRESTO
                WHERE ARR_ID_ARRESTO = :id
