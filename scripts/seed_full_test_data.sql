SET SERVEROUTPUT ON;

DECLARE
    v_suffix                  VARCHAR2(30) := TO_CHAR(SYSTIMESTAMP, 'YYYYMMDDHH24MISSFF3');
    v_num_suffix              NUMBER := TO_NUMBER(TO_CHAR(SYSTIMESTAMP, 'FF3'));

    v_aerolinea_id            NUMBER;
    v_aeropuerto_origen_id    NUMBER;
    v_aeropuerto_destino_id   NUMBER;
    v_categoria_id            NUMBER;
    v_modelo_id               NUMBER;
    v_metodo_pago_id          NUMBER;
    v_promocion_id            NUMBER;
    v_proveedor_id            NUMBER;
    v_departamento_id         NUMBER;
    v_puesto_id               NUMBER;
    v_empleado_id             NUMBER;
    v_hangar_id               NUMBER;
    v_punto_venta_id          NUMBER;
    v_avion_id                NUMBER;
    v_programa_id             NUMBER;
    v_dia_vuelo_id            NUMBER;
    v_escala_id               NUMBER;
    v_vuelo_id                NUMBER;
    v_pasajero_id             NUMBER;
    v_reserva_id              NUMBER;
    v_transaccion_id          NUMBER;
    v_venta_id                NUMBER;
    v_detalle_venta_id        NUMBER;
    v_tripulacion_id          NUMBER;
    v_asistencia_id           NUMBER;
    v_licencia_id             NUMBER;
    v_planilla_id             NUMBER;
    v_sesion_id               NUMBER;
    v_busqueda_id             NUMBER;
    v_click_id                NUMBER;
    v_carrito_id              NUMBER;
    v_item_carrito_id         NUMBER;
    v_mantenimiento_id        NUMBER;
    v_repuesto_id             NUMBER;
    v_movimiento_id           NUMBER;
    v_orden_compra_id         NUMBER;
    v_detalle_orden_id        NUMBER;
    v_repuesto_utilizado_id   NUMBER;
    v_arresto_id              NUMBER;
    v_objeto_id               NUMBER;
    v_auditoria_id            NUMBER;
    v_preferencia_id          NUMBER;
    v_uso_promocion_id        NUMBER;
    v_asignacion_hangar_id    NUMBER;

    v_airline_iata            VARCHAR2(3) := 'A' || LPAD(MOD(v_num_suffix, 99), 2, '0');
    v_airline_icao            VARCHAR2(4) := 'A' || LPAD(MOD(v_num_suffix, 999), 3, '0');
    v_airport1_iata           VARCHAR2(3) := 'G' || LPAD(MOD(v_num_suffix + 10, 99), 2, '0');
    v_airport1_icao           VARCHAR2(4) := 'G' || LPAD(MOD(v_num_suffix + 10, 999), 3, '0');
    v_airport2_iata           VARCHAR2(3) := 'S' || LPAD(MOD(v_num_suffix + 20, 99), 2, '0');
    v_airport2_icao           VARCHAR2(4) := 'S' || LPAD(MOD(v_num_suffix + 20, 999), 3, '0');
BEGIN
    INSERT INTO AER_AEROLINEA
    (
        ARL_CODIGO_AEROLINEA,
        ARL_NOMBRE,
        ARL_PAIS_ORIGEN,
        ARL_CODIGO_IATA,
        ARL_CODIGO_ICAO,
        ARL_ESTADO,
        ARL_TELEFONO,
        ARL_EMAIL,
        ARL_SITIO_WEB
    )
    VALUES
    (
        'ARL-' || SUBSTR(v_suffix, -6),
        'AEROLINEA DEMO ' || SUBSTR(v_suffix, -4),
        'GUATEMALA',
        v_airline_iata,
        v_airline_icao,
        'ACTIVA',
        '55550001',
        'airline' || SUBSTR(v_suffix, -6) || '@demo.com',
        'https://airline-demo.com'
    )
    RETURNING ARL_ID INTO v_aerolinea_id;

    INSERT INTO AER_AEROPUERTO
    (
        AER_CODIGO_AEROPUERTO,
        AER_NOMBRE,
        AER_CIUDAD,
        AER_PAIS,
        AER_ZONA_HORARIA,
        AER_ESTADO,
        AER_TIPO,
        AER_LATITUD,
        AER_LONGITUD,
        AER_CODIGO_IATA,
        AER_CODIGO_ICAO
    )
    VALUES
    (
        'AER-' || SUBSTR(v_suffix, -6),
        'AEROPUERTO ORIGEN DEMO',
        'GUATEMALA',
        'GUATEMALA',
        'America/Guatemala',
        'ACTIVO',
        'INTERNACIONAL',
        14.583300,
        -90.527500,
        v_airport1_iata,
        v_airport1_icao
    )
    RETURNING AER_ID INTO v_aeropuerto_origen_id;

    INSERT INTO AER_AEROPUERTO
    (
        AER_CODIGO_AEROPUERTO,
        AER_NOMBRE,
        AER_CIUDAD,
        AER_PAIS,
        AER_ZONA_HORARIA,
        AER_ESTADO,
        AER_TIPO,
        AER_LATITUD,
        AER_LONGITUD,
        AER_CODIGO_IATA,
        AER_CODIGO_ICAO
    )
    VALUES
    (
        'AED-' || SUBSTR(v_suffix, -6),
        'AEROPUERTO DESTINO DEMO',
        'SAN SALVADOR',
        'EL SALVADOR',
        'America/El_Salvador',
        'ACTIVO',
        'INTERNACIONAL',
        13.440900,
        -89.055700,
        v_airport2_iata,
        v_airport2_icao
    )
    RETURNING AER_ID INTO v_aeropuerto_destino_id;

    INSERT INTO AER_CATEGORIAREPUESTO
    (
        CAT_NOMBRE,
        CAT_DESCRIPCION
    )
    VALUES
    (
        'CATEGORIA DEMO ' || SUBSTR(v_suffix, -4),
        'Categoria de repuestos de prueba'
    )
    RETURNING CAT_ID_CATEGORIA INTO v_categoria_id;

    INSERT INTO AER_MODELOAVION
    (
        MOD_NOMBRE_MODELO,
        MOD_FABRICANTE,
        MOD_CAPACIDAD_PASAJEROS,
        MOD_CAPACIDAD_CARGA,
        MOD_ALCANCE_KM,
        MOD_VELOCIDAD_CRUCERO,
        MOD_ANIO_INTRODUCCION,
        MOD_TIPO_MOTOR
    )
    VALUES
    (
        'MODELO DEMO ' || SUBSTR(v_suffix, -4),
        'BOEING',
        180,
        22000,
        5500,
        850,
        2018,
        'TURBOFAN'
    )
    RETURNING MOD_ID_MODELO INTO v_modelo_id;

    INSERT INTO AER_METODOPAGO
    (
        MET_NOMBRE,
        MET_TIPO,
        MET_ESTADO,
        MET_COMISION_PORCENTAJE
    )
    VALUES
    (
        'TARJETA VISA DEMO',
        'TARJETA_CREDITO',
        'ACTIVO',
        3.50
    )
    RETURNING MET_ID_METODO_PAGO INTO v_metodo_pago_id;

    INSERT INTO AER_PROMOCION
    (
        PRO_CODIGO_PROMOCION,
        PRO_DESCRIPCION,
        PRO_TIPO_DESCUENTO,
        PRO_VALOR_DESCUENTO,
        PRO_FECHA_INICIO,
        PRO_FECHA_FIN,
        PRO_USOS_MAXIMOS,
        PRO_USOS_ACTUALES,
        PRO_ESTADO
    )
    VALUES
    (
        'PROMO-' || SUBSTR(v_suffix, -6),
        'Promocion general de prueba',
        'PORCENTAJE',
        10.00,
        TRUNC(SYSDATE),
        TRUNC(SYSDATE) + 30,
        100,
        0,
        'ACTIVA'
    )
    RETURNING PRO_ID_PROMOCION INTO v_promocion_id;

    INSERT INTO AER_PROVEEDOR
    (
        PRV_NOMBRE_EMPRESA,
        PRV_NIT,
        PRV_DIRECCION,
        PRV_TELEFONO,
        PRV_EMAIL,
        PRV_CONTACTO_PRINCIPAL,
        PRV_PAIS,
        PRV_ESTADO,
        PRV_CALIFICACION
    )
    VALUES
    (
        'PROVEEDOR DEMO ' || SUBSTR(v_suffix, -4),
        'NIT-' || SUBSTR(v_suffix, -6),
        'Zona 10, Ciudad de Guatemala',
        '55550002',
        'proveedor' || SUBSTR(v_suffix, -6) || '@demo.com',
        'Carlos Lopez',
        'GUATEMALA',
        'ACTIVO',
        4.50
    )
    RETURNING PRV_ID_PROVEEDOR INTO v_proveedor_id;

    INSERT INTO AER_DEPARTAMENTO
    (
        DEP_NOMBRE,
        DEP_DESCRIPCION,
        DEP_ID_AEROPUERTO,
        DEP_ESTADO
    )
    VALUES
    (
        'OPERACIONES DEMO',
        'Departamento de operaciones para datos de prueba',
        v_aeropuerto_origen_id,
        'ACTIVO'
    )
    RETURNING DEP_ID_DEPARTAMENTO INTO v_departamento_id;

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
        'PILOTO DEMO',
        v_departamento_id,
        'Puesto de piloto de prueba',
        9000.00,
        15000.00,
        'S'
    )
    RETURNING PUE_ID_PUESTO INTO v_puesto_id;

    INSERT INTO AER_EMPLEADO
    (
        EMP_NUMERO_EMPLEADO,
        EMP_NOMBRES,
        EMP_APELLIDOS,
        EMP_FECHA_NACIMIENTO,
        EMP_DPI,
        EMP_NIT,
        EMP_DIRECCION,
        EMP_TELEFONO,
        EMP_EMAIL,
        EMP_FECHA_CONTRATACION,
        EMP_ID_PUESTO,
        EMP_ID_DEPARTAMENTO,
        EMP_SALARIO_ACTUAL,
        EMP_TIPO_CONTRATO,
        EMP_ESTADO,
        EMP_FOTO
    )
    VALUES
    (
        'EMP-' || SUBSTR(v_suffix, -6),
        'JUAN',
        'PEREZ DEMO',
        DATE '1990-04-15',
        'DPI-' || SUBSTR(v_suffix, -10),
        'NITEMP-' || SUBSTR(v_suffix, -6),
        'Zona 1, Guatemala',
        '55550003',
        'empleado' || SUBSTR(v_suffix, -6) || '@demo.com',
        TRUNC(SYSDATE) - 300,
        v_puesto_id,
        v_departamento_id,
        12000.00,
        'PERMANENTE',
        'ACTIVO',
        NULL
    )
    RETURNING EMP_ID_EMPLEADO INTO v_empleado_id;

    INSERT INTO AER_HANGAR
    (
        HAN_CODIGO_HANGAR,
        HAN_NOMBRE,
        HAN_ID_AEROPUERTO,
        HAN_CAPACIDAD_AVIONES,
        HAN_AREA_M2,
        HAN_ALTURA_MAXIMA,
        HAN_TIPO,
        HAN_ESTADO
    )
    VALUES
    (
        'HAN-' || SUBSTR(v_suffix, -5),
        'HANGAR DEMO',
        v_aeropuerto_origen_id,
        4,
        1800.50,
        22.50,
        'MANTENIMIENTO',
        'DISPONIBLE'
    )
    RETURNING HAN_ID_HANGAR INTO v_hangar_id;

    INSERT INTO AER_PUNTOVENTA
    (
        PUV_CODIGO_PUNTO,
        PUV_NOMBRE,
        PUV_ID_AEROPUERTO,
        PUV_UBICACION,
        PUV_ESTADO
    )
    VALUES
    (
        'PV-' || SUBSTR(v_suffix, -5),
        'PUNTO VENTA DEMO',
        v_aeropuerto_origen_id,
        'Terminal principal',
        'ACTIVO'
    )
    RETURNING PUV_ID_PUNTO_VENTA INTO v_punto_venta_id;

    INSERT INTO AER_AVION
    (
        AVI_MATRICULA,
        AVI_ID_MODELO,
        AVI_ID_AEROLINEA,
        AVI_ANIO_FABRICACION,
        AVI_ESTADO,
        AVI_ULTIMA_REVISION,
        AVI_PROXIMA_REVISION,
        AVI_HORAS_VUELO
    )
    VALUES
    (
        'TG-' || SUBSTR(v_suffix, -5),
        v_modelo_id,
        v_aerolinea_id,
        2020,
        'ACTIVO',
        TRUNC(SYSDATE) - 15,
        TRUNC(SYSDATE) + 180,
        3200
    )
    RETURNING AVI_ID INTO v_avion_id;

    INSERT INTO AER_PROGRAMAVUELO
    (
        PRV_NUMERO_VUELO,
        PRV_ID_AEROLINEA,
        PRV_ID_AEROPUERTO_ORIGEN,
        PRV_ID_AEROPUERTO_DESTINO,
        PRV_HORA_SALIDA_PROGRAMADA,
        PRV_HORA_LLEGADA_PROGRAMADA,
        PRV_DURACION_ESTIMADA,
        PRV_TIPO_VUELO,
        PRV_ESTADO
    )
    VALUES
    (
        'AV-' || SUBSTR(v_suffix, -6),
        v_aerolinea_id,
        v_aeropuerto_origen_id,
        v_aeropuerto_destino_id,
        '08:30',
        '10:00',
        90,
        'INTERNACIONAL',
        'ACTIVO'
    )
    RETURNING PRV_ID INTO v_programa_id;

    INSERT INTO AER_DIASVUELO
    (
        DIA_ID_PROGRAMA_VUELO,
        DIA_DIA_SEMANA
    )
    VALUES
    (
        v_programa_id,
        2
    )
    RETURNING DIA_ID_DIA_VUELO INTO v_dia_vuelo_id;

    INSERT INTO AER_ESCALATECNICA
    (
        ESC_ID_PROGRAMA_VUELO,
        ESC_ID_AEROPUERTO,
        ESC_NUMERO_ORDEN,
        ESC_HORA_LLEGADA_ESTIMADA,
        ESC_HORA_SALIDA_ESTIMADA,
        ESC_DURACION_ESCALA
    )
    VALUES
    (
        v_programa_id,
        v_aeropuerto_destino_id,
        1,
        '09:15',
        '09:35',
        20
    )
    RETURNING ESC_ID_ESCALA INTO v_escala_id;

    INSERT INTO AER_VUELO
    (
        VUE_ID_PROGRAMA_VUELO,
        VUE_ID_AVION,
        VUE_FECHA_VUELO,
        VUE_HORA_SALIDA_REAL,
        VUE_HORA_LLEGADA_REAL,
        VUE_PLAZAS_OCUPADAS,
        VUE_PLAZAS_VACIAS,
        VUE_ESTADO,
        VUE_FECHA_REPROGRAMACION,
        VUE_MOTIVO_CANCELACION,
        VUE_PUERTA_EMBARQUE,
        VUE_TERMINAL,
        VUE_RETRASO_MINUTOS
    )
    VALUES
    (
        v_programa_id,
        v_avion_id,
        TRUNC(SYSDATE) + 7,
        SYSTIMESTAMP + INTERVAL '7' DAY + INTERVAL '8' HOUR + INTERVAL '35' MINUTE,
        SYSTIMESTAMP + INTERVAL '7' DAY + INTERVAL '10' HOUR + INTERVAL '5' MINUTE,
        25,
        155,
        'ATERRIZADO',
        NULL,
        NULL,
        'B4',
        'T1',
        5
    )
    RETURNING VUE_ID_VUELO INTO v_vuelo_id;

    INSERT INTO AER_PASAJERO
    (
        PAS_NUMERO_DOCUMENTO,
        PAS_TIPO_DOCUMENTO,
        PAS_NOMBRES,
        PAS_APELLIDOS,
        PAS_FECHA_NACIMIENTO,
        PAS_NACIONALIDAD,
        PAS_SEXO,
        PAS_TELEFONO,
        PAS_EMAIL
    )
    VALUES
    (
        'DOC-' || SUBSTR(v_suffix, -10),
        'PASAPORTE',
        'MARIA',
        'GOMEZ DEMO',
        DATE '1996-08-21',
        'GUATEMALTECA',
        'F',
        '55550004',
        'pasajero' || SUBSTR(v_suffix, -6) || '@demo.com'
    )
    RETURNING PAS_ID_PASAJERO INTO v_pasajero_id;

    INSERT INTO AER_RESERVA
    (
        RES_ID_VUELO,
        RES_ID_PASAJERO,
        RES_NUMERO_ASIENTO,
        RES_CLASE,
        RES_FECHA_RESERVA,
        RES_ESTADO,
        RES_EQUIPAJE_FACTURADO,
        RES_PESO_EQUIPAJE,
        RES_TARIFA_PAGADA,
        RES_CODIGO_RESERVA
    )
    VALUES
    (
        v_vuelo_id,
        v_pasajero_id,
        '12A',
        'ECONOMICA',
        TRUNC(SYSDATE),
        'CONFIRMADA',
        1,
        18,
        950.00,
        'RES-' || SUBSTR(v_suffix, -6)
    )
    RETURNING RES_ID_RESERVA INTO v_reserva_id;

    INSERT INTO AER_TRANSACCIONPAGO
    (
        TRA_ID_RESERVA,
        TRA_ID_METODO_PAGO,
        TRA_MONTO_TOTAL,
        TRA_MONEDA,
        TRA_FECHA_TRANSACCION,
        TRA_ESTADO,
        TRA_NUMERO_AUTORIZACION,
        TRA_REFERENCIA_EXTERNA,
        TRA_IP_CLIENTE,
        TRA_DETALLES_TARJETA
    )
    VALUES
    (
        v_reserva_id,
        v_metodo_pago_id,
        950.00,
        'GTQ',
        SYSTIMESTAMP,
        'APROBADA',
        'AUTH-' || SUBSTR(v_suffix, -6),
        'REF-' || SUBSTR(v_suffix, -6),
        '192.168.1.10',
        'VISA **** 4242'
    )
    RETURNING TRA_ID_TRANSACCION INTO v_transaccion_id;

    INSERT INTO AER_VENTABOLETO
    (
        VEN_NUMERO_VENTA,
        VEN_ID_PUNTO_VENTA,
        VEN_ID_EMPLEADO_VENDEDOR,
        VEN_ID_PASAJERO,
        VEN_FECHA_VENTA,
        VEN_MONTO_SUBTOTAL,
        VEN_IMPUESTOS,
        VEN_DESCUENTOS,
        VEN_MONTO_TOTAL,
        VEN_ID_METODO_PAGO,
        VEN_CANAL_VENTA,
        VEN_ESTADO
    )
    VALUES
    (
        'VEN-' || SUBSTR(v_suffix, -6),
        v_punto_venta_id,
        v_empleado_id,
        v_pasajero_id,
        SYSTIMESTAMP,
        900.00,
        75.00,
        25.00,
        950.00,
        v_metodo_pago_id,
        'WEB',
        'COMPLETADA'
    )
    RETURNING VEN_ID_VENTA INTO v_venta_id;

    INSERT INTO AER_DETALLEVENTABOLETO
    (
        DEV_ID_VENTA,
        DEV_ID_RESERVA,
        DEV_PRECIO_BASE,
        DEV_CARGOS_ADICIONALES
    )
    VALUES
    (
        v_venta_id,
        v_reserva_id,
        900.00,
        50.00
    )
    RETURNING DEV_ID_DETALLE_VENTA INTO v_detalle_venta_id;

    INSERT INTO AER_TRIPULACION
    (
        TRI_ID_VUELO,
        TRI_ID_EMPLEADO,
        TRI_ROL,
        TRI_HORAS_VUELO
    )
    VALUES
    (
        v_vuelo_id,
        v_empleado_id,
        'PILOTO',
        1.50
    )
    RETURNING TRI_ID_TRIPULACION INTO v_tripulacion_id;

    INSERT INTO AER_ASISTENCIA
    (
        ASI_ID_EMPLEADO,
        ASI_FECHA,
        ASI_HORA_ENTRADA,
        ASI_HORA_SALIDA,
        ASI_HORAS_TRABAJADAS,
        ASI_TIPO,
        ASI_ESTADO
    )
    VALUES
    (
        v_empleado_id,
        TRUNC(SYSDATE),
        SYSTIMESTAMP - INTERVAL '9' HOUR,
        SYSTIMESTAMP,
        9.00,
        'REGULAR',
        'PRESENTE'
    )
    RETURNING ASI_ID_ASISTENCIA INTO v_asistencia_id;

    INSERT INTO AER_LICENCIAEMPLEADO
    (
        LIC_ID_EMPLEADO,
        LIC_TIPO_LICENCIA,
        LIC_NUMERO_LICENCIA,
        LIC_FECHA_EMISION,
        LIC_FECHA_VENCIMIENTO,
        LIC_AUTORIDAD_EMISORA,
        LIC_ESTADO
    )
    VALUES
    (
        v_empleado_id,
        'PILOTO_COMERCIAL',
        'LIC-' || SUBSTR(v_suffix, -6),
        TRUNC(SYSDATE) - 180,
        TRUNC(SYSDATE) + 365,
        'DGAC',
        'VIGENTE'
    )
    RETURNING LIC_ID_LICENCIA INTO v_licencia_id;

    INSERT INTO AER_PLANILLA
    (
        PLA_ID_EMPLEADO,
        PLA_PERIODO_INICIO,
        PLA_PERIODO_FIN,
        PLA_SALARIO_BASE,
        PLA_BONIFICACIONES,
        PLA_HORAS_EXTRA,
        PLA_DEDUCCIONES,
        PLA_SALARIO_NETO,
        PLA_FECHA_PAGO,
        PLA_ESTADO
    )
    VALUES
    (
        v_empleado_id,
        TRUNC(SYSDATE, 'MM'),
        LAST_DAY(SYSDATE),
        12000.00,
        500.00,
        250.00,
        300.00,
        12450.00,
        TRUNC(SYSDATE) + 3,
        'PAGADA'
    )
    RETURNING PLA_ID_PLANILLA INTO v_planilla_id;

    INSERT INTO AER_SESIONUSUARIO
    (
        SES_SESION_ID,
        SES_ID_PASAJERO,
        SES_IP_ADDRESS,
        SES_NAVEGADOR,
        SES_SISTEMA_OPERATIVO,
        SES_DISPOSITIVO,
        SES_FECHA_INICIO,
        SES_FECHA_FIN,
        SES_DURACION_SEGUNDOS
    )
    VALUES
    (
        'SES-' || SUBSTR(v_suffix, -10),
        v_pasajero_id,
        '10.0.0.15',
        'Chrome',
        'Windows 11',
        'Desktop',
        SYSTIMESTAMP - INTERVAL '45' MINUTE,
        SYSTIMESTAMP,
        2700
    )
    RETURNING SES_ID_SESION INTO v_sesion_id;

    INSERT INTO AER_BUSQUEDAVUELO
    (
        BUS_ID_SESION,
        BUS_ID_AEROPUERTO_ORIGEN,
        BUS_ID_AEROPUERTO_DESTINO,
        BUS_FECHA_IDA,
        BUS_FECHA_VUELTA,
        BUS_NUMERO_PASAJEROS,
        BUS_CLASE,
        BUS_FECHA_BUSQUEDA,
        BUS_SE_CONVIRTIO_COMPRA
    )
    VALUES
    (
        v_sesion_id,
        v_aeropuerto_origen_id,
        v_aeropuerto_destino_id,
        TRUNC(SYSDATE) + 7,
        TRUNC(SYSDATE) + 10,
        1,
        'ECONOMICA',
        SYSTIMESTAMP,
        'S'
    )
    RETURNING BUS_ID_BUSQUEDA INTO v_busqueda_id;

    INSERT INTO AER_CLICKDESTINO
    (
        CLI_ID_SESION,
        CLI_ID_AEROPUERTO_DESTINO,
        CLI_FECHA_CLICK,
        CLI_ORIGEN_BUSQUEDA,
        CLI_FECHA_VIAJE_BUSCADA,
        CLI_NUMERO_PASAJEROS,
        CLI_CLASE_BUSCADA
    )
    VALUES
    (
        v_sesion_id,
        v_aeropuerto_destino_id,
        SYSTIMESTAMP,
        'WEB',
        TRUNC(SYSDATE) + 7,
        1,
        'ECONOMICA'
    )
    RETURNING CLI_ID_CLICK INTO v_click_id;

    INSERT INTO AER_CARRITOCOMPRA
    (
        CAR_ID_PASAJERO,
        CAR_SESION_ID,
        CAR_FECHA_CREACION,
        CAR_FECHA_EXPIRACION,
        CAR_ESTADO
    )
    VALUES
    (
        v_pasajero_id,
        'CART-' || SUBSTR(v_suffix, -10),
        SYSTIMESTAMP,
        SYSTIMESTAMP + INTERVAL '2' HOUR,
        'ACTIVO'
    )
    RETURNING CAR_ID_CARRITO INTO v_carrito_id;

    INSERT INTO AER_ITEMCARRITO
    (
        ITE_ID_CARRITO,
        ITE_ID_VUELO,
        ITE_NUMERO_ASIENTO,
        ITE_CLASE,
        ITE_PRECIO_UNITARIO,
        ITE_CANTIDAD
    )
    VALUES
    (
        v_carrito_id,
        v_vuelo_id,
        '14B',
        'ECONOMICA',
        950.00,
        1
    )
    RETURNING ITE_ID_ITEM_CARRITO INTO v_item_carrito_id;

    INSERT INTO AER_MANTENIMIENTOAVION
    (
        MAN_ID_AVION,
        MAN_TIPO_MANTENIMIENTO,
        MAN_FECHA_INICIO,
        MAN_FECHA_FIN_ESTIMADA,
        MAN_FECHA_FIN_REAL,
        MAN_DESCRIPCION_TRABAJO,
        MAN_ID_EMPLEADO_RESPONSABLE,
        MAN_COSTO_MANO_OBRA,
        MAN_COSTO_REPUESTOS,
        MAN_COSTO_TOTAL,
        MAN_ESTADO
    )
    VALUES
    (
        v_avion_id,
        'PREVENTIVO',
        SYSTIMESTAMP - INTERVAL '2' DAY,
        SYSTIMESTAMP + INTERVAL '1' DAY,
        NULL,
        'Inspeccion general y mantenimiento preventivo de prueba.',
        v_empleado_id,
        1500.00,
        800.00,
        2300.00,
        'PROGRAMADO'
    )
    RETURNING MAN_ID_MANTENIMIENTO INTO v_mantenimiento_id;

    INSERT INTO AER_REPUESTO
    (
        REP_CODIGO_REPUESTO,
        REP_NOMBRE,
        REP_DESCRIPCION,
        REP_ID_CATEGORIA,
        REP_ID_MODELO_AVION,
        REP_NUMERO_PARTE_FABRICANTE,
        REP_STOCK_MINIMO,
        REP_STOCK_ACTUAL,
        REP_STOCK_MAXIMO,
        REP_PRECIO_UNITARIO,
        REP_UBICACION_BODEGA,
        REP_ESTADO
    )
    VALUES
    (
        'REP-' || SUBSTR(v_suffix, -6),
        'FILTRO DEMO',
        'Repuesto de prueba para mantenimiento',
        v_categoria_id,
        v_modelo_id,
        'PART-' || SUBSTR(v_suffix, -6),
        2,
        15,
        40,
        425.00,
        'BOD-A1',
        'ACTIVO'
    )
    RETURNING REP_ID_REPUESTO INTO v_repuesto_id;

    INSERT INTO AER_MOVIMIENTOREPUESTO
    (
        MOV_ID_REPUESTO,
        MOV_TIPO_MOVIMIENTO,
        MOV_CANTIDAD,
        MOV_FECHA_MOVIMIENTO,
        MOV_ID_EMPLEADO,
        MOV_MOTIVO,
        MOV_REFERENCIA
    )
    VALUES
    (
        v_repuesto_id,
        'ENTRADA',
        10,
        SYSTIMESTAMP,
        v_empleado_id,
        'Ingreso inicial de inventario de prueba',
        'MOV-' || SUBSTR(v_suffix, -6)
    )
    RETURNING MOV_ID_MOVIMIENTO INTO v_movimiento_id;

    INSERT INTO AER_ORDENCOMPRAREPUESTO
    (
        ORC_NUMERO_ORDEN,
        ORC_ID_PROVEEDOR,
        ORC_FECHA_ORDEN,
        ORC_FECHA_ENTREGA_ESPERADA,
        ORC_FECHA_ENTREGA_REAL,
        ORC_MONTO_TOTAL,
        ORC_ESTADO,
        ORC_ID_EMPLEADO_SOLICITA,
        ORC_OBSERVACIONES
    )
    VALUES
    (
        'ORD-' || SUBSTR(v_suffix, -6),
        v_proveedor_id,
        TRUNC(SYSDATE),
        TRUNC(SYSDATE) + 5,
        NULL,
        1275.00,
        'PENDIENTE',
        v_empleado_id,
        'Orden de compra de prueba'
    )
    RETURNING ORC_ID_ORDEN_COMPRA INTO v_orden_compra_id;

    INSERT INTO AER_DETALLEORDENCOMPRA
    (
        DET_ID_ORDEN_COMPRA,
        DET_ID_REPUESTO,
        DET_CANTIDAD,
        DET_PRECIO_UNITARIO,
        DET_SUBTOTAL
    )
    VALUES
    (
        v_orden_compra_id,
        v_repuesto_id,
        3,
        425.00,
        1275.00
    )
    RETURNING DET_ID_DETALLE INTO v_detalle_orden_id;

    INSERT INTO AER_REPUESTOUTILIZADO
    (
        RUT_ID_MANTENIMIENTO,
        RUT_ID_REPUESTO,
        RUT_CANTIDAD
    )
    VALUES
    (
        v_mantenimiento_id,
        v_repuesto_id,
        2
    )
    RETURNING RUT_ID_REPUESTO_UTILIZADO INTO v_repuesto_utilizado_id;

    INSERT INTO AER_ARRESTO
    (
        ARR_ID_PASAJERO,
        ARR_ID_VUELO,
        ARR_ID_AEROPUERTO,
        ARR_FECHA_HORA_ARRESTO,
        ARR_MOTIVO,
        ARR_AUTORIDAD_CARGO,
        ARR_DESCRIPCION_INCIDENTE,
        ARR_UBICACION_ARRESTO,
        ARR_ESTADO_CASO,
        ARR_NUMERO_EXPEDIENTE
    )
    VALUES
    (
        v_pasajero_id,
        v_vuelo_id,
        v_aeropuerto_origen_id,
        SYSTIMESTAMP,
        'Revision preventiva de seguridad',
        'Policia Aeroportuaria',
        'Incidente de prueba registrado para poblar la tabla de arrestos.',
        'TERMINAL',
        'EN_PROCESO',
        'EXP-' || SUBSTR(v_suffix, -6)
    )
    RETURNING ARR_ID_ARRESTO INTO v_arresto_id;

    INSERT INTO AER_OBJETOPERDIDO
    (
        OBJ_ID_VUELO,
        OBJ_ID_AEROPUERTO,
        OBJ_DESCRIPCION,
        OBJ_FECHA_REPORTE,
        OBJ_UBICACION_ENCONTRADO,
        OBJ_ESTADO,
        OBJ_NOMBRE_REPORTANTE,
        OBJ_CONTACTO_REPORTANTE,
        OBJ_FECHA_ENTREGA,
        OBJ_NOMBRE_RECLAMANTE
    )
    VALUES
    (
        v_vuelo_id,
        v_aeropuerto_origen_id,
        'Maletin negro encontrado en sala de abordaje',
        TRUNC(SYSDATE),
        'Puerta B4',
        'REPORTADO',
        'Agente de seguridad',
        '55550005',
        NULL,
        NULL
    )
    RETURNING OBJ_ID_OBJETO INTO v_objeto_id;

    INSERT INTO AER_AUDITORIA
    (
        AUD_TABLA_AFECTADA,
        AUD_OPERACION,
        AUD_USUARIO,
        AUD_FECHA_HORA,
        AUD_IP_ADDRESS,
        AUD_DATOS_ANTERIORES,
        AUD_DATOS_NUEVOS
    )
    VALUES
    (
        'AER_RESERVA',
        'INSERT',
        'AEROPUERTO_AURORA',
        SYSTIMESTAMP,
        '127.0.0.1',
        '{"antes":null}',
        '{"despues":"reserva de prueba creada"}'
    )
    RETURNING AUD_ID_AUDITORIA INTO v_auditoria_id;

    INSERT INTO AER_PREFERENCIACLIENTE
    (
        PRF_ID_PASAJERO,
        PRF_TIPO_PREFERENCIA,
        PRF_VALOR_PREFERENCIA,
        PRF_FECHA_REGISTRO
    )
    VALUES
    (
        v_pasajero_id,
        'ASIENTO_VENTANA',
        'Fila media',
        SYSTIMESTAMP
    )
    RETURNING PRF_ID_PREFERENCIA INTO v_preferencia_id;

    INSERT INTO AER_USOPROMOCION
    (
        USO_ID_PROMOCION,
        USO_ID_RESERVA,
        USO_FECHA_USO,
        USO_MONTO_DESCUENTO
    )
    VALUES
    (
        v_promocion_id,
        v_reserva_id,
        SYSTIMESTAMP,
        25.00
    )
    RETURNING USO_ID_USO INTO v_uso_promocion_id;

    INSERT INTO AER_ASIGNACIONHANGAR
    (
        ASH_ID_HANGAR,
        ASH_ID_AVION,
        ASH_FECHA_ENTRADA,
        ASH_FECHA_SALIDA_PROGRAMADA,
        ASH_FECHA_SALIDA_REAL,
        ASH_MOTIVO,
        ASH_COSTO_HORA,
        ASH_COSTO_TOTAL,
        ASH_ESTADO
    )
    VALUES
    (
        v_hangar_id,
        v_avion_id,
        SYSTIMESTAMP - INTERVAL '3' DAY,
        SYSTIMESTAMP + INTERVAL '1' DAY,
        NULL,
        'Inspeccion preventiva',
        150.00,
        600.00,
        'ACTIVA'
    )
    RETURNING ASH_ID_ASIGNACION INTO v_asignacion_hangar_id;

    COMMIT;

    DBMS_OUTPUT.PUT_LINE('Datos de prueba insertados correctamente en toda la base.');
    DBMS_OUTPUT.PUT_LINE('AEROLINEA: ' || v_aerolinea_id);
    DBMS_OUTPUT.PUT_LINE('AEROPUERTO ORIGEN: ' || v_aeropuerto_origen_id);
    DBMS_OUTPUT.PUT_LINE('AEROPUERTO DESTINO: ' || v_aeropuerto_destino_id);
    DBMS_OUTPUT.PUT_LINE('EMPLEADO: ' || v_empleado_id);
    DBMS_OUTPUT.PUT_LINE('PASAJERO: ' || v_pasajero_id);
    DBMS_OUTPUT.PUT_LINE('VUELO: ' || v_vuelo_id);
    DBMS_OUTPUT.PUT_LINE('RESERVA: ' || v_reserva_id);
    DBMS_OUTPUT.PUT_LINE('VENTA: ' || v_venta_id);
    DBMS_OUTPUT.PUT_LINE('ORDEN COMPRA: ' || v_orden_compra_id);
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        DBMS_OUTPUT.PUT_LINE('Error al insertar datos de prueba: ' || SQLERRM);
        RAISE;
END;
/
