using System;
using System.Collections.Generic;

namespace Application.Libraries.SAP.DB.Models;

public partial class @FACILITYLOCATION
{
    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal U_Duration { get; set; }
}
