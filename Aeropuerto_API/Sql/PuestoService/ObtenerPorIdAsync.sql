SELECT
                    PUE_ID_PUESTO,
                    PUE_NOMBRE,
                    PUE_ID_DEPARTAMENTO,
                    PUE_DESCRIPCION,
                    PUE_SALARIO_MINIMO,
                    PUE_SALARIO_MAXIMO,
                    PUE_REQUIERE_LICENCIA
                FROM AER_PUESTO
                WHERE PUE_ID_PUESTO = :id
