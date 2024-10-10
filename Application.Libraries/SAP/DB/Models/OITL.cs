using System;
using System.Collections.Generic;

namespace Application.Libraries.SAP.DB.Models;

public partial class OITL
{
    public int LogEntry { get; set; }

    public int? TransId { get; set; }

    public string? ItemCode { get; set; }

    public string? ItemName { get; set; }

    public int? ManagedBy { get; set; }

    public int? DocEntry { get; set; }

    public int? DocLine { get; set; }

    public int? DocType { get; set; }

    public int? DocNum { get; set; }

    public int? BaseEntry { get; set; }

    public int? BaseLine { get; set; }

    public int? BaseType { get; set; }

    public int? ApplyEntry { get; set; }

    public int? ApplyLine { get; set; }

    public int? ApplyType { get; set; }

    public DateTime? DocDate { get; set; }

    public string? CardCode { get; set; }

    public string? CardName { get; set; }

    public decimal? DocQty { get; set; }

    public decimal? StockQty { get; set; }

    public decimal? DefinedQty { get; set; }

    public int? StockEff { get; set; }

    public DateTime? CreateDate { get; set; }

    public short? LocType { get; set; }

    public string? LocCode { get; set; }

    public int? AppDocNum { get; set; }

    public string? VersionNum { get; set; }

    public string? Transfered { get; set; }

    public short? Instance { get; set; }

    public int? SubLineNum { get; set; }

    public int? BSubLineNo { get; set; }

    public int? AppSubLine { get; set; }

    public int? ActBaseTp { get; set; }

    public int? ActBaseEnt { get; set; }

    public int? ActBaseLn { get; set; }

    public int? ActBasSubL { get; set; }

    public int? AllocateTp { get; set; }

    public int? AllocatEnt { get; set; }

    public int? AllocateLn { get; set; }

    public short? CreateTime { get; set; }
}
