-- crear la db
CREATE DATABASE ev_final;

-- usar la db
USE ev_final;

-- tabla de pacientes
CREATE TABLE Paciente(
	id INT IDENTITY(1,1) PRIMARY KEY,
	codigo VARCHAR(20),
	nombreCompleto VARCHAR(150)
);

-- TABLA DE BOX
CREATE TABLE Box(
	id INT IDENTITY(1,1) PRIMARY KEY,
	nombre VARCHAR(50)
);

-- TABLA DE ESTADOS DE ATENCION
CREATE TABLE EstadoAtencion(
	id INT IDENTITY(1,1) PRIMARY KEY,
	nombre VARCHAR(50)
);

-- TABLA DE ATENCION
CREATE TABLE Atencion(
	id INT IDENTITY(1,1) PRIMARY KEY,
	idBox INT FOREIGN KEY REFERENCES Box(id),
	idPaciente INT FOREIGN KEY REFERENCES Paciente(id),
	idEstado INT FOREIGN KEY REFERENCES EstadoAtencion(id),
	nombreDoctor VARCHAR(150),
	fechaAtencion DATE
);

-- carga de box
INSERT INTO [dbo].[Box] ([nombre]) VALUES ('Box 1')
INSERT INTO [dbo].[Box] ([nombre]) VALUES ('Box 2')
INSERT INTO [dbo].[Box] ([nombre]) VALUES ('Box 3')
INSERT INTO [dbo].[Box] ([nombre]) VALUES ('Box 4')
INSERT INTO [dbo].[Box] ([nombre]) VALUES ('Box 5')
INSERT INTO [dbo].[Box] ([nombre]) VALUES ('Box 6')
INSERT INTO [dbo].[Box] ([nombre]) VALUES ('Box 7')
INSERT INTO [dbo].[Box] ([nombre]) VALUES ('Box 8')
INSERT INTO [dbo].[Box] ([nombre]) VALUES ('Box 9')
INSERT INTO [dbo].[Box] ([nombre]) VALUES ('Box 10')

-- carga de estados
INSERT INTO [dbo].[EstadoAtencion] ([nombre]) VALUES ('Ingresado')
INSERT INTO [dbo].[EstadoAtencion] ([nombre]) VALUES ('Atendido')

-- carga de pacientes de prueba
INSERT INTO [dbo].[Paciente] ([codigo],[nombreCompleto]) VALUES ('C0001', 'Daniel López')
INSERT INTO [dbo].[Paciente] ([codigo],[nombreCompleto]) VALUES ('C0002', 'Pepito Los Palotes')

-- procedimiento de insert atenciones
CREATE PROCEDURE sp_insertar_atencion
	@fechaAtencion AS DATE,
	@nombreDoctor AS VARCHAR(150),
	@box AS INT,
	@nombrePaciente AS VARCHAR(150),
	@codigoPaciente AS VARCHAR(20)
	AS
	DECLARE @idPaciente INT
	DECLARE @existeBox INT
	DECLARE @cantBox INT

	BEGIN
		SELECT @existeBox = COUNT(1) FROM dbo.Box b WHERE b.id = @box

		IF @existeBox = 0
			RAISERROR('El box no existe', 16, 1)
		ELSE
			BEGIN TRANSACTION
				BEGIN TRY
					SELECT @cantBox = COUNT(1) FROM dbo.Atencion a WHERE a.idBox = @box

					IF @cantBox > 0
						UPDATE dbo.Atencion SET idEstado = 2 WHERE idBox = @box

					SELECT TOP 1 @idPaciente = p.id FROM dbo.Paciente p WHERE p.codigo = @codigoPaciente

					IF @idPaciente IS NULL
						INSERT INTO [dbo].[Paciente] ([codigo],[nombreCompleto]) VALUES (@codigoPaciente, @nombrePaciente)

					SELECT TOP 1 @idPaciente = p.id FROM dbo.Paciente p WHERE p.codigo = @codigoPaciente
			
					INSERT INTO [dbo].[Atencion] ([idBox],[idPaciente],[idEstado],[nombreDoctor],[fechaAtencion]) 
					VALUES (@box, @idPaciente, 1, @nombreDoctor, @fechaAtencion)

					COMMIT TRANSACTION
				END TRY
				BEGIN CATCH
					DECLARE @ErrorMessage NVARCHAR(4000)
					DECLARE @ErrorSeverity INT
					DECLARE @ErrorState INT

					SELECT @ErrorMessage = ERROR_MESSAGE(),
						   @ErrorSeverity = ERROR_SEVERITY(),
						   @ErrorState = ERROR_STATE();

					RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)
					IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
				END CATCH
	END
GO

-- procedimiento actualizar estado 20 min
CREATE PROCEDURE sp_update_estado_atendido
	@idAtencion AS INT
	
	AS
	BEGIN
		UPDATE dbo.Atencion SET idEstado = 2 WHERE id = @idAtencion
	END
GO

-- procedimiento lista ultimos llamados
CREATE PROCEDURE sp_list_ultimosllamados
AS
	SELECT TOP 3 a.id, b.nombre box, a.nombreDoctor, p.codigo codigoPaciente 
	FROM dbo.Atencion a INNER JOIN dbo.Box b ON a.idBox = b.id
	INNER JOIN dbo.Paciente p ON a.idPaciente = p.id
	WHERE a.idEstado = 1 ORDER BY a.id DESC
GO

-- procedimiento carrusel
CREATE PROCEDURE sp_list_carrusel
	@lastIdAtencion AS INT
AS
	SELECT TOP 4 a.id, b.nombre box, a.nombreDoctor, p.codigo codigoPaciente 
	FROM dbo.Atencion a INNER JOIN dbo.Box b ON a.idBox = b.id
	INNER JOIN dbo.Paciente p ON a.idPaciente = p.id
	WHERE a.idEstado = 1 AND a.id > @lastIdAtencion ORDER BY a.id ASC
GO

-- test de carga
EXEC sp_insertar_atencion '21-09-2019', 'Pablo Sepulveda', 1, 'Adriano Castillo', 'C00019'
EXEC sp_update_estado_atendido 5
select * from Paciente
select * from Atencion


