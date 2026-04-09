UPDATE AER_REPUESTO
                SET
                    REP_CODIGO_REPUESTO = :codigoRepuesto,
                    REP_NOMBRE = :nombre,
                    REP_DESCRIPCION = :descripcion,
                    REP_ID_CATEGORIA = :idCategoria,
                    REP_ID_MODELO_AVION = :idModeloAvion,
                    REP_NUMERO_PARTE_FABRICANTE = :numeroParteFabricante,
                    REP_STOCK_MINIMO = :stockMinimo,
                    REP_STOCK_ACTUAL = :stockActual,
                    REP_STOCK_MAXIMO = :stockMaximo,
                    REP_PRECIO_UNITARIO = :precioUnitario,
                    REP_UBICACION_BODEGA = :ubicacionBodega,
                    REP_ESTADO = :estado
                WHERE REP_ID_REPUESTO = :id
