namespace DataManager.Models.Bins;

[Table("OBMP")]
public class BinMapping
{
	[Key]
	[Column(Order = 0)]
	public string WarehouseCode { get; set; }
	public string WarehouseName { get; set; }
	[Key]
	[Column(Order = 1)]
	public string Shelf { get; set; }
	public string ImageName { get; set; }
	public DateTime CreateDate { get; set; }
	public DateTime? UpdateDate { get; set; }
	public ICollection<BinMappingPin> BinMappingPins { get; set; }
}
