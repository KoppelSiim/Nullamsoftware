CREATE PROC db_get_participants @Id int
AS
SELECT Eesnimi, Perenimi, Isikukood
FROM dbo.Participants
WHERE dbo.Participants.Fk_Participant=@Id
GO
