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

CREATE TABLE [Objetos] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [Descricao] nvarchar(max) NOT NULL,
    [LocalidadePrimaria] nvarchar(max) NOT NULL,
    [LocalidadeSecundaria] nvarchar(max) NOT NULL,
    [LocalidadeTercearia] nvarchar(max) NOT NULL,
    [Situacao] int NOT NULL,
    [IdTipoObjeto] int NOT NULL,
    [DtInclusao] datetime2 NOT NULL,
    [IdUsuarioInclusao] int NOT NULL,
    [DtAtualizao] datetime2 NOT NULL,
    [IdUsuarioAtualizacao] int NOT NULL,
    CONSTRAINT [PK_Objetos] PRIMARY KEY ([Id])
);

CREATE TABLE [Requerimentos] (
    [Id] int NOT NULL IDENTITY,
    [Momento] datetime2 NOT NULL,
    [IdTipoRequerimento] int NOT NULL,
    [IdLocacao] int NOT NULL,
    [Observacao] nvarchar(max) NOT NULL,
    [Situacao] int NOT NULL,
    [DataAtualizacao] datetime2 NOT NULL,
    [IdUsuarioAtualizacao] int NOT NULL,
    [UsuarioId] int NOT NULL,
    CONSTRAINT [PK_Requerimentos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Requerimentos_Usuarios_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Requerimentos_UsuarioId] ON [Requerimentos] ([UsuarioId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250823195721_CriacaoObjeto', N'9.0.7');

DELETE FROM [Usuarios]
WHERE [Id] = 3;
SELECT @@ROWCOUNT;


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Cpf', N'DtNascimento', N'DtSituacao', N'Email', N'IdUsuarioSituacao', N'Login', N'Nome', N'RepresentanteLegalId', N'Senha', N'Situacao', N'Telefone', N'TipoUsuarioId') AND [object_id] = OBJECT_ID(N'[Usuarios]'))
    SET IDENTITY_INSERT [Usuarios] ON;
INSERT INTO [Usuarios] ([Id], [Cpf], [DtNascimento], [DtSituacao], [Email], [IdUsuarioSituacao], [Login], [Nome], [RepresentanteLegalId], [Senha], [Situacao], [Telefone], [TipoUsuarioId])
VALUES (2, N'12345678900', '2010-05-15T00:00:00.0000000', '2025-07-14T00:00:00.0000000', N'joao.silva@example.com', 1, N'joaos', N'João Silva', 1, N'*senha123*', 1, N'11987654321', 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Cpf', N'DtNascimento', N'DtSituacao', N'Email', N'IdUsuarioSituacao', N'Login', N'Nome', N'RepresentanteLegalId', N'Senha', N'Situacao', N'Telefone', N'TipoUsuarioId') AND [object_id] = OBJECT_ID(N'[Usuarios]'))
    SET IDENTITY_INSERT [Usuarios] OFF;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250823215425_CriadoHash', N'9.0.7');

DELETE FROM [Usuarios]
WHERE [Id] = 2;
SELECT @@ROWCOUNT;


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Cpf', N'DtNascimento', N'DtSituacao', N'Email', N'IdUsuarioSituacao', N'Login', N'Nome', N'RepresentanteLegalId', N'Senha', N'Situacao', N'Telefone', N'TipoUsuarioId') AND [object_id] = OBJECT_ID(N'[Usuarios]'))
    SET IDENTITY_INSERT [Usuarios] ON;
INSERT INTO [Usuarios] ([Id], [Cpf], [DtNascimento], [DtSituacao], [Email], [IdUsuarioSituacao], [Login], [Nome], [RepresentanteLegalId], [Senha], [Situacao], [Telefone], [TipoUsuarioId])
VALUES (3, N'12345678900', '2010-05-15T00:00:00.0000000', '2025-07-14T00:00:00.0000000', N'joao.silva@example.com', 1, N'joaos', N'João Silva', 1, N'*senha123*', 1, N'11987654321', 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Cpf', N'DtNascimento', N'DtSituacao', N'Email', N'IdUsuarioSituacao', N'Login', N'Nome', N'RepresentanteLegalId', N'Senha', N'Situacao', N'Telefone', N'TipoUsuarioId') AND [object_id] = OBJECT_ID(N'[Usuarios]'))
    SET IDENTITY_INSERT [Usuarios] OFF;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250823223313_HashDasSenhas', N'9.0.7');

EXEC sp_rename N'[Requerimentos].[IdTipoRequerimento]', N'TipoRequerimentoId', 'COLUMN';

CREATE TABLE [TiposRequerimento] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [Descricao] nvarchar(max) NOT NULL,
    [Valor] real NOT NULL,
    [Situacao] int NOT NULL,
    [DataInclusão] datetime2 NOT NULL,
    [IdUsuarioInclusão] int NOT NULL,
    [DataAlteracao] datetime2 NOT NULL,
    [IdUsuarioAtualizacao] int NOT NULL,
    CONSTRAINT [PK_TiposRequerimento] PRIMARY KEY ([Id])
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DataAlteracao', N'DataInclusão', N'Descricao', N'IdUsuarioAtualizacao', N'IdUsuarioInclusão', N'Nome', N'Situacao', N'Valor') AND [object_id] = OBJECT_ID(N'[TiposRequerimento]'))
    SET IDENTITY_INSERT [TiposRequerimento] ON;
INSERT INTO [TiposRequerimento] ([Id], [DataAlteracao], [DataInclusão], [Descricao], [IdUsuarioAtualizacao], [IdUsuarioInclusão], [Nome], [Situacao], [Valor])
VALUES (1, '2025-08-26T00:00:00.0000000', '2025-08-26T00:00:00.0000000', N'Solicitação para trancar matrícula do semestre', 1, 1, N'Trancamento de Matrícula', 0, CAST(0 AS real));
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DataAlteracao', N'DataInclusão', N'Descricao', N'IdUsuarioAtualizacao', N'IdUsuarioInclusão', N'Nome', N'Situacao', N'Valor') AND [object_id] = OBJECT_ID(N'[TiposRequerimento]'))
    SET IDENTITY_INSERT [TiposRequerimento] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DataAtualizacao', N'IdLocacao', N'IdUsuarioAtualizacao', N'Momento', N'Observacao', N'Situacao', N'TipoRequerimentoId', N'UsuarioId') AND [object_id] = OBJECT_ID(N'[Requerimentos]'))
    SET IDENTITY_INSERT [Requerimentos] ON;
INSERT INTO [Requerimentos] ([Id], [DataAtualizacao], [IdLocacao], [IdUsuarioAtualizacao], [Momento], [Observacao], [Situacao], [TipoRequerimentoId], [UsuarioId])
VALUES (1, '2025-08-26T10:00:00.0000000', 101, 0, '2025-08-26T10:00:00.0000000', N'Solicitação enviada pelo aluno João', 3, 1, 3);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DataAtualizacao', N'IdLocacao', N'IdUsuarioAtualizacao', N'Momento', N'Observacao', N'Situacao', N'TipoRequerimentoId', N'UsuarioId') AND [object_id] = OBJECT_ID(N'[Requerimentos]'))
    SET IDENTITY_INSERT [Requerimentos] OFF;

CREATE INDEX [IX_Requerimentos_TipoRequerimentoId] ON [Requerimentos] ([TipoRequerimentoId]);

ALTER TABLE [Requerimentos] ADD CONSTRAINT [FK_Requerimentos_TiposRequerimento_TipoRequerimentoId] FOREIGN KEY ([TipoRequerimentoId]) REFERENCES [TiposRequerimento] ([Id]) ON DELETE CASCADE;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250826190044_AddRequerimentoAddTipoRequerimento', N'9.0.7');

EXEC sp_rename N'[TiposRequerimento].[IdUsuarioInclusão]', N'IdUsuarioInclusao', 'COLUMN';

EXEC sp_rename N'[TiposRequerimento].[DataInclusão]', N'DataInclusao', 'COLUMN';

ALTER TABLE [TiposRequerimento] ADD [UsuarioAtualizacaoId] int NULL;

ALTER TABLE [TiposRequerimento] ADD [UsuarioInclusaoId] int NULL;

UPDATE [TiposRequerimento] SET [UsuarioAtualizacaoId] = NULL, [UsuarioInclusaoId] = NULL
WHERE [Id] = 1;
SELECT @@ROWCOUNT;


CREATE INDEX [IX_TiposRequerimento_UsuarioAtualizacaoId] ON [TiposRequerimento] ([UsuarioAtualizacaoId]);

CREATE INDEX [IX_TiposRequerimento_UsuarioInclusaoId] ON [TiposRequerimento] ([UsuarioInclusaoId]);

ALTER TABLE [TiposRequerimento] ADD CONSTRAINT [FK_TiposRequerimento_Usuarios_UsuarioAtualizacaoId] FOREIGN KEY ([UsuarioAtualizacaoId]) REFERENCES [Usuarios] ([Id]);

ALTER TABLE [TiposRequerimento] ADD CONSTRAINT [FK_TiposRequerimento_Usuarios_UsuarioInclusaoId] FOREIGN KEY ([UsuarioInclusaoId]) REFERENCES [Usuarios] ([Id]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250826201254_AlterandoTipoReqController', N'9.0.7');

CREATE TABLE [PlanosLocacao] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [DtInicio] datetime2 NOT NULL,
    [DtFim] datetime2 NOT NULL,
    [Valor] real NOT NULL,
    [InicioLocacao] nvarchar(max) NOT NULL,
    [FimLocacao] nvarchar(max) NOT NULL,
    [PrazoPagamento] int NOT NULL,
    [Situacao] int NOT NULL,
    [DtInclusao] datetime2 NOT NULL,
    [IdUsuarioInclusao] int NOT NULL,
    [DtAtualizacao] datetime2 NOT NULL,
    [IdUsuarioAtualizacao] int NOT NULL,
    [UsuarioId] int NOT NULL,
    CONSTRAINT [PK_PlanosLocacao] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PlanosLocacao_Usuarios_IdUsuarioAtualizacao] FOREIGN KEY ([IdUsuarioAtualizacao]) REFERENCES [Usuarios] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PlanosLocacao_Usuarios_IdUsuarioInclusao] FOREIGN KEY ([IdUsuarioInclusao]) REFERENCES [Usuarios] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PlanosLocacao_Usuarios_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuarios] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [TipoObjeto] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [Situacao] int NOT NULL,
    [DtInclusao] datetime2 NOT NULL,
    [IdUsuarioInclusao] int NOT NULL,
    [DtAtualizao] datetime2 NOT NULL,
    [IdUsuarioAtualizacao] int NOT NULL,
    CONSTRAINT [PK_TipoObjeto] PRIMARY KEY ([Id])
);

CREATE TABLE [PropostaLocacao] (
    [Id] int NOT NULL IDENTITY,
    [Data] datetime2 NOT NULL,
    [IdUsuario] int NOT NULL,
    [UsuarioId] int NOT NULL,
    [IdObjeto] int NOT NULL,
    [ObjetoId] int NOT NULL,
    [IdPlanoLocacao] int NOT NULL,
    [PlanoLocacaoId] int NOT NULL,
    [DtInicio] datetime2 NOT NULL,
    [DtFim] datetime2 NOT NULL,
    [DtValidade] datetime2 NOT NULL,
    [Valor] real NOT NULL,
    [Situacao] int NOT NULL,
    [DtSituacao] datetime2 NOT NULL,
    [IdUsuarioSituacao] int NOT NULL,
    CONSTRAINT [PK_PropostaLocacao] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PropostaLocacao_Objetos_ObjetoId] FOREIGN KEY ([ObjetoId]) REFERENCES [Objetos] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PropostaLocacao_PlanosLocacao_PlanoLocacaoId] FOREIGN KEY ([PlanoLocacaoId]) REFERENCES [PlanosLocacao] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PropostaLocacao_Usuarios_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [PlanoLocacaoObjeto] (
    [Id] int NOT NULL IDENTITY,
    [IdPlanoLocacao] int NOT NULL,
    [IdTipoObjeto] int NOT NULL,
    [TipoObjetoId] int NOT NULL,
    [Situacao] int NOT NULL,
    [DtInclusao] datetime2 NOT NULL,
    [IdUsuarioInclusao] int NOT NULL,
    [DtAtualizacao] datetime2 NOT NULL,
    [IdUsuarioAtualizacao] int NOT NULL,
    CONSTRAINT [PK_PlanoLocacaoObjeto] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PlanoLocacaoObjeto_PlanosLocacao_IdPlanoLocacao] FOREIGN KEY ([IdPlanoLocacao]) REFERENCES [PlanosLocacao] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PlanoLocacaoObjeto_TipoObjeto_TipoObjetoId] FOREIGN KEY ([TipoObjetoId]) REFERENCES [TipoObjeto] ([Id]) ON DELETE CASCADE
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DtAtualizacao', N'DtFim', N'DtInclusao', N'DtInicio', N'FimLocacao', N'IdUsuarioAtualizacao', N'IdUsuarioInclusao', N'InicioLocacao', N'Nome', N'PrazoPagamento', N'Situacao', N'UsuarioId', N'Valor') AND [object_id] = OBJECT_ID(N'[PlanosLocacao]'))
    SET IDENTITY_INSERT [PlanosLocacao] ON;
INSERT INTO [PlanosLocacao] ([Id], [DtAtualizacao], [DtFim], [DtInclusao], [DtInicio], [FimLocacao], [IdUsuarioAtualizacao], [IdUsuarioInclusao], [InicioLocacao], [Nome], [PrazoPagamento], [Situacao], [UsuarioId], [Valor])
VALUES (1, '2025-09-08T00:00:00.0000000', '2025-10-08T23:59:59.0000000', '2025-09-08T00:00:00.0000000', '2025-09-08T00:00:00.0000000', N'22:00', 1, 1, N'08:00', N'Plano Mensal Armário', 5, 1, 1, CAST(59.9 AS real));
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DtAtualizacao', N'DtFim', N'DtInclusao', N'DtInicio', N'FimLocacao', N'IdUsuarioAtualizacao', N'IdUsuarioInclusao', N'InicioLocacao', N'Nome', N'PrazoPagamento', N'Situacao', N'UsuarioId', N'Valor') AND [object_id] = OBJECT_ID(N'[PlanosLocacao]'))
    SET IDENTITY_INSERT [PlanosLocacao] OFF;

CREATE INDEX [IX_PlanoLocacaoObjeto_IdPlanoLocacao] ON [PlanoLocacaoObjeto] ([IdPlanoLocacao]);

CREATE INDEX [IX_PlanoLocacaoObjeto_TipoObjetoId] ON [PlanoLocacaoObjeto] ([TipoObjetoId]);

CREATE INDEX [IX_PlanosLocacao_IdUsuarioAtualizacao] ON [PlanosLocacao] ([IdUsuarioAtualizacao]);

CREATE INDEX [IX_PlanosLocacao_IdUsuarioInclusao] ON [PlanosLocacao] ([IdUsuarioInclusao]);

CREATE INDEX [IX_PlanosLocacao_UsuarioId] ON [PlanosLocacao] ([UsuarioId]);

CREATE INDEX [IX_PropostaLocacao_ObjetoId] ON [PropostaLocacao] ([ObjetoId]);

CREATE INDEX [IX_PropostaLocacao_PlanoLocacaoId] ON [PropostaLocacao] ([PlanoLocacaoId]);

CREATE INDEX [IX_PropostaLocacao_UsuarioId] ON [PropostaLocacao] ([UsuarioId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250908191007_AddPlanoLocacao', N'9.0.7');

DELETE FROM [Usuarios]
WHERE [Id] = 1;
SELECT @@ROWCOUNT;


DELETE FROM [Usuarios]
WHERE [Id] = 3;
SELECT @@ROWCOUNT;


INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250910011433_excluidoBaseUsuario', N'9.0.7');

UPDATE [Requerimentos] SET [UsuarioId] = 1
WHERE [Id] = 1;
SELECT @@ROWCOUNT;


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Cpf', N'DtNascimento', N'DtSituacao', N'Email', N'IdUsuarioSituacao', N'Login', N'Nome', N'RepresentanteLegalId', N'Senha', N'Situacao', N'Telefone', N'TipoUsuarioId') AND [object_id] = OBJECT_ID(N'[Usuarios]'))
    SET IDENTITY_INSERT [Usuarios] ON;
INSERT INTO [Usuarios] ([Id], [Cpf], [DtNascimento], [DtSituacao], [Email], [IdUsuarioSituacao], [Login], [Nome], [RepresentanteLegalId], [Senha], [Situacao], [Telefone], [TipoUsuarioId])
VALUES (1, N'00000000000', '1990-01-01T00:00:00.0000000', '2025-09-14T11:12:52.3049662-03:00', N'sistema@lockai.com', 1, N'sistema', N'Usuário do Sistema', 1, N'senha123', 1, N'0000000000', 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Cpf', N'DtNascimento', N'DtSituacao', N'Email', N'IdUsuarioSituacao', N'Login', N'Nome', N'RepresentanteLegalId', N'Senha', N'Situacao', N'Telefone', N'TipoUsuarioId') AND [object_id] = OBJECT_ID(N'[Usuarios]'))
    SET IDENTITY_INSERT [Usuarios] OFF;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250914141253_CorrigidoEstruturaData', N'9.0.7');

COMMIT;
GO

