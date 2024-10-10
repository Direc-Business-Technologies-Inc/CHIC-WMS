namespace DataManager.Models.QCOrder;

[Table("COR2")]
public class QCOrderSampleList
{
	[Key]
	public int Id { get; set; }
	[ForeignKey("Id")]
	public int SampleId { get; set; }
	public QCOrderSampleDetail QCOrderSampleDetail { get; set; }
	public int SampleNo { get; set; }
	public string Status { get; set; }
	public int NoOfPassed { get; set; }
	public int NoOfFailed { get; set; }
	public string QABy { get; set; }
	public string ApprovedBy { get; set; }
	public ICollection<QCOrderParameterList> QCOrderParameterList { get; set; } = new List<QCOrderParameterList>();
}
