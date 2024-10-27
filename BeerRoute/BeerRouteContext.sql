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
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241025155137_InitialCreate')
BEGIN
    CREATE TABLE [Cervejaria] (
        [Id] int NOT NULL IDENTITY,
        [Nome] nvarchar(max) NOT NULL,
        [Endereco] nvarchar(max) NOT NULL,
        [TipoCerveja] int NOT NULL,
        [Latitude] float NOT NULL,
        [Longitude] float NOT NULL,
        [Preco] decimal(18,2) NOT NULL,
        [Descricao] nvarchar(max) NOT NULL,
        [Telefone] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Site] nvarchar(max) NOT NULL,
        [Facebook] nvarchar(max) NOT NULL,
        [Instagram] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Cervejaria] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241025155137_InitialCreate')
BEGIN
    CREATE TABLE [Usuario] (
        [Id] int NOT NULL IDENTITY,
        [Nome] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Senha] nvarchar(max) NOT NULL,
        [Creditos] int NOT NULL,
        CONSTRAINT [PK_Usuario] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241025155137_InitialCreate')
BEGIN
    CREATE TABLE [Evento] (
        [Id] int NOT NULL IDENTITY,
        [CervejariaId] int NOT NULL,
        [Nome] nvarchar(max) NOT NULL,
        [Descricao] nvarchar(max) NOT NULL,
        [DataEvento] datetime2 NOT NULL,
        CONSTRAINT [PK_Evento] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Evento_Cervejaria_CervejariaId] FOREIGN KEY ([CervejariaId]) REFERENCES [Cervejaria] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241025155137_InitialCreate')
BEGIN
    CREATE TABLE [Visita] (
        [Id] int NOT NULL IDENTITY,
        [UsuarioId] int NOT NULL,
        [CervejariaId] int NOT NULL,
        [DataVisita] datetime2 NOT NULL,
        [CreditosUtilizados] int NOT NULL,
        [Avaliacao] int NOT NULL,
        [Comentario] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Visita] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Visita_Cervejaria_CervejariaId] FOREIGN KEY ([CervejariaId]) REFERENCES [Cervejaria] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Visita_Usuario_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuario] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241025155137_InitialCreate')
BEGIN
    CREATE INDEX [IX_Evento_CervejariaId] ON [Evento] ([CervejariaId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241025155137_InitialCreate')
BEGIN
    CREATE INDEX [IX_Visita_CervejariaId] ON [Visita] ([CervejariaId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241025155137_InitialCreate')
BEGIN
    CREATE INDEX [IX_Visita_UsuarioId] ON [Visita] ([UsuarioId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20241025155137_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241025155137_InitialCreate', N'6.0.35');
END;
GO

COMMIT;
GO