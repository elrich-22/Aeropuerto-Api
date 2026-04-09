UPDATE AER_AVION
                SET
                    AVI_MATRICULA = :matricula,
                    AVI_ID_MODELO = :idModelo,
                    AVI_ID_AEROLINEA = :idAerolinea,
                    AVI_ANIO_FABRICACION = :anioFabricacion,
                    AVI_ESTADO = :estado,
                    AVI_ULTIMA_REVISION = :ultimaRevision,
                    AVI_PROXIMA_REVISION = :proximaRevision,
                    AVI_HORAS_VUELO = :horasVuelo
                WHERE AVI_ID = :id
