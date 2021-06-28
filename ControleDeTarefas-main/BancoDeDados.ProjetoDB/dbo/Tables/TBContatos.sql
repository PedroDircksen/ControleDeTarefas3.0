CREATE TABLE [dbo].[TBContatos] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [Nome]     VARCHAR (50) NULL,
    [Email]    VARCHAR (50) NULL,
    [Telefone] VARCHAR (50) NULL,
    [Empresa]  VARCHAR (50) NULL,
    [Cargo]    VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

