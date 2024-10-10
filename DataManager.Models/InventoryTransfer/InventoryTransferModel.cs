using DataManager.Models.Bins;
using DataManager.Models.Enums;

namespace DataManager.Models.InventoryTransfer;

public class InventoryTransferModel
{
    public int DocNum { get; set; }
    public DateTime DocDate { get; set; }
    public TransferTypeEnum TransferType { get; set; }
    public string UTransferType { get; set; } = string.Empty;
    public string LocationCode { get; set; } = string.Empty;
    public string DisplayStatus { get; set; } = string.Empty;
    public int SortCodeStatus { get; set; }
    public List<InventoryTransferLine> InventoryTransferLines { get; set; }
}

public class InventoryTransferLine
{
    public string BinCode { get; set; } = string.Empty;
    public string PalletCode { get; set; } = string.Empty;
    public string ItemCode { get; set; } = string.Empty;
    public string ItemName { get; set; } = string.Empty;
    public int PlannedBoxQty { get; set; }
    public int ActualBoxQty { get; set; }
    public string WarehouseCode { get; set; } = string.Empty;
    public string FromWarehouseCode { get; set; } = string.Empty;
    public List<InventoryTransferBatch> BatchNumbers { get; set; }
    public List<BinAllocation> StockTransferLinesBinAllocations { get; set; }
}

public class InventoryTransferBatch
{
    public int SystemSerialNumber { get; set; }
    public string BatchNumber { get; set; } = string.Empty;
    public string ItemCode { get; set; } = string.Empty;
    public string ManufacturerSerialNumber { get; set; } = string.Empty;
    public string InternalSerialNumber { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public DateTime? ManufacturingDate { get; set; }
    public DateTime AddmisionDate { get; set; }
    public int BaseLineNumber { get; set; }

}

