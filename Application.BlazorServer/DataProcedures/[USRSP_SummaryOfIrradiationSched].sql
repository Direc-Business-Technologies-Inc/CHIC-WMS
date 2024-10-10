
/****** Object:  StoredProcedure [dbo].[USRSP_SummaryOfIrradiationSched]    Script Date: 8/29/2024 9:29:21 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[USRSP_SummaryOfIrradiationSched]
	-- Add the parameters for the stored procedure here
 @datef date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT DISTINCT
A.DocDate,
CASE WHEN IrradStart.TimeStart >= '0000' AND IrradStart.TimeStart <= '1159' THEN  STUFF(STUFF(STUFF(IrradStart.TimeStart, 1, 0, REPLICATE('0', 4 - LEN(IrradStart.TimeStart))),3,0,':'),4,0,'')  + ' AM'
	WHEN IrradStart.TimeStart >= '1200'  AND IrradStart.TimeStart <= '1259'THEN STUFF(STUFF(STUFF(IrradStart.TimeStart, 1, 0, REPLICATE('0', 4 - LEN(IrradStart.TimeStart))),3,0,':'),4,0,'')  + ' PM'
	WHEN IrradStart.TimeStart >= '1300'  AND IrradStart.TimeStart <= '2359'THEN STUFF(STUFF(STUFF(IrradStart.TimeStart-1200, 1, 0, REPLICATE('0', 4 - LEN(IrradStart.TimeStart-1200))),3,0,':'),4,0,'')  + ' PM'
	END AS [TIME START],
CASE WHEN IrradEnd.TimeEnd >= '0000' AND IrradEnd.TimeEnd <= '1159' THEN  STUFF(STUFF(STUFF(IrradEnd.TimeEnd, 1, 0, REPLICATE('0', 4 - LEN(IrradEnd.TimeEnd))),3,0,':'),4,0,'')  + ' AM'
	WHEN IrradEnd.TimeEnd >= '1200'  AND IrradEnd.TimeEnd <= '1259'THEN STUFF(STUFF(STUFF(IrradEnd.TimeEnd, 1, 0, REPLICATE('0', 4 - LEN(IrradEnd.TimeEnd))),3,0,''),4,0,'')  + ' PM'
	WHEN IrradEnd.TimeEnd >= '1300'  AND IrradEnd.TimeEnd <= '2359'THEN STUFF(STUFF(STUFF(IrradEnd.TimeEnd-1200, 1, 0, REPLICATE('0',4 - LEN(IrradEnd.TimeEnd-1200))),3,0,':'),4,0,'')  + ' PM'
	END AS [TIME END],
(((IrradEnd.TimeEnd - IrradStart.TimeStart) * .6)/60) [Duration],
A.DocNum [SO],
A.CardName,
B.Dscription,
C.U_Dosage [MinValue],
B.ItemCode,
C.U_TIMS_Frequency [EBActual],
C.U_TIMS_ConvSpeed [CSActual],
F.Quantity AS NoOfBoxes
FROM ORDR A
RIGHT JOIN RDR1 B ON A.U_CustItems = B.ItemCode
RIGHT JOIN OITM C ON B.ItemCode = C.ItemCode
RIGHT JOIN OITB D ON C.ItmsGrpCod = D.ItmsGrpCod
RIGHT JOIN OIGN E ON E.U_SONo = A.DocNum
RIGHT JOIN IGN1 F ON F.DocEntry = E.DocEntry AND F.ItemCode = A.U_CustItems
RIGHT JOIN "@SERVICE_DATA_ROW" G ON G.Code = A.U_ServiceType AND G.U_DisplayStatus = 'At Irradiation'
OUTER APPLY 
(
    SELECT TOP 1 COUNT(x.AbsEntry) as TimSCount 
    FROM obtn x 
    WHERE B.ItemCode = x.ItemCode 
    AND A.DocNum = x.U_SONO 
    AND ISNULL(x.U_TIMS_ITSortCode, 0) > G.U_SortCode
)IrradiatedCount
OUTER APPLY 
(
    SELECT MIN(x.U_TIMS_IrradLoadTime) as TimeStart 
    FROM obtn x 
    WHERE B.ItemCode = x.ItemCode 
    AND A.DocNum = x.U_SONO 
    AND ISNULL(x.U_TIMS_IrradLoadTime, '') <> ''
)IrradStart
OUTER APPLY 
(
    SELECT MAX(x.U_TIMS_IUBTime) as TimeEnd 
    FROM obtn x 
    WHERE B.ItemCode = x.ItemCode 
    AND A.DocNum = x.U_SONO 
    AND ISNULL(x.U_TIMS_IUBTime, '') <> ''
)IrradEnd
WHERE D.ItmsGrpNam = 'Customer Items'  
AND IrradiatedCount.TimSCount = F.Quantity
AND A.DocDate = @datef
END
GO


