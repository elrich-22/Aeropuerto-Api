SELECT
                    RUT_ID_REPUESTO_UTILIZADO,
                    RUT_ID_MANTENIMIENTO,
                    RUT_ID_REPUESTO,
                    RUT_CANTIDAD
                FROM AER_REPUESTOUTILIZADO
                WHERE RUT_ID_REPUESTO_UTILIZADO = :id
