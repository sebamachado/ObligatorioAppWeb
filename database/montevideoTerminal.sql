USE MASTER
GO

IF EXISTS(SELECT * FROM sys.databases WHERE NAME = 'montevideoTerminal')
BEGIN
	DROP DATABASE montevideoTerminal
END
GO

-- Crear la base de datos
CREATE DATABASE montevideoTerminal;
GO

-- Usar la base de datos creada
USE montevideoTerminal;
GO

-- Crear la tabla Companias
CREATE TABLE Companias (
    nombre VARCHAR(100) PRIMARY KEY,
    direccion_matriz VARCHAR(255) NOT NULL
);
GO

-- Crear la tabla Telefonos_Contacto
CREATE TABLE Telefonos_Contacto (
    nombre_compania VARCHAR(100) NOT NULL,
    telefono VARCHAR(20) NOT NULL,
	PRIMARY KEY (nombre_compania, telefono),
    FOREIGN KEY (nombre_compania) REFERENCES Companias(nombre)
);
GO

-- Crear la tabla Terminales
CREATE TABLE Terminales (
    id CHAR(6) PRIMARY KEY,
    ciudad VARCHAR(100) NOT NULL
);
GO

-- Crear la tabla iTerminales
CREATE TABLE iTerminales (
    id CHAR(6) PRIMARY KEY,
    pais VARCHAR(100) NOT NULL,
    FOREIGN KEY (id) REFERENCES Terminales(id)
);
GO

-- Crear la tabla nTerminales
CREATE TABLE nTerminales (
    id CHAR(6) PRIMARY KEY,
    servicio_taxi BIT NOT NULL,
    FOREIGN KEY (id) REFERENCES Terminales(id)
);
GO

-- Crear la tabla Viajes
CREATE TABLE Viajes (
    id INT PRIMARY KEY IDENTITY(1,1),
    dt_salida DATETIME NOT NULL,
    dt_llegada DATETIME NOT NULL,
    max_pasajeros INT NOT NULL,
    precio_boleto DECIMAL(10, 2) NOT NULL,
    num_anden INT NOT NULL,
    nombre_compania VARCHAR(100) NOT NULL,
    id_terminal CHAR(6) NOT NULL,
    FOREIGN KEY (nombre_compania) REFERENCES Companias(nombre),
    FOREIGN KEY (id_terminal) REFERENCES Terminales(id)
);
GO

-- Crear la tabla Terminales
CREATE TABLE Parametros (
    nombre VARCHAR(100) NOT NULL PRIMARY KEY,
    valor VARCHAR(100) NOT NULL
);
GO

-- Registros de prueba
INSERT INTO Parametros (nombre, valor) VALUES 
('ultimoDeploy', '19/08/2024 22:00');
GO
-- Insertar registros en la tabla Companias
INSERT INTO Companias (nombre, direccion_matriz) VALUES 
('Transporte Uruguayo', 'Av. 18 de Julio 1234, Montevideo, Uruguay'),
('Viajes Internacionales', 'Av. Brasil 567, Montevideo, Uruguay'),
('Expreso del Este', 'Av. Italia 890, Montevideo, Uruguay'),
('Turismo Oriental', 'Av. Rivera 4321, Montevideo, Uruguay'),
('Rutas del Sur', 'Av. Luis Alberto de Herrera 987, Montevideo, Uruguay');
GO

-- Insertar registros en la tabla Telefonos_Contacto
INSERT INTO Telefonos_Contacto (nombre_compania, telefono) VALUES 
('Transporte Uruguayo', '+598 2 123 4567'),
('Viajes Internacionales', '+598 2 345 6789'),
('Expreso del Este', '+598 2 456 7890'),
('Turismo Oriental', '+598 2 567 8901'),
('Rutas del Sur', '+598 2 678 9012'),
('Rutas del Sur', '+598 2 678 9013');
GO

-- Insertar registros en la tabla Terminales
INSERT INTO Terminales (id, ciudad) VALUES 
('TER001', 'Rio Branco'),
('TER002', 'Punta del Este'),
('TER003', 'Colonia del Sacramento'),
('TER004', 'Rivera'),
('TER005', 'Porto Alegre'),
('TER006', 'Montevideo'),
('TER007', 'Paysandú'),
('TER008', 'Salto'),
('TER009', 'Melo'),
('TER010', 'Canelones'),
('TER011', 'Tacuarembó'),
('TER012', 'Mercedes'),
('TER013', 'Durazno'),
('TER014', 'San José'),
('TER015', 'Cerro Largo'),
('TER016', 'Treinta y Tres'),
('TER017', 'Florida'),
('TER018', 'Rocha'),
('TER019', 'Lavalleja'),
('TER020', 'Artigas'),
('TER021', 'Bariloche'),
('TER022', 'Florianopolis'),
('TER023', 'Santiago'),
('TER024', 'Asuncion'),
('TER025', 'Gramado');
GO

-- Insertar registros en la tabla iTerminales
INSERT INTO iTerminales (id, pais) VALUES 
('TER005', 'Brasil'),
('TER021', 'Argentina'),
('TER022', 'Brasil'),
('TER023', 'Chile'),
('TER024', 'Paraguay'),
('TER025', 'Brasil');
GO

-- Insertar registros en la tabla nTerminales
INSERT INTO nTerminales (id, servicio_taxi) VALUES 
('TER001', 1),
('TER002', 0),
('TER003', 1),
('TER004', 1),
('TER006', 1),
('TER007', 0),
('TER008', 1),
('TER009', 1),
('TER010', 0),
('TER011', 1),
('TER012', 1),
('TER013', 1),
('TER014', 1),
('TER015', 1),
('TER016', 0),
('TER017', 1);
GO

-- Insertar registros en la tabla Viajes
INSERT INTO Viajes (dt_salida, dt_llegada, max_pasajeros, precio_boleto, num_anden, nombre_compania, id_terminal) VALUES 
('2024-07-01 08:00:00', '2024-07-01 10:00:00', 50, 500.00, 1, 'Transporte Uruguayo', 'TER003'),
('2024-07-02 09:00:00', '2024-07-02 15:00:00', 50, 1200.00, 2, 'Viajes Internacionales', 'TER001'),
('2024-07-03 10:00:00', '2024-07-03 17:00:00', 50, 1400.00, 3, 'Expreso del Este', 'TER004'),
('2024-07-04 11:00:00', '2024-07-04 14:00:00', 50, 600.00, 4, 'Turismo Oriental', 'TER002'),
('2024-07-05 12:00:00', '2024-07-05 23:00:00', 50, 3400.00, 5, 'Rutas del Sur', 'TER005');
GO

------------------------------------------------------------TERMINALES-----------------------------------------------------------------
--SP BuscarTerminal
CREATE PROCEDURE BuscarTerminal
    @codigo_terminal CHAR(6),
    @tipo_terminal CHAR(1)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;
    SET @Error = 0;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Verificar si el código existe en nTerminales (terminal nacional)
        DECLARE @existe_nacional BIT;
        SET @existe_nacional = 0;

        IF EXISTS (SELECT 1 FROM nTerminales WHERE id = @codigo_terminal)
        BEGIN
            SET @existe_nacional = 1;
        END

        -- Verificar si el código existe en iTerminales (terminal internacional)
        DECLARE @existe_internacional BIT;
        SET @existe_internacional = 0;

        IF EXISTS (SELECT 1 FROM iTerminales WHERE id = @codigo_terminal)
        BEGIN
            SET @existe_internacional = 1;
        END

        IF @tipo_terminal = 'N'
        BEGIN
            -- Verificar si la terminal es nacional pero el código también está en iTerminales
            IF @existe_internacional = 1
            BEGIN
                SET @Error = -6; -- Error de tipo de terminal incorrecto
                GOTO ErrorHandling;
            END

            -- Buscar terminal nacional
            SELECT 
                t.*, 
                nt.servicio_taxi
            FROM 
                Terminales t
            JOIN 
                nTerminales nt ON nt.id = t.id
            WHERE 
                t.id = @codigo_terminal;
        END
        ELSE IF @tipo_terminal = 'I'
        BEGIN
            -- Verificar si la terminal es internacional pero el código también está en nTerminales
            IF @existe_nacional = 1
            BEGIN
                SET @Error = -6; -- Error de tipo de terminal incorrecto
                GOTO ErrorHandling;
            END

            -- Buscar terminal internacional
            SELECT 
                t.*, 
                it.pais
            FROM 
                Terminales t
            JOIN 
                iTerminales it ON it.id = t.id
            WHERE 
                t.id = @codigo_terminal;
        END
        ELSE
        BEGIN
            -- Tipo de terminal no válido
            SET @Error = -2; -- Tipo de terminal no válido
            GOTO ErrorHandling;
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        SET @Error = -3; -- Otro error
        GOTO ErrorHandling;
    END CATCH;

ErrorHandling:
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
    RETURN @Error;
END;
GO


--SP AgregarTerminal
CREATE PROCEDURE AgregarTerminal
    @id CHAR(6),
    @ciudad VARCHAR(100),
    @pais VARCHAR(100) = NULL,  -- Puede ser NULL para terminales nacionales
    @servicio_taxi BIT = NULL,  -- Puede ser NULL para terminales internacionales
    @tipo_terminal CHAR(1)  -- 'N' para Nacional, 'I' para Internacional
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;
    SET @Error = 0;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Verificar si la terminal ya existe
        IF EXISTS (SELECT 1 FROM Terminales WHERE id = @id)
        BEGIN
            SET @Error = -1; -- La terminal ya existe en la base de datos
            GOTO ErrorHandling;
        END

        -- Agregar la nueva Terminal
        INSERT INTO Terminales(id, ciudad)
        VALUES (@id, @ciudad);

        -- Agregar la terminal nacional o internacional según el tipo
        IF @tipo_terminal = 'N'
        BEGIN
            -- Verificar que el servicio de taxi no sea NULL
            IF @servicio_taxi IS NULL
            BEGIN
                SET @Error = -2; -- El servicio de taxi debe ser proporcionado para terminales nacionales
                GOTO ErrorHandling;
            END
            INSERT INTO nTerminales(id, servicio_taxi)
            VALUES (@id, @servicio_taxi);
        END
        ELSE IF @tipo_terminal = 'I'
        BEGIN
            -- Verificar que el país no sea NULL
            IF @pais IS NULL
            BEGIN
                SET @Error = -2; -- El país debe ser proporcionado para terminales internacionales
                GOTO ErrorHandling;
            END
            INSERT INTO iTerminales(id, pais)
            VALUES (@id, @pais);
        END
        ELSE
        BEGIN
            SET @Error = -4; -- Tipo de terminal no válido
            GOTO ErrorHandling;
        END

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        SET @Error = -3; -- Otro error
        GOTO ErrorHandling;
    END CATCH;

ErrorHandling:
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
    RETURN @Error;
END;
GO

--SP ModificarTerminal
CREATE PROCEDURE ModificarTerminal
    @id CHAR(6),
    @nueva_ciudad VARCHAR(100),
    @nuevo_pais VARCHAR(100)  = NULL,  -- Puede ser NULL para terminales nacionales,
    @nuevo_servicio_taxi BIT  = NULL,  -- Puede ser NULL para terminales internacionales,
    @tipo_terminal CHAR(1)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;
    SET @Error = 0;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Verificar si la terminal existe en la tabla Terminales
        IF NOT EXISTS (SELECT 1 FROM Terminales WHERE id = @id)
        BEGIN
            SET @Error = -1; -- La terminal no existe
            GOTO ErrorHandling;
        END

        -- Modificar la terminal en la tabla Terminales
        UPDATE Terminales
        SET ciudad = @nueva_ciudad
        WHERE id = @id;

        -- Modificar la terminal nacional o internacional según el tipo
        IF @tipo_terminal = 'N'
        BEGIN
            -- Verificar que el servicio de taxi no sea NULL
            IF @nuevo_servicio_taxi IS NULL
            BEGIN
                SET @Error = -2; -- El servicio de taxi debe ser proporcionado para terminales nacionales
                GOTO ErrorHandling;
            END

            -- Actualizar en la tabla nTerminales
            UPDATE nTerminales
            SET servicio_taxi = @nuevo_servicio_taxi
            WHERE id = @id;
        END
        ELSE IF @tipo_terminal = 'I'
        BEGIN
            -- Verificar que el país no sea NULL
            IF @nuevo_pais IS NULL
            BEGIN
                SET @Error = -2; -- El país debe ser proporcionado para terminales internacionales
                GOTO ErrorHandling;
            END

            -- Actualizar en la tabla iTerminales
            UPDATE iTerminales
            SET pais = @nuevo_pais
            WHERE id = @id;
        END
        ELSE
        BEGIN
            SET @Error = -4; -- Tipo de terminal no válido
            GOTO ErrorHandling;
        END

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        SET @Error = -3; -- Otro error
        GOTO ErrorHandling;
    END CATCH;

ErrorHandling:
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
    RETURN @Error;
END;
GO

--SP Eliminar Terminal
CREATE PROCEDURE EliminarTerminal
    @codigo_terminal CHAR(6)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Verificar si la terminal existe
        IF NOT EXISTS (SELECT 1 FROM Terminales WHERE id = @codigo_terminal)
        BEGIN
            SET @Error = -1; -- Código no existe en la base de datos
            GOTO ErrorHandling;
        END

        -- Verificar si hay viajes asociados a la terminal
        IF EXISTS (SELECT 1 FROM Viajes WHERE id_terminal = @codigo_terminal)
        BEGIN
            SET @Error = -2; -- Hay viajes asociados y no se puede eliminar
            GOTO ErrorHandling;
        END

        -- Eliminar la terminal
        DELETE FROM nTerminales WHERE id = @codigo_terminal;
        DELETE FROM iTerminales WHERE id = @codigo_terminal;
        DELETE FROM Terminales WHERE id = @codigo_terminal;

        COMMIT TRANSACTION;
        SET @Error = 1; -- Eliminación correcta
        RETURN @Error;

    END TRY
    BEGIN CATCH
        SET @Error = -3; -- Otro error
        GOTO ErrorHandling;
    END CATCH;

ErrorHandling:
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
    RETURN @Error;
END;
GO

--SP ListarTerminales
CREATE PROCEDURE ListarTerminales
    @tipo_terminal CHAR(1)
AS
BEGIN
    SET NOCOUNT ON;

    IF @tipo_terminal = 'I'
    BEGIN
        -- Consulta para terminales internacionales
        SELECT 
            t.*, 
            i.pais 
        FROM 
            Terminales t
        JOIN 
            iTerminales i ON i.id = t.id;
    END
    ELSE IF @tipo_terminal = 'N'
    BEGIN
        -- Consulta para terminales nacionales
        SELECT 
            t.*, 
            CASE 
                WHEN n.servicio_taxi = 1 THEN 1
                ELSE 0
            END AS servicio_taxi
        FROM 
            Terminales t
        JOIN 
            nTerminales n ON n.id = t.id;
    END
END;
GO

--SP Buscar Compañia
CREATE PROCEDURE BuscarTerminalPorCodigo
@codigo_terminal CHAR(6)
AS
BEGIN
    SET NOCOUNT ON;

	SELECT * from Terminales where id=@codigo_terminal;

END;
GO
--------------------------------------------------------------VIAJES------------------------------------------------------------------

--SP Agregar Viaje
CREATE PROCEDURE AgregarViaje
    @dt_salida DATETIME,
    @dt_llegada DATETIME,
    @max_pasajeros INT,
    @precio_boleto DECIMAL(10, 2),
    @num_anden INT,
    @nombre_compania VARCHAR(100),
    @id_terminal CHAR(6)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NuevoCodigoViaje INT;
    DECLARE @Error INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Verificar si la compañía existe
        IF NOT EXISTS (SELECT 1 FROM Companias WHERE nombre = @nombre_compania)
        BEGIN
            SET @Error = -1; -- La compañía no existe en la base de datos
            GOTO ErrorHandling;
        END

        -- Verificar si la terminal existe
        IF NOT EXISTS (SELECT 1 FROM Terminales WHERE id = @id_terminal)
        BEGIN
            SET @Error = -2; -- La terminal no existe en la base de datos
            GOTO ErrorHandling;
        END

        -- Agregar el nuevo viaje
        INSERT INTO Viajes (dt_salida, dt_llegada, max_pasajeros, precio_boleto, num_anden, nombre_compania, id_terminal)
        VALUES (@dt_salida, @dt_llegada, @max_pasajeros, @precio_boleto, @num_anden, @nombre_compania, @id_terminal);

        -- Obtener el código del nuevo viaje
        SET @NuevoCodigoViaje = SCOPE_IDENTITY();

        COMMIT TRANSACTION;
        SET @Error = @NuevoCodigoViaje; -- Registro del viaje agregado correctamente
        RETURN @Error;

    END TRY
    BEGIN CATCH
        SET @Error = -3; -- Otro error
        GOTO ErrorHandling;
    END CATCH;

ErrorHandling:
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
    RETURN @Error;
END;
GO

CREATE PROCEDURE ListadoViajesTerminalMesAño
    @id_terminal CHAR(6),
    @mes INT,
    @anio INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;
    SET @Error = 0;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Verificar si la terminal existe
        IF NOT EXISTS (SELECT 1 FROM Terminales WHERE id = @id_terminal)
        BEGIN
            SET @Error = -1; -- La terminal no existe en la base de datos
            GOTO ErrorHandling;
        END

        -- Seleccionar los viajes según el terminal y las fechas indicadas
        SELECT 
            v.*
        FROM 
            Viajes v
        WHERE 
            v.id_terminal = @id_terminal
            AND MONTH(v.dt_salida) = @mes
            AND YEAR(v.dt_salida) = @anio;

        COMMIT TRANSACTION;
        RETURN @Error;

    END TRY
    BEGIN CATCH
        SET @Error = -3; -- Otro error
        GOTO ErrorHandling;
    END CATCH;

ErrorHandling:
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
    RETURN @Error;
END;
GO

CREATE PROCEDURE ListadoViajes
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;
    SET @Error = 0;

    BEGIN TRY
        BEGIN TRANSACTION;

        SELECT 
            v.*
        FROM 
            Viajes v;

        COMMIT TRANSACTION;
        RETURN @Error;

    END TRY
    BEGIN CATCH
        SET @Error = -3; -- Otro error
        GOTO ErrorHandling;
    END CATCH;

ErrorHandling:
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
    RETURN @Error;
END;
GO

--------------------------------------------------------------COMPANIAS------------------------------------------------------------------
--SP Buscar Compañia
CREATE PROCEDURE BuscarCompania
@nombre_compania VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

	SELECT C.direccion_matriz,
		   STUFF((
			   SELECT ';' + t.telefono
			   FROM Telefonos_Contacto t
			   WHERE t.nombre_compania = C.nombre
			   FOR XML PATH(''), TYPE
		   ).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS telefonos
	FROM Companias C
	WHERE C.nombre = @nombre_compania;

END;
GO

--SP AgregarCompania
CREATE PROCEDURE AgregarCompania
    @nombre_compania VARCHAR(100),
    @direccion_matriz VARCHAR(100),
    @telefonos NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;
	 SET @Error = 0;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Verificar si la compañía existe
        IF EXISTS (SELECT 1 FROM Companias WHERE nombre = @nombre_compania)
        BEGIN
            SET @Error = -1; -- La compañía ya existe en la base de datos
            GOTO ErrorHandling;
        END

        -- Agregar la nueva compañía
        INSERT INTO Companias(nombre, direccion_matriz)
        VALUES (@nombre_compania, @direccion_matriz);

        -- Agregar los teléfonos a la compañía
        INSERT INTO Telefonos_Contacto(nombre_compania, telefono)
        SELECT 
            @nombre_compania, 
            value AS telefono
        FROM 
            STRING_SPLIT(@telefonos, ';');

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        SET @Error = -3; -- Otro error
        GOTO ErrorHandling;
    END CATCH;

ErrorHandling:
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
    RETURN @Error;
END;
GO

-- SP ModificarCompania
CREATE PROCEDURE ModificarCompania
    @nombre_compania VARCHAR(100),
    @direccion_matriz VARCHAR(255) = NULL,
    @telefonos NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;
    SET @Error = 0;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Verificar si la compañía existe
        IF NOT EXISTS (SELECT 1 FROM Companias WHERE nombre = @nombre_compania)
        BEGIN
            SET @Error = -1; -- La compañía no existe en la base de datos
            GOTO ErrorHandling;
        END

        -- Actualizar la dirección de la compañía solo si se proporciona un valor no vacío
        IF @direccion_matriz IS NOT NULL AND LEN(@direccion_matriz) > 0
        BEGIN
            UPDATE Companias
            SET direccion_matriz = @direccion_matriz
            WHERE nombre = @nombre_compania;
        END

        -- Actualizar los teléfonos de la compañía solo si se proporciona una lista no vacía
        IF @telefonos IS NOT NULL AND LEN(@telefonos) > 0
        BEGIN
            -- Eliminar los teléfonos antiguos
            DELETE FROM Telefonos_Contacto
            WHERE nombre_compania = @nombre_compania;

            -- Agregar los nuevos teléfonos
            INSERT INTO Telefonos_Contacto(nombre_compania, telefono)
            SELECT 
                @nombre_compania, 
                value AS telefono
            FROM 
                STRING_SPLIT(@telefonos, ';');
        END

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        SET @Error = -2; -- Otro error
        GOTO ErrorHandling;
    END CATCH;

ErrorHandling:
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
    RETURN @Error;
END;
GO

--SP EliminarCompania
CREATE PROCEDURE EliminarCompania
    @nombre_compania VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;
    SET @Error = 0;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Verificar si la compañía existe
        IF NOT EXISTS (SELECT 1 FROM Companias WHERE nombre = @nombre_compania)
        BEGIN
            SET @Error = -1; -- La compañía no existe
            GOTO ErrorHandling;
        END

        -- Verificar si la compañía tiene viajes asociados
        IF EXISTS (SELECT 1 FROM Viajes WHERE nombre_compania = @nombre_compania)
        BEGIN
            SET @Error = -2; -- La compañía tiene viajes asociados, no se puede eliminar
            GOTO ErrorHandling;
        END

        -- Eliminar los teléfonos de la compañía
        DELETE FROM Telefonos_Contacto WHERE nombre_compania = @nombre_compania;

        -- Eliminar la compañía
        DELETE FROM Companias WHERE nombre = @nombre_compania;

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        SET @Error = -3; -- Otro error
        GOTO ErrorHandling;
    END CATCH;

ErrorHandling:
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
    RETURN @Error;
END;
GO

CREATE PROCEDURE ListarCompanias
AS
BEGIN
    SET NOCOUNT ON;

	SELECT c.*,STUFF((
			   SELECT ';' + t.telefono
			   FROM Telefonos_Contacto t
			   WHERE t.nombre_compania = C.nombre
			   FOR XML PATH(''), TYPE
		   ).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS telefono
		FROM Companias c
		JOIN Telefonos_Contacto t on t.nombre_compania=c.nombre;
END;
GO

--SP ListarTerminales
CREATE PROCEDURE BuscarParametro
    @parametro varchar(100)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN
	select * from Parametros where nombre=@parametro;
    END
END;
GO

------------------------------------------------------------------------------------------------------------------------------------


-- Sentencias para probar los SP

DECLARE @Resultado INT;
-- Llamar al procedimiento almacenado para eliminar una terminal
EXEC @Resultado = EliminarTerminal @codigo_terminal = 'TER001';

-- Verificar el resultado
IF @Resultado = 1
    PRINT 'La terminal se eliminó correctamente.';
ELSE IF @Resultado = -1
    PRINT 'La terminal no existe en la base de datos.';
ELSE IF @Resultado = -2
    PRINT 'La terminal tiene viajes asociados y no se puede eliminar.';
ELSE IF @Resultado = -3
    PRINT 'Ocurrió un error al intentar eliminar la terminal.';
GO

DECLARE @Resultado INT;
-- Llamar al procedimiento almacenado para modificar un viaje
EXEC @Resultado = ModificarViaje 
                    @codigo_viaje = 1,
                    @dt_salida = '2024-07-10 08:00:00',
                    @dt_llegada = '2024-07-10 10:00:00',
                    @max_pasajeros = 60,
                    @precio_boleto = 180.00,
                    @num_anden = 2,
                    @nombre_compania = 'Transporte Uruguayo',
                    @id_terminal = 'TER003';

-- Verificar el resultado
IF @Resultado = 1
    PRINT 'El viaje se modificó correctamente.';
ELSE IF @Resultado = -1
    PRINT 'El código de viaje no existe en la base de datos.';
ELSE IF @Resultado = -2
    PRINT 'El viaje ya se ha realizado y no se puede modificar.';
ELSE IF @Resultado = -3
    PRINT 'Ocurrió un error al intentar modificar el viaje.';
GO

DECLARE @Resultado INT;
-- Llamar al procedimiento almacenado para agregar un nuevo viaje
EXEC @Resultado = AgregarViaje 
                    @dt_salida = '2024-07-10 08:00:00',
                    @dt_llegada = '2024-07-10 10:00:00',
                    @max_pasajeros = 50,
                    @precio_boleto = 100.00,
                    @num_anden = 1,
                    @nombre_compania = 'Transporte Uruguayo',
                    @id_terminal = 'TER001';

-- Verificar el resultado
IF @Resultado > 0
    PRINT 'Se agregó el viaje correctamente. Código del nuevo viaje: ' + CAST(@Resultado AS VARCHAR(10));
ELSE IF @Resultado = -1
    PRINT 'La compañía especificada no existe en la base de datos.';
ELSE IF @Resultado = -2
    PRINT 'La terminal especificada no existe en la base de datos.';
ELSE IF @Resultado = -3
    PRINT 'Ocurrió un error al intentar agregar el viaje.';
GO

-- Llamar al procedimiento almacenado para obtener un listado de viajes
EXEC ListadoDeViajes;
GO

-- Llamar al procedimiento almacenado para obtener el total de viajes por compañía
EXEC TotalViajesPorCompania;
GO

-- Llamar al procedimiento almacenado para obtener el total de viajes internacionales por compañía
EXEC TotalViajesInternacionalesPorCompania;
GO

-- Llamar al procedimiento almacenado para buscar compañía
EXEC BuscarCompania @nombre_compania = 'Rutas del Sur';
GO