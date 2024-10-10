namespace DataManager.Models.QCMaintenance;

[Table("QCIP")]
public class InspectionPlan
{
	[Key]
	public string InspectionPlanCode { get; set; }
	public DateTime Date { get; set; }
	public string InspectionPlanName { get; set; }
	public string Section { get; set; }
	public string Version { get; set; }
	public string Approver { get; set; }
	public string ItemCode { get; set; }
	public string ItemName { get; set; }
	public string CustomerCode { get; set; }
	public string CustomerName { get; set; }
	public string NoOfSamples { get; set; }
	public string TotalNumberOfBoxes { get; set; }
	public string SamplePassTolerancePercentage { get; set; }
	public string OverallPassTolerancePercentage { get; set; }
	public string NumberOfDosimeters { get; set; }
	public string DosimeterLocation { get; set; }
	public string PlanType { get; set; }
	public float TotalWeight { get; set; }
	public DateTime CreateDate { get; set; }
	public DateTime? UpdateDate { get; set; }
	public ICollection<InspectionPlanParameter> ParameterList { get; set; } = new List<InspectionPlanParameter>();
}
