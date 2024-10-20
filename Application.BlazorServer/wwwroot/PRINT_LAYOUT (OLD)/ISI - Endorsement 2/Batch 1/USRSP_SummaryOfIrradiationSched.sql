USE [SBOTEST_ISI_FINALV1]
GO
/****** Object:  StoredProcedure [dbo].[USRSP_SummaryOfIrradiationSched]    Script Date: 12/11/2023 3:59:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[USRSP_SummaryOfIrradiationSched]
	-- Add the parameters for the stored procedure here
 @datef date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT DISTINCT
FQ.ActualFQ,
CS.ActualCS,
Dos.MinDos,
D.DocNo,
D.ItemCode,
D.DocDate,
E.ItemCode collate SQL_Latin1_General_CP1_CI_AS,
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
CEILING(BatchCount.TimSCount * 1.0 / F.U_NoBoxesPallet) AS NoOfPallets

FROM
SAOTEST_TIMSCORE.DBO.COR3 A 
INNER JOIN  SAOTEST_TIMSCORE.DBO.COR2 B ON A.SampleId = B.Id
INNER JOIN SAOTEST_TIMSCORE.DBO.COR1 C ON B.SampleId = C.Id
INNER JOIN SAOTEST_TIMSCORE.DBO.QCOR D ON C.QCOrderNo = D.QCOrderNo
INNER JOIN ORDR G ON D.DocNo = G.DocNum
INNER JOIN RDR1 E ON D.ItemCode = E.ItemCode collate SQL_Latin1_General_CP1_CI_AS
INNER JOIN OITM F ON E.ItemCode = F.ItemCode
INNER JOIN OITB H ON F.ItmsGrpCod = H.ItmsGrpCod
LEFT JOIN (Select  x.ActualValue[ActualFQ], x.SampleId[FQID] from SAOTest_TIMSCore.DBO.COR3 x where  x.Parameter = 'EB Frequency' ) FQ ON A.SampleId = FQ.FQID
LEFT JOIN (Select  x.ActualValue[ActualCS], x.SampleId[CSID] from SAOTest_TIMSCore.DBO.COR3 x where  x.Parameter = 'Conveyor Speed' ) CS ON A.SampleId = CS.CSID
LEFT JOIN (Select  x.MinValue[MinDos], x.SampleId[DosID] from SAOTest_TIMSCore.DBO.COR3 x where  x.Parameter = 'Dosimetry' ) DOS ON A.SampleId = Dos.DosID
OUTER APPLY 
(
    SELECT COUNT(x.U_TIMS_IUBBinTime) as TimSCount 
    FROM obtn x 
    WHERE E.ItemCode = x.ItemCode 
    AND G.DocNum = x.U_SONO 
    AND ISNULL(x.U_TIMS_IUBBinTime, '') <> ''
)BatchCount
WHERE H.ItmsGrpNam = 'Customer Items' AND G.DocDate = @datef
END
