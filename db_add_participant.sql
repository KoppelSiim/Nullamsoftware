CREATE PROC db_add_participant

@Isikut TINYINT,
@Eesnimi NVARCHAR(250),
@Perenimi NVARCHAR(250),
@Isikukood VARCHAR(50),
@Maksmisviis TINYINT,
@Lisainfoisik NVARCHAR(250)

AS
BEGIN
INSERT INTO dbo.Participants
(
Isikut,
Eesnimi,
Perenimi,
Isikukood,
Maksmisviis,
Lisainfoisik
)
VALUES
(
@Isikut,
@Eesnimi,
@Perenimi,
@Isikukood,
@Maksmisviis,
@Lisainfoisik
)
END