/*
Procedure: [dbo].[Sp_SalvarLogradouro]
Author: Jackson Costa
Date Created: 20/04/2024
Description: Esta procedure INSERE se não existe ou ATUALIZA caso exista um Logradouro na tabela Logradouros.
*/

USE [ThomasGreg_JacksonCosta]

DECLARE @tipoAcao VARCHAR(8)

IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'Sp_SalvarLogradouro')
BEGIN 
	SET @tipoAcao = 'CREATE'
END
ELSE 
BEGIN 
	SET @tipoAcao = 'ALTER' 
END

EXEC (@tipoAcao + ' 
	PROCEDURE [dbo].[Sp_SalvarLogradouro]
		@Id 		INT,
		@ClienteId 	INT,
		@Endereco 	VARCHAR(255),
		@Numero 	VARCHAR(15),
		@Bairro 	VARCHAR(100),
		@Cidade 	VARCHAR(100),
		@Estado 	CHAR(2),
		@Cep 		CHAR(9),
	
		@RowCount 	INT OUTPUT
	AS
	BEGIN
		IF EXISTS (SELECT 1 FROM Logradouros WHERE Id = @Id)
		BEGIN
			UPDATE Logradouros
			SET
				Endereco = @Endereco,
				Numero = @Numero,
				Bairro = @Bairro,
				Cidade = @Cidade,
				Estado = @Estado,
				Cep = @Cep
			WHERE 
				Id = @Id
			
			SET @RowCount = @@ROWCOUNT
		END
		ELSE
		BEGIN
			INSERT INTO Logradouros(ClienteId, 
									Endereco, 
									Numero, 
									Bairro, 
									Cidade, 
									Estado, 
									Cep)
			VALUES(@ClienteId, 
				   @Endereco, 
				   @Numero, 
				   @Bairro, 
				   @Cidade, 
				   @Estado, 
				   @Cep)
			   
			SET @RowCount = @@ROWCOUNT
		END
	END'
)