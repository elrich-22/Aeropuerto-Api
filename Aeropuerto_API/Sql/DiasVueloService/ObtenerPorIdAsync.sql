SELECT
                    DIA_ID_DIA_VUELO,
                    DIA_ID_PROGRAMA_VUELO,
                    DIA_DIA_SEMANA
                FROM AER_DIASVUELO
                WHERE DIA_ID_DIA_VUELO = :id
