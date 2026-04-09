UPDATE AER_PUESTO
                SET
                    PUE_NOMBRE = :nombre,
                    PUE_ID_DEPARTAMENTO = :idDepartamento,
                    PUE_DESCRIPCION = :descripcion,
                    PUE_SALARIO_MINIMO = :salarioMinimo,
                    PUE_SALARIO_MAXIMO = :salarioMaximo,
                    PUE_REQUIERE_LICENCIA = :requiereLicencia
                WHERE PUE_ID_PUESTO = :id
