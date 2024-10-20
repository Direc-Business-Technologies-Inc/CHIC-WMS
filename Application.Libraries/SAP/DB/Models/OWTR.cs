﻿using System;
using System.Collections.Generic;

namespace Application.Libraries.SAP.DB.Models;

public partial class OWTR
{
    public int DocEntry { get; set; }

    public int DocNum { get; set; }

    public string? DocType { get; set; }

    public string? CANCELED { get; set; }

    public string? Handwrtten { get; set; }

    public string? Printed { get; set; }

    public string? DocStatus { get; set; }

    public string? InvntSttus { get; set; }

    public string? Transfered { get; set; }

    public string? ObjType { get; set; }

    public DateTime? DocDate { get; set; }

    public DateTime? DocDueDate { get; set; }

    public string? CardCode { get; set; }

    public string? CardName { get; set; }

    public string? Address { get; set; }

    public string? NumAtCard { get; set; }

    public decimal? VatPercent { get; set; }

    public decimal? VatSum { get; set; }

    public decimal? VatSumFC { get; set; }

    public decimal? DiscPrcnt { get; set; }

    public decimal? DiscSum { get; set; }

    public decimal? DiscSumFC { get; set; }

    public string? DocCur { get; set; }

    public decimal? DocRate { get; set; }

    public decimal? DocTotal { get; set; }

    public decimal? DocTotalFC { get; set; }

    public decimal? PaidToDate { get; set; }

    public decimal? PaidFC { get; set; }

    public decimal? GrosProfit { get; set; }

    public decimal? GrosProfFC { get; set; }

    public string? Ref1 { get; set; }

    public string? Ref2 { get; set; }

    public string? Comments { get; set; }

    public string? JrnlMemo { get; set; }

    public int? TransId { get; set; }

    public int? ReceiptNum { get; set; }

    public short? GroupNum { get; set; }

    public short? DocTime { get; set; }

    public int? SlpCode { get; set; }

    public short? TrnspCode { get; set; }

    public string? PartSupply { get; set; }

    public string? Confirmed { get; set; }

    public short? GrossBase { get; set; }

    public int? ImportEnt { get; set; }

    public string? CreateTran { get; set; }

    public string? SummryType { get; set; }

    public string? UpdInvnt { get; set; }

    public string? UpdCardBal { get; set; }

    public short Instance { get; set; }

    public int? Flags { get; set; }

    public string? InvntDirec { get; set; }

    public int? CntctCode { get; set; }

    public string? ShowSCN { get; set; }

    public string? FatherCard { get; set; }

    public decimal? SysRate { get; set; }

    public string? CurSource { get; set; }

    public decimal? VatSumSy { get; set; }

    public decimal? DiscSumSy { get; set; }

    public decimal? DocTotalSy { get; set; }

    public decimal? PaidSys { get; set; }

    public string? FatherType { get; set; }

    public decimal? GrosProfSy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? IsICT { get; set; }

    public DateTime? CreateDate { get; set; }

    public decimal? Volume { get; set; }

    public short? VolUnit { get; set; }

    public decimal? Weight { get; set; }

    public short? WeightUnit { get; set; }

    public int? Series { get; set; }

    public DateTime? TaxDate { get; set; }

    public string? Filler { get; set; }

    public string? DataSource { get; set; }

    public string? StampNum { get; set; }

    public string? isCrin { get; set; }

    public int? FinncPriod { get; set; }

    public short? UserSign { get; set; }

    public string? selfInv { get; set; }

    public decimal? VatPaid { get; set; }

    public decimal? VatPaidFC { get; set; }

    public decimal? VatPaidSys { get; set; }

    public short? UserSign2 { get; set; }

    public string? WddStatus { get; set; }

    public int? draftKey { get; set; }

    public decimal? TotalExpns { get; set; }

    public decimal? TotalExpFC { get; set; }

    public decimal? TotalExpSC { get; set; }

    public int? DunnLevel { get; set; }

    public string? Address2 { get; set; }

    public int? LogInstanc { get; set; }

    public string? Exported { get; set; }

    public int? StationID { get; set; }

    public string? Indicator { get; set; }

    public string? NetProc { get; set; }

    public decimal? AqcsTax { get; set; }

    public decimal? AqcsTaxFC { get; set; }

    public decimal? AqcsTaxSC { get; set; }

    public decimal? CashDiscPr { get; set; }

    public decimal? CashDiscnt { get; set; }

    public decimal? CashDiscFC { get; set; }

    public decimal? CashDiscSC { get; set; }

    public string? ShipToCode { get; set; }

    public string? LicTradNum { get; set; }

    public string? PaymentRef { get; set; }

    public decimal? WTSum { get; set; }

    public decimal? WTSumFC { get; set; }

    public decimal? WTSumSC { get; set; }

    public decimal? RoundDif { get; set; }

    public decimal? RoundDifFC { get; set; }

    public decimal? RoundDifSy { get; set; }

    public string? CheckDigit { get; set; }

    public int? Form1099 { get; set; }

    public string? Box1099 { get; set; }

    public string? submitted { get; set; }

    public string? PoPrss { get; set; }

    public string? Rounding { get; set; }

    public string? RevisionPo { get; set; }

    public short Segment { get; set; }

    public DateTime? ReqDate { get; set; }

    public DateTime? CancelDate { get; set; }

    public string? PickStatus { get; set; }

    public string? Pick { get; set; }

    public string? BlockDunn { get; set; }

    public string? PeyMethod { get; set; }

    public string? PayBlock { get; set; }

    public int? PayBlckRef { get; set; }

    public string? MaxDscn { get; set; }

    public string? Reserve { get; set; }

    public decimal? Max1099 { get; set; }

    public string? CntrlBnk { get; set; }

    public string? PickRmrk { get; set; }

    public string? ISRCodLine { get; set; }

    public decimal? ExpAppl { get; set; }

    public decimal? ExpApplFC { get; set; }

    public decimal? ExpApplSC { get; set; }

    public string? Project { get; set; }

    public string? DeferrTax { get; set; }

    public string? LetterNum { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public decimal? WTApplied { get; set; }

    public decimal? WTAppliedF { get; set; }

    public string? BoeReserev { get; set; }

    public string? AgentCode { get; set; }

    public decimal? WTAppliedS { get; set; }

    public decimal? EquVatSum { get; set; }

    public decimal? EquVatSumF { get; set; }

    public decimal? EquVatSumS { get; set; }

    public short? Installmnt { get; set; }

    public string? VATFirst { get; set; }

    public decimal? NnSbAmnt { get; set; }

    public decimal? NnSbAmntSC { get; set; }

    public decimal? NbSbAmntFC { get; set; }

    public decimal? ExepAmnt { get; set; }

    public decimal? ExepAmntSC { get; set; }

    public decimal? ExepAmntFC { get; set; }

    public DateTime? VatDate { get; set; }

    public string? CorrExt { get; set; }

    public int? CorrInv { get; set; }

    public int? NCorrInv { get; set; }

    public string? CEECFlag { get; set; }

    public decimal? BaseAmnt { get; set; }

    public decimal? BaseAmntSC { get; set; }

    public decimal? BaseAmntFC { get; set; }

    public string? CtlAccount { get; set; }

    public int? BPLId { get; set; }

    public string? BPLName { get; set; }

    public string? VATRegNum { get; set; }

    public string? TxInvRptNo { get; set; }

    public DateTime? TxInvRptDt { get; set; }

    public string? KVVATCode { get; set; }

    public string? WTDetails { get; set; }

    public int? SumAbsId { get; set; }

    public DateTime? SumRptDate { get; set; }

    public string PIndicator { get; set; } = null!;

    public string? ManualNum { get; set; }

    public string? UseShpdGd { get; set; }

    public decimal? BaseVtAt { get; set; }

    public decimal? BaseVtAtSC { get; set; }

    public decimal? BaseVtAtFC { get; set; }

    public decimal? NnSbVAt { get; set; }

    public decimal? NnSbVAtSC { get; set; }

    public decimal? NbSbVAtFC { get; set; }

    public decimal? ExptVAt { get; set; }

    public decimal? ExptVAtSC { get; set; }

    public decimal? ExptVAtFC { get; set; }

    public decimal? LYPmtAt { get; set; }

    public decimal? LYPmtAtSC { get; set; }

    public decimal? LYPmtAtFC { get; set; }

    public decimal? ExpAnSum { get; set; }

    public decimal? ExpAnSys { get; set; }

    public decimal? ExpAnFrgn { get; set; }

    public string DocSubType { get; set; } = null!;

    public string? DpmStatus { get; set; }

    public decimal? DpmAmnt { get; set; }

    public decimal? DpmAmntSC { get; set; }

    public decimal? DpmAmntFC { get; set; }

    public string? DpmDrawn { get; set; }

    public decimal? DpmPrcnt { get; set; }

    public decimal? PaidSum { get; set; }

    public decimal? PaidSumFc { get; set; }

    public decimal? PaidSumSc { get; set; }

    public string? FolioPref { get; set; }

    public int? FolioNum { get; set; }

    public decimal? DpmAppl { get; set; }

    public decimal? DpmApplFc { get; set; }

    public decimal? DpmApplSc { get; set; }

    public int? LPgFolioN { get; set; }

    public string? Header { get; set; }

    public string? Footer { get; set; }

    public string? Posted { get; set; }

    public int? OwnerCode { get; set; }

    public string? BPChCode { get; set; }

    public int? BPChCntc { get; set; }

    public string? PayToCode { get; set; }

    public string? IsPaytoBnk { get; set; }

    public string? BnkCntry { get; set; }

    public string? BankCode { get; set; }

    public string? BnkAccount { get; set; }

    public string? BnkBranch { get; set; }

    public string? isIns { get; set; }

    public string? TrackNo { get; set; }

    public string? VersionNum { get; set; }

    public int? LangCode { get; set; }

    public string? BPNameOW { get; set; }

    public string? BillToOW { get; set; }

    public string? ShipToOW { get; set; }

    public string? RetInvoice { get; set; }

    public DateTime? ClsDate { get; set; }

    public int? MInvNum { get; set; }

    public DateTime? MInvDate { get; set; }

    public short? SeqCode { get; set; }

    public int? Serial { get; set; }

    public string? SeriesStr { get; set; }

    public string? SubStr { get; set; }

    public string? Model { get; set; }

    public decimal? TaxOnExp { get; set; }

    public decimal? TaxOnExpFc { get; set; }

    public decimal? TaxOnExpSc { get; set; }

    public decimal? TaxOnExAp { get; set; }

    public decimal? TaxOnExApF { get; set; }

    public decimal? TaxOnExApS { get; set; }

    public string? LastPmnTyp { get; set; }

    public int? LndCstNum { get; set; }

    public string? UseCorrVat { get; set; }

    public string? BlkCredMmo { get; set; }

    public string? OpenForLaC { get; set; }

    public string? Excised { get; set; }

    public DateTime? ExcRefDate { get; set; }

    public string? ExcRmvTime { get; set; }

    public decimal? SrvGpPrcnt { get; set; }

    public int? DepositNum { get; set; }

    public string? CertNum { get; set; }

    public string? DutyStatus { get; set; }

    public string? AutoCrtFlw { get; set; }

    public DateTime? FlwRefDate { get; set; }

    public string? FlwRefNum { get; set; }

    public int? VatJENum { get; set; }

    public decimal? DpmVat { get; set; }

    public decimal? DpmVatFc { get; set; }

    public decimal? DpmVatSc { get; set; }

    public decimal? DpmAppVat { get; set; }

    public decimal? DpmAppVatF { get; set; }

    public decimal? DpmAppVatS { get; set; }

    public string? InsurOp347 { get; set; }

    public string? IgnRelDoc { get; set; }

    public string? BuildDesc { get; set; }

    public string? ResidenNum { get; set; }

    public int? Checker { get; set; }

    public int? Payee { get; set; }

    public int? CopyNumber { get; set; }

    public string? SSIExmpt { get; set; }

    public int? PQTGrpSer { get; set; }

    public int? PQTGrpNum { get; set; }

    public string? PQTGrpHW { get; set; }

    public string? ReopOriDoc { get; set; }

    public string? ReopManCls { get; set; }

    public string? DocManClsd { get; set; }

    public short? ClosingOpt { get; set; }

    public DateTime? SpecDate { get; set; }

    public string? Ordered { get; set; }

    public string? NTSApprov { get; set; }

    public short? NTSWebSite { get; set; }

    public string? NTSeTaxNo { get; set; }

    public string? NTSApprNo { get; set; }

    public string? PayDuMonth { get; set; }

    public short? ExtraMonth { get; set; }

    public short? ExtraDays { get; set; }

    public short? CdcOffset { get; set; }

    public string? SignMsg { get; set; }

    public string? SignDigest { get; set; }

    public string? CertifNum { get; set; }

    public int? KeyVersion { get; set; }

    public string? EDocGenTyp { get; set; }

    public short? ESeries { get; set; }

    public string? EDocNum { get; set; }

    public int? EDocExpFrm { get; set; }

    public string? OnlineQuo { get; set; }

    public string? POSEqNum { get; set; }

    public string? POSManufSN { get; set; }

    public int? POSCashN { get; set; }

    public string? EDocStatus { get; set; }

    public string? EDocCntnt { get; set; }

    public string? EDocProces { get; set; }

    public string? EDocErrCod { get; set; }

    public string? EDocErrMsg { get; set; }

    public string? EDocCancel { get; set; }

    public string? EDocTest { get; set; }

    public string? EDocPrefix { get; set; }

    public int? CUP { get; set; }

    public int? CIG { get; set; }

    public string? DpmAsDscnt { get; set; }

    public string? Attachment { get; set; }

    public int? AtcEntry { get; set; }

    public string? SupplCode { get; set; }

    public string? GTSRlvnt { get; set; }

    public decimal? BaseDisc { get; set; }

    public decimal? BaseDiscSc { get; set; }

    public decimal? BaseDiscFc { get; set; }

    public decimal? BaseDiscPr { get; set; }

    public int? CreateTS { get; set; }

    public int? UpdateTS { get; set; }

    public string? SrvTaxRule { get; set; }

    public int? AnnInvDecR { get; set; }

    public string? Supplier { get; set; }

    public int? Releaser { get; set; }

    public int? Receiver { get; set; }

    public string? ToWhsCode { get; set; }

    public DateTime? AssetDate { get; set; }

    public string? Requester { get; set; }

    public string? ReqName { get; set; }

    public short? Branch { get; set; }

    public short? Department { get; set; }

    public string? Email { get; set; }

    public string? Notify { get; set; }

    public int? ReqType { get; set; }

    public string? OriginType { get; set; }

    public string? IsReuseNum { get; set; }

    public string? IsReuseNFN { get; set; }

    public string? DocDlvry { get; set; }

    public decimal? PaidDpm { get; set; }

    public decimal? PaidDpmF { get; set; }

    public decimal? PaidDpmS { get; set; }

    public int? EnvTypeNFe { get; set; }

    public int? AgrNo { get; set; }

    public string? IsAlt { get; set; }

    public int? AltBaseTyp { get; set; }

    public int? AltBaseEnt { get; set; }

    public string? AuthCode { get; set; }

    public DateTime? StDlvDate { get; set; }

    public int? StDlvTime { get; set; }

    public DateTime? EndDlvDate { get; set; }

    public int? EndDlvTime { get; set; }

    public string? VclPlate { get; set; }

    public string? ElCoStatus { get; set; }

    public string? AtDocType { get; set; }

    public string? ElCoMsg { get; set; }

    public string? PrintSEPA { get; set; }

    public decimal? FreeChrg { get; set; }

    public decimal? FreeChrgFC { get; set; }

    public decimal? FreeChrgSC { get; set; }

    public decimal? NfeValue { get; set; }

    public string? FiscDocNum { get; set; }

    public int? RelatedTyp { get; set; }

    public int? RelatedEnt { get; set; }

    public int? CCDEntry { get; set; }

    public int? NfePrntFo { get; set; }

    public int? ZrdAbs { get; set; }

    public int? POSRcptNo { get; set; }

    public decimal? FoCTax { get; set; }

    public decimal? FoCTaxFC { get; set; }

    public decimal? FoCTaxSC { get; set; }

    public int? TpCusPres { get; set; }

    public DateTime? ExcDocDate { get; set; }

    public decimal? FoCFrght { get; set; }

    public decimal? FoCFrghtFC { get; set; }

    public decimal? FoCFrghtSC { get; set; }

    public short? InterimTyp { get; set; }

    public string? PTICode { get; set; }

    public string? Letter { get; set; }

    public int? FolNumFrom { get; set; }

    public int? FolNumTo { get; set; }

    public int? FolSeries { get; set; }

    public decimal? SplitTax { get; set; }

    public decimal? SplitTaxFC { get; set; }

    public decimal? SplitTaxSC { get; set; }

    public string? ToBinCode { get; set; }

    public string? PriceMode { get; set; }

    public string? PoDropPrss { get; set; }

    public string? PermitNo { get; set; }

    public string? MYFtype { get; set; }

    public string? DocTaxID { get; set; }

    public DateTime? DateReport { get; set; }

    public string? RepSection { get; set; }

    public string? ExclTaxRep { get; set; }

    public int? PosCashReg { get; set; }

    public string? DmpTransID { get; set; }

    public string? ECommerBP { get; set; }

    public string? EComerGSTN { get; set; }

    public string? Revision { get; set; }

    public string? RevRefNo { get; set; }

    public DateTime? RevRefDate { get; set; }

    public string? RevCreRefN { get; set; }

    public DateTime? RevCreRefD { get; set; }

    public string? TaxInvNo { get; set; }

    public DateTime? FrmBpDate { get; set; }

    public string? GSTTranTyp { get; set; }

    public int? BaseType { get; set; }

    public int? BaseEntry { get; set; }

    public string? ComTrade { get; set; }

    public string? UseBilAddr { get; set; }

    public short? IssReason { get; set; }

    public string? ComTradeRt { get; set; }

    public string? SplitPmnt { get; set; }

    public int? SOIWizId { get; set; }

    public string? SelfPosted { get; set; }

    public string? EnBnkAcct { get; set; }

    public string? EncryptIV { get; set; }

    public string? DPPStatus { get; set; }

    public string? SAPPassprt { get; set; }

    public string? EWBGenType { get; set; }

    public decimal? CtActTax { get; set; }

    public decimal? CtActTaxFC { get; set; }

    public decimal? CtActTaxSC { get; set; }

    public string? EDocType { get; set; }

    public string? QRCodeSrc { get; set; }

    public string? AggregDoc { get; set; }

    public int? DataVers { get; set; }

    public string? ShipState { get; set; }

    public string? ShipPlace { get; set; }

    public string? CustOffice { get; set; }

    public string? FCI { get; set; }

    public decimal? NnSbCuAmnt { get; set; }

    public decimal? NnSbCuSC { get; set; }

    public decimal? NnSbCuFC { get; set; }

    public decimal? ExepCuAmnt { get; set; }

    public decimal? ExepCuSC { get; set; }

    public decimal? ExepCuFC { get; set; }

    public string? AddLegIn { get; set; }

    public int? LegTextF { get; set; }

    public string? IndFinal { get; set; }

    public string? DANFELgTxt { get; set; }

    public string? PostPmntWT { get; set; }

    public string? QRCodeSPGn { get; set; }

    public string? FCEPmnMean { get; set; }

    public string? U_SINo { get; set; }

    public string? U_PrepBy { get; set; }

    public string? U_RevBy { get; set; }

    public string? U_AppBy { get; set; }

    public string? U_RetType { get; set; }

    public string? U_Remarks { get; set; }

    public string? U_PRNo { get; set; }

    public string? U_CheckBy { get; set; }

    public string? U_PONo { get; set; }

    public string? U_Division { get; set; }

    public int? U_Attach { get; set; }

    public string? U_TransCat { get; set; }

    public string? U_DRNo { get; set; }

    public string? U_SONo { get; set; }

    public string? U_2307rep { get; set; }

    public DateTime? U_2307repdate { get; set; }

    public string? U_1601rep { get; set; }

    public DateTime? U_1601repdate { get; set; }

    public string U_Submitted2307 { get; set; } = null!;

    public string? U_CWTValidated { get; set; }

    public string U_WtaxValStat { get; set; } = null!;

    public DateTime? U_WtaxValDate { get; set; }

    public string? U_PlateNo { get; set; }

    public string? U_Driver { get; set; }

    public string? U_RecBy { get; set; }

    public string? U_DelBy { get; set; }

    public string? U_ReqBy { get; set; }

    public string? U_EmpCode { get; set; }

    public string? U_ReqForCancel { get; set; }

    public string? U_ReturnRsn { get; set; }

    public string? U_RegName { get; set; }

    public string? U_CancelType { get; set; }

    public string? U_Department { get; set; }

    public string? U_Location { get; set; }

    public string? U_CMType { get; set; }

    public string? U_TransType { get; set; }

    public string? U_TransferType { get; set; }

    public int? U_RMSNo { get; set; }

    public string? U_Budgeted { get; set; }

    public string? U_ServiceType { get; set; }

    public DateTime? U_IrridiationDate { get; set; }

    public short? U_IrridiationStart { get; set; }

    public short? U_IrridiationEnd { get; set; }

    public DateTime? U_PickUpDate { get; set; }

    public string? U_PaymentSettlement { get; set; }

    public DateTime? U_DueDatePayment { get; set; }

    public string? U_SOStatus { get; set; }

    public string? U_ReasonARCM { get; set; }

    public string? U_ARDPNo { get; set; }

    public string? U_IPNo { get; set; }

    public DateTime? U_ActIrridiationDt { get; set; }

    public decimal? U_ExcessDays { get; set; }

    public string? U_CustItems { get; set; }

    public string? U_FacilityLoc { get; set; }

    public string? U_TIMS_ManufLotNo { get; set; }

    public string? U_TIMS_SerOrNo { get; set; }

    public string? U_EBStatus { get; set; }
}
