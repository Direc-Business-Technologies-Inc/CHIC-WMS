﻿using System;
using System.Collections.Generic;

namespace Application.Libraries.SAP.DB.Models;

public partial class OITM
{
    public string ItemCode { get; set; } = null!;

    public string? ItemName { get; set; }

    public string? FrgnName { get; set; }

    public short? ItmsGrpCod { get; set; }

    public short? CstGrpCode { get; set; }

    public string? VatGourpSa { get; set; }

    public string? CodeBars { get; set; }

    public string? VATLiable { get; set; }

    public string? PrchseItem { get; set; }

    public string? SellItem { get; set; }

    public string? InvntItem { get; set; }

    public decimal? OnHand { get; set; }

    public decimal? IsCommited { get; set; }

    public decimal? OnOrder { get; set; }

    public string? IncomeAcct { get; set; }

    public string? ExmptIncom { get; set; }

    public decimal? MaxLevel { get; set; }

    public string? DfltWH { get; set; }

    public string? CardCode { get; set; }

    public string? SuppCatNum { get; set; }

    public string? BuyUnitMsr { get; set; }

    public decimal? NumInBuy { get; set; }

    public decimal? ReorderQty { get; set; }

    public decimal? MinLevel { get; set; }

    public decimal? LstEvlPric { get; set; }

    public DateTime? LstEvlDate { get; set; }

    public decimal? CustomPer { get; set; }

    public string? Canceled { get; set; }

    public int? MnufctTime { get; set; }

    public string? WholSlsTax { get; set; }

    public string? RetilrTax { get; set; }

    public decimal? SpcialDisc { get; set; }

    public short? DscountCod { get; set; }

    public string? TrackSales { get; set; }

    public string? SalUnitMsr { get; set; }

    public decimal? NumInSale { get; set; }

    public decimal? Consig { get; set; }

    public int? QueryGroup { get; set; }

    public decimal? Counted { get; set; }

    public decimal? OpenBlnc { get; set; }

    public string? EvalSystem { get; set; }

    public short? UserSign { get; set; }

    public string? FREE { get; set; }

    public string? PicturName { get; set; }

    public string? Transfered { get; set; }

    public string? BlncTrnsfr { get; set; }

    public string? UserText { get; set; }

    public string? SerialNum { get; set; }

    public decimal? CommisPcnt { get; set; }

    public decimal? CommisSum { get; set; }

    public short? CommisGrp { get; set; }

    public string? TreeType { get; set; }

    public decimal? TreeQty { get; set; }

    public decimal? LastPurPrc { get; set; }

    public string? LastPurCur { get; set; }

    public DateTime? LastPurDat { get; set; }

    public string? ExitCur { get; set; }

    public decimal? ExitPrice { get; set; }

    public string? ExitWH { get; set; }

    public string? AssetItem { get; set; }

    public string? WasCounted { get; set; }

    public string? ManSerNum { get; set; }

    public decimal? SHeight1 { get; set; }

    public short? SHght1Unit { get; set; }

    public decimal? SHeight2 { get; set; }

    public short? SHght2Unit { get; set; }

    public decimal? SWidth1 { get; set; }

    public short? SWdth1Unit { get; set; }

    public decimal? SWidth2 { get; set; }

    public short? SWdth2Unit { get; set; }

    public decimal? SLength1 { get; set; }

    public short? SLen1Unit { get; set; }

    public decimal? Slength2 { get; set; }

    public short? SLen2Unit { get; set; }

    public decimal? SVolume { get; set; }

    public short? SVolUnit { get; set; }

    public decimal? SWeight1 { get; set; }

    public short? SWght1Unit { get; set; }

    public decimal? SWeight2 { get; set; }

    public short? SWght2Unit { get; set; }

    public decimal? BHeight1 { get; set; }

    public short? BHght1Unit { get; set; }

    public decimal? BHeight2 { get; set; }

    public short? BHght2Unit { get; set; }

    public decimal? BWidth1 { get; set; }

    public short? BWdth1Unit { get; set; }

    public decimal? BWidth2 { get; set; }

    public short? BWdth2Unit { get; set; }

    public decimal? BLength1 { get; set; }

    public short? BLen1Unit { get; set; }

    public decimal? Blength2 { get; set; }

    public short? BLen2Unit { get; set; }

    public decimal? BVolume { get; set; }

    public short? BVolUnit { get; set; }

    public decimal? BWeight1 { get; set; }

    public short? BWght1Unit { get; set; }

    public decimal? BWeight2 { get; set; }

    public short? BWght2Unit { get; set; }

    public string? FixCurrCms { get; set; }

    public short? FirmCode { get; set; }

    public DateTime? LstSalDate { get; set; }

    public string? QryGroup1 { get; set; }

    public string? QryGroup2 { get; set; }

    public string? QryGroup3 { get; set; }

    public string? QryGroup4 { get; set; }

    public string? QryGroup5 { get; set; }

    public string? QryGroup6 { get; set; }

    public string? QryGroup7 { get; set; }

    public string? QryGroup8 { get; set; }

    public string? QryGroup9 { get; set; }

    public string? QryGroup10 { get; set; }

    public string? QryGroup11 { get; set; }

    public string? QryGroup12 { get; set; }

    public string? QryGroup13 { get; set; }

    public string? QryGroup14 { get; set; }

    public string? QryGroup15 { get; set; }

    public string? QryGroup16 { get; set; }

    public string? QryGroup17 { get; set; }

    public string? QryGroup18 { get; set; }

    public string? QryGroup19 { get; set; }

    public string? QryGroup20 { get; set; }

    public string? QryGroup21 { get; set; }

    public string? QryGroup22 { get; set; }

    public string? QryGroup23 { get; set; }

    public string? QryGroup24 { get; set; }

    public string? QryGroup25 { get; set; }

    public string? QryGroup26 { get; set; }

    public string? QryGroup27 { get; set; }

    public string? QryGroup28 { get; set; }

    public string? QryGroup29 { get; set; }

    public string? QryGroup30 { get; set; }

    public string? QryGroup31 { get; set; }

    public string? QryGroup32 { get; set; }

    public string? QryGroup33 { get; set; }

    public string? QryGroup34 { get; set; }

    public string? QryGroup35 { get; set; }

    public string? QryGroup36 { get; set; }

    public string? QryGroup37 { get; set; }

    public string? QryGroup38 { get; set; }

    public string? QryGroup39 { get; set; }

    public string? QryGroup40 { get; set; }

    public string? QryGroup41 { get; set; }

    public string? QryGroup42 { get; set; }

    public string? QryGroup43 { get; set; }

    public string? QryGroup44 { get; set; }

    public string? QryGroup45 { get; set; }

    public string? QryGroup46 { get; set; }

    public string? QryGroup47 { get; set; }

    public string? QryGroup48 { get; set; }

    public string? QryGroup49 { get; set; }

    public string? QryGroup50 { get; set; }

    public string? QryGroup51 { get; set; }

    public string? QryGroup52 { get; set; }

    public string? QryGroup53 { get; set; }

    public string? QryGroup54 { get; set; }

    public string? QryGroup55 { get; set; }

    public string? QryGroup56 { get; set; }

    public string? QryGroup57 { get; set; }

    public string? QryGroup58 { get; set; }

    public string? QryGroup59 { get; set; }

    public string? QryGroup60 { get; set; }

    public string? QryGroup61 { get; set; }

    public string? QryGroup62 { get; set; }

    public string? QryGroup63 { get; set; }

    public string? QryGroup64 { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? ExportCode { get; set; }

    public decimal? SalFactor1 { get; set; }

    public decimal? SalFactor2 { get; set; }

    public decimal? SalFactor3 { get; set; }

    public decimal? SalFactor4 { get; set; }

    public decimal? PurFactor1 { get; set; }

    public decimal? PurFactor2 { get; set; }

    public decimal? PurFactor3 { get; set; }

    public decimal? PurFactor4 { get; set; }

    public string? SalFormula { get; set; }

    public string? PurFormula { get; set; }

    public string? VatGroupPu { get; set; }

    public decimal? AvgPrice { get; set; }

    public string? PurPackMsr { get; set; }

    public decimal? PurPackUn { get; set; }

    public string? SalPackMsr { get; set; }

    public decimal? SalPackUn { get; set; }

    public short? SCNCounter { get; set; }

    public string? ManBtchNum { get; set; }

    public string? ManOutOnly { get; set; }

    public string? DataSource { get; set; }

    public string? validFor { get; set; }

    public DateTime? validFrom { get; set; }

    public DateTime? validTo { get; set; }

    public string? frozenFor { get; set; }

    public DateTime? frozenFrom { get; set; }

    public DateTime? frozenTo { get; set; }

    public string? BlockOut { get; set; }

    public string? ValidComm { get; set; }

    public string? FrozenComm { get; set; }

    public int? LogInstanc { get; set; }

    public string? ObjType { get; set; }

    public string? SWW { get; set; }

    public string? Deleted { get; set; }

    public int? DocEntry { get; set; }

    public string? ExpensAcct { get; set; }

    public string? FrgnInAcct { get; set; }

    public short? ShipType { get; set; }

    public string? GLMethod { get; set; }

    public string? ECInAcct { get; set; }

    public string? FrgnExpAcc { get; set; }

    public string? ECExpAcc { get; set; }

    public string? TaxType { get; set; }

    public string? ByWh { get; set; }

    public string? WTLiable { get; set; }

    public string? ItemType { get; set; }

    public string? WarrntTmpl { get; set; }

    public string? BaseUnit { get; set; }

    public string? CountryOrg { get; set; }

    public decimal? StockValue { get; set; }

    public string? Phantom { get; set; }

    public string? IssueMthd { get; set; }

    public string? FREE1 { get; set; }

    public decimal? PricingPrc { get; set; }

    public string? MngMethod { get; set; }

    public decimal? ReorderPnt { get; set; }

    public string? InvntryUom { get; set; }

    public string? PlaningSys { get; set; }

    public string? PrcrmntMtd { get; set; }

    public short? OrdrIntrvl { get; set; }

    public decimal? OrdrMulti { get; set; }

    public decimal? MinOrdrQty { get; set; }

    public int? LeadTime { get; set; }

    public string? IndirctTax { get; set; }

    public string? TaxCodeAR { get; set; }

    public string? TaxCodeAP { get; set; }

    public int? OSvcCode { get; set; }

    public int? ISvcCode { get; set; }

    public int? ServiceGrp { get; set; }

    public int? NCMCode { get; set; }

    public string? MatType { get; set; }

    public int? MatGrp { get; set; }

    public string? ProductSrc { get; set; }

    public int? ServiceCtg { get; set; }

    public string? ItemClass { get; set; }

    public string? Excisable { get; set; }

    public int? ChapterID { get; set; }

    public string? NotifyASN { get; set; }

    public string? ProAssNum { get; set; }

    public decimal? AssblValue { get; set; }

    public int? DNFEntry { get; set; }

    public short? UserSign2 { get; set; }

    public string? Spec { get; set; }

    public string? TaxCtg { get; set; }

    public int? Series { get; set; }

    public int? Number { get; set; }

    public int? FuelCode { get; set; }

    public string? BeverTblC { get; set; }

    public string? BeverGrpC { get; set; }

    public int? BeverTM { get; set; }

    public string? Attachment { get; set; }

    public int? AtcEntry { get; set; }

    public int? ToleranDay { get; set; }

    public int? UgpEntry { get; set; }

    public int? PUoMEntry { get; set; }

    public int? SUoMEntry { get; set; }

    public int? IUoMEntry { get; set; }

    public short? IssuePriBy { get; set; }

    public string? AssetClass { get; set; }

    public string? AssetGroup { get; set; }

    public string? InventryNo { get; set; }

    public int? Technician { get; set; }

    public int? Employee { get; set; }

    public int? Location { get; set; }

    public string? StatAsset { get; set; }

    public string? Cession { get; set; }

    public string? DeacAftUL { get; set; }

    public string? AsstStatus { get; set; }

    public DateTime? CapDate { get; set; }

    public DateTime? AcqDate { get; set; }

    public DateTime? RetDate { get; set; }

    public string? GLPickMeth { get; set; }

    public string? NoDiscount { get; set; }

    public string? MgrByQty { get; set; }

    public string? AssetRmk1 { get; set; }

    public string? AssetRmk2 { get; set; }

    public decimal? AssetAmnt1 { get; set; }

    public decimal? AssetAmnt2 { get; set; }

    public string? DeprGroup { get; set; }

    public string? AssetSerNo { get; set; }

    public string? CntUnitMsr { get; set; }

    public decimal? NumInCnt { get; set; }

    public int? INUoMEntry { get; set; }

    public string? OneBOneRec { get; set; }

    public string? RuleCode { get; set; }

    public string? ScsCode { get; set; }

    public string? SpProdType { get; set; }

    public decimal? IWeight1 { get; set; }

    public short? IWght1Unit { get; set; }

    public decimal? IWeight2 { get; set; }

    public short? IWght2Unit { get; set; }

    public string? CompoWH { get; set; }

    public int? CreateTS { get; set; }

    public int? UpdateTS { get; set; }

    public string? VirtAstItm { get; set; }

    public string? SouVirAsst { get; set; }

    public string? InCostRoll { get; set; }

    public decimal? PrdStdCst { get; set; }

    public string? EnAstSeri { get; set; }

    public string? LinkRsc { get; set; }

    public decimal? OnHldPert { get; set; }

    public decimal? onHldLimt { get; set; }

    public int? PriceUnit { get; set; }

    public string? GSTRelevnt { get; set; }

    public int? SACEntry { get; set; }

    public string? GstTaxCtg { get; set; }

    public decimal? AssVal4WTR { get; set; }

    public int? ExcImpQUoM { get; set; }

    public decimal? ExcFixAmnt { get; set; }

    public decimal? ExcRate { get; set; }

    public string? SOIExc { get; set; }

    public string? TNVED { get; set; }

    public string? Imported { get; set; }

    public string? AutoBatch { get; set; }

    public string? CstmActing { get; set; }

    public int? StdItemId { get; set; }

    public int? CommClass { get; set; }

    public string? TaxCatCode { get; set; }

    public int? DataVers { get; set; }

    public string? NVECode { get; set; }

    public int? CESTCode { get; set; }

    public decimal? CtrSealQty { get; set; }

    public string? LegalText { get; set; }

    public string? QRCodeSrc { get; set; }

    public string? Traceable { get; set; }

    public string? U_Dim1 { get; set; }

    public string? U_Dim2 { get; set; }

    public string? U_Dim3 { get; set; }

    public string? U_Dim4 { get; set; }

    public string? U_Dim5 { get; set; }

    public string? U_IndividualItmSz { get; set; }

    public string? U_ItmSubgroup { get; set; }

    public int? U_NoBoxesPallet { get; set; }

    public int? U_NoBoxesContainer { get; set; }

    public int? U_NumberPacks { get; set; }

    public string? U_IndividualWghtItms { get; set; }

    public string? U_WeightPallet { get; set; }

    public string? U_ReceivingTemp { get; set; }

    public string? U_PackagingMat { get; set; }

    public string? U_CustCode { get; set; }

    public string? U_IrriFeeType { get; set; }

    public string? U_WhseFeeType { get; set; }

    public string? U_ISIServices { get; set; }

    public string? U_PackingSpecification { get; set; }

    public string? U_TIMS_Whse { get; set; }

    public string U_TIMS_StorCon { get; set; } = null!;

    public decimal? U_MinAmount { get; set; }

    public decimal? U_MinVol { get; set; }

    public int? U_RevHR { get; set; }

    public decimal? U_TIMS_DoseUnifRatio { get; set; }

    public string? U_TIMS_RDMP { get; set; }

    public decimal? U_TIMS_RelDoseMin { get; set; }

    public decimal? U_TIMS_RelDoseMax { get; set; }

    public decimal? U_TIMS_BeamEnergy { get; set; }

    public decimal? U_TIMS_BeamPower { get; set; }

    public decimal? U_TIMS_Frequency { get; set; }

    public decimal? U_TIMS_BeamCurrent { get; set; }

    public decimal? U_TIMS_ConvSpeed { get; set; }

    public decimal? U_TIMS_SidePass { get; set; }

    public decimal? U_TIMS_NoOfPass { get; set; }

    public decimal? U_TIMS_DoseRate { get; set; }

    public decimal? U_TIMS_EstTrpHr { get; set; }

    public decimal? U_TIMS_SurfDensity { get; set; }

    public decimal? U_TIMS_PackDensity { get; set; }

    public decimal? U_Density { get; set; }

    public decimal? U_Dosage { get; set; }

    public decimal? U_Dose { get; set; }

    public string? U_BoxLength { get; set; }

    public string? U_BoxHeight { get; set; }

    public string? U_BoxWidth { get; set; }

    public string? U_BoxWeight { get; set; }

    public string? U_BoxVol { get; set; }
}
