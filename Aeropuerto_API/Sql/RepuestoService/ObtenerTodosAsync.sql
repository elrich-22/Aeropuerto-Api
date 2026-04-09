SELECT
                    REP_ID_REPUESTO,
                    REP_CODIGO_REPUESTO,
                    REP_NOMBRE,
                    REP_DESCRIPCION,
                    REP_ID_CATEGORIA,
                    REP_ID_MODELO_AVION,
                    REP_NUMERO_PARTE_FABRICANTE,
                    REP_STOCK_MINIMO,
                    REP_STOCK_ACTUAL,
                    REP_STOCK_MAXIMO,
                    REP_PRECIO_UNITARIO,
                    REP_UBICACION_BODEGA,
                    REP_ESTADO
                FROM AER_REPUESTO
                ORDER BY REP_ID_REPUESTO
