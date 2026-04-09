SELECT
                    MET_ID_METODO_PAGO,
                    MET_NOMBRE,
                    MET_TIPO,
                    MET_ESTADO,
                    MET_COMISION_PORCENTAJE
                FROM AER_METODOPAGO
                WHERE MET_ID_METODO_PAGO = :id
