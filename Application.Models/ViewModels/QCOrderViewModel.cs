namespace Application.Models.ViewModels;

public class QCOrderViewModel
{
	public SalesOrderDetail SalesOrderDetails = new SalesOrderDetail();
	public List<SalesOrderDetail> SalesOrderList = new List<SalesOrderDetail>();
	public class SalesOrderDetail
	{
		public int SONo { get; set; }
		public string PlanType { get; set; } = "QA/QC Receiving Inspection Plan";
		public int SampleSize { get; set; } = 0;
		public int Quantity { get; set; } = 0;
		public string CustomerCode { get; set; }
		public string CustomerName { get; set; }
		public string ItemCode { get; set; }
		public string ItemName { get; set; }
		public string NoOfBoxes { get; set; }
		public string UoM { get; set; }
		public string ReceivingDate { get; set; }
		public string StorageConditions { get; set; }
		public string U_SOStatus { get; set; }
	}

	public QCOrderDetail QCOrderDetails = new QCOrderDetail();
	public class QCOrderDetail
	{
		public string QCOrderNo { get; set; } = "";
		public string InspectionPlan { get => (InspectionPlanCode == "" ? "" : $"{InspectionPlanCode} / {InspectionPlanName}"); }
		public string InspectionPlanCode { get; set; } = "";
		public string InspectionPlanName { get; set; } = "";
		public string Customer { get => (CustomerCode == "" ? "" : $"{CustomerCode} / {CustomerName}"); }
		public string CustomerCode { get; set; } = "";
		public string CustomerName { get; set; } = "";
		public string Item { get => (ItemCode == "" ? "" : $"{ItemCode} / {ItemName}"); }
		public string ItemCode { get; set; } = "";
		public string ItemName { get; set; } = "";
		public string Remarks { get; set; } = "";
		public int DocNo { get; set; }
		public string DocDate { get; set; } = DateTime.Today.ToString("MM-dd-yyyy");
		public string DocType { get; set; } = "Sales Order No.";
		public string InspectionPlanType { get; set; } = "QA/QC Receiving Inspection Plan";
		public string Quantity { get; set; } = "";
		public string UoM { get; set; } = "";
		public string RefNo { get; set; } = "";
		public string Status { get; set; } = "";
		public int SampleSize { get; set; } = 0;
		public string SamplePassTolerancePercentage { get; set; } = "";
		public string OverallPassTolerancePercentage { get; set; } = "";
		public string PONo { get; set; } = "";
        public string ManufacturingLotNo { get; set; } = "";
        public string ServiceOrderNo { get; set; } = "";
        public string StorageConditions { get; set; } = "";
        public Sample SampleDetails { get; set; } = new Sample();
		public Dosimetry DosimetryReport { get; set; } = new Dosimetry();
		public string DosimetryUsed { get; set; } = ""; 
	}

	public class Sample
	{
		public int Open { get; set; }
		public int TotalNoOfPassed { get; set; }
		public int TotalNoOfFailed { get; set; }
		public int TotalNoSamples { get; set; }
		public List<SampleDetail> SampleDetailList { get; set; } = new List<SampleDetail>();
	}

	public List<SampleDetail> SampleList { get; set; } = new List<SampleDetail>();
	public List<SampleDetail> SampleDetailList { get; set; } = new List<SampleDetail>();
	public class SampleDetail
	{
		public int SampleNo { get; set; }
		public int NoOfPassed { get; set; }
		public int NoOfFailed { get; set; }
		public string Status { get; set; } = "Pending";
		public string QABy { get; set; } = "";
		public string ApprovedBy { get; set; } = "";

		public ICollection<ParameterDetail> ParameterDetailList { get; set; } = new List<ParameterDetail>();
	}

	public List<InspectionPlan> InspectionPlanList { get; set; } = new List<InspectionPlan>();
	public class InspectionPlan
	{
		public string InspectionPlanCode { get; set; }
		public string InspectionPlanName { get; set; }
		public string PlanType { get; set; }
		public string Version { get; set; }
		public string ItemCode { get; set; }
		public string ItemName { get; set; }
		public string CustomerCode { get; set; }
		public string CustomerName { get; set; }
		public string SamplePassTolerancePercentage { get; set; }
		public string OverallPassTolerancePercentage { get; set; }
	}

	public ICollection<ParameterDetail> ParameterList { get; set; } = new List<ParameterDetail>();
	public class ParameterDetail
	{
		public int ParameterNo { get; set; }
		public string Parameter { get; set; } = "";
		public string ParameterType { get; set; } = "";
		public string UoM { get; set; } = "";
		public float Weight { get; set; }
		public string MinValue { get; set; } = "";
		public string MaxValue { get; set; } = "";
		public string TargetValue { get; set; } = "";
		public string ActualValue { get; set; } = "";
		public bool Active { get; set; }
		public bool Visible { get; set; }
		public string Result { get => AutomateResult(); }
		public string Remarks { get; set; } = "";

		public string AutomateResult()
		{
			if(ActualValue == "")
			{
				return "For QA";
			}

			string result = "";
			if (ParameterType == "Quantitative")
			{
				float val = 0;
				float minValue = Convert.ToSingle(MinValue == "" ? "0" : MinValue);
				float maxValue = Convert.ToSingle(MaxValue == "" ? "0" : MaxValue);
				float actualValue = Convert.ToSingle(ActualValue == "" ? "0" : ActualValue);

				if (minValue < 0 && maxValue < 0 && actualValue < 0)
				{
					minValue = Math.Abs(minValue);
					maxValue = Math.Abs(maxValue);
					actualValue = Math.Abs(actualValue);
				}

				if (minValue > actualValue ||  maxValue < actualValue)
				{
					if (minValue == actualValue || maxValue == actualValue)
					{
						result = "Passed";
					}
					else
					{
						result = "Failed";
					}
				}
				else
				{
					result = "Passed";
				}
			}
			else if (ParameterType == "Qualitative")
			{
				if(TargetValue == "No" || TargetValue == "Failed" || TargetValue != ActualValue)
				{
					result = "Failed";
				}
				else
				{
					result = "Passed";
				}
			}

			return result;
		}
	}

	public List<QCOrders> QCOrderList { get; set; } = new List<QCOrders>();
	public class QCOrders
	{
		public string QCOrderNo { get; set; }
		public string CustomerName { get; set; }
		public string ItemName { get; set; }
		public string InspectionPlanType { get; set; }
	}

	public class Dosimetry
	{
		public string EBOperationLog { get; set; } = "";
		public int ActualEnergy { get; set; }
		public int ActualPower { get; set; }
		public int ActualFrequency { get; set; }
		public int TotalProductsBeforeIrradiation { get; set; }
		public int TotalProductsAfterIrradiation { get; set; }
		public string NCReport { get; set; } = "";
		public string Remarks { get; set; } = "";
		public string DosimetryReportNo { get; set; } = "";
	}

	//public List<UoM> UoMList { get; set; } = new List<UoM>();
	//public class UoM
	//{
	//	public string Code { get; set; }
	//	public string Name { get; set; }
	//}

	public List<DosimetryType> DosimetryList { get; set; }
	public class DosimetryType
	{
		public string Code { get; set; } = "";
		public string Name { get; set; } = "";
	}
}
