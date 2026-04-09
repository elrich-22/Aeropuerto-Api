UPDATE AER_VENTABOLETO
                SET
                    VEN_NUMERO_VENTA = :numeroVenta,
                    VEN_ID_PUNTO_VENTA = :idPuntoVenta,
                    VEN_ID_EMPLEADO_VENDEDOR = :idEmpleadoVendedor,
                    VEN_ID_PASAJERO = :idPasajero,
                    VEN_FECHA_VENTA = :fechaVenta,
                    VEN_MONTO_SUBTOTAL = :montoSubtotal,
                    VEN_IMPUESTOS = :impuestos,
                    VEN_DESCUENTOS = :descuentos,
                    VEN_MONTO_TOTAL = :montoTotal,
                    VEN_ID_METODO_PAGO = :idMetodoPago,
                    VEN_CANAL_VENTA = :canalVenta,
                    VEN_ESTADO = :estado
                WHERE VEN_ID_VENTA = :id
