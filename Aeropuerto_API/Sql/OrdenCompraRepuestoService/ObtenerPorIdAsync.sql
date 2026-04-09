SELECT
                    ORC_ID_ORDEN_COMPRA,
                    ORC_NUMERO_ORDEN,
                    ORC_ID_PROVEEDOR,
                    ORC_FECHA_ORDEN,
                    ORC_FECHA_ENTREGA_ESPERADA,
                    ORC_FECHA_ENTREGA_REAL,
                    ORC_MONTO_TOTAL,
                    ORC_ESTADO,
                    ORC_ID_EMPLEADO_SOLICITA,
                    ORC_OBSERVACIONES
                FROM AER_ORDENCOMPRAREPUESTO
                WHERE ORC_ID_ORDEN_COMPRA = :id
