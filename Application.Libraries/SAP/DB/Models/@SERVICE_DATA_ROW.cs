using System;
using System.Collections.Generic;

namespace Application.Libraries.SAP.DB.Models;

public partial class @SERVICE_DATA_ROW
{
    public string Code { get; set; } = null!;

    public int LineId { get; set; }

    public string? Object { get; set; }

    public int? LogInst { get; set; }

    public string? U_TransferType { get; set; }

    public string? U_FromWarehouseCode { get; set; }

    public string? U_WarehouseCode { get; set; }

    public int? U_SortCode { get; set; }

    public string? U_DisplayStatus { get; set; }

    public string? U_FieldBatchDate { get; set; }

    public string? U_FieldBatchTime { get; set; }
}
