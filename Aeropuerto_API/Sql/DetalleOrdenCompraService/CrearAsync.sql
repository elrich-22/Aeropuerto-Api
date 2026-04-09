INSERT INTO AER_DETALLEORDENCOMPRA
                (
                    DET_ID_ORDEN_COMPRA,
                    DET_ID_REPUESTO,
                    DET_CANTIDAD,
                    DET_PRECIO_UNITARIO,
                    DET_SUBTOTAL
                )
                VALUES
                (
                    :idOrdenCompra,
                    :idRepuesto,
                    :cantidad,
                    :precioUnitario,
                    :subtotal
                )
