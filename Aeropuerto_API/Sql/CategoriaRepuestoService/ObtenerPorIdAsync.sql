SELECT
                    CAT_ID_CATEGORIA,
                    CAT_NOMBRE,
                    CAT_DESCRIPCION
                FROM AER_CATEGORIAREPUESTO
                WHERE CAT_ID_CATEGORIA = :id
