using System;
using System.Numerics;
using System.Reflection.Metadata;

namespace Application.Models.ViewModels;
public class QCMaintenanceViewModel
{
	public QCMaintenance QcMaintenance { get; set; } = new QCMaintenance();
	public class QCMaintenance
	{
		public string InspectionPlanCode { get; set; } = "";
		public string Date { get; set; } = DateTime.Today.ToString("yyyy-MM-dd");
		public string InspectionPlanName { get; set; }
		public string Section { get; set; } = "";
		public string Version { get; set; } = "1";
		public string Approver { get; set; } = "";
		public string ItemCode { get; set; }
		public string ItemName { get; set; }	
		public string CustomerCode { get; set; } = "";
		public string CustomerName { get; set; } = "";
		public string NoOfSamples { get; set; } = "";
		public string TotalNumberOfBoxes { get; set; } = "";
		public string SamplePassTolerancePercentage { get; set; } = "100";
		public string OverallPassTolerancePercentage { get; set; } = "100";
		public string NumberOfDosimeters { get; set; } = "3";
		public string DosimeterLocation { get; set; } = "";
		public string PlanType { get; set; } = "QA/QC Receiving Inspection Plan";
		public float TotalWeight { get; set; } = 0;

		public DateTime CreateDate { get; set; }
		public ICollection<Parameters> ParameterList { get; set; } = new List<Parameters>();
	}

	public List<InspectionPlans> InspectionPlanList { get; set; } = new List<InspectionPlans>();
	public class InspectionPlans
	{
		public string InspectionPlanCode { get; set; }
		public string InspectionPlanName { get; set; }
		public string Version { get; set; }
		public string ItemName { get; set; }
		public string CustomerName { get; set; }
	}

	public List<Employees> EmployeeList { get; set; } = new List<Employees>();
	public class Employees
	{
		public string EmployeeCode { get; set; }
		public string EmployeeName { get; set; }
	}

	public List<Customers> CustomerList { get; set; } = new List<Customers>();
	public class Customers
	{
		public string CardCode { get; set; }
		public string CardName { get; set; }
	}

	public List<UoM> UoMList { get; set; }
	public class UoM
	{
		public string UgpCode { get; set; }
		public string UgpName { get; set; }
	}

	public List<Item> ItemList { get; set; } = new List<Item>();
	public class Item
	{
		public string ItemCode { get; set; }
		public string ItemName { get; set; }
	}

	public List<DosimeterLocation> DosimeterLocationList { get; set; } = new List<DosimeterLocation>();
	public class DosimeterLocation
	{
		public string LocationName { get; set; }
	}

	public List<Parameters> DefaultParameterList { get; set; } = new List<Parameters>();
	public class Parameters
	{
		public string SelectId { get; set; }
		public string InspectionPlanCode { get; set; } = "";
		public string InspectionPlanName { get; set; } = "";
		public string Version { get; set; } = "";
		public string ParameterType { get; set; } = "Quantitative";
		public string Parameter { get; set; } = "";
		public string UoM { get; set; } = "N/A";
		public float Weight { get; set; }
		public string MinValue { get; set; } = "";
		public string MaxValue { get; set; } = "";
		public string TargetValue { get; set; } = "";
		public string SpecificationType { get; set; } = "Item Specifications";
		public bool Visible { get; set; } = true;
		public bool Active { get; set; } = true;
		public bool Selected { get; set; } = false;
		public bool DefaultParameter { get; set; } = false;
		public string PlanType { get; set; } = "";
	}

	public List<Version> VersionList { get; set; } = new List<Version>();
	public class Version
	{
		public string VersionNumber { get; set; }
	}
}
