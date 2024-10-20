USE [SBOTEST_ISI_20240402]
GO
/****** Object:  StoredProcedure [dbo].[USRSP_DailyIrradiationSched]    Script Date: 12/11/2023 2:00:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[USRSP_DailyIrradiationSched]
	-- Add the parameters for the stored procedure here
 @datef date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT 
I.InspectionPlanCode,
A.MinValue,
MAX(J.Version) [Version] ,
A.ActualValue,
D.DocNo,
D.ItemCode,
D.DocDate,
CASE WHEN G.U_IrridiationStart >= '0000' AND G.U_IrridiationStart <= '1159' THEN  STUFF(STUFF(STUFF(G.U_IrridiationStart, 1, 0, REPLICATE('0', 4 - LEN(G.U_IrridiationStart))),3,0,':'),4,0,'')  + ' AM'
	WHEN G.U_IrridiationStart >= '1200'  AND G.U_IrridiationStart <= '1259'THEN STUFF(STUFF(STUFF(G.U_IrridiationStart, 1, 0, REPLICATE('0', 4 - LEN(G.U_IrridiationStart))),3,0,':'),4,0,'')  + ' PM'
	WHEN G.U_IrridiationStart >= '1300'  AND G.U_IrridiationStart <= '2359'THEN STUFF(STUFF(STUFF(G.U_IrridiationStart-1200, 1, 0, REPLICATE('0', 4 - LEN(G.U_IrridiationStart-1200))),3,0,':'),4,0,'')  + ' PM'
	END AS [TIME START],
CASE WHEN G.U_IrridiationEnd >= '0000' AND G.U_IrridiationEnd <= '1159' THEN  STUFF(STUFF(STUFF(G.U_IrridiationEnd, 1, 0, REPLICATE('0', 4 - LEN(G.U_IrridiationEnd))),3,0,':'),4,0,'')  + ' AM'
	WHEN G.U_IrridiationEnd >= '1200'  AND G.U_IrridiationEnd <= '1259'THEN STUFF(STUFF(STUFF(G.U_IrridiationEnd, 1, 0, REPLICATE('0', 4 - LEN(G.U_IrridiationEnd))),3,0,''),4,0,'')  + ' PM'
	WHEN G.U_IrridiationEnd >= '1300'  AND G.U_IrridiationEnd <= '2359'THEN STUFF(STUFF(STUFF(G.U_IrridiationEnd-1200, 1, 0, REPLICATE('0',4 - LEN(G.U_IrridiationEnd-1200))),3,0,':'),4,0,'')  + ' PM'
	END AS [TIME END],
(((G.U_IrridiationEnd - G.U_IrridiationStart) * .6)/60) [Duration],
G.DocNum [SO],
G.CardName,
G.DocDate,
E.Dscription,
E.ItemCode,
BatchCount.TimSCount AS NoOfBoxes,
F.U_NoBoxesPallet,
CEILING(BatchCount.TimSCount * 1.0 / F.U_NoBoxesPallet) AS NoOfPallets,
J.Parameter [CIP Param],
EB.EBActual,
CS.CSActual

FROM
SAOTest_TIMSCore.DBO.COR3 A 
RIGHT JOIN  SAOTest_TIMSCore.DBO.COR2 B ON A.SampleId = B.Id
RIGHT JOIN SAOTest_TIMSCore.DBO.COR1 C ON B.SampleId = C.Id
RIGHT JOIN SAOTest_TIMSCore.DBO.QCOR D ON C.QCOrderNo = D.QCOrderNo
RIGHT JOIN SAOTest_TIMSCore.DBO.QCIP I ON D.InspectionPlanCode = I.InspectionPlanCode and D.InspectionPlanType = I.PlanType
RIGHT JOIN SAOTest_TIMSCore.DBO.CIP1 J ON I.InspectionPlanCode = J.InspectionPlanCode
RIGHT JOIN ORDR G ON D.DocNo = G.DocNum
RIGHT JOIN RDR1 E ON D.ItemCode = E.ItemCode collate SQL_Latin1_General_CP1_CI_AS
RIGHT JOIN OITM F ON E.ItemCode = F.ItemCode
RIGHT JOIN OITB H ON F.ItmsGrpCod = H.ItmsGrpCod
LEFT JOIN (Select x.ActualValue[EBActual], x.SampleId [EBId] from SAOTest_TIMSCore.DBO.COR3 x where  x.Parameter = 'EB Frequency' ) EB ON A.SampleId = EB.EBId
LEFT JOIN (Select x.ActualValue[CSActual], x.SampleId [CSId] from SAOTest_TIMSCore.DBO.COR3 x where  x.Parameter = 'Conveyor Speed' ) CS ON A.SampleId = CS.CSId
OUTER APPLY 
(
    SELECT COUNT(x.U_TIMS_IUBBinTime) as TimSCount 
    FROM obtn x 
    WHERE E.ItemCode = x.ItemCode 
    AND G.DocNum = x.U_SONO 
    AND ISNULL(x.U_TIMS_IUBBinTime, '') <> ''
)BatchCount
WHERE H.ItmsGrpNam = 'Customer Items'  AND G.DocDate = @datef AND J.Parameter IN ('EB Frequency','Conveyor Speed')
GROUP BY 
A.ActualValue,
D.DocNo,
D.ItemCode,
D.DocDate,
E.ItemCode collate SQL_Latin1_General_CP1_CI_AS,
G.U_IrridiationStart,
G.U_IrridiationEnd,
G.DocNum,
G.CardName,
G.DocDate,
E.Dscription,
E.ItemCode,
BatchCount.TimSCount,
F.U_NoBoxesPallet,
J.Parameter,
A.MinValue,
I.InspectionPlanCode,
Eb.EBActual,
CS.CSActual
END
