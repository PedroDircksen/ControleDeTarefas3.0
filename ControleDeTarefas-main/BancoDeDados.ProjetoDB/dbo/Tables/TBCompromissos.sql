CREATE TABLE [dbo].[TBCompromissos] (
    [Id]              INT IDENTITY  NOT NULL,
    [Assunto]         VARCHAR (50) NULL,
    [Local]           VARCHAR(50)   NULL,
    [DataCompromisso] DATETIME     NULL,
    [HoraInicio]      TIME (7)     NULL,
    [HoraTermino]     TIME (7)     NULL,
    [Id_Contato]      INT          NULL,
    CONSTRAINT [PK_TBCompromissos] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TBCompromissos_TBContatos] FOREIGN KEY ([Id_Contato]) REFERENCES [dbo].[TBContatos] ([Id])
);


