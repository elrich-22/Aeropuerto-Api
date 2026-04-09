UPDATE AER_PROMOCION
                SET
                    PRO_CODIGO_PROMOCION = :codigoPromocion,
                    PRO_DESCRIPCION = :descripcion,
                    PRO_TIPO_DESCUENTO = :tipoDescuento,
                    PRO_VALOR_DESCUENTO = :valorDescuento,
                    PRO_FECHA_INICIO = :fechaInicio,
                    PRO_FECHA_FIN = :fechaFin,
                    PRO_USOS_MAXIMOS = :usosMaximos,
                    PRO_USOS_ACTUALES = :usosActuales,
                    PRO_ESTADO = :estado
                WHERE PRO_ID_PROMOCION = :id
