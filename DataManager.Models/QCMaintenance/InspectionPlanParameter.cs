namespace DataManager.Models.QCMaintenance;

[Table("CIP1")]
public class InspectionPlanParameter
{
	[Key]
	public int Id { get; set; }
	[ForeignKey("InspectionPlanCode")]
	public string InspectionPlanCode { get; set; }
	public InspectionPlan InspectionPlan { get; set; }
	public string Version { get; set; }
	public string ParameterType { get; set; }
	public string Parameter { get; set; }
	public string UoM { get; set; }
	public float Weight { get; set; }
	public string MinValue { get; set; }
	public string MaxValue { get; set; }
	public string TargetValue { get; set; }
	public string SpecificationType { get; set; }
	public bool Visible { get; set; }
	public bool Active { get; set; }
	public bool DefaultParameter { get; set; }
}
