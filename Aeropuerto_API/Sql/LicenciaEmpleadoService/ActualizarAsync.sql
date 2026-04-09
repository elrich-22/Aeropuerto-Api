UPDATE AER_LICENCIAEMPLEADO
                SET
                    LIC_ID_EMPLEADO = :idEmpleado,
                    LIC_TIPO_LICENCIA = :tipoLicencia,
                    LIC_NUMERO_LICENCIA = :numeroLicencia,
                    LIC_FECHA_EMISION = :fechaEmision,
                    LIC_FECHA_VENCIMIENTO = :fechaVencimiento,
                    LIC_AUTORIDAD_EMISORA = :autoridadEmisora,
                    LIC_ESTADO = :estado
                WHERE LIC_ID_LICENCIA = :id
