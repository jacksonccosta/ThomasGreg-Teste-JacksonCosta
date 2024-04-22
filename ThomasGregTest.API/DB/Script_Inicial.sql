IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME IN ('Clientes', 'Logradouros', 'RefreshTokens', 'Usuarios'))
BEGIN
    CREATE TABLE [dbo].[Clientes](
		[Id]		INT				IDENTITY(1,1) NOT NULL,
		[Nome]		VARCHAR(150)	NOT NULL,
		[Email]		VARCHAR(255)	NOT NULL,
		[Logotipo]	VARCHAR(50)		NOT NULL,

		CONSTRAINT	[PK_Clientes] PRIMARY KEY CLUSTERED ([Id] ASC)
	) ON [PRIMARY]
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Logradouros')
BEGIN
    CREATE TABLE [dbo].[Logradouros](
		[Id]		INT				IDENTITY(1,1) NOT NULL,
		[ClienteId] INT				NOT NULL,
		[Endereco]	VARCHAR(255)	NOT NULL,
		[Numero]	VARCHAR(15)		NOT NULL,
		[Bairro]	VARCHAR(100)	NOT NULL,
		[Cidade]	VARCHAR(100)	NOT NULL,
		[Estado]	CHAR(2)			NOT NULL,
		[Cep]		CHAR(9)			NOT NULL,

		CONSTRAINT [PK_Logradouros] PRIMARY KEY CLUSTERED ([Id] ASC)
	) ON [PRIMARY]
	
	ALTER TABLE [dbo].[Logradouros] 
		WITH CHECK ADD CONSTRAINT [FK_Logradouros_Clientes] FOREIGN KEY([ClienteId])

	REFERENCES [dbo].[Clientes] ([Id]) ON DELETE CASCADE

	ALTER TABLE [dbo].[Logradouros] CHECK CONSTRAINT [FK_Logradouros_Clientes]
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'RefreshTokens')
BEGIN
    CREATE TABLE [dbo].[RefreshTokens](
		[Token]				VARCHAR(255)	NOT NULL,
		[UsuarioId]			INT				NOT NULL,
		[ExpirationDate]	DATETIME		NOT NULL,

		CONSTRAINT [PK_RefreshToken] PRIMARY KEY CLUSTERED ([Token] ASC)
	) ON [PRIMARY]
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Usuarios')
BEGIN
    CREATE TABLE [dbo].[Usuarios](
		[Id]	INT IDENTITY(1,1) NOT NULL,
		[Nome]	VARCHAR(150)		NOT NULL,
		[Email] VARCHAR(255)		NOT NULL,
		[Senha] VARCHAR(255)		NOT NULL,
		[Ativo] BIT					NOT NULL,

		CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED ([Id] ASC)
	) ON [PRIMARY]
END