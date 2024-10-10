namespace Application.Models.DTOs;
public class CreateDispatchDTO
{
    public int SalesOrderId { get; set; }
    public List<Pallet>? Pallets { get; set; }

    public class Pallet
    {
        public string? Code { get; set; }
        public string? Series { get; set; }
        public int PlannedQuantity { get; set; }
        public int ActualQuantity { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public int SalesOrderDocNum { get; set; }
        public string? WarehouseCode { get; set; }
        public string? FromWarehouseCode { get; set; }
        public int BinEntry { get; set; }
        public string? BinCode { get; set; }
        public string? ServiceType { get; set; }
        public string? TransferType { get; set; }
        public string? DisplayStatus { get; set; }
        public List<Box>? Boxes { get; set; } = new();
    }
    public class Box
    {
        public string? Id { get; set; }
        public int Quantity { get; set; } = 1;
        public string? PalletCode { get; set; }
        public string? Status { get; set; }
        public int? SalesOrderDocNum { get; set; }
    }
}