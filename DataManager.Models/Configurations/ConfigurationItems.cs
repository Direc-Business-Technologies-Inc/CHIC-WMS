using DataManager.Models.QCMaintenance;

namespace DataManager.Models.Configurations;

public class ConfigurationItems
{
	public int Id { get; set; }
	[ForeignKey("Code")]
	public string ConfigurationCode { get; set; }
	public Configurations Configuration { get; set; }
	public string SubGroup { get; set; }
	public string Sequence { get; set; }
	public string ItemName { get; set; }
	public string ItemValue { get; set; }
	public bool Active { get; set; }
	public DateTime CreateDate { get; set; }
	public string CreateUserId { get; set; }
	public DateTime? UpdateDate { get; set; }
	public string UpdateUserId { get; set; }
}
