IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [RepresentanteLegal] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [Cpf] nvarchar(max) NOT NULL,
    [Telefone] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [IdUsuario] int NOT NULL,
    CONSTRAINT [PK_RepresentanteLegal] PRIMARY KEY ([Id])
);

CREATE TABLE [TiposUsuario] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_TiposUsuario] PRIMARY KEY ([Id])
);

CREATE TABLE [Usuarios] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [Cpf] nvarchar(max) NOT NULL,
    [Login] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [DtNascimento] datetime2 NOT NULL,
    [Telefone] nvarchar(max) NOT NULL,
    [IdTipoUsuario] int NOT NULL,
    [Senha] nvarchar(max) NOT NULL,
    [Situacao] int NOT NULL,
    [DtSituacao] datetime2 NOT NULL,
    [IdUsuarioSituacao] int NOT NULL,
    [RepresentanteLegalId] int NULL,
    [TipoUsuarioId] int NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Usuarios_RepresentanteLegal_RepresentanteLegalId] FOREIGN KEY ([RepresentanteLegalId]) REFERENCES [RepresentanteLegal] ([Id]),
    CONSTRAINT [FK_Usuarios_TiposUsuario_TipoUsuarioId] FOREIGN KEY ([TipoUsuarioId]) REFERENCES [TiposUsuario] ([Id])
);

CREATE TABLE [UsuarioImagem] (
    [IdImagem] int NOT NULL IDENTITY,
    [IdUsuario] int NOT NULL,
    [UsuarioId] int NOT NULL,
    [EndImagem] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_UsuarioImagem] PRIMARY KEY ([IdImagem]),
    CONSTRAINT [FK_UsuarioImagem_Usuarios_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Nome') AND [object_id] = OBJECT_ID(N'[TiposUsuario]'))
    SET IDENTITY_INSERT [TiposUsuario] ON;
INSERT INTO [TiposUsuario] ([Id], [Nome])
VALUES (1, N'Usuario'),
(2, N'Gestor'),
(3, N'Financeiro');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Nome') AND [object_id] = OBJECT_ID(N'[TiposUsuario]'))
    SET IDENTITY_INSERT [TiposUsuario] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Cpf', N'DtNascimento', N'DtSituacao', N'Email', N'IdTipoUsuario', N'IdUsuarioSituacao', N'Login', N'Nome', N'RepresentanteLegalId', N'Senha', N'Situacao', N'Telefone', N'TipoUsuarioId') AND [object_id] = OBJECT_ID(N'[Usuarios]'))
    SET IDENTITY_INSERT [Usuarios] ON;
INSERT INTO [Usuarios] ([Id], [Cpf], [DtNascimento], [DtSituacao], [Email], [IdTipoUsuario], [IdUsuarioSituacao], [Login], [Nome], [RepresentanteLegalId], [Senha], [Situacao], [Telefone], [TipoUsuarioId])
VALUES (1, N'46284605874', '2006-04-07T00:00:00.0000000', '2025-07-10T00:00:00.0000000', N'adm@gmail.com', 2, 1, N'ADM', N'Admin', NULL, N'*123456HAS*', 1, N'11971949976', NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Cpf', N'DtNascimento', N'DtSituacao', N'Email', N'IdTipoUsuario', N'IdUsuarioSituacao', N'Login', N'Nome', N'RepresentanteLegalId', N'Senha', N'Situacao', N'Telefone', N'TipoUsuarioId') AND [object_id] = OBJECT_ID(N'[Usuarios]'))
    SET IDENTITY_INSERT [Usuarios] OFF;

CREATE INDEX [IX_UsuarioImagem_UsuarioId] ON [UsuarioImagem] ([UsuarioId]);

CREATE UNIQUE INDEX [IX_Usuarios_RepresentanteLegalId] ON [Usuarios] ([RepresentanteLegalId]) WHERE [RepresentanteLegalId] IS NOT NULL;

CREATE INDEX [IX_Usuarios_TipoUsuarioId] ON [Usuarios] ([TipoUsuarioId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250711004435_InitialCreate', N'9.0.7');

ALTER TABLE [UsuarioImagem] DROP CONSTRAINT [FK_UsuarioImagem_Usuarios_UsuarioId];

ALTER TABLE [UsuarioImagem] DROP CONSTRAINT [PK_UsuarioImagem];

EXEC sp_rename N'[UsuarioImagem]', N'UsuarioImagens', 'OBJECT';

EXEC sp_rename N'[UsuarioImagens].[IX_UsuarioImagem_UsuarioId]', N'IX_UsuarioImagens_UsuarioId', 'INDEX';

DECLARE @var sysname;
SELECT @var = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UsuarioImagens]') AND [c].[name] = N'UsuarioId');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [UsuarioImagens] DROP CONSTRAINT [' + @var + '];');
ALTER TABLE [UsuarioImagens] ALTER COLUMN [UsuarioId] int NULL;

ALTER TABLE [UsuarioImagens] ADD CONSTRAINT [PK_UsuarioImagens] PRIMARY KEY ([IdImagem]);

ALTER TABLE [UsuarioImagens] ADD CONSTRAINT [FK_UsuarioImagens_Usuarios_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuarios] ([Id]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250711010846_AddUsuarioImagem', N'9.0.7');

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Cpf', N'Email', N'IdUsuario', N'Nome', N'Telefone') AND [object_id] = OBJECT_ID(N'[RepresentanteLegal]'))
    SET IDENTITY_INSERT [RepresentanteLegal] ON;
INSERT INTO [RepresentanteLegal] ([Id], [Cpf], [Email], [IdUsuario], [Nome], [Telefone])
VALUES (1, N'12345678901', N'mariana.alves@example.com', 1, N'Mariana Alves', N'11912345678'),
(2, N'98765432100', N'carlos.henrique@example.com', 2, N'Carlos Henrique', N'21998765432'),
(3, N'45678912333', N'fernanda.costa@example.com', 3, N'Fernanda Costa', N'31934567890');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Cpf', N'Email', N'IdUsuario', N'Nome', N'Telefone') AND [object_id] = OBJECT_ID(N'[RepresentanteLegal]'))
    SET IDENTITY_INSERT [RepresentanteLegal] OFF;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250711220051_RepresentanteLegal', N'9.0.7');

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Cpf', N'DtNascimento', N'DtSituacao', N'Email', N'IdTipoUsuario', N'IdUsuarioSituacao', N'Login', N'Nome', N'RepresentanteLegalId', N'Senha', N'Situacao', N'Telefone', N'TipoUsuarioId') AND [object_id] = OBJECT_ID(N'[Usuarios]'))
    SET IDENTITY_INSERT [Usuarios] ON;
INSERT INTO [Usuarios] ([Id], [Cpf], [DtNascimento], [DtSituacao], [Email], [IdTipoUsuario], [IdUsuarioSituacao], [Login], [Nome], [RepresentanteLegalId], [Senha], [Situacao], [Telefone], [TipoUsuarioId])
VALUES (4, N'12345678900', '2010-05-15T00:00:00.0000000', '2025-07-14T00:00:00.0000000', N'joao.silva@example.com', 1, 1, N'joaos', N'João Silva', 1, N'*senha123*', 1, N'11987654321', NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Cpf', N'DtNascimento', N'DtSituacao', N'Email', N'IdTipoUsuario', N'IdUsuarioSituacao', N'Login', N'Nome', N'RepresentanteLegalId', N'Senha', N'Situacao', N'Telefone', N'TipoUsuarioId') AND [object_id] = OBJECT_ID(N'[Usuarios]'))
    SET IDENTITY_INSERT [Usuarios] OFF;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250714235200_AlterarRepresentanteLegal', N'9.0.7');

DROP INDEX [IX_Usuarios_RepresentanteLegalId] ON [Usuarios];

DELETE FROM [Usuarios]
WHERE [Id] = 4;
SELECT @@ROWCOUNT;


DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RepresentanteLegal]') AND [c].[name] = N'IdUsuario');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [RepresentanteLegal] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [RepresentanteLegal] DROP COLUMN [IdUsuario];

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Cpf', N'DtNascimento', N'DtSituacao', N'Email', N'IdTipoUsuario', N'IdUsuarioSituacao', N'Login', N'Nome', N'RepresentanteLegalId', N'Senha', N'Situacao', N'Telefone', N'TipoUsuarioId') AND [object_id] = OBJECT_ID(N'[Usuarios]'))
    SET IDENTITY_INSERT [Usuarios] ON;
INSERT INTO [Usuarios] ([Id], [Cpf], [DtNascimento], [DtSituacao], [Email], [IdTipoUsuario], [IdUsuarioSituacao], [Login], [Nome], [RepresentanteLegalId], [Senha], [Situacao], [Telefone], [TipoUsuarioId])
VALUES (3, N'12345678900', '2010-05-15T00:00:00.0000000', '2025-07-14T00:00:00.0000000', N'joao.silva@example.com', 1, 1, N'joaos', N'João Silva', 1, N'*senha123*', 1, N'11987654321', NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Cpf', N'DtNascimento', N'DtSituacao', N'Email', N'IdTipoUsuario', N'IdUsuarioSituacao', N'Login', N'Nome', N'RepresentanteLegalId', N'Senha', N'Situacao', N'Telefone', N'TipoUsuarioId') AND [object_id] = OBJECT_ID(N'[Usuarios]'))
    SET IDENTITY_INSERT [Usuarios] OFF;

CREATE INDEX [IX_Usuarios_RepresentanteLegalId] ON [Usuarios] ([RepresentanteLegalId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250715004531_CorrigeRelacionamentoRepresentante', N'9.0.7');

ALTER TABLE [Usuarios] DROP CONSTRAINT [FK_Usuarios_TiposUsuario_TipoUsuarioId];

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Usuarios]') AND [c].[name] = N'IdTipoUsuario');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Usuarios] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Usuarios] DROP COLUMN [IdTipoUsuario];

DROP INDEX [IX_Usuarios_TipoUsuarioId] ON [Usuarios];
DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Usuarios]') AND [c].[name] = N'TipoUsuarioId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Usuarios] DROP CONSTRAINT [' + @var3 + '];');
UPDATE [Usuarios] SET [TipoUsuarioId] = 0 WHERE [TipoUsuarioId] IS NULL;
ALTER TABLE [Usuarios] ALTER COLUMN [TipoUsuarioId] int NOT NULL;
ALTER TABLE [Usuarios] ADD DEFAULT 0 FOR [TipoUsuarioId];
CREATE INDEX [IX_Usuarios_TipoUsuarioId] ON [Usuarios] ([TipoUsuarioId]);

UPDATE [Usuarios] SET [TipoUsuarioId] = 2
WHERE [Id] = 1;
SELECT @@ROWCOUNT;


UPDATE [Usuarios] SET [TipoUsuarioId] = 1
WHERE [Id] = 3;
SELECT @@ROWCOUNT;


ALTER TABLE [Usuarios] ADD CONSTRAINT [FK_Usuarios_TiposUsuario_TipoUsuarioId] FOREIGN KEY ([TipoUsuarioId]) REFERENCES [TiposUsuario] ([Id]) ON DELETE CASCADE;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250717223511_AddTipoUsuario', N'9.0.7');

COMMIT;
GO

