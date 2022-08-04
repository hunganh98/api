IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210725192136_initiate')
BEGIN
    CREATE TABLE [Blogs] (
        [Id] int NOT NULL IDENTITY,
        [RefId] nvarchar(max) NULL,
        [Name] nvarchar(max) NULL,
        [CreatedTime] datetime2 NOT NULL,
        [CreatedBy] int NOT NULL,
        [LastModifiedTime] datetime2 NOT NULL,
        [LastModifiedBy] int NOT NULL,
        CONSTRAINT [PK_Blogs] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210725192136_initiate')
BEGIN
    CREATE TABLE [Categories] (
        [Id] int NOT NULL IDENTITY,
        [CreatedTime] datetime2 NOT NULL,
        [CreatedBy] int NOT NULL,
        [LastModifiedTime] datetime2 NOT NULL,
        [LastModifiedBy] int NOT NULL,
        [Name] nvarchar(max) NULL,
        [Code] nvarchar(max) NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210725192136_initiate')
BEGIN
    CREATE TABLE [QuestionTypes] (
        [Id] int NOT NULL IDENTITY,
        [CreatedTime] datetime2 NOT NULL,
        [CreatedBy] int NOT NULL,
        [LastModifiedTime] datetime2 NOT NULL,
        [LastModifiedBy] int NOT NULL,
        [QuestionTypeName] nvarchar(max) NULL,
        [QuestionTypeCode] nvarchar(10) NULL,
        CONSTRAINT [PK_QuestionTypes] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210725192136_initiate')
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(max) NULL,
        [LastName] nvarchar(max) NULL,
        [Username] nvarchar(max) NULL,
        [Password] nvarchar(max) NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210725192136_initiate')
BEGIN
    CREATE TABLE [Questions] (
        [Id] int NOT NULL IDENTITY,
        [CreatedTime] datetime2 NOT NULL,
        [CreatedBy] int NOT NULL,
        [LastModifiedTime] datetime2 NOT NULL,
        [LastModifiedBy] int NOT NULL,
        [Content] nvarchar(max) NULL,
        [Title] nvarchar(max) NULL,
        [QuestionTypeId] int NOT NULL,
        [BlogId] int NOT NULL,
        CONSTRAINT [PK_Questions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Questions_Blogs_BlogId] FOREIGN KEY ([BlogId]) REFERENCES [Blogs] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Questions_QuestionTypes_QuestionTypeId] FOREIGN KEY ([QuestionTypeId]) REFERENCES [QuestionTypes] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210725192136_initiate')
BEGIN
    CREATE TABLE [Answers] (
        [Id] int NOT NULL IDENTITY,
        [CreatedTime] datetime2 NOT NULL,
        [CreatedBy] int NOT NULL,
        [LastModifiedTime] datetime2 NOT NULL,
        [LastModifiedBy] int NOT NULL,
        [Title] nvarchar(max) NULL,
        [Content] nvarchar(max) NULL,
        [IsRightAnswer] bit NULL,
        [QuestionId] int NOT NULL,
        CONSTRAINT [PK_Answers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Answers_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210725192136_initiate')
BEGIN
    CREATE INDEX [IX_Answers_QuestionId] ON [Answers] ([QuestionId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210725192136_initiate')
BEGIN
    CREATE INDEX [IX_Questions_BlogId] ON [Questions] ([BlogId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210725192136_initiate')
BEGIN
    CREATE INDEX [IX_Questions_QuestionTypeId] ON [Questions] ([QuestionTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210725192136_initiate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210725192136_initiate', N'3.1.4');
END;

GO

