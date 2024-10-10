namespace DataManager.Models.Bins;

[Table("BPM1")]
public class BinMappingPin
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	[ForeignKey("BinMapping")]
	public string WarehouseCode { get; set; }
	public BinMapping BinMapping { get; set; }
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
