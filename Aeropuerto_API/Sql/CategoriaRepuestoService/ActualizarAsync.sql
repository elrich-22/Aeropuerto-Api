UPDATE AER_CATEGORIAREPUESTO
                SET
                    CAT_NOMBRE = :nombre,
                    CAT_DESCRIPCION = :descripcion
                WHERE CAT_ID_CATEGORIA = :id
