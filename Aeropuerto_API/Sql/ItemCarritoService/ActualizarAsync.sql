UPDATE AER_ITEMCARRITO
                SET
                    ITE_ID_CARRITO = :idCarrito,
                    ITE_ID_VUELO = :idVuelo,
                    ITE_NUMERO_ASIENTO = :numeroAsiento,
                    ITE_CLASE = :clase,
                    ITE_PRECIO_UNITARIO = :precioUnitario,
                    ITE_CANTIDAD = :cantidad
                WHERE ITE_ID_ITEM_CARRITO = :id
