
/****** Object:  StoredProcedure [dbo].[USRSP_SOReport-QA]    Script Date: 8/29/2024 9:28:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
CREATE OR ALTER PROCEDURE [dbo].[USRSP_SOReport-QA]
	-- Add the parameters for the stored procedure here
 @DocKey NVARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT DISTINCT
G.Docnum [SO],
G.U_PONo [PO],
E.Dscription collate SQL_Latin1_General_CP1_CI_AS [SO ItemCode],
D.ManufacturingLotNo,
A.MinValue,
A.MaxValue,
D.ItemCode,
D.StorageConditions,
A.ActualValue,
A.Parameter,
D.QCOrderNo,
D.OverallPassTolerancePercentage [PASS Criteria],
(D.OverallPassTolerancePercentage - 100) [Fail Criteria],
(((C.TotalNoOfPassed)/C.TotalNoSamples)*100) [Inspection Result Pass],
(((C.TotalNoOfFailed)/C.TotalNoSamples)*100) [Inspection ResultFailed],
(((C.TotalNoOfPassed + C.TotalNoOfFailed)/C.TotalNoSamples)*100) [Inspection Result],
D.Quantity [Qty Per SO],
BatchCount.QtyActual [Sum of all Batches in SO with value in Irrad Unloading Time],
D.Remarks [QCOR Rems],
I.EBOperationLog,
I.NCReport,
G.U_IrridiationDate,
BatchC3.TimsCount [Sum of all Batches in SO with value in Irrad Loading Time],
--'' [Sum of all Batches in SO with value in Irrad Unloading Time],
A.TargetValue,
A.Result,
B.SampleNo,
A.ActualValue,
D.DocNo,
D.ItemCode,
D.DocDate,
F.U_TIMS_BeamEnergy [PlanEnergy],
F.U_TIMS_BeamPower [PlanPower],
F.U_TIMS_Frequency [PlanFQ],
En.*,
Pow.*,
FQ.*,
Dosi.*,
CASE WHEN ActRec.RecLastRow >= '0000' AND ActRec.RecLastRow <= '1159' THEN  STUFF(STUFF(STUFF(ActRec.RecLastRow, 1, 0, REPLICATE('0', 4 - LEN(ActRec.RecLastRow))),3,0,':'),4,0,'')  + ' AM'
	WHEN ActRec.RecLastRow >= '1200'  AND ActRec.RecLastRow <= '1259'THEN STUFF(STUFF(STUFF(ActRec.RecLastRow, 1, 0, REPLICATE('0', 4 - LEN(ActRec.RecLastRow))),3,0,':'),4,0,'')  + ' PM'
	WHEN ActRec.RecLastRow >= '1300'  AND ActRec.RecLastRow <= '2359'THEN STUFF(STUFF(STUFF(ActRec.RecLastRow-1200, 1, 0, REPLICATE('0', 4 - LEN(ActRec.RecLastRow-1200))),3,0,':'),4,0,'')  + ' PM'
	END AS [ACTREC_TIMEEND],
CASE WHEN ActRec.Rec1stRow >= '0000' AND ActRec.Rec1stRow <= '1159' THEN  STUFF(STUFF(STUFF(ActRec.Rec1stRow, 1, 0, REPLICATE('0', 4 - LEN(ActRec.Rec1stRow))),3,0,':'),4,0,'')  + ' AM'
	WHEN ActRec.Rec1stRow >= '1200'  AND ActRec.Rec1stRow <= '1259'THEN STUFF(STUFF(STUFF(ActRec.Rec1stRow, 1, 0, REPLICATE('0', 4 - LEN(ActRec.Rec1stRow))),3,0,':'),4,0,'')  + ' PM'
	WHEN ActRec.Rec1stRow >= '1300'  AND ActRec.Rec1stRow <= '2359'THEN STUFF(STUFF(STUFF(ActRec.Rec1stRow-1200, 1, 0, REPLICATE('0', 4 - LEN(ActRec.Rec1stRow-1200))),3,0,':'),4,0,'')  + ' PM'
	END AS [ACTREC_TIMESTART],
ActRec.RecDuration,
CASE WHEN ActISBI.ISBILastRow >= '0000' AND ActISBI.ISBILastRow <= '1159' THEN  STUFF(STUFF(STUFF(ActISBI.ISBILastRow, 1, 0, REPLICATE('0', 4 - LEN(ActISBI.ISBILastRow))),3,0,':'),4,0,'')  + ' AM'
	WHEN ActISBI.ISBILastRow >= '1200'  AND ActISBI.ISBILastRow <= '1259'THEN STUFF(STUFF(STUFF(ActISBI.ISBILastRow, 1, 0, REPLICATE('0', 4 - LEN(ActISBI.ISBILastRow))),3,0,':'),4,0,'')  + ' PM'
	WHEN ActISBI.ISBILastRow >= '1300'  AND ActISBI.ISBILastRow <= '2359'THEN STUFF(STUFF(STUFF(ActISBI.ISBILastRow-1200, 1, 0, REPLICATE('0', 4 - LEN(ActISBI.ISBILastRow-1200))),3,0,':'),4,0,'')  + ' PM'
	END AS [ACTISBI_TIMEEND],
CASE WHEN ActISBI.ISBI1stRow >= '0000' AND ActISBI.ISBI1stRow <= '1159' THEN  STUFF(STUFF(STUFF(ActISBI.ISBI1stRow, 1, 0, REPLICATE('0', 4 - LEN(ActISBI.ISBI1stRow))),3,0,':'),4,0,'')  + ' AM'
	WHEN ActISBI.ISBI1stRow >= '1200'  AND ActISBI.ISBI1stRow <= '1259'THEN STUFF(STUFF(STUFF(ActISBI.ISBI1stRow, 1, 0, REPLICATE('0', 4 - LEN(ActISBI.ISBI1stRow))),3,0,':'),4,0,'')  + ' PM'
	WHEN ActISBI.ISBI1stRow >= '1300'  AND ActISBI.ISBI1stRow <= '2359'THEN STUFF(STUFF(STUFF(ActISBI.ISBI1stRow-1200, 1, 0, REPLICATE('0', 4 - LEN(ActISBI.ISBI1stRow-1200))),3,0,':'),4,0,'')  + ' PM'
	END AS [ACTISBI_TIMESTART],
ActISBI.ISBIDuration,
CASE WHEN ActForIrrad.FRLastRow >= '0000' AND ActForIrrad.FRLastRow <= '1159' THEN  STUFF(STUFF(STUFF(ActForIrrad.FRLastRow, 1, 0, REPLICATE('0', 4 - LEN(ActForIrrad.FRLastRow))),3,0,':'),4,0,'')  + ' AM'
	WHEN ActForIrrad.FRLastRow >= '1200'  AND ActForIrrad.FRLastRow <= '1259'THEN STUFF(STUFF(STUFF(ActForIrrad.FRLastRow, 1, 0, REPLICATE('0', 4 - LEN(ActForIrrad.FRLastRow))),3,0,':'),4,0,'')  + ' PM'
	WHEN ActForIrrad.FRLastRow >= '1300'  AND ActForIrrad.FRLastRow <= '2359'THEN STUFF(STUFF(STUFF(ActForIrrad.FRLastRow-1200, 1, 0, REPLICATE('0', 4 - LEN(ActForIrrad.FRLastRow-1200))),3,0,':'),4,0,'')  + ' PM'
	END AS [ACTFR_TIMEEND],
CASE WHEN ActForIrrad.FR1stRow >= '0000' AND ActForIrrad.FR1stRow <= '1159' THEN  STUFF(STUFF(STUFF(ActForIrrad.FR1stRow, 1, 0, REPLICATE('0', 4 - LEN(ActForIrrad.FR1stRow))),3,0,':'),4,0,'')  + ' AM'
	WHEN ActForIrrad.FR1stRow >= '1200'  AND ActForIrrad.FR1stRow <= '1259'THEN STUFF(STUFF(STUFF(ActForIrrad.FR1stRow, 1, 0, REPLICATE('0', 4 - LEN(ActForIrrad.FR1stRow))),3,0,':'),4,0,'')  + ' PM'
	WHEN ActForIrrad.FR1stRow >= '1300'  AND ActForIrrad.FR1stRow <= '2359'THEN STUFF(STUFF(STUFF(ActForIrrad.FR1stRow-1200, 1, 0, REPLICATE('0', 4 - LEN(ActForIrrad.FR1stRow-1200))),3,0,':'),4,0,'')  + ' PM'
	END AS [ACTFR_TIMESTART],
ActForIrrad.FRDuration,
CASE WHEN ActATIrrad.AILastRow >= '0000' AND ActATIrrad.AILastRow <= '1159' THEN  STUFF(STUFF(STUFF(ActATIrrad.AILastRow, 1, 0, REPLICATE('0', 4 - LEN(ActATIrrad.AILastRow))),3,0,':'),4,0,'')  + ' AM'
	WHEN ActATIrrad.AILastRow >= '1200'  AND ActATIrrad.AILastRow <= '1259'THEN STUFF(STUFF(STUFF(ActATIrrad.AILastRow, 1, 0, REPLICATE('0', 4 - LEN(ActATIrrad.AILastRow))),3,0,':'),4,0,'')  + ' PM'
	WHEN ActATIrrad.AILastRow >= '1300'  AND ActATIrrad.AILastRow <= '2359'THEN STUFF(STUFF(STUFF(ActATIrrad.AILastRow-1200, 1, 0, REPLICATE('0', 4 - LEN(ActATIrrad.AILastRow-1200))),3,0,':'),4,0,'')  + ' PM'
	END AS [ACTAI_TIMEEND],
CASE WHEN ActATIrrad.AI1stRow >= '0000' AND ActATIrrad.AI1stRow <= '1159' THEN  STUFF(STUFF(STUFF(ActATIrrad.AI1stRow, 1, 0, REPLICATE('0', 4 - LEN(ActATIrrad.AI1stRow))),3,0,':'),4,0,'')  + ' AM'
	WHEN ActATIrrad.AI1stRow >= '1200'  AND ActATIrrad.AI1stRow <= '1259'THEN STUFF(STUFF(STUFF(ActATIrrad.AI1stRow, 1, 0, REPLICATE('0', 4 - LEN(ActATIrrad.AI1stRow))),3,0,':'),4,0,'')  + ' PM'
	WHEN ActATIrrad.AI1stRow >= '1300'  AND ActATIrrad.AI1stRow <= '2359'THEN STUFF(STUFF(STUFF(ActATIrrad.AI1stRow-1200, 1, 0, REPLICATE('0', 4 - LEN(ActATIrrad.AI1stRow-1200))),3,0,':'),4,0,'')  + ' PM'
	END AS [ACTAI_TIMESTART],
ActATIrrad.AIDuration,
CASE WHEN ActISAI.ISAILastRow >= '0000' AND ActISAI.ISAILastRow <= '1159' THEN  STUFF(STUFF(STUFF(ActISAI.ISAILastRow, 1, 0, REPLICATE('0', 4 - LEN(ActISAI.ISAILastRow))),3,0,':'),4,0,'')  + ' AM'
	WHEN ActISAI.ISAILastRow >= '1200'  AND ActISAI.ISAILastRow <= '1259'THEN STUFF(STUFF(STUFF(ActISAI.ISAILastRow, 1, 0, REPLICATE('0', 4 - LEN(ActISAI.ISAILastRow))),3,0,':'),4,0,'')  + ' PM'
	WHEN ActISAI.ISAILastRow >= '1300'  AND ActISAI.ISAILastRow <= '2359'THEN STUFF(STUFF(STUFF(ActISAI.ISAILastRow-1200, 1, 0, REPLICATE('0', 4 - LEN(ActISAI.ISAILastRow-1200))),3,0,':'),4,0,'')  + ' PM'
	END AS [ACTISAI_TIMEEND],
CASE WHEN ActISAI.ISAI1stRow >= '0000' AND ActISAI.ISAI1stRow <= '1159' THEN  STUFF(STUFF(STUFF(ActISAI.ISAI1stRow, 1, 0, REPLICATE('0', 4 - LEN(ActISAI.ISAI1stRow))),3,0,':'),4,0,'')  + ' AM'
	WHEN ActISAI.ISAI1stRow >= '1200'  AND ActISAI.ISAI1stRow <= '1259'THEN STUFF(STUFF(STUFF(ActISAI.ISAI1stRow, 1, 0, REPLICATE('0', 4 - LEN(ActISAI.ISAI1stRow))),3,0,':'),4,0,'')  + ' PM'
	WHEN ActISAI.ISAI1stRow >= '1300'  AND ActISAI.ISAI1stRow <= '2359'THEN STUFF(STUFF(STUFF(ActISAI.ISAI1stRow-1200, 1, 0, REPLICATE('0', 4 - LEN(ActISAI.ISAI1stRow-1200))),3,0,':'),4,0,'')  + ' PM'
	END AS [ACTISAI_TIMESTART],
ActISAI.ISAIDuration,
/*CASE WHEN ActAQCR.AQCRLastRow >= '0000' AND ActAQCR.AQCRLastRow <= '1159' THEN  STUFF(STUFF(STUFF(ActAQCR.AQCRLastRow, 1, 0, REPLICATE('0', 4 - LEN(ActAQCR.AQCRLastRow))),3,0,':'),4,0,'')  + ' AM'
	WHEN ActAQCR.AQCRLastRow >= '1200'  AND ActAQCR.AQCRLastRow <= '1259'THEN STUFF(STUFF(STUFF(ActAQCR.AQCRLastRow, 1, 0, REPLICATE('0', 4 - LEN(ActAQCR.AQCRLastRow))),3,0,':'),4,0,'')  + ' PM'
	WHEN ActAQCR.AQCRLastRow >= '1300'  AND ActAQCR.AQCRLastRow <= '2359'THEN STUFF(STUFF(STUFF(ActAQCR.AQCRLastRow-1200, 1, 0, REPLICATE('0', 4 - LEN(ActAQCR.AQCRLastRow-1200))),3,0,':'),4,0,'')  + ' PM'
	END AS [ACTDIS_TIMEEND],*/
CASE WHEN ActAQCR.AQCR1stRow >= '0000' AND ActAQCR.AQCR1stRow <= '1159' THEN  STUFF(STUFF(STUFF(ActAQCR.AQCR1stRow, 1, 0, REPLICATE('0', 4 - LEN(ActAQCR.AQCR1stRow))),3,0,':'),4,0,'')  + ' AM'
	WHEN ActAQCR.AQCR1stRow >= '1200'  AND ActAQCR.AQCR1stRow <= '1259'THEN STUFF(STUFF(STUFF(ActAQCR.AQCR1stRow, 1, 0, REPLICATE('0', 4 - LEN(ActAQCR.AQCR1stRow))),3,0,':'),4,0,'')  + ' PM'
	WHEN ActAQCR.AQCR1stRow >= '1300'  AND ActAQCR.AQCR1stRow <= '2359'THEN STUFF(STUFF(STUFF(ActAQCR.AQCR1stRow-1200, 1, 0, REPLICATE('0', 4 - LEN(ActAQCR.AQCR1stRow-1200))),3,0,':'),4,0,'')  + ' PM'
	END AS [AQCR1stRow],
	  RIGHT(CONVERT(VARCHAR,  Actaqcr.AQCRLastRow, 100),7) AS [12HourFormat],
ActAQCR.TimeDifferenceInHours,
ActAQCR.RemainingMinutes,
CASE WHEN ActDispatch.DisLastRow >= '0000' AND ActDispatch.DisLastRow <= '1159' THEN  STUFF(STUFF(STUFF(ActDispatch.DisLastRow, 1, 0, REPLICATE('0', 4 - LEN(ActDispatch.DisLastRow))),3,0,':'),4,0,'')  + ' AM'
	WHEN ActDispatch.DisLastRow >= '1200'  AND ActDispatch.DisLastRow <= '1259'THEN STUFF(STUFF(STUFF(ActDispatch.DisLastRow, 1, 0, REPLICATE('0', 4 - LEN(ActDispatch.DisLastRow))),3,0,':'),4,0,'')  + ' PM'
	WHEN ActDispatch.DisLastRow >= '1300'  AND ActDispatch.DisLastRow <= '2359'THEN STUFF(STUFF(STUFF(ActDispatch.DisLastRow-1200, 1, 0, REPLICATE('0', 4 - LEN(ActDispatch.DisLastRow-1200))),3,0,':'),4,0,'')  + ' PM'
	END AS [ACTDIS_TIMEEND],
CASE WHEN ActDispatch.Dis1stRow >= '0000' AND ActDispatch.Dis1stRow <= '1159' THEN  STUFF(STUFF(STUFF(ActDispatch.Dis1stRow, 1, 0, REPLICATE('0', 4 - LEN(ActDispatch.Dis1stRow))),3,0,':'),4,0,'')  + ' AM'
	WHEN ActDispatch.Dis1stRow >= '1200'  AND ActDispatch.Dis1stRow <= '1259'THEN STUFF(STUFF(STUFF(ActDispatch.Dis1stRow, 1, 0, REPLICATE('0', 4 - LEN(ActDispatch.Dis1stRow))),3,0,':'),4,0,'')  + ' PM'
	WHEN ActDispatch.Dis1stRow >= '1300'  AND ActDispatch.Dis1stRow <= '2359'THEN STUFF(STUFF(STUFF(ActDispatch.Dis1stRow-1200, 1, 0, REPLICATE('0', 4 - LEN(ActDispatch.Dis1stRow-1200))),3,0,':'),4,0,'')  + ' PM'
	END AS [ACTDIS_TIMESTART],
ActDispatch.DisDuration,
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
F.U_NoBoxesPallet,
CEILING(BatchC2.TimSCount * 1.0 / F.U_NoBoxesPallet) AS NoOfPallets
  FROM
        SAOLIVE_TIMS.DBO.QCOR D
       INNER JOIN SAOLIVE_TIMS.DBO.COR1 C ON D.QCOrderNo = C.QCOrderNo
       INNER JOIN SAOLIVE_TIMS.DBO.COR2 B ON C.Id = B.SampleId
       INNER JOIN SAOLIVE_TIMS.DBO.COR3 A ON B.Id = A.SampleId
       INNER JOIN SAOLIVE_TIMS.DBO.COR4 I ON I.QCOrderNo = D.QCOrderNo
       INNER JOIN ORDR G ON D.DocNo = G.DocNum
       INNER JOIN RDR1 E ON D.ItemCode COLLATE SQL_Latin1_General_CP1_CI_AS = E.ItemCode
       INNER JOIN OITM F ON E.ItemCode = F.ItemCode
       INNER JOIN OITB H ON F.ItmsGrpCod = H.ItmsGrpCod
	   LEFT JOIN (
			SELECT 
				x.U_SONo,
				x.ItemCode,
				MAX(x.U_TIMS_RecTime)[RecLastRow],
				MIN(x.U_TIMS_RecTime)[Rec1stRow],
				(MAX(x.U_TIMS_RecTime) - MIN(x.U_TIMS_RecTime)) [RecDuration]
			FROM
				OBTN x
				INNER JOIN OIGN y ON x.U_SONo = y.U_SONo 
				INNER JOIN IGN1 z ON y.DocEntry = z.DocEntry AND x.ItemCode = z.ItemCode
			GROUP BY
			X.U_SONo,
			X.ItemCode
	   ) ActRec ON G.DocNum = ActRec.U_SONo AND E.ItemCode = ActRec.ItemCode
	    LEFT JOIN (
			SELECT 
				x.U_SONo,
				x.ItemCode,
				MAX(x.U_TIMS_ITRecBinTime)[ISBILastRow],
				MIN(x.U_TIMS_ITRecBinTime)[ISBI1stRow],
				(MAX(x.U_TIMS_ITRecBinTime) - MIN(x.U_TIMS_ITRecBinTime)) [ISBIDuration]
			FROM
				OBTN x
				INNER JOIN OIGN y ON x.U_SONo = y.U_SONo 
				INNER JOIN IGN1 z ON y.DocEntry = z.DocEntry AND x.ItemCode = z.ItemCode
			GROUP BY
			X.U_SONo,
			X.ItemCode
	   ) ActISBI ON G.DocNum = ActISBI.U_SONo AND E.ItemCode = ActISBI.ItemCode
	    LEFT JOIN (
			SELECT 
				x.U_SONo,
				x.ItemCode,
				MAX(x.U_TIMS_ITBinILBTime)[FRLastRow],
				MIN(x.U_TIMS_ITBinILBTime)[FR1stRow],
				(MAX(x.U_TIMS_ITBinILBTime) - MIN(x.U_TIMS_ITBinILBTime)) [FRDuration]
			FROM
				OBTN x
				INNER JOIN OIGN y ON x.U_SONo = y.U_SONo 
				INNER JOIN IGN1 z ON y.DocEntry = z.DocEntry AND x.ItemCode = z.ItemCode
			GROUP BY
			X.U_SONo,
			X.ItemCode
	   ) ActForIrrad ON G.DocNum = ActForIrrad.U_SONo AND E.ItemCode = ActForIrrad.ItemCode
	    LEFT JOIN (
			SELECT 
				x.U_SONo,
				x.ItemCode,
				MAX(x.U_TIMS_IUBTime)[AILastRow],
				MIN(x.U_TIMS_IrradLoadTime)[AI1stRow],
				(MAX(x.U_TIMS_IUBTime) - MIN(x.U_TIMS_IrradLoadTime)) [AIDuration]
			FROM
				OBTN x
				INNER JOIN OIGN y ON x.U_SONo = y.U_SONo 
				INNER JOIN IGN1 z ON y.DocEntry = z.DocEntry AND x.ItemCode = z.ItemCode
			GROUP BY
			X.U_SONo,
			X.ItemCode
	   ) ActATIrrad ON G.DocNum = ActATIrrad.U_SONo AND E.ItemCode = ActATIrrad.ItemCode
	    LEFT JOIN (
			SELECT 
				x.U_SONo,
				x.ItemCode,
				MAX(x.U_TIMS_ITBinDispTime)[ISAILastRow],
				MIN(x.U_TIMS_ITBinDispTime)[ISAI1stRow],
				(MAX(x.U_TIMS_ITBinDispTime) - MIN(x.U_TIMS_ITBinDispTime)) [ISAIDuration]
			FROM
				OBTN x
				INNER JOIN OIGN y ON x.U_SONo = y.U_SONo 
				INNER JOIN IGN1 z ON y.DocEntry = z.DocEntry AND x.ItemCode = z.ItemCode
			GROUP BY
			X.U_SONo,
			X.ItemCode
	   ) ActISAI ON G.DocNum = ActISAI.U_SONo AND E.ItemCode = ActISAI.ItemCode
	    LEFT JOIN (
			SELECT 
    x.U_SONo,
    x.ItemCode,
    MAX(aa.CreateDate) AS AQCRLastRow,
	MIN(x.U_TIMS_IUBTime) AS AQCR1stRow,
	--STUFF(MIN(x.U_TIMS_IUBTime), 3, 0, ':') AS AQCR1stRow,
     ABS(DATEDIFF(HOUR, 
        CAST(DATEADD(MINUTE, DATEDIFF(MINUTE, 
            CAST(DATEADD(HOUR, -12, MAX(aa.CreateDate)) AS TIME),
            CAST(STUFF(STUFF(STUFF(MIN(x.U_TIMS_IUBTime), 1, 0, REPLICATE('0', 4 - LEN(MIN(x.U_TIMS_IUBTime)))), 3, 0, ':'), 4, 0, '') AS TIME)
        ), 0) AS TIME),
        0
    )) AS TimeDifferenceInHours,
    DATEDIFF(MINUTE, 
        CAST(DATEADD(HOUR, -12, MAX(aa.CreateDate)) AS TIME),
        CAST(STUFF(STUFF(STUFF(MIN(x.U_TIMS_IUBTime), 1, 0, REPLICATE('0', 4 - LEN(MIN(x.U_TIMS_IUBTime)))), 3, 0, ':'), 4, 0, '') AS TIME)
    ) % 60 AS RemainingMinutes

FROM
    OBTN x
    INNER JOIN OIGN y ON x.U_SONo = y.U_SONo 
    INNER JOIN IGN1 z ON y.DocEntry = z.DocEntry AND x.ItemCode = z.ItemCode
    INNER JOIN SAOLIVE_TIMS.DBO.OCOI aa ON x.U_SONo = aa.DocNo AND x.ItemCode = aa.ItemCode COLLATE SQL_Latin1_General_CP1_CI_AS 
GROUP BY
    x.U_SONo,
    x.ItemCode

	   ) ActAQCR ON G.DocNum = ActAQCR.U_SONo AND E.ItemCode = ActAQCR.ItemCode

	   	    LEFT JOIN (
			SELECT 
				x.U_SONo,
				x.ItemCode, 
				MAX(x.U_TIMS_DispatchTime)[DisLastRow],
				MIN(x.U_TIMS_DispatchTime)[Dis1stRow],
				(MAX(x.U_TIMS_DispatchTime) - MIN(x.U_TIMS_DispatchTime)) [DisDuration]
			FROM
				OBTN x
				INNER JOIN OIGE y ON x.U_SONo = y.U_SONo 
				INNER JOIN IGE1 z ON y.DocEntry = z.DocEntry AND x.ItemCode = z.ItemCode
			GROUP BY
			X.U_SONo,
			X.ItemCode
	   ) ActDispatch ON G.DocNum = ActDispatch.U_SONo AND E.ItemCode = ActDispatch.ItemCode
       LEFT JOIN (
            SELECT
                x.U_SONO,
                x.ItemCode,
                COUNT(x.U_TIMS_IUBTime) AS QtyActual
            FROM
                obtn x
            WHERE
                ISNULL(x.U_TIMS_IUBTime, '') <> ''
            GROUP BY
                x.U_SONO, x.ItemCode
        ) BatchCount ON G.DocNum = BatchCount.U_SONO AND E.ItemCode = BatchCount.ItemCode
		LEFT JOIN (
            SELECT
                x.U_SONO,
                x.ItemCode,
                COUNT(x.U_TIMS_IUBBinTime) AS TimSCount
            FROM
                obtn x
            WHERE
                ISNULL(x.U_TIMS_IUBBinTime, '') <> ''
            GROUP BY
                x.U_SONO, x.ItemCode
        ) BatchC2 ON G.DocNum = BatchC2.U_SONO AND E.ItemCode = BatchC2.ItemCode
		LEFT JOIN (
            SELECT
                x.U_SONO,
                x.ItemCode,
                COUNT(x.U_TIMS_IrradLoadTime) AS TimSCount
            FROM
                obtn x
            WHERE
                ISNULL(x.U_TIMS_IrradLoadTime, '') <> ''
            GROUP BY
                x.U_SONO, x.ItemCode
        ) BatchC3 ON G.DocNum = BatchC3.U_SONO AND E.ItemCode = BatchC3.ItemCode
	LEFT JOIN (Select /*x.TargetValue[PlanEnergy],*/ x.ActualValue[ActualEnergy], x.SampleId[EnergyID] from SAOLIVE_TIMS.DBO.COR3 x where  x.Parameter = 'Energy' ) En ON A.SampleId = En.EnergyID
	LEFT JOIN (Select /*x.TargetValue[PlanPower],*/ x.ActualValue[ActualPower], x.SampleId[PowerID] from SAOLIVE_TIMS.DBO.COR3 x where  x.Parameter = 'Power' ) Pow ON A.SampleId = Pow.PowerID
	LEFT JOIN (Select /*x.TargetValue[PlanFQ],*/ x.ActualValue[ActualFQ], x.SampleId[FQID] from SAOLIVE_TIMS.DBO.COR3 x where  x.Parameter = 'EB Frequency' ) FQ ON A.SampleId = FQ.FQID
	LEFT JOIN (Select x.Result [DosiResult], x.ActualValue[DosiActual], x.MaxValue[DosiMax], x.MinValue[DosiMin], x.SampleId[DosiID] from SAOLIVE_TIMS.DBO.COR3 x where  x.Parameter = 'Dosimetry' ) Dosi ON A.SampleId = Dosi.DosiID
	

    WHERE
        H.ItmsGrpNam = 'Customer Items' AND CAST(ISNULL(G.DocNum ,0) AS NVARCHAR(50)) = @DocKey AND A.Parameter NOT IN ('Visual','Weight','Height', 'Width','Length','Conveyor Speed')

END
GO


