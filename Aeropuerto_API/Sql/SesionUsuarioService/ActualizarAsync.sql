UPDATE AER_SESIONUSUARIO
                SET
                    SES_SESION_ID = :sesionId,
                    SES_ID_PASAJERO = :idPasajero,
                    SES_IP_ADDRESS = :ipAddress,
                    SES_NAVEGADOR = :navegador,
                    SES_SISTEMA_OPERATIVO = :sistemaOperativo,
                    SES_DISPOSITIVO = :dispositivo,
                    SES_FECHA_INICIO = :fechaInicio,
                    SES_FECHA_FIN = :fechaFin,
                    SES_DURACION_SEGUNDOS = :duracionSegundos
                WHERE SES_ID_SESION = :id
