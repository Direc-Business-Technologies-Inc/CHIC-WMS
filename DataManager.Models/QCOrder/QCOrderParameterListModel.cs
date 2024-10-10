namespace DataManager.Models.QCOrder;

[Table("COR3")]
public class QCOrderParameterList
{
	[Key]
	public int Id { get; set; }
	[ForeignKey("Id")]
	public int SampleId { get; set; }
	public QCOrderSampleList QCOrderSampleList { get; set; }
	public int ParameterNo { get; set; }
	public string Parameter { get; set; }
	public string ParameterType { get; set; }
	public string UoM { get; set; }
	public float Weight { get; set; }
	public string MinValue { get; set; }
	public string MaxValue { get; set; }
	public string TargetValue { get; set; }
	public string ActualValue { get; set; }
	public string Result { get; set; }
	public string Remarks { get; set; }
}
