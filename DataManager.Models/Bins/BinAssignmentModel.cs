namespace DataManager.Models.Bins;

[Table("OBAS")]
public class BinAssignment
{
	[Key]
	public string BinCode { get; set; }
	public string SONo { get; set; }
	public string PalletNo { get; set; }
	public string WarehouseCode { get; set; }
	public DateTime CreateDate { get; set; }
	public DateTime? UpdateDate { get; set; }
}
