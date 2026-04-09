UPDATE AER_TRIPULACION
                SET
                    TRI_ID_VUELO = :idVuelo,
                    TRI_ID_EMPLEADO = :idEmpleado,
                    TRI_ROL = :rol,
                    TRI_HORAS_VUELO = :horasVuelo
                WHERE TRI_ID_TRIPULACION = :id
