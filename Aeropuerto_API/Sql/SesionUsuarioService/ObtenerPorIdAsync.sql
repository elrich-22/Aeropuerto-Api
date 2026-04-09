SELECT
                    SES_ID_SESION,
                    SES_SESION_ID,
                    SES_ID_PASAJERO,
                    SES_IP_ADDRESS,
                    SES_NAVEGADOR,
                    SES_SISTEMA_OPERATIVO,
                    SES_DISPOSITIVO,
                    SES_FECHA_INICIO,
                    SES_FECHA_FIN,
                    SES_DURACION_SEGUNDOS
                FROM AER_SESIONUSUARIO
                WHERE SES_ID_SESION = :id
