UPDATE AER_PASAJERO
                SET
                    PAS_NUMERO_DOCUMENTO = :numeroDocumento,
                    PAS_TIPO_DOCUMENTO = :tipoDocumento,
                    PAS_NOMBRES = :nombres,
                    PAS_APELLIDOS = :apellidos,
                    PAS_FECHA_NACIMIENTO = :fechaNacimiento,
                    PAS_NACIONALIDAD = :nacionalidad,
                    PAS_SEXO = :sexo,
                    PAS_TELEFONO = :telefono,
                    PAS_EMAIL = :email
                WHERE PAS_ID_PASAJERO = :id
