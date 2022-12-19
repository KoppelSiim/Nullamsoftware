CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Yrituse nimi] NVARCHAR(MAX) NULL, 
    [Toimumisaeg] DATETIME NULL, 
    [Koht] NVARCHAR(MAX) NULL, 
    [Lisainfo] NVARCHAR(MAX) NULL
)
