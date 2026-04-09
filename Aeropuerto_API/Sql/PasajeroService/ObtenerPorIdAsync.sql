SELECT
                    PAS_ID_PASAJERO,
                    PAS_NUMERO_DOCUMENTO,
                    PAS_TIPO_DOCUMENTO,
                    PAS_NOMBRES,
                    PAS_APELLIDOS,
                    PAS_FECHA_NACIMIENTO,
                    PAS_NACIONALIDAD,
                    PAS_SEXO,
                    PAS_TELEFONO,
                    PAS_EMAIL,
                    PAS_FECHA_REGISTRO
                FROM AER_PASAJERO
                WHERE PAS_ID_PASAJERO = :id
