INSERT INTO AER_TRIPULACION
                (
                    TRI_ID_VUELO,
                    TRI_ID_EMPLEADO,
                    TRI_ROL,
                    TRI_HORAS_VUELO
                )
                VALUES
                (
                    :idVuelo,
                    :idEmpleado,
                    :rol,
                    :horasVuelo
                )
