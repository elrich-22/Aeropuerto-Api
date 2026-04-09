UPDATE AER_DETALLEORDENCOMPRA
                SET
                    DET_ID_ORDEN_COMPRA = :idOrdenCompra,
                    DET_ID_REPUESTO = :idRepuesto,
                    DET_CANTIDAD = :cantidad,
                    DET_PRECIO_UNITARIO = :precioUnitario,
                    DET_SUBTOTAL = :subtotal
                WHERE DET_ID_DETALLE = :id
