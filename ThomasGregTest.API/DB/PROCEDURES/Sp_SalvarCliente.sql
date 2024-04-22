/*
Procedure: [dbo].[Sp_SalvarCliente]
Author: Jackson Costa
Date Created: 20/04/2024
Description: Esta procedure INSERE se não existe ou ATUALIZA caso exista um Cliente na tabela Clientes.
*/

USE [ThomasGreg_JacksonCosta]

DECLARE @tipoAcao VARCHAR(8)

IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'Sp_SalvarCliente')
BEGIN 
	SET @tipoAcao = 'CREATE'
END
ELSE 
BEGIN 
	SET @tipoAcao = 'ALTER' 
END

EXEC (@tipoAcao + ' 
	PROCEDURE [dbo].[Sp_SalvarCliente]
		@Id			INT,
		@Nome		VARCHAR(150),
		@Email		VARCHAR(255),
		@Logotipo	VARCHAR(50),

		@RowCount	INT OUTPUT
	AS
	BEGIN
		IF EXISTS (SELECT 1 FROM Clientes WHERE Id = @Id)
		BEGIN
			UPDATE Clientes
			SET
				Nome		= @Nome,
				Email		= @Email,
				Logotipo	= @Logotipo
			WHERE 
				Id = @Id

			SET @RowCount = @@ROWCOUNT
		END
		ELSE
		BEGIN
			INSERT INTO Clientes(Nome, 
								 Email, 
								 Logotipo)
			VALUES(@Nome, 
				   @Email, 
				   @Logotipo)

			SET @RowCount = @@ROWCOUNT
		END
	END'
)