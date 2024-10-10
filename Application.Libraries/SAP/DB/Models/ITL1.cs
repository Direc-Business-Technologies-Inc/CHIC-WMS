using System;
using System.Collections.Generic;

namespace Application.Libraries.SAP.DB.Models;

public partial class ITL1
{
    public int LogEntry { get; set; }

    public string ItemCode { get; set; } = null!;

    public int SysNumber { get; set; }

    public decimal? Quantity { get; set; }

    public decimal? AllocQty { get; set; }

    public int? MdAbsEntry { get; set; }

    public decimal? ReleaseQty { get; set; }

    public decimal? PickedQty { get; set; }

    public decimal? OrderedQty { get; set; }
}
