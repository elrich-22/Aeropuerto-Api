INSERT INTO AER_ITEMCARRITO
                (
                    ITE_ID_CARRITO,
                    ITE_ID_VUELO,
                    ITE_NUMERO_ASIENTO,
                    ITE_CLASE,
                    ITE_PRECIO_UNITARIO,
                    ITE_CANTIDAD
                )
                VALUES
                (
                    :idCarrito,
                    :idVuelo,
                    :numeroAsiento,
                    :clase,
                    :precioUnitario,
                    :cantidad
                )
