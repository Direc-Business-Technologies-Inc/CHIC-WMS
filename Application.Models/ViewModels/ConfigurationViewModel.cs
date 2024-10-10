using DataManager.Models.Configurations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models.ViewModels;

public class ConfigurationViewModel
{
	public List<Configuration> Configurations { get; set; } = new List<Configuration>();
	public class Configuration
	{
		public int Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public int Sequence { get; set; }
	}

	public List<ConfigurationItem> ConfigurationItems { get; set; } = new List<ConfigurationItem>();
	public class ConfigurationItem
	{
		public int Id { get; set; }
		public string ConfigurationCode { get; set; }
		public string SubGroup { get; set; }
		public string Sequence { get; set; }
		public string ItemName { get; set; }
		public string ItemValue { get; set; }
		public bool Active { get; set; }
	}

	#region QC Maintenance Config
	public List<QCMaintenanceItem> QCMaintenanceItems { get; set; } = new List<QCMaintenanceItem>();
	public class QCMaintenanceItem
	{
		public string PlanType { get; set; }
		public string ParameterType { get; set; }
		public string Parameter { get; set; }
		public string UoM { get; set; }
		public string SpecificationType { get; set; }
    }

	public List<UoM> UoMList { get; set; } = new List<UoM>();
	public class UoM
	{
		public string UgpCode { get; set; }
		public string UgpName { get; set; }
	}
	#endregion
}
