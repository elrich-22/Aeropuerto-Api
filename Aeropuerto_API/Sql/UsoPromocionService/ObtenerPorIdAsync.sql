SELECT
                    USO_ID_USO,
                    USO_ID_PROMOCION,
                    USO_ID_RESERVA,
                    USO_FECHA_USO,
                    USO_MONTO_DESCUENTO
                FROM AER_USOPROMOCION
                WHERE USO_ID_USO = :id
