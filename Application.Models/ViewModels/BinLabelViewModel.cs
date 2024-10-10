using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ViewModels
{
    public class BinLabelViewModel
    {
        public List<BinLabel> BinLabelList { get; set; }

        public class BinLabel
        {
            public string WarehouseCode { get; set; }
            /*public string WarehouseName { get; set; }*/
            public string Shelf { get; set; }
            public string Row { get; set; }
            public string Level { get; set; }
            /*public string BinCode { get; set; }*/

        }

        public List<WarehouseCode> WarehousesCodeList { get; set; }
        public class WarehouseCode
        {
            public string whsCode { get; set; }
            public string whsName { get; set; }

        }

        public class PrinterNames
        {
            public string printerName { get; set; }
        }
    }
}
