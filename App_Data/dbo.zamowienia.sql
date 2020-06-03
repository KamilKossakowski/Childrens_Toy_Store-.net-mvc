CREATE TABLE [dbo].[zamowienia] (
    [Id_zamowienia]   INT            IDENTITY (1, 1) NOT NULL,
    [data_zamowienia] DATE           NULL,
    [id_klienta]      NVARCHAR (128) NOT NULL,
    [status]          INT            DEFAULT ((0)) NULL,
    [id_produktu]     INT            NOT NULL,
    [id_pracownika]   NVARCHAR (128) NULL,
    PRIMARY KEY CLUSTERED ([Id_zamowienia] ASC)
);

