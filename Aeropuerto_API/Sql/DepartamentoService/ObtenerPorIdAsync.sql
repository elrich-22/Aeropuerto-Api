SELECT
                    DEP_ID_DEPARTAMENTO,
                    DEP_NOMBRE,
                    DEP_DESCRIPCION,
                    DEP_ID_AEROPUERTO,
                    DEP_ESTADO
                FROM AER_DEPARTAMENTO
                WHERE DEP_ID_DEPARTAMENTO = :id
