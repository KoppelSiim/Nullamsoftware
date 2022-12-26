CREATE PROC get_future_events
AS
SELECT TOP 5 Yritusenimi,Toimumisaeg
FROM dbo.Eventlist
WHERE Toimumisaeg > cast(sysdatetime() as date)
ORDER BY Toimumisaeg
GO