
/****** Object:  StoredProcedure [dbo].[USRSP_SOReport]    Script Date: 8/29/2024 9:26:52 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
CREATE PROCEDURE [dbo].[USRSP_SOReport]
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
G.U_TIMS_ManufLotNo [ManufacturingLotNo],
A.MinValue,
A.MaxValue,
D.ItemCode,
D.StorageConditions,
vis.* ,
leng.*,
Wid.*,
Hei.*,
Wei.*,
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
	LEFT JOIN (Select x.ActualValue [VisActual], x.MaxValue[VisMax], x.MinValue[VisMin], x.SampleId [VisId] from SAOLIVE_TIMS.DBO.COR3 x where  x.Parameter = 'Visual' ) VIS ON A.SampleId = VIS.VisId
	LEFT JOIN (Select x.ActualValue[LenActual], x.MaxValue[LenMax], x.MinValue[LenMin], x.SampleId[lenId] from SAOLIVE_TIMS.DBO.COR3 x where  x.Parameter = 'Length' ) Leng ON A.SampleId = Leng.lenId
	LEFT JOIN (Select x.ActualValue[WidActual], x.MaxValue[WidMax], x.MinValue[WidMin], x.SampleId[WidId] from SAOLIVE_TIMS.DBO.COR3 x where  x.Parameter = 'Width' ) Wid ON A.SampleId = Wid.WidId
	LEFT JOIN (Select x.ActualValue[HeiActual], x.MaxValue[HeiMax], x.MinValue[HeiMin], x.SampleId[HeiID] from SAOLIVE_TIMS.DBO.COR3 x where  x.Parameter = 'Height' ) Hei ON A.SampleId = Hei.HeiID
	LEFT JOIN (Select x.ActualValue[WeiActual], x.MaxValue[WeiMax], x.MinValue[WeiMin], x.SampleId[WeiID] from SAOLIVE_TIMS.DBO.COR3 x where  x.Parameter = 'Weight' ) Wei ON A.SampleId = Wei.WeiID
    WHERE
        H.ItmsGrpNam = 'Customer Items' AND CAST(ISNULL(G.DocNum ,0) AS NVARCHAR(50)) = @DocKey AND A.Parameter NOT IN ('EB Frequency', 'Conveyor Speed', 'Energy','Power','Frequency','Dosimetry')
END
GO


