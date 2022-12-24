
CREATE PROC db_add_event

@Yritusenimimi NVARCHAR(250),
@Toimumisaeg DATETIME,
@Koht NVARCHAR(250),
@Lisainfo NVARCHAR(250)
AS
BEGIN
INSERT INTO dbo.Eventlist
(
Yritusenimi,
Toimumisaeg,
Koht,
Lisainfo
)
VALUES
(
@Yritusenimimi,
@Toimumisaeg,
@Koht,
@Lisainfo
)
END