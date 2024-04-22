/*
Procedure: [dbo].[Sp_DeletaLogradouro]
Author: Jackson Costa
Date Created: 20/04/2024
Description: Esta procedure exclui um Cliente da tabela Clientes.
Observations:
    - O parâmetro @Id especifica o ID do cliente a ser excluído.
    - O parâmetro @RowCount retorna o número de linhas afetadas pela operação de exclusão.
*/

USE [ThomasGreg_JacksonCosta]

DECLARE @tipoAcao VARCHAR(8)

IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'Sp_DeletaClientePorId')
BEGIN 
	SET @tipoAcao = 'CREATE' 
END
ELSE 
BEGIN 
	SET @tipoAcao = 'ALTER' 
END

EXEC (@tipoAcao + ' 
	PROCEDURE [dbo].[Sp_DeletaClientePorId]
		@Id			INT,
		@RowCount	INT OUTPUT
	AS
	BEGIN
		IF EXISTS (SELECT 1 FROM Clientes WHERE Id = @Id)
		BEGIN
			DELETE FROM Clientes 
			WHERE Id = @Id
        
			SET @RowCount = @@ROWCOUNT
		END
		ELSE
		BEGIN
			SET @RowCount = 0
		END
	END'
)