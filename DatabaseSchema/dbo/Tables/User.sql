CREATE TABLE [dbo].[User] (
    [id]        INT        IDENTITY (1, 1) NOT NULL,
    [FirstName] NCHAR (50) NULL,
    [LastName]  NCHAR (50) NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([id] ASC)
);

