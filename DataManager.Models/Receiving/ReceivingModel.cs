using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Models.Receiving
{
    public class ReceivingModel
    {
        public string Id { get; set; }
        public string SalesOrderId { get; set; }
        public string SalesOrderNum { get; set; }
        public double ActualBoxNo { get; set; }
        public double PlannedBoxNo { get; set; }
        public List<Pallet> Pallets { get; set; } = new();
    }

    public class Pallet
    {
        public string Label { get; set; }
        public double Quantity { get; set; }
        public double BoxNo { get; set; }
        public List<Box> Boxes { get; set; } = new();
    }

    public class Box
    {
        public string SeriesId { get; set; }
    }

}
