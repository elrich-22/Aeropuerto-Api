$ErrorActionPreference = 'Stop'

$projectRoot = Split-Path -Parent $PSScriptRoot
$sqlRoot = Join-Path $projectRoot 'Aeropuerto_API\Sql'
$outputPath = Join-Path $projectRoot 'scripts\procedimientos_oracle_servicios.sql'

function Get-SqlText {
    param(
        [string]$Path
    )

    if (Test-Path $Path) {
        return (Get-Content -Path $Path -Raw).Trim()
    }

    return $null
}

function Split-Columns {
    param(
        [string]$Text
    )

    if ([string]::IsNullOrWhiteSpace($Text)) {
        return @()
    }

    return @(($Text -split ',') |
        ForEach-Object { $_.Trim() } |
        Where-Object { $_ })
}

function Get-ParamName {
    param(
        [string]$Column
    )

    return 'P_' + $Column.ToUpper()
}

function Parse-Insert {
    param(
        [string]$Sql
    )

    if ([string]::IsNullOrWhiteSpace($Sql)) {
        return $null
    }

    $match = [regex]::Match(
        $Sql,
        'INSERT\s+INTO\s+([A-Z0-9_]+)\s*\((.*?)\)\s*VALUES\s*\((.*?)\)',
        'IgnoreCase, Singleline'
    )

    if (-not $match.Success) {
        return $null
    }

    return [pscustomobject]@{
        Table   = $match.Groups[1].Value.Trim().ToUpper()
        Columns = Split-Columns $match.Groups[2].Value
    }
}

function Parse-Update {
    param(
        [string]$Sql
    )

    if ([string]::IsNullOrWhiteSpace($Sql)) {
        return $null
    }

    $match = [regex]::Match(
        $Sql,
        'UPDATE\s+([A-Z0-9_]+)\s+SET\s+(.*?)\s+WHERE\s+([A-Z0-9_]+)\s*=\s*(:[A-Z0-9_]+)',
        'IgnoreCase, Singleline'
    )

    if (-not $match.Success) {
        return $null
    }

    $assignments = @()
    foreach ($assignment in (Split-Columns $match.Groups[2].Value)) {
        $parts = $assignment -split '=', 2
        $assignments += [pscustomobject]@{
            Column = $parts[0].Trim().ToUpper()
        }
    }

    return [pscustomobject]@{
        Table       = $match.Groups[1].Value.Trim().ToUpper()
        WhereColumn = $match.Groups[3].Value.Trim().ToUpper()
        Assignments = $assignments
    }
}

function Parse-Delete {
    param(
        [string]$Sql
    )

    if ([string]::IsNullOrWhiteSpace($Sql)) {
        return $null
    }

    $match = [regex]::Match(
        $Sql,
        'DELETE\s+FROM\s+([A-Z0-9_]+)\s+WHERE\s+([A-Z0-9_]+)\s*=\s*(:[A-Z0-9_]+)',
        'IgnoreCase, Singleline'
    )

    if (-not $match.Success) {
        return $null
    }

    return [pscustomobject]@{
        Table       = $match.Groups[1].Value.Trim().ToUpper()
        WhereColumn = $match.Groups[2].Value.Trim().ToUpper()
    }
}

function Parse-Select {
    param(
        [string]$Sql
    )

    if ([string]::IsNullOrWhiteSpace($Sql)) {
        return $null
    }

    $match = [regex]::Match(
        $Sql,
        'SELECT\s+(.*?)\s+FROM\s+([A-Z0-9_]+)(?:\s+WHERE\s+([A-Z0-9_]+)\s*=\s*(:[A-Z0-9_]+))?(?:\s+ORDER\s+BY\s+(.*?))?$',
        'IgnoreCase, Singleline'
    )

    if (-not $match.Success) {
        return $null
    }

    return [pscustomobject]@{
        Columns     = Split-Columns $match.Groups[1].Value
        Table       = $match.Groups[2].Value.Trim().ToUpper()
        WhereColumn = $match.Groups[3].Value.Trim().ToUpper()
        OrderBy     = $match.Groups[5].Value.Trim()
    }
}

$lines = New-Object 'System.Collections.Generic.List[string]'
$null = $lines.Add('-- ================================================================')
$null = $lines.Add('-- PROCEDIMIENTOS PL/SQL GENERADOS DESDE Aeropuerto_API/Sql')
$null = $lines.Add('-- Fecha de generacion: ' + (Get-Date -Format 'yyyy-MM-dd HH:mm:ss'))
$null = $lines.Add('-- Cada bloque sigue el CRUD ya definido en los servicios de la API.')
$null = $lines.Add('-- ================================================================')
$null = $lines.Add('SET SERVEROUTPUT ON;')
$null = $lines.Add('')

$serviceDirs = Get-ChildItem -Path $sqlRoot -Directory | Sort-Object Name

foreach ($serviceDir in $serviceDirs) {
    $create = Parse-Insert (Get-SqlText (Join-Path $serviceDir.FullName 'CrearAsync.sql'))
    $update = Parse-Update (Get-SqlText (Join-Path $serviceDir.FullName 'ActualizarAsync.sql'))
    $delete = Parse-Delete (Get-SqlText (Join-Path $serviceDir.FullName 'EliminarAsync.sql'))
    $one = Parse-Select (Get-SqlText (Join-Path $serviceDir.FullName 'ObtenerPorIdAsync.sql'))

    $allPath = Join-Path $serviceDir.FullName 'ObtenerTodosAsync.sql'
    if (-not (Test-Path $allPath)) {
        $allPath = Join-Path $serviceDir.FullName 'ObtenerTodasAsync.sql'
    }
    $all = Parse-Select (Get-SqlText $allPath)

    $tableName = $null
    foreach ($candidate in @($create, $update, $delete, $one, $all)) {
        if ($candidate -and $candidate.Table) {
            $tableName = $candidate.Table
            break
        }
    }

    if (-not $tableName) {
        continue
    }

    $null = $lines.Add('-- ---------------------------------------------------------------')
    $null = $lines.Add('-- SERVICIO: ' + $serviceDir.Name)
    $null = $lines.Add('-- TABLA: ' + $tableName)
    $null = $lines.Add('-- ---------------------------------------------------------------')
    $null = $lines.Add('')

    if ($create) {
        $null = $lines.Add('CREATE OR REPLACE PROCEDURE SP_' + $tableName + '_CREAR (')
        for ($i = 0; $i -lt $create.Columns.Count; $i++) {
            $col = $create.Columns[$i].ToUpper()
            $suffix = if ($i -lt $create.Columns.Count - 1) { ',' } else { '' }
            $null = $lines.Add('    ' + (Get-ParamName $col) + ' IN ' + $tableName + '.' + $col + '%TYPE' + $suffix)
        }
        $null = $lines.Add(')')
        $null = $lines.Add('AS')
        $null = $lines.Add('BEGIN')
        $null = $lines.Add('    INSERT INTO ' + $tableName + ' (')
        for ($i = 0; $i -lt $create.Columns.Count; $i++) {
            $col = $create.Columns[$i].ToUpper()
            $suffix = if ($i -lt $create.Columns.Count - 1) { ',' } else { '' }
            $null = $lines.Add('        ' + $col + $suffix)
        }
        $null = $lines.Add('    )')
        $null = $lines.Add('    VALUES (')
        for ($i = 0; $i -lt $create.Columns.Count; $i++) {
            $col = $create.Columns[$i].ToUpper()
            $suffix = if ($i -lt $create.Columns.Count - 1) { ',' } else { '' }
            $null = $lines.Add('        ' + (Get-ParamName $col) + $suffix)
        }
        $null = $lines.Add('    );')
        $null = $lines.Add("    DBMS_OUTPUT.PUT_LINE('Registro creado en ${tableName}.');")
        $null = $lines.Add('EXCEPTION')
        $null = $lines.Add('    WHEN OTHERS THEN')
        $null = $lines.Add("        DBMS_OUTPUT.PUT_LINE('Error al crear en ${tableName}: ' || SQLERRM);")
        $null = $lines.Add('        RAISE;')
        $null = $lines.Add('END;')
        $null = $lines.Add('/')
        $null = $lines.Add('')
    }

    if ($update) {
        $params = @($update.WhereColumn) + @($update.Assignments | ForEach-Object { $_.Column })
        $null = $lines.Add('CREATE OR REPLACE PROCEDURE SP_' + $tableName + '_ACTUALIZAR (')
        for ($i = 0; $i -lt $params.Count; $i++) {
            $col = $params[$i].ToUpper()
            $suffix = if ($i -lt $params.Count - 1) { ',' } else { '' }
            $null = $lines.Add('    ' + (Get-ParamName $col) + ' IN ' + $tableName + '.' + $col + '%TYPE' + $suffix)
        }
        $null = $lines.Add(')')
        $null = $lines.Add('AS')
        $null = $lines.Add('BEGIN')
        $null = $lines.Add('    UPDATE ' + $tableName)
        $null = $lines.Add('       SET')
        for ($i = 0; $i -lt $update.Assignments.Count; $i++) {
            $col = $update.Assignments[$i].Column
            $suffix = if ($i -lt $update.Assignments.Count - 1) { ',' } else { '' }
            $null = $lines.Add('           ' + $col + ' = ' + (Get-ParamName $col) + $suffix)
        }
        $null = $lines.Add('     WHERE ' + $update.WhereColumn + ' = ' + (Get-ParamName $update.WhereColumn) + ';')
        $null = $lines.Add('')
        $null = $lines.Add('    IF SQL%ROWCOUNT = 0 THEN')
        $null = $lines.Add("        DBMS_OUTPUT.PUT_LINE('No se encontro registro para actualizar en ${tableName}.');")
        $null = $lines.Add('    ELSE')
        $null = $lines.Add("        DBMS_OUTPUT.PUT_LINE('Registro actualizado en ${tableName}.');")
        $null = $lines.Add('    END IF;')
        $null = $lines.Add('EXCEPTION')
        $null = $lines.Add('    WHEN OTHERS THEN')
        $null = $lines.Add("        DBMS_OUTPUT.PUT_LINE('Error al actualizar en ${tableName}: ' || SQLERRM);")
        $null = $lines.Add('        RAISE;')
        $null = $lines.Add('END;')
        $null = $lines.Add('/')
        $null = $lines.Add('')
    }

    if ($delete) {
        $null = $lines.Add('CREATE OR REPLACE PROCEDURE SP_' + $tableName + '_ELIMINAR (')
        $null = $lines.Add('    ' + (Get-ParamName $delete.WhereColumn) + ' IN ' + $tableName + '.' + $delete.WhereColumn + '%TYPE')
        $null = $lines.Add(')')
        $null = $lines.Add('AS')
        $null = $lines.Add('BEGIN')
        $null = $lines.Add('    DELETE FROM ' + $tableName)
        $null = $lines.Add('     WHERE ' + $delete.WhereColumn + ' = ' + (Get-ParamName $delete.WhereColumn) + ';')
        $null = $lines.Add('')
        $null = $lines.Add('    IF SQL%ROWCOUNT = 0 THEN')
        $null = $lines.Add("        DBMS_OUTPUT.PUT_LINE('No se encontro registro para eliminar en ${tableName}.');")
        $null = $lines.Add('    ELSE')
        $null = $lines.Add("        DBMS_OUTPUT.PUT_LINE('Registro eliminado de ${tableName}.');")
        $null = $lines.Add('    END IF;')
        $null = $lines.Add('EXCEPTION')
        $null = $lines.Add('    WHEN OTHERS THEN')
        $null = $lines.Add("        DBMS_OUTPUT.PUT_LINE('Error al eliminar en ${tableName}: ' || SQLERRM);")
        $null = $lines.Add('        RAISE;')
        $null = $lines.Add('END;')
        $null = $lines.Add('/')
        $null = $lines.Add('')
    }

    if ($one -and $one.WhereColumn) {
        $null = $lines.Add('CREATE OR REPLACE PROCEDURE SP_' + $tableName + '_OBTENER_POR_ID (')
        $null = $lines.Add('    ' + (Get-ParamName $one.WhereColumn) + ' IN ' + $tableName + '.' + $one.WhereColumn + '%TYPE,')
        $null = $lines.Add('    P_RESULTADO OUT SYS_REFCURSOR')
        $null = $lines.Add(')')
        $null = $lines.Add('AS')
        $null = $lines.Add('BEGIN')
        $null = $lines.Add('    OPEN P_RESULTADO FOR')
        $null = $lines.Add('        SELECT')
        for ($i = 0; $i -lt $one.Columns.Count; $i++) {
            $col = $one.Columns[$i].ToUpper()
            $suffix = if ($i -lt $one.Columns.Count - 1) { ',' } else { '' }
            $null = $lines.Add('            ' + $col + $suffix)
        }
        $null = $lines.Add('          FROM ' + $tableName)
        $null = $lines.Add('         WHERE ' + $one.WhereColumn + ' = ' + (Get-ParamName $one.WhereColumn) + ';')
        $null = $lines.Add('END;')
        $null = $lines.Add('/')
        $null = $lines.Add('')
    }

    if ($all) {
        $null = $lines.Add('CREATE OR REPLACE PROCEDURE SP_' + $tableName + '_OBTENER_TODOS (')
        $null = $lines.Add('    P_RESULTADO OUT SYS_REFCURSOR')
        $null = $lines.Add(')')
        $null = $lines.Add('AS')
        $null = $lines.Add('BEGIN')
        $null = $lines.Add('    OPEN P_RESULTADO FOR')
        $null = $lines.Add('        SELECT')
        for ($i = 0; $i -lt $all.Columns.Count; $i++) {
            $col = $all.Columns[$i].ToUpper()
            $suffix = if ($i -lt $all.Columns.Count - 1) { ',' } else { '' }
            $null = $lines.Add('            ' + $col + $suffix)
        }
        $null = $lines.Add('          FROM ' + $tableName)
        if (-not [string]::IsNullOrWhiteSpace($all.OrderBy)) {
            $null = $lines.Add('         ORDER BY ' + $all.OrderBy.ToUpper() + ';')
        } else {
            $null = $lines.Add(';')
        }
        $null = $lines.Add('END;')
        $null = $lines.Add('/')
        $null = $lines.Add('')
    }
}

$content = [string]::Join([Environment]::NewLine, $lines)
Set-Content -Path $outputPath -Value $content -Encoding UTF8

Write-Output ('Servicios procesados: ' + $serviceDirs.Count)
Write-Output ('Lineas generadas: ' + $lines.Count)
Write-Output ('Archivo generado: ' + $outputPath)
