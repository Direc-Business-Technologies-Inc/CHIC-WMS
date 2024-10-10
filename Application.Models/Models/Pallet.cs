using System;
using System.Collections.Generic;

namespace Application.Models;

public class Pallet
{
    public Pallet() { }
    public Pallet(string Code, int boxCountInSalesOrder)
    {
        var x = Code.Split('-');
        var so = int.Parse(x[0]);
        var palletSeries = int.Parse(x[1]);
        var boxPerPallet = int.Parse(x[2]);

        Init(so, palletSeries, boxPerPallet, boxCountInSalesOrder);
    }
    public Pallet(int salesOrderNum, int palletIdx, int boxPerPallet, int boxCountInSalesOrder)
    {
        Init(salesOrderNum, palletIdx, boxPerPallet, boxCountInSalesOrder);
    }

    public void Init(int salesOrderNum, int palletIdx, int boxPerPallet, int boxCountInSalesOrder)
    {
        Dictionary<string, int> seriesList = GeneratePalletSeries(boxPerPallet, boxCountInSalesOrder);

        var end = seriesList.ElementAt(palletIdx - 1).Value;

        this.Code = $"{salesOrderNum}-{seriesList.ElementAt(palletIdx-1).Key}-{end}";
        this.SalesOrderDocNum = salesOrderNum;
        this.PlannedQuantity = seriesList.ElementAt(palletIdx-1).Value;
        this.ActualQuantity = this.PlannedQuantity;
    }

    public static Dictionary<string, int> GeneratePalletSeries(int boxPerPallet, int boxCountInSalesOrder)
    {
        Dictionary<string, int> seriesList = new();

        var palletCount = Math.Ceiling((decimal)boxCountInSalesOrder / boxPerPallet);

        for (int i = 1; i <= palletCount; i++) seriesList.Add(
            i.ToString("D3"), boxPerPallet
            );

        var remainder = boxCountInSalesOrder % boxPerPallet;
        int lastQty = remainder == 0 ?
            boxPerPallet :
            remainder;
        var lastKey = seriesList.Last().Key;
        seriesList[lastKey] = lastQty;

        return seriesList;
    }

    public string Code { get; set; }
    public string Series { get; set; }
    public int PlannedQuantity { get; set; }
    public int ActualQuantity { get; set; }
    public string ItemCode { get; set; }
    public string ItemName { get; set; }
    public int SalesOrderDocNum { get; set; }
    public string WarehouseCode { get; set; }
    public string? FromWarehouseCode { get; set; }
    public int BinEntry { get; set; }
    public string BinCode { get; set; }
    public string ServiceType { get; set; }
    public string TransferType { get; set; }
    public string DisplayStatus { get; set; }
    public int SortCodeStatus { get; set; }
    public List<Box> Boxes { get; set; } = new();
}
