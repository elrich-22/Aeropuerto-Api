UPDATE AER_MODELOAVION
                SET
                    MOD_NOMBRE_MODELO = :nombreModelo,
                    MOD_FABRICANTE = :fabricante,
                    MOD_CAPACIDAD_PASAJEROS = :capacidadPasajeros,
                    MOD_CAPACIDAD_CARGA = :capacidadCarga,
                    MOD_ALCANCE_KM = :alcanceKm,
                    MOD_VELOCIDAD_CRUCERO = :velocidadCrucero,
                    MOD_ANIO_INTRODUCCION = :anioIntroduccion,
                    MOD_TIPO_MOTOR = :tipoMotor
                WHERE MOD_ID_MODELO = :id
