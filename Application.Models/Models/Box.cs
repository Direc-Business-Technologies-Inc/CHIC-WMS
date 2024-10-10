using DataManager.Models.Enums;

namespace Application.Models;
public class Box
{
    public string Id { get; set; }
    public int Quantity { get; set; } = 1;
    public string PalletCode { get; set; }
    public string? Status { get; set; }
    public int? SalesOrderDocNum { get; set; }
}
