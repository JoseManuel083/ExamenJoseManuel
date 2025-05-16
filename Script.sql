/*
Autor:Jose Manuel Gomez Alavez
Fecha:15/05/2025
Descripcion: Script para generar la base de datos BdiExamen para Bansi, solo ejecutar el script para crear los objetos sql
necesarios para el examen
*/
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'BdiExamen')
BEGIN
    CREATE DATABASE BdiExamen;
END;
GO

--Usar la base de datos recién creada
USE BdiExamen;
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblExamen')
BEGIN
	CREATE TABLE tblExamen(
	IdExamen int primary key,
	Nombre varchar(255),
	Descripcion varchar(255)
)
END
GO


CREATE PROCEDURE spAgregar
    @Id INT,
    @Nombre NVARCHAR(50),
    @Descripcion NVARCHAR(255)
AS
BEGIN
    BEGIN TRY
        INSERT INTO tblexamen (IdExamen, Nombre, Descripcion)
        VALUES (@Id, @Nombre, @Descripcion);

        SELECT 0 AS CodigoRetorno
		SELECT 'Registro insertado satisfactoriamente' AS DescripcionRetorno;
    END TRY
    BEGIN CATCH
        SELECT ERROR_NUMBER() AS CodigoRetorno, ERROR_MESSAGE() AS DescripcionRetorno;
    END CATCH
END;
GO

CREATE PROCEDURE spActualizar
    @Id INT,
    @Nombre NVARCHAR(50),
    @Descripcion NVARCHAR(255)
AS
BEGIN
    BEGIN TRY
        UPDATE tblexamen
			SET Nombre = @Nombre, 
			Descripcion = @Descripcion
        WHERE IdExamen = @Id;

        SELECT 0 AS CodigoRetorno
		SELECT 'Registro actualizado satisfactoriamente' AS DescripcionRetorno;
    END TRY
    BEGIN CATCH
        SELECT ERROR_NUMBER() AS CodigoRetorno
		SELECT ERROR_MESSAGE() AS DescripcionRetorno;
    END CATCH
END;
GO

CREATE PROCEDURE spEliminar
    @Id INT
AS
BEGIN
    BEGIN TRY
        DELETE FROM tblexamen
        WHERE IdExamen = @Id;

        IF @@ROWCOUNT = 0
        BEGIN
            SELECT 1 AS CodigoRetorno;
            SELECT 'No se encontró el registro para eliminar.' AS DescripcionRetorno;
        END
        ELSE
        BEGIN
            SELECT 0 AS CodigoRetorno;
            SELECT 'Registro eliminado satisfactoriamente.' AS DescripcionRetorno;
        END
    END TRY
    BEGIN CATCH
        SELECT ERROR_NUMBER() AS CodigoRetorno;
        SELECT ERROR_MESSAGE() AS DescripcionRetorno
    END CATCH
END;
GO
CREATE PROCEDURE spConsultar
    @Nombre NVARCHAR(100) = NULL,
    @Descripcion NVARCHAR(255) = NULL
AS
BEGIN
    SELECT IdExamen, Nombre, Descripcion
    FROM tblexamen
    WHERE (@Nombre IS NULL OR Nombre LIKE '%' + @Nombre + '%')
      AND (@Descripcion IS NULL OR Descripcion LIKE '%' + @Descripcion + '%');
END;

