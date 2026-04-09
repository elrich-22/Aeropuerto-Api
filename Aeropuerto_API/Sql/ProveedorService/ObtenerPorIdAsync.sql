SELECT
                    PRV_ID_PROVEEDOR,
                    PRV_NOMBRE_EMPRESA,
                    PRV_NIT,
                    PRV_DIRECCION,
                    PRV_TELEFONO,
                    PRV_EMAIL,
                    PRV_CONTACTO_PRINCIPAL,
                    PRV_PAIS,
                    PRV_ESTADO,
                    PRV_CALIFICACION
                FROM AER_PROVEEDOR
                WHERE PRV_ID_PROVEEDOR = :id
