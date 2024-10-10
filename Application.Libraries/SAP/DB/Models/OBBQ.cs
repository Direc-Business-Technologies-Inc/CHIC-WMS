using System;
using System.Collections.Generic;

namespace Application.Libraries.SAP.DB.Models;

public partial class OBBQ
{
    public int AbsEntry { get; set; }

    public string? ItemCode { get; set; }

    public int SnBMDAbs { get; set; }

    public int BinAbs { get; set; }

    public decimal? OnHandQty { get; set; }

    public string? WhsCode { get; set; }
}
