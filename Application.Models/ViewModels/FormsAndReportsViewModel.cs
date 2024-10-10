namespace Application.Models.ViewModels;

public class FormsAndReportsViewModel
{
    #region Irradiation Label Printing
    public class IrradiationLabelPrintingViewModel
    {
        public List<IrradiationLabel> IrradiationLabelList { get; set; } = new List<IrradiationLabel>();
        public class IrradiationLabel
        {
            public string SONo { get; set; }
            public string CustomerName { get; set; }
            public string ItemName { get; set; }
            public DateTime IrradiationDate { get; set; }
        }
    }
    #endregion

    #region Irradiation Sales Order Details
    public class IrradiationSalesOrderDetailsViewModel
    {
        public IrradiationSalesOrder SalesOrderDetail { get; set; } = new IrradiationSalesOrder();
        public class IrradiationSalesOrder
        {
            public string SONo { get; set; }
            public string CustomerName { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string MinDose { get; set; }
            public string MaxDose { get; set; }
            public string ItemGroup { get; set; }
            public int NoBoxesPallet { get; set; }
            public int Quantity { get; set; }
            public DateTime ReceivingDate { get; set; }
            public DateTime IrradiationDate { get; set; }
            public DateTime DispatchingDate { get; set; }
            public List<LotNo> IrradiationLotNoList { get; set; } = new List<LotNo>();

        }
        public class LotNo
        {
            public string IrradiationLotNo { get; set; }
        }
    }
	#endregion

	#region Pallet Label Printing
    public class PalletLabelViewModel
    {
        public List<PalletLabel> PalletLabelList { get; set; } = new List<PalletLabel>();
		public class PalletLabel
        {
			public string SONo { get; set; }
			public string CustomerName { get; set; }
			public string ItemName { get; set; }
			public DateTime ReceivingDate { get; set; }
			public DateTime DispatchDate { get; set; }
		}


    }
	#endregion

	#region Pallet Label Sales Order Details
	public class PalletLabelSalesOrderDetailsViewModel
	{
		public List<PalletLabelSalesOrder> SalesOrderList { get; set; } = new List<PalletLabelSalesOrder>();
		public PalletLabelSalesOrder SalesOrderDetail { get; set; } = new PalletLabelSalesOrder();
		public class PalletLabelSalesOrder
		{
			public string SONo { get; set; }
			public string CustomerName { get; set; }
			public string ItemCode { get; set; }
			public string ItemName { get; set; }
			public int NoBoxesPallet { get; set; }
			public int Quantity { get; set; }
			public int NoBoxes { get; set; }
			public DateTime ReceivingDate { get; set; }
			public DateTime DispatchingDate { get; set; }
			public List<PalletLabelDetails> PalletLabelList { get; set; } = new List<PalletLabelDetails>();

		}
		public class PalletLabelDetails
		{
			public string PalletNo { get; set; }
            public string BinLocation { get; set; } = "";
			public int NoBoxesPallet { get; set; }
            public bool IsReceived { get; set; } = false;
		}

		public List<BinLabelDetails> BinLabelList { get; set; } = new List<BinLabelDetails>();
		public class BinLabelDetails
        {
            public string BinCode { get => $"{WarehouseCode}-{Shelf}-{Row}-{Level}-{IO}";}
            public string WarehouseCode { get; set;}
            public string Shelf { get; set;}
            public string Row { get; set; }
            public string Level { get; set; }
            public string IO { get; set; }
            public string BinStatus { get; set; } = "Available";
        }
	}
    #endregion

    #region Forms And Reports
    public List<Report> Reports { get; set; } = new List<Report>();
    public class Report
    {
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
		#endregion
	}

    public List<SalesOrderDetail> SalesOrderList = new List<SalesOrderDetail>();
    public class SalesOrderDetail
    {
        public string SONo { get; set; }
        public string PlanType { get; set; } = "QA/QC Receiving Inspection Plan";
        public int SampleSize { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string NoOfBoxes { get; set; }
        public string UoM { get; set; }
        public string ReceivingDate { get; set; }
    }

	public List<FormItem> ItemList = new List<FormItem>();
	public class FormItem
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
    }
}
