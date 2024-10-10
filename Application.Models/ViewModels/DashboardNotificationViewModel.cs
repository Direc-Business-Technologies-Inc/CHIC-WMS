using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ViewModels
{
    public class DashboardNotificationViewModel
    {
        public int DocEntry { get; set; }
        public int DocNum { get; set; }
        public string Status { get; set; } = string.Empty;
        public string StorageConditions { get; set; } = string.Empty;
		public string EBStatus { get; set; } = string.Empty;
		public string IrradiationDate { get; set; } = string.Empty;
		public string DispatchDate { get; set; } = string.Empty;
		public string BeamEnergy { get; set; } = string.Empty;
		public string BeamPower { get; set; } = string.Empty;
		public string Frequency { get; set; } = string.Empty;
		public int SOQUantity { get; set; }
		public List<DashboardNotificationLineViewModel> Lines { get; set; } = new();
        public class DashboardNotificationLineViewModel
        {
            public string PalletNo { get; set; } = string.Empty;
            public string Status { get; set; } = string.Empty;
            public string BinCode { get; set; } = string.Empty;
            public string OngoingStatus { get; set; } = string.Empty;
        }
    }
}
