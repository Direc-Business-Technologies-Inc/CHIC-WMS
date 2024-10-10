using DataManager.Models.QCMaintenance;

namespace DataManager.Models.QCOrder;

[Table("COR1")]
public class QCOrderSampleDetail
{
	[Key]
	public int Id { get; set; }
	[ForeignKey("QCOrderNo")]
	public string QCOrderNo { get; set; }
	public QCOrder QCOrder { get; set; }
	public int Open { get; set; }
	public int TotalNoOfPassed { get; set; }
	public int TotalNoOfFailed { get; set; }
	public int TotalNoSamples { get; set; }
	public ICollection<QCOrderSampleList> QCOrderSampleList { get; set; } = new List<QCOrderSampleList>();
}
