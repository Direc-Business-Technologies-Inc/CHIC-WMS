using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ViewModels
{
    public class BatchSerialViewModel
    {
        public BatchSerial batchSerial { get; set; }
        public List<BatchSerial> batchSerialList { get; set; }
        public class BatchSerial
        {
            public int AbsEntry { get; set; }
            public string ItemCode { get; set; }
            [MaxLength(36)]
            public string DistNumber { get; set; }
            public decimal? Quantity { get; set; }
            [MaxLength(36)]
            public string MnfSerial { get; set; }
            [MaxLength(36)]
            public string LotNumber { get; set; } = string.Empty;
            public DateTime? ExpDate { get; set; }
            public string Location { get; set; } = string.Empty;
            public string Notes { get; set; } = string.Empty;
            public int BaseLineNumber { get; set; }
            public string? U_Field1 { get; set; } = string.Empty;
            public string? U_Field2 { get; set; } = string.Empty;
            public string? U_Field3 { get; set; } = string.Empty;
            public int BinAbs { get; set; }
            public string BinCode { get; set; } = string.Empty;
            public decimal? OnHandQty { get; set; }
            public bool CheckState { get; set; } = false;
        }
    }
}
