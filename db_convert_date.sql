﻿--This script is compatible with SQL Server 2005 and above.

DECLARE @datetime DATETIME
SET @datetime = GETDATE()
SELECT
@datetime AS [Datetime with seconds and millisconds]
,CONVERT(DATETIME,CONVERT(VARCHAR(13),@datetime,120)+ ':00')
AS [Datetime without seconds & millisconds]
GO
