UPDATE AER_ARRESTO
                SET
                    ARR_ID_PASAJERO = :idPasajero,
                    ARR_ID_VUELO = :idVuelo,
                    ARR_ID_AEROPUERTO = :idAeropuerto,
                    ARR_FECHA_HORA_ARRESTO = :fechaHoraArresto,
                    ARR_MOTIVO = :motivo,
                    ARR_AUTORIDAD_CARGO = :autoridadCargo,
                    ARR_DESCRIPCION_INCIDENTE = :descripcionIncidente,
                    ARR_UBICACION_ARRESTO = :ubicacionArresto,
                    ARR_ESTADO_CASO = :estadoCaso,
                    ARR_NUMERO_EXPEDIENTE = :numeroExpediente
                WHERE ARR_ID_ARRESTO = :id
