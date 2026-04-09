INSERT INTO AER_PUESTO
                (
                    PUE_NOMBRE,
                    PUE_ID_DEPARTAMENTO,
                    PUE_DESCRIPCION,
                    PUE_SALARIO_MINIMO,
                    PUE_SALARIO_MAXIMO,
                    PUE_REQUIERE_LICENCIA
                )
                VALUES
                (
                    :nombre,
                    :idDepartamento,
                    :descripcion,
                    :salarioMinimo,
                    :salarioMaximo,
                    :requiereLicencia
                )
