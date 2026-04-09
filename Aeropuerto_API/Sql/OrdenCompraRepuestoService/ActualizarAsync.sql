UPDATE AER_ORDENCOMPRAREPUESTO
                SET
                    ORC_NUMERO_ORDEN = :numeroOrden,
                    ORC_ID_PROVEEDOR = :idProveedor,
                    ORC_FECHA_ORDEN = :fechaOrden,
                    ORC_FECHA_ENTREGA_ESPERADA = :fechaEntregaEsperada,
                    ORC_FECHA_ENTREGA_REAL = :fechaEntregaReal,
                    ORC_MONTO_TOTAL = :montoTotal,
                    ORC_ESTADO = :estado,
                    ORC_ID_EMPLEADO_SOLICITA = :idEmpleadoSolicita,
                    ORC_OBSERVACIONES = :observaciones
                WHERE ORC_ID_ORDEN_COMPRA = :id
