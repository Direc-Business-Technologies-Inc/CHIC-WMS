using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ViewModels
{
    public class ReceivingViewModel
    {
        public List<SalesOrder> soList { get; set; }
        public SalesOrder selectedSo { get; set; }
        public class SalesOrder
        {
            public int DocEntry { get; set; }
            public int DocNum { get; set; }
            public string BpName { get; set; }
            public string ItemName { get; set; }
            public DateTime DocDate { get; set; }
            public int BoxesPallet { get; set; }
            public double PlannedBoxNo { get; set; }
            public List<Pallet> Pallets { get; set; } = new();

        }
        public class Pallet
        {
            public string Label { get; set; }
            private double _quantity { get; set; }
            public double Quantity { 
                get => _quantity;
                set
                {
                    _quantity = value >= 0 ? value : 0;
                }
            }
        }
    }
}

/*
 
 
2-0001
2-0002
2-0003
2-0004
2-0005

2-0006

 * */

