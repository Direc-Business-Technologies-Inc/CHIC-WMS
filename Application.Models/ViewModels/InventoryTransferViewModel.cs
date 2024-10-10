using DataManager.Models.Enums;
using DataManager.Models.InventoryTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ViewModels;

public class InventoryTransferViewModel
{
    public InventoryTransfer inventoryTransfer { get; set; } = new();
    public Dictionary<string, string> locations { get; set; } = new();
    public string UTransferType { get; set; }
    public Dictionary<TransferTypeEnum, string> TransferTypes { get; set; } = TransferType.Data.ToDictionary(x=>x.Key, x=>x.Value);
    public Pallet ScannedPallet { get; set; }
    public string ItemCode  { get; set; }
    public string PalletBarcode { get; set; }
    public string BinBarcode { get; set; }
    private bool _isBusy { get; set; } = false;
    public bool IsBusy
    {
        get
        {
            return _isBusy || IsPosting;
        }
        set
        {
            _isBusy = value;
        }
    }

    public bool IsPosting { get; set; } = false;
    public List<BatchSerialViewModel.BatchSerial> batchList { get; set; }
    public BatchSerialViewModel.BatchSerial selectedBatch { get; set; }

    public ServiceData servicedata { get; set; }
    public class ServiceData
    {
        public string Code { get; set; }
        public string Name { get; set; }

    }
    public class SERVICE_DATA_ROWCollection
    {
        public string U_TransferType { get; set; }
        public string U_FromWarehouseCode { get; set; }
        public string U_WarehouseCode { get; set; }
        public string U_SortCode { get; set; }
        public string U_DisplayStatus { get; set; }
    }
}

public class InventoryTransfer
{
    public int DocNum { get; set; }
    public DateTime DocDate { get; set; }
    public TransferTypeEnum TransferType { get; set; }
    public string UTransferType { get; set; } = string.Empty;
    public string LocationCode { get; set; } = string.Empty;
    public bool ShowLocation { get; set; }
    public string DestLocation { get; set; } = string.Empty;
    public bool ShowDestLocation { get; set; }
    public string ServiceType { get; set; } = string.Empty;
    public string DisplayStatus { get; set; } = string.Empty;
    public int SortCodeStatus { get; set; }
    public List<InventoryTransferLine> InventoryTransferLines { get; set; } = new();
}

public class InventoryTransferLine
{
    public int LineNum { get; set; }
    public string ItemCode { get; set; } = string.Empty;
    public string PalletCode { get; set; } = string.Empty;
    public string BinCode { get; set; } = string.Empty;
    public string ItemName { get; set; } = string.Empty;
    public int SalesOrderNum { get; set; }
    public int PlannedBoxQty { get; set; }
    public int ActualBoxQty { get; set; }
    public string Warehousecode { get; set;} = string.Empty;
    public string FromWarehousecode { get; set; } = string.Empty;
    public List<InventoryTransferBatch> BatchNumbers { get; set; } = new();
    public List<BinAllocation> StockTransferLinesBinAllocations { get; set; } = new();
}