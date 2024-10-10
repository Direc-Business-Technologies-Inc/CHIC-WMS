
/****** Object:  StoredProcedure [dbo].[USRSP_DailyIrradiationSched]    Script Date: 8/29/2024 9:27:39 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USRSP_DailyIrradiationSched]
	-- Add the parameters for the stored procedure here
 @datef date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT DISTINCT
A.DocDate,
CASE WHEN A.U_IrridiationStart >= '0000' AND A.U_IrridiationStart <= '1159' THEN  STUFF(STUFF(STUFF(A.U_IrridiationStart, 1, 0, REPLICATE('0', 4 - LEN(A.U_IrridiationStart))),3,0,':'),4,0,'')  + ' AM'
	WHEN A.U_IrridiationStart >= '1200'  AND A.U_IrridiationStart <= '1259'THEN STUFF(STUFF(STUFF(A.U_IrridiationStart, 1, 0, REPLICATE('0', 4 - LEN(A.U_IrridiationStart))),3,0,':'),4,0,'')  + ' PM'
	WHEN A.U_IrridiationStart >= '1300'  AND A.U_IrridiationStart <= '2359'THEN STUFF(STUFF(STUFF(A.U_IrridiationStart-1200, 1, 0, REPLICATE('0', 4 - LEN(A.U_IrridiationStart-1200))),3,0,':'),4,0,'')  + ' PM'
	END AS [TIME START],
CASE WHEN A.U_IrridiationEnd >= '0000' AND A.U_IrridiationEnd <= '1159' THEN  STUFF(STUFF(STUFF(A.U_IrridiationEnd, 1, 0, REPLICATE('0', 4 - LEN(A.U_IrridiationEnd))),3,0,':'),4,0,'')  + ' AM'
	WHEN A.U_IrridiationEnd >= '1200'  AND A.U_IrridiationEnd <= '1259'THEN STUFF(STUFF(STUFF(A.U_IrridiationEnd, 1, 0, REPLICATE('0', 4 - LEN(A.U_IrridiationEnd))),3,0,''),4,0,'')  + ' PM'
	WHEN A.U_IrridiationEnd >= '1300'  AND A.U_IrridiationEnd <= '2359'THEN STUFF(STUFF(STUFF(A.U_IrridiationEnd-1200, 1, 0, REPLICATE('0',4 - LEN(A.U_IrridiationEnd-1200))),3,0,':'),4,0,'')  + ' PM'
	END AS [TIME END],
((DATEDIFF(MINUTE,
CAST(STUFF(RIGHT('0000' + CAST(A.U_IrridiationStart AS VARCHAR(4)), 4), 3, 0, ':') AS TIME)
,CAST(STUFF(RIGHT('0000' + CAST(A.U_IrridiationEnd AS VARCHAR(4)), 4), 3, 0, ':') AS TIME))) * 0.01) [Duration],
--(((A.U_IrridiationEnd - A.U_IrridiationStart) * 0.6)/60) [Duration],
A.DocNum [SO],
A.CardName,
B.Dscription,
C.U_Dosage [MinValue],
B.ItemCode,
C.U_TIMS_Frequency [EBActual],
C.U_TIMS_ConvSpeed [CSActual],
C.U_NoBoxesPallet AS NoOfBoxes
FROM ORDR A
RIGHT JOIN RDR1 B ON A.U_CustItems = B.ItemCode
RIGHT JOIN OITM C ON B.ItemCode = C.ItemCode
RIGHT JOIN OITB D ON C.ItmsGrpCod = D.ItmsGrpCod
OUTER APPLY 
(
    SELECT COUNT(x.AbsEntry) as TimSCount 
    FROM obtn x 
    WHERE B.ItemCode = x.ItemCode 
    AND A.DocNum = x.U_SONO 
)BatchCount
WHERE D.ItmsGrpNam = 'Customer Items'  AND A.U_IrridiationDate = @datef
END
GO


