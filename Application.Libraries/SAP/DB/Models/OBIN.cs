using System;
using System.Collections.Generic;

namespace Application.Libraries.SAP.DB.Models;

public partial class OBIN
{
    public int AbsEntry { get; set; }

    public string BinCode { get; set; } = null!;

    public string? WhsCode { get; set; }

    public string? SysBin { get; set; }

    public int? SL1Abs { get; set; }

    public string? SL1Code { get; set; }

    public int? SL2Abs { get; set; }

    public string? SL2Code { get; set; }

    public int? SL3Abs { get; set; }

    public string? SL3Code { get; set; }

    public int? SL4Abs { get; set; }

    public string? SL4Code { get; set; }

    public int? Attr1Abs { get; set; }

    public string? Attr1Val { get; set; }

    public int? Attr2Abs { get; set; }

    public string? Attr2Val { get; set; }

    public int? Attr3Abs { get; set; }

    public string? Attr3Val { get; set; }

    public int? Attr4Abs { get; set; }

    public string? Attr4Val { get; set; }

    public int? Attr5Abs { get; set; }

    public string? Attr5Val { get; set; }

    public int? Attr6Abs { get; set; }

    public string? Attr6Val { get; set; }

    public int? Attr7Abs { get; set; }

    public string? Attr7Val { get; set; }

    public int? Attr8Abs { get; set; }

    public string? Attr8Val { get; set; }

    public int? Attr9Abs { get; set; }

    public string? Attr9Val { get; set; }

    public int? Attr10Abs { get; set; }

    public string? Attr10Val { get; set; }

    public string? Disabled { get; set; }

    public string? Descr { get; set; }

    public string? BarCode { get; set; }

    public string? AltSortCod { get; set; }

    public short? ItmRtrictT { get; set; }

    public string? SpcItmCode { get; set; }

    public short? SpcItmGrpC { get; set; }

    public string? SngBatch { get; set; }

    public short? RtrictType { get; set; }

    public string? RtrictResn { get; set; }

    public DateTime? RtrictDate { get; set; }

    public string? DataSource { get; set; }

    public short? UserSign { get; set; }

    public string? Transfered { get; set; }

    public short? Instance { get; set; }

    public int? LogInstanc { get; set; }

    public DateTime? CreateDate { get; set; }

    public short? UserSign2 { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? Deleted { get; set; }

    public decimal? MinLevel { get; set; }

    public decimal? MaxLevel { get; set; }

    public string? ReceiveBin { get; set; }

    public string? NoAutoAllc { get; set; }

    public decimal? MaxWeight1 { get; set; }

    public short? Wght1Unit { get; set; }

    public decimal? MaxWeight2 { get; set; }

    public short? Wght2Unit { get; set; }

    public short? UoMRtrict { get; set; }

    public int? SpcUoMCode { get; set; }

    public int? SpcUGPCode { get; set; }

    public int? SngUoMCode { get; set; }
}
