using DataManager.Models.QCOrder;
using static Application.Models.ControlViewModels.MacroInitialPageViewModel;
using static Application.Models.ControlViewModels.MacroTableViewModel;

namespace Application.Models.ViewModels
{
	public class DashboardViewModel
	{
		#region Dashboard
		public SalesOrders SalesOrder { get; set; } = new SalesOrders();
		public List<SalesOrders> SalesOrderList { get; set; } = new List<SalesOrders>();
		public class SalesOrders
		{
			public string SONo { get; set; } = string.Empty;
			public string CustomerName { get; set; } = string.Empty;
			public string ItemCode { get; set; } = string.Empty;
			public string ItemName { get; set; } = string.Empty;
			public string ProductType { get; set; } = string.Empty;
			public DateTime ReceivingDate { get; set; }

		}

		public List<InitialPageColumnList> SalesOrderColumnList { get; set; } = new List<InitialPageColumnList>
		{
			 new InitialPageColumnList {Key = "SOBatchNumber", Value = "SO Batch Number"}
			,new InitialPageColumnList { Key = "CustomerName", Value = "Customer Name" }
			,new InitialPageColumnList { Key = "ItemCode", Value = "Item Code" }
			,new InitialPageColumnList { Key = "ItemName", Value = "Item Name" }
			,new InitialPageColumnList { Key = "ProductType", Value = "Product Type" }
			,new InitialPageColumnList { Key = "CustomerBatchNo", Value = "Customer Batch No." }
			,new InitialPageColumnList { Key = "PalletStatus", Value = "Pallet Status" }
		};
		#endregion

		#region Sales Order Details
		// QUANTITIES/SPECIFICATIONS
		public List<QuantitySpecs> QuantitySpecificationList { get; set; } = new();
		public List<Schedule> ScheduleList { get; set; } = new();
		public List<IrradiationParameter> IrradiationParameters { get; set; } = new();
		//public List<BatchStatus> BatchStatuses { get; set; } = new();
		public List<SalesOrderActivity> SalesOrderActivities { get; set; } = new();
		public List<PalletDetails> PalletDetailsList { get; set; } = new();
		public List<Batch> Batches { get; set; } = new();
		public class QuantitySpecs
		{
			public double NoofBoxes { get; set; }
			public double NonConformity { get; set; }
			public int DocNum { get; set; }
			public double WeightBox { get; set; }
			public double TotalWeight { get; set; }
			public double NoBoxesPallet { get; set; }
			public double NoPallet { get; set; }
			public double BoxLength { get; set; }
			public double BoxWidth { get; set; }
			public double BoxHeight { get; set; }
			public double BoxVolume { get; set; }
			public double Density { get; set; }

		}
		public class Schedule
		{
			public int DocNum { get; set; }
			public DateTime? DeliveryDate { get; set; }
			public DateTime? IrradiationDate { get; set; }
			public DateTime? PickupReleaseDate { get; set; }
		}
		public class IrradiationParameter
		{
			public int? DocNum { get; set; }
			public string Current { get; set; }
			public string Energy { get; set; }
			public decimal ConveyorSpeed { get; set; }
			public decimal MinDose { get; set; }
			public decimal MaxDose { get; set; }
		}

		//public class BatchStatus
		//{
		//	public string DistNumber { get; set; } = "";
		//	public string UdfField { get; set; } = "";
		//	public string Activity { get; set; } = "";
		//	public DateTime? Start { get; set; }
		//	public DateTime? End { get; set; }
		//	public TimeSpan? Duration { get => (End - Start); }
		//}

		public class SalesOrderActivity
		{
			public string Activity { get; set; }
			public DateTime? Start { get; set; }
			public DateTime? End { get; set; }
			public string Duration { get; set; }
		}

		public class PalletDetails
		{
			public string PalletNo { get; set; }
			public string Activity { get; set; }
			public string Status { get; set; }
			public List<Batch> BatchDetails { get; set; } = new List<Batch>();
		}
		public class Batch
		{
			public string PalletNo { get; set; }
			public string BatchNo { get; set; }
			public string Activity { get; set; }
		}

		public List<UsersTableColumnList> QuantitySpecsColumnList { get; set; } = new List<UsersTableColumnList>
		{
			 new UsersTableColumnList { Key = "NoofBoxes", Value = "No. of Boxes"}
			,new UsersTableColumnList { Key = "WeightBox", Value = "Weight/Box" }
			,new UsersTableColumnList { Key = "TotalWeight", Value = "Total Weight" }
			,new UsersTableColumnList { Key = "NoBoxesPallet", Value = "No. of Boxes/Pallet" }
			,new UsersTableColumnList { Key = "NoPallet", Value = "No. Pallet" }
			,new UsersTableColumnList { Key = "BoxLength", Value = "Box Length" }
			,new UsersTableColumnList { Key = "BoxWidth", Value = "Box Width" }
			,new UsersTableColumnList { Key = "BoxHeight", Value = "Box Height" }
			,new UsersTableColumnList { Key = "BoxVolume", Value = "Box Volume" }
		};
		#endregion

		#region Bin Dashboard
		public class BinDashboardViewModel
		{
			public List<Warehouse> WarehouseList { get; set; } = new List<Warehouse>();
			public class Warehouse
			{
				public string WarehouseCode { get; set; }
				public string WarehouseName { get; set; }
			}

			public List<Shelf> Shelves { get; set; } = new List<Shelf>();
			public class Shelf
			{
				public string ShelfName { get; set; }
			}

			public BinMapping BinMappingDetails { get; set; } = new BinMapping();
			public class BinMapping
			{
				public string WarehouseCode { get; set; }
				public string WarehouseName { get; set; }
				public string Shelf { get; set; }
				public string ImageUrl { get; set; } = "assets/img/no_image.png";
				public string FileName { get; set; } = "no_image.png";
				public ICollection<BinMappingPin> BinMappingPins { get; set; } = new List<BinMappingPin>();
			}

			public class BinMappingPin
			{
				public string WarehouseCode { get; set; }
				public string Shelf { get; set; }
				public string BinCode { get; set; }
				public float Left { get; set; }
				public float Top { get; set; }
				public float Radius { get; set; }
				public string Text { get; set; }
				public string Status { get; set; } = "Available";
				public string SONo { get; set; }
				public string CustomerName { get; set; }
				public string IrradiationDate { get; set; }
				public string DispatchDate { get; set; }
				public string ReceivingDate { get; set; }
				public string ItemName { get; set; }
				public string NoOfBoxesPerPallet { get; set; }
				public string PalletNo { get; set; }
			}

			public class SalesOrders
			{
				public int SONo { get; set; }
				public string CustomerName { get; set; }
				public DateTime IrradiationDate { get; set; }
				public DateTime DispatchDate { get; set; }
				public DateTime ReceivingDate { get; set; }
				public string ItemName { get; set; }
				public int NoOfBoxesPerPallet { get; set; }
			}
		}
		#endregion
	}
}
