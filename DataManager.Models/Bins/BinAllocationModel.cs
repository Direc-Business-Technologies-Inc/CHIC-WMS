using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Models.Bins
{
    public class BinAllocation
    {
        public int BinAbsEntry { get; set; }
        public decimal Quantity { get; set; }
        public int SerialAndBatchNumbersBaseLine { get; set; }
        public int BaseLineNumber { get; set; }
        public string BinActionType { get; set; } = string.Empty;
        public string BatchNumber { get; set; } = string.Empty;
    }
}
