UPDATE AER_DIASVUELO
                SET
                    DIA_ID_PROGRAMA_VUELO = :idProgramaVuelo,
                    DIA_DIA_SEMANA = :diaSemana
                WHERE DIA_ID_DIA_VUELO = :id
