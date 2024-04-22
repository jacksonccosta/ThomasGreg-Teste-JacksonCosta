/*
Procedure: [dbo].[Sp_DeletaLogradouro]
Author: Jackson Costa
Date Created: 20/04/2024
Description: Esta procedure exclui um Logradouro da tabela Logradouros.
Observations:
    - O parâmetro @Id especifica o ID do logradouro a ser excluído.
	- O parâmetro @ClienteId especifica o Id do cliente atrelado ao logradouro que será excluído (um check a mais para segurnaça da exclusão)
    - O parâmetro @RowCount retorna o número de linhas afetadas pela operação de exclusão.
*/

USE [ThomasGreg_JacksonCosta]

DECLARE @tipoAcao VARCHAR(8)

IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'Sp_DeletaLogradouro')
BEGIN 
	SET @tipoAcao = 'CREATE' 
END
ELSE 
BEGIN 
	SET @tipoAcao = 'ALTER' 
END

EXEC (@tipoAcao + ' 
	PROCEDURE [dbo].[Sp_DeletaLogradouro]
		@Id			INT,
		@ClienteId	INT,
		@RowCount	INT OUTPUT
	AS
	BEGIN
		IF EXISTS (SELECT 1 FROM Logradouros WHERE Id = @Id AND ClienteId = @ClienteId)
		BEGIN
			DELETE FROM Logradouros 
			WHERE Id = @Id 
				AND ClienteId = @ClienteId

			SET @RowCount = @@ROWCOUNT
		END
		ELSE
		BEGIN
			SET @RowCount = 0;
		END
	END'
)