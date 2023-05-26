BEGIN TRANSACTION;
GO

CREATE TABLE [File] (
    [ID] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [LinkFile] nvarchar(max) NOT NULL,
    [CreateBy] nvarchar(max) NOT NULL,
    [CreateDate] datetime2 NOT NULL,
    [UpdatedBy] nvarchar(max) NOT NULL,
    [UpdateDate] datetime2 NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_File] PRIMARY KEY ([ID])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230526073712_20230526071511_newdatabasefile', N'6.0.0');
GO

COMMIT;
GO

