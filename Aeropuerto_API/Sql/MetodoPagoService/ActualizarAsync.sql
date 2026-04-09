UPDATE AER_METODOPAGO
                SET
                    MET_NOMBRE = :nombre,
                    MET_TIPO = :tipo,
                    MET_ESTADO = :estado,
                    MET_COMISION_PORCENTAJE = :comisionPorcentaje
                WHERE MET_ID_METODO_PAGO = :id
