namespace Application.Models.ViewModels;

public class BinViewModel : Profile
{ 
	public class BinMappingViewModel
	{
		public BinMappingHeaders BinMappingHeader { get; set; } = new BinMappingHeaders();
		public class BinMappingHeaders
		{
			public string WarehouseCode { get; set; }
			public string WarehouseName { get; set; }
			public string Shelf { get; set; }
			public string Level { get; set; }
			public string Aisle { get; set; }
			public string ImageUrl { get; set; } = "assets/img/no_image.png";
			public string FileName { get; set; } = "no_image.png";
			public string FacilityCode { get; set; }
			public DateTime CreateDate { get; set; }
			public ICollection<BinMappingPins> BinMappingPins { get; set; } = new List<BinMappingPins>();
		}

		public List<BinMappingPins> BinMappingPinList { get; set; } = new List<BinMappingPins>();
		public class BinMappingPins
		{
			public string WarehouseCode { get; set; }
			public string Shelf { get; set; }
			public string BinCode { get; set; }
			public float Left { get; set; }
			public float Top { get; set; }
			public float Radius { get; set; }
			public string Text { get; set; }
			public int Row { get; set; }
			public int Level { get; set; }
			public string Aisle { get; set; }
		}

		public List<Warehouses> WarehouseList { get; set; } = new List<Warehouses>();
		public class Warehouses
		{
			public string WarehouseCode { get; set; }
			public string WarehouseName { get; set; }
		}

		public List<Shelves> ShelfList { get; set; } = new List<Shelves>();
		public class Shelves
		{
			public string Shelf { get; set; }
			//public int Count { get; set; }
		}

		public List<Levels> LevelList { get; set; } = new List<Levels>();
		public class Levels
		{
			public string Level { get; set; }
			//public int Count { get; set; }
		}

		public List<Aisles> AisleList { get; set; } = new List<Aisles>();
		public class Aisles
		{
			public string Aisle { get; set; }
			public string Code { get; set; }
		}

		//public dynamic[] breadCrumbs { get; set; }
		public List<Rows> RowList { get; set; }
		public class Rows
		{
			public string Row { get; set; }
			//public int Count { get; set; }
		}

		public IEnumerable<Countries> CountryList { get; set; }
		public class Countries
		{
			public string Code { get; set; }
			public string Name { get; set; }
		}
		public class BinAccumulator
		{
            public int BinAbs { get; set; }
            public int AbsEntry { get; set; }
			public string ItemCode { get; set; }
			public int SnBMDAbs { get; set; }
			public int BaseLineNumber { get; set; }            
			public string BinCode { get; set; }
			public decimal? OnHandQty { get; set; }
			public string WhsCode { get; set; }
			public string BatchNumber { get;set; }
			public bool CheckState { get; set; }
        }
	}

}
