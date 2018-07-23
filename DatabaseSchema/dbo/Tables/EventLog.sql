CREATE TABLE [dbo].[EventLog] (
    [Id]              BIGINT          IDENTITY (1, 1) NOT NULL,
    [CreatedDateTime] DATETIME        NOT NULL,
    [EventId]         INT             NULL,
    [LogLevel]        NVARCHAR (50)   NOT NULL,
    [Logger]          NVARCHAR (100)  NOT NULL,
    [Message]         NVARCHAR (4000) NULL,
    [Host]            NVARCHAR (100)  NOT NULL,
    [Server]          NVARCHAR (100)  NULL
);

