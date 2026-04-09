UPDATE AER_REPUESTOUTILIZADO
                SET
                    RUT_ID_MANTENIMIENTO = :idMantenimiento,
                    RUT_ID_REPUESTO = :idRepuesto,
                    RUT_CANTIDAD = :cantidad
                WHERE RUT_ID_REPUESTO_UTILIZADO = :id
