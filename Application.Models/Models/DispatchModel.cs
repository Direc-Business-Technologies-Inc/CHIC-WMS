using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Models
{
    public class DispatchModel
    {
        public DateTime DocDate { get; set; }
        public int DocNum { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int SalesOrderId { get; set; }
        public int PlannedBoxNo { get; set; }
        public int ActualBoxNo { get; set; }
        public string Remarks { get; set; }
        public List<Pallet> Pallets { get; set; }
    }
}
