namespace DataManager.Models.QCOrder;

[Table("COR4")]
public class QCOrderDosimetryReport
{
	[Key]
	public int Id { get;set; }
	[ForeignKey("QCOrderNo")]
	public string QCOrderNo { get; set; }
	public QCOrder QCOrder { get; set; }
	public string EBOperationLog { get; set; }
	public int ActualEnergy { get; set; }
	public int ActualPower { get; set; }
	public int ActualFrequency { get; set; }
	public int TotalProductsBeforeIrradiation { get; set; }
	public int TotalProductsAfterIrradiation { get; set; }
	public string NCReport { get; set; }
	public string Remarks { get; set; }
	public string DosimetryReportNo { get; set; }
}
