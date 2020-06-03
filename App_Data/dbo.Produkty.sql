CREATE TABLE [dbo].[Produkty] (
    [Id_produktu]   INT          IDENTITY (1, 1) NOT NULL,
    [nazwa]         VARCHAR (50) NOT NULL,
    [opis]          VARCHAR (max) NULL,
    [cena]          FLOAT (53)   NOT NULL,
    [obrazek]       VARCHAR (max) NULL,
    [dozw_wiek]     INT          NULL,
    [rok_produkcji] INT          NULL,
    PRIMARY KEY CLUSTERED ([Id_produktu] ASC)
);

