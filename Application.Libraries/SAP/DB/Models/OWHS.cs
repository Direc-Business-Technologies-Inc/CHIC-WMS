﻿using System;
using System.Collections.Generic;

namespace Application.Libraries.SAP.DB.Models;

public partial class OWHS
{
    public string WhsCode { get; set; } = null!;

    public string? WhsName { get; set; }

    public int? IntrnalKey { get; set; }

    public string? Grp_Code { get; set; }

    public string? BalInvntAc { get; set; }

    public string? SaleCostAc { get; set; }

    public string? TransferAc { get; set; }

    public string? Locked { get; set; }

    public string? DataSource { get; set; }

    public short? UserSign { get; set; }

    public string? RevenuesAc { get; set; }

    public string? VarianceAc { get; set; }

    public string? DecreasAc { get; set; }

    public string? IncreasAc { get; set; }

    public string? ReturnAc { get; set; }

    public string? ExpensesAc { get; set; }

    public string? EURevenuAc { get; set; }

    public string? EUExpensAc { get; set; }

    public string? FrRevenuAc { get; set; }

    public string? FrExpensAc { get; set; }

    public string? VatGroup { get; set; }

    public string? Street { get; set; }

    public string? Block { get; set; }

    public string? ZipCode { get; set; }

    public string? City { get; set; }

    public string? County { get; set; }

    public string? Country { get; set; }

    public string? State { get; set; }

    public int? Location { get; set; }

    public string? DropShip { get; set; }

    public string? ExmptIncom { get; set; }

    public string? UseTax { get; set; }

    public string? PriceDifAc { get; set; }

    public string? ExchangeAc { get; set; }

    public string? BalanceAcc { get; set; }

    public string? PurchaseAc { get; set; }

    public string? PAReturnAc { get; set; }

    public string? PurchOfsAc { get; set; }

    public string? FedTaxID { get; set; }

    public string? Building { get; set; }

    public string? ShpdGdsAct { get; set; }

    public string? VatRevAct { get; set; }

    public string? DecresGlAc { get; set; }

    public string? IncresGlAc { get; set; }

    public string? Nettable { get; set; }

    public string? StokRvlAct { get; set; }

    public string? StkOffsAct { get; set; }

    public string? WipAcct { get; set; }

    public string? WipVarAcct { get; set; }

    public string? CostRvlAct { get; set; }

    public string? CstOffsAct { get; set; }

    public string? ExpClrAct { get; set; }

    public string? ExpOfstAct { get; set; }

    public string? objType { get; set; }

    public int? logInstanc { get; set; }

    public DateTime? createDate { get; set; }

    public short? userSign2 { get; set; }

    public DateTime? updateDate { get; set; }

    public string? ARCMAct { get; set; }

    public string? ARCMFrnAct { get; set; }

    public string? ARCMEUAct { get; set; }

    public string? ARCMExpAct { get; set; }

    public string? APCMAct { get; set; }

    public string? APCMFrnAct { get; set; }

    public string? APCMEUAct { get; set; }

    public string? RevRetAct { get; set; }

    public int? BPLid { get; set; }

    public string? OwnerCode { get; set; }

    public string? NegStckAct { get; set; }

    public string? StkInTnAct { get; set; }

    public string? AddrType { get; set; }

    public string? StreetNo { get; set; }

    public string? PurBalAct { get; set; }

    public string? Excisable { get; set; }

    public string? WhICenAct { get; set; }

    public string? WhOCenAct { get; set; }

    public string? WhShipTo { get; set; }

    public string? WipOffset { get; set; }

    public string? StockOffst { get; set; }

    public int? StorKeeper { get; set; }

    public string? Shipper { get; set; }

    public string? BinActivat { get; set; }

    public string? BinSeptor { get; set; }

    public int? DftBinAbs { get; set; }

    public string? DftBinEnfd { get; set; }

    public short? AutoIssMtd { get; set; }

    public string? ManageSnB { get; set; }

    public short? RecItemsBy { get; set; }

    public string? RecBinEnab { get; set; }

    public string? GlblLocNum { get; set; }

    public string? RecvEmpBin { get; set; }

    public string? Inactive { get; set; }

    public string? RecvMaxQty { get; set; }

    public short? AutoRecvMd { get; set; }

    public string? RecvMaxWT { get; set; }

    public string? RecvUpTo { get; set; }

    public string? FreeChrgSA { get; set; }

    public string? FreeChrgPU { get; set; }

    public string? TaxOffice { get; set; }

    public string? Address2 { get; set; }

    public string? Address3 { get; set; }

    public string? External { get; set; }

    public string? LegalText { get; set; }
}
