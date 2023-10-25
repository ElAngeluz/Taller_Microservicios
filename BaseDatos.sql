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

CREATE TABLE [person] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [Gender] int NOT NULL,
    [YearsOld] bigint NOT NULL,
    [Identification] nvarchar(15) NOT NULL,
    [Address] nvarchar(100) NOT NULL,
    [Phone] nvarchar(25) NOT NULL,
    CONSTRAINT [PK_person] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [cliente] (
    [ClientId] uniqueidentifier NOT NULL,
    [Password] nvarchar(50) NOT NULL,
    [State] bit NOT NULL,
    [PersonId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_cliente] PRIMARY KEY ([ClientId]),
    CONSTRAINT [FK_cliente_person_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [person] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Account] (
    [Id] uniqueidentifier NOT NULL,
    [Type] int NOT NULL,
    [State] bit NOT NULL,
    [Number] nvarchar(50) NOT NULL,
    [Balance] decimal(10,5) NOT NULL,
    [ClientId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Account] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Account_cliente_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [cliente] ([ClientId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Movement] (
    [Id] uniqueidentifier NOT NULL,
    [Date] datetime2 NOT NULL,
    [MovementType] int NOT NULL,
    [Value] decimal(10,5) NOT NULL,
    [ValueBalance] decimal(10,5) NOT NULL,
    [AccountId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Movement] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Movement_Account_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Account] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Account_ClientId] ON [Account] ([ClientId]);
GO

CREATE UNIQUE INDEX [IX_cliente_PersonId] ON [cliente] ([PersonId]);
GO

CREATE INDEX [IX_Movement_AccountId] ON [Movement] ([AccountId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231022193727_InitDB', N'6.0.23');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [person] ADD [State] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Movement] ADD [State] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231023061919_updatefields', N'6.0.23');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231024013749_identityField', N'6.0.23');
GO

COMMIT;
GO

