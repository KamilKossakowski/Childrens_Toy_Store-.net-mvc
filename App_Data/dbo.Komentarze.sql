CREATE TABLE [dbo].[Komentarze] (
    [Id_koment]      INT            IDENTITY (1, 1) NOT NULL,
    [data_dodania]   DATE           NULL,
    [id_uzytkownika] NVARCHAR (128) NOT NULL,
	[e-mail]		nvarchar(128) not null,
    [id_produktu]    INT            NOT NULL,
    [koment]         TEXT           NULL,
    [ocena]          INT            NULL,
    PRIMARY KEY CLUSTERED ([Id_koment] ASC)
);

