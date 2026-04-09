INSERT INTO AER_METODOPAGO
                (
                    MET_NOMBRE,
                    MET_TIPO,
                    MET_ESTADO,
                    MET_COMISION_PORCENTAJE
                )
                VALUES
                (
                    :nombre,
                    :tipo,
                    :estado,
                    :comisionPorcentaje
                )
