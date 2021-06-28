CREATE TABLE [dbo].[TBTarefas] (
    [Id]                  INT          IDENTITY (1, 1) NOT NULL,
    [Prioridade]          VARCHAR (50) NULL,
    [Titulo]              VARCHAR (50) NULL,
    [DataCriacao]         DATETIME     NULL,
    [DataConclusao]       DATETIME     NULL,
    [PercentualConcluido] DECIMAL (18) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

