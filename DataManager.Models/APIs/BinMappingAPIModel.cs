using AutoMapper;
using DataManager.Models.Bins;

namespace DataManager.Models.APIs;

public class BinMappingAPIModel
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
		public DateTime CreateDate { get; set; }
		public ICollection<BinMappingPins> BinMappingPins { get; set; }
	}

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

}
