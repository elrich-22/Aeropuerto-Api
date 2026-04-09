UPDATE AER_PROVEEDOR
                SET
                    PRV_NOMBRE_EMPRESA = :nombreEmpresa,
                    PRV_NIT = :nit,
                    PRV_DIRECCION = :direccion,
                    PRV_TELEFONO = :telefono,
                    PRV_EMAIL = :email,
                    PRV_CONTACTO_PRINCIPAL = :contactoPrincipal,
                    PRV_PAIS = :pais,
                    PRV_ESTADO = :estado,
                    PRV_CALIFICACION = :calificacion
                WHERE PRV_ID_PROVEEDOR = :id
