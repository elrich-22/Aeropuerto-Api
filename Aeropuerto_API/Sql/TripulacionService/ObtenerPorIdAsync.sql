SELECT
                    TRI_ID_TRIPULACION,
                    TRI_ID_VUELO,
                    TRI_ID_EMPLEADO,
                    TRI_ROL,
                    TRI_HORAS_VUELO
                FROM AER_TRIPULACION
                WHERE TRI_ID_TRIPULACION = :id
