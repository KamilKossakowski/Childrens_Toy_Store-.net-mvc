CREATE TABLE [dbo].[Klienci] (
    [Id_uzytkownik] NVARCHAR (128) NOT NULL,
    [imie]          VARCHAR (50)   NULL,
    [email]         VARCHAR (50)   NULL,
    [nazwisko]      VARCHAR (50)   NULL,
    [ulica]         VARCHAR (50)   NULL,
    [numer_domu]    VARCHAR (10)   NULL,
    [kod_pocztowy]  VARCHAR (6)    NULL,
    [poczta]        VARCHAR (30)   NULL,
    PRIMARY KEY CLUSTERED ([Id_uzytkownik] ASC)
);

