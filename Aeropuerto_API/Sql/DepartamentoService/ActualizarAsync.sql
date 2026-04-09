UPDATE AER_DEPARTAMENTO
                SET
                    DEP_NOMBRE = :nombre,
                    DEP_DESCRIPCION = :descripcion,
                    DEP_ID_AEROPUERTO = :idAeropuerto,
                    DEP_ESTADO = :estado
                WHERE DEP_ID_DEPARTAMENTO = :id
