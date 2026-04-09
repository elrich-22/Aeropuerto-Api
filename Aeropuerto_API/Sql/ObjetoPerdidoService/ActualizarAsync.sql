UPDATE AER_OBJETOPERDIDO
                SET
                    OBJ_ID_VUELO = :idVuelo,
                    OBJ_ID_AEROPUERTO = :idAeropuerto,
                    OBJ_DESCRIPCION = :descripcion,
                    OBJ_FECHA_REPORTE = :fechaReporte,
                    OBJ_UBICACION_ENCONTRADO = :ubicacionEncontrado,
                    OBJ_ESTADO = :estado,
                    OBJ_NOMBRE_REPORTANTE = :nombreReportante,
                    OBJ_CONTACTO_REPORTANTE = :contactoReportante,
                    OBJ_FECHA_ENTREGA = :fechaEntrega,
                    OBJ_NOMBRE_RECLAMANTE = :nombreReclamante
                WHERE OBJ_ID_OBJETO = :id
