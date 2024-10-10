using Application.BlazorServer.Extensions;
using Microsoft.AspNetCore.SignalR.Client;

namespace Application.BlazorServer.Pages.QCOrder;

public partial class QCOrder : ComponentBase
{
	[Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;
	[Inject] protected IQCOrderService _qcOrder { get; set; }
	[Inject] private NavigationManager _navManager { get; set; }
	private IJSObjectReference _js { get; set; } = default!;
	[Inject] private IConfiguration _configuration { get; set; }
	[Inject] private IWebHostEnvironment _environment { get; set; }
	//[Inject] ContextMenuService ContextMenuService { get; set; }

	List<QCOrderViewModel.SalesOrderDetail> filteredSOGenerate = new List<QCOrderViewModel.SalesOrderDetail>();
	List<QCOrderViewModel.SalesOrderDetail> filteredSO = new List<QCOrderViewModel.SalesOrderDetail>();
	List<QCOrders> filteredQCOrders = new List<QCOrders>();
	List<QCOrderViewModel.DosimetryType> filteredDosimetry = new List<DosimetryType>();

	private string SOGenerateSearchValue { get; set; } = string.Empty;
	private string SOSearchValue { get; set; } = string.Empty;
	private string QCOrderSearchValue { get; set; } = string.Empty;
	private string DosTypeSearchValue { get; set; } = string.Empty;

	#region Application Events
	private HubConnection? _hubConnection;

	readonly HashSet<IDisposable> _hubRegistrations = new();
	async Task InitializeApplicationEventsConnection()
	{
		_hubConnection = new HubConnectionBuilder()
		.WithUrl(_navManager.ToAbsoluteUri("/hubs/dashboard-notification"), (opts) =>
		{
			opts.HttpMessageHandlerFactory = (message) =>
			{
				if (message is HttpClientHandler clientHandler)
					// bypass SSL certificate
					clientHandler.ServerCertificateCustomValidationCallback +=
						(sender, certificate, chain, sslPolicyErrors) => { return true; };
				return message;
			};
		})
		.Build();
		await _hubConnection.StartAsync();
	}
	#endregion

	dynamic Breadcrumbs = new dynamic[]
	{
		"QCOrder",
		"QC Order"
	};

	CertificateOfIrradiation COI = new CertificateOfIrradiation();
	QCOrderViewModel model = new QCOrderViewModel();

	int sampleNo = 0;

	public List<string> PlanType = new List<string>();
	public List<string> FilteredPlanType = new List<string>();
	public List<string> FilteredPlanTypeGenerate = new List<string>();

	string HeaderQCOrderNo = "";
	string HeaderQCOrderStatus = "";
	string HeaderQCPlanType = "";
	bool CanCOI = false;

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			_js = await _jSRuntime.InvokeAsync<IJSObjectReference>("import", "../scripts/CustomScripts/QCOrder.js");

			//For Calling of backend function from JS
			await _js.InvokeVoidAsync("setdotnetInstance", DotNetObjectReference.Create(this));

			string InitializePopOver = @"(function () {
					  const popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle=""popover""]'));
					  const popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
						// added { html: true, sanitize: false } option to render button in content area of popover
						return new bootstrap.Popover(popoverTriggerEl, { html: true, sanitize: false });
					  });
					})();";

			await _jSRuntime.InvokeVoidAsync("eval", InitializePopOver);
			await _js.InvokeVoidAsync("InitializeStepper");
		}
	}

	protected override async Task OnInitializedAsync()
	{
		try
		{
			model = _qcOrder.InitializeQCOrder();

			PlanType.Add("QA/QC Receiving Inspection Plan");
			PlanType.Add("Dosimetry Quality Control Order");
			PlanType.Add("Others");

			FilteredPlanType = PlanType;
			FilteredPlanTypeGenerate = PlanType;
			filteredSOGenerate = model.SalesOrderList;
			filteredSO = model.SalesOrderList;
			filteredQCOrders = model.QCOrderList;
			filteredDosimetry = model.DosimetryList;
			//model.SampleList.Add(new QCOrderViewModel.SampleDetail { Open = "0", Passed = "0", Failed = "0", TotalNoSamples = "0" });

			await InitializeApplicationEventsConnection();
		}
		catch (Exception)
		{

			throw;
		}
	}

	public void SelectInput(string InputName, string value)
	{
		if (InputName == "EB Operation Log" || InputName == "NC Report")
		{
			model.QCOrderDetails.DosimetryReport.GetType().GetProperty(InputName.Replace(" ", "")).SetValue(model.QCOrderDetails.DosimetryReport, value);
		}
		//else
		//{
		//	model.QCOrderDetails.GetType().GetProperty(InputName.Replace(" ", "")).SetValue(model.QCOrderDetails, value);
		//}
		if (InputName == "Inspection Plan")
		{
			//var inspectionPlan = model.InspectionPlanList.Where(x => x.InspectionPlanCode == value).FirstOrDefault();
			//model.QCOrderDetails.InspectionPlanCode = inspectionPlan?.InspectionPlanCode ?? "";
			//model.QCOrderDetails.InspectionPlanName = inspectionPlan?.InspectionPlanName ?? "";
			//model.QCOrderDetails.SamplePassTolerancePercentage = inspectionPlan?.SamplePassTolerancePercentage ?? "";
			//model.QCOrderDetails.OverallPassTolerancePercentage = inspectionPlan?.OverallPassTolerancePercentage ?? "";


			////GET INSPECTION PLAN PARAMETERS
			//model.ParameterList = _qcOrder.GetParameters(inspectionPlan.InspectionPlanCode, inspectionPlan.Version);

			//foreach (var param in model.ParameterList)
			//{
			//	param.MinValue = GetActualValue(param.MinValue);
			//	param.MaxValue = GetActualValue(param.MaxValue);

			//	param.ParameterNo = (model.ParameterList.ToList().IndexOf(param) + 1);
			//}

		}
		if (InputName == "Document No")
		{

			var soDetails = model.SalesOrderList.Where(x => x.SONo == Convert.ToInt32(value)).FirstOrDefault();

			//#region Fetch QC Order if existing
			//var fetchedQCOrder = SelectQCOrder(soDetails.ItemCode, model.QCOrderDetails.InspectionPlanType);
			//#endregion

			//if (!fetchedQCOrder.Result)
			//{
			//	#region Create New QC Order
			model.QCOrderDetails = new QCOrderDetail();
			model.QCOrderDetails.DocNo = soDetails?.SONo ?? 0;
			model.QCOrderDetails.CustomerCode = soDetails?.CustomerCode ?? "";
			model.QCOrderDetails.CustomerName = soDetails?.CustomerName ?? "";
			model.QCOrderDetails.ItemCode = soDetails?.ItemCode ?? "";
			model.QCOrderDetails.ItemName = soDetails?.ItemName ?? "";
			model.QCOrderDetails.InspectionPlanType = model.SalesOrderDetails.PlanType;
			model.QCOrderDetails.Quantity = soDetails?.NoOfBoxes ?? "";
			model.QCOrderDetails.UoM = soDetails?.UoM ?? "";
			model.QCOrderDetails.StorageConditions = soDetails?.StorageConditions ?? "";
			//	#endregion
			//}

			//Set FilteredPlanType depending on SO
			var inspectionPlanList = model.InspectionPlanList.Where(x => x.ItemCode == soDetails.ItemCode).DistinctBy(x => x.PlanType)
				.Select(x => new
				{
					PlanType = x.PlanType
				})
				.ToList();

			FilteredPlanType = new List<string>();

			foreach (var plan in inspectionPlanList)
			{
				FilteredPlanType.Add(plan.PlanType);
			}

			//if (!fetchedQCOrder.Result)
			//{
			//	#region For New QC Order
			model.QCOrderDetails.InspectionPlanType = FilteredPlanType.FirstOrDefault();

			//Inspection Plan
			var inspectionPlan = model.InspectionPlanList.Where(x => x.ItemCode == model.QCOrderDetails.ItemCode && x.PlanType == model.QCOrderDetails.InspectionPlanType).FirstOrDefault();
			model.QCOrderDetails.InspectionPlanCode = inspectionPlan?.InspectionPlanCode ?? "";
			model.QCOrderDetails.InspectionPlanName = inspectionPlan?.InspectionPlanName ?? "";
			model.QCOrderDetails.SamplePassTolerancePercentage = inspectionPlan?.SamplePassTolerancePercentage ?? "";
			model.QCOrderDetails.OverallPassTolerancePercentage = inspectionPlan?.OverallPassTolerancePercentage ?? "";


			//GET INSPECTION PLAN PARAMETERS
			model.ParameterList = _qcOrder.GetParameters(inspectionPlan?.InspectionPlanCode ?? "", inspectionPlan?.Version ?? "") ?? new List<ParameterDetail>();

			foreach (var param in model.ParameterList)
			{
				param.MinValue = GetActualValue(param.MinValue);
				param.MaxValue = GetActualValue(param.MaxValue);

				param.ParameterNo = (model.ParameterList.ToList().IndexOf(param) + 1);
			}
			//	#endregion
			//}

			model.QCOrderDetails.DocNo = Convert.ToInt32(value);
			StateHasChanged();
			//model.QcMaintenance.GetType().GetProperty("CustomerName").SetValue(mo66del.QcMaintenance, model.CustomerList.Where(x => x.CardCode == value).FirstOrDefault()?.CardName ?? "");
		}
		if (InputName == "SO Plan Type")
		{
			model.SalesOrderDetails.PlanType = value;
		}
		if (InputName == "SO Sample Size")
		{
			model.SalesOrderDetails.SampleSize = Convert.ToInt32(value);
		}
		if (InputName == "SO No")
		{
			var soDetails = model.SalesOrderList.Where(x => x.SONo == Convert.ToInt32(value)).FirstOrDefault();

			//model.SalesOrderDetails.SONo = Convert.ToInt32(value);
			model.SalesOrderDetails.SONo = soDetails?.SONo ?? 0;
			model.SalesOrderDetails.Quantity = Convert.ToInt32(soDetails?.NoOfBoxes ?? "0");

			//Set FilteredPlanTypeGenerate depending on SO
			var inspectionPlanList = model.InspectionPlanList.Where(x => x.ItemCode == soDetails.ItemCode).DistinctBy(x => x.PlanType)
				.Select(x => new
				{
					PlanType = x.PlanType
				})
				.ToList();

			FilteredPlanTypeGenerate = new List<string>();

			foreach (var plan in inspectionPlanList)
			{
				FilteredPlanTypeGenerate.Add(plan.PlanType);
			}

			model.SalesOrderDetails.PlanType = FilteredPlanTypeGenerate.FirstOrDefault();
			_js.InvokeVoidAsync("setDocNum", value, model.SalesOrderDetails.Quantity, FilteredPlanTypeGenerate);
		}
		if (InputName == "Reference No")
		{
			model.QCOrderDetails.RefNo = value;
		}
		if (InputName == "PO No")
		{
			model.QCOrderDetails.PONo = value;
		}
		if (InputName == "Manufacturing Lot No")
		{
			model.QCOrderDetails.ManufacturingLotNo = value;
		}
		if (InputName == "Service Order No")
		{
			model.QCOrderDetails.ServiceOrderNo = value;
		}
		if (InputName == "Storage Conditions")
		{
			model.QCOrderDetails.StorageConditions = value;
		}
		if (InputName == "DosimetryType")
		{
			model.QCOrderDetails.DosimetryUsed = value;
		}
		_jSRuntime.InvokeVoidAsync("HideModal");
	}

	[JSInvokable("SelectInputBackend")]
	public async Task SelectInputBackend(string InputName, string value)
	{
		SelectInput(InputName, value);
	}

	[JSInvokable("GenerateSO")]
	public async Task GenerateSO()
	{

		var soDetails = model.SalesOrderList.Where(x => x.SONo == model.SalesOrderDetails.SONo).FirstOrDefault();

		//#region Fetch QC Order if existing
		//var fetchedQCOrder = await SelectQCOrder(soDetails.ItemCode, model.SalesOrderDetails.PlanType);
		//#endregion

		//if (!fetchedQCOrder)
		//{
		//#region New QC Order
		model.QCOrderDetails = new QCOrderDetail();
		model.QCOrderDetails.DocNo = soDetails?.SONo ?? 0;
		model.QCOrderDetails.CustomerCode = soDetails?.CustomerCode ?? "";
		model.QCOrderDetails.CustomerName = soDetails?.CustomerName ?? "";
		model.QCOrderDetails.ItemCode = soDetails?.ItemCode ?? "";
		model.QCOrderDetails.ItemName = soDetails?.ItemName ?? "";
		model.QCOrderDetails.Quantity = soDetails?.NoOfBoxes ?? "";
		model.QCOrderDetails.UoM = soDetails?.UoM ?? "";
		model.QCOrderDetails.SampleSize = model.SalesOrderDetails.SampleSize;
		model.QCOrderDetails.StorageConditions = soDetails?.StorageConditions ?? "";

		model.QCOrderDetails.SampleDetails.SampleDetailList = new List<QCOrderViewModel.SampleDetail>();
		for (int count = 1; count <= Convert.ToInt32(model.SalesOrderDetails.SampleSize); count++)
		{
			model.QCOrderDetails.SampleDetails.SampleDetailList.Add(new QCOrderViewModel.SampleDetail { SampleNo = count, Status = "Pending", NoOfFailed = 0, NoOfPassed = 0, QABy = "QA1" });
		}

		model.QCOrderDetails.SampleDetails.TotalNoSamples = model.QCOrderDetails.SampleSize;
		model.QCOrderDetails.SampleDetails.Open = model.QCOrderDetails.SampleDetails.SampleDetailList.Where(x => x.Status == "Pending").Count();

		//Inspection Plan
		var inspectionPlan = model.InspectionPlanList.Where(x => x.ItemCode == model.QCOrderDetails.ItemCode && x.PlanType == model.SalesOrderDetails.PlanType).FirstOrDefault();
		model.QCOrderDetails.InspectionPlanCode = inspectionPlan?.InspectionPlanCode ?? "";
		model.QCOrderDetails.InspectionPlanName = inspectionPlan?.InspectionPlanName ?? "";
		model.QCOrderDetails.SamplePassTolerancePercentage = inspectionPlan?.SamplePassTolerancePercentage ?? "";
		model.QCOrderDetails.OverallPassTolerancePercentage = inspectionPlan?.OverallPassTolerancePercentage ?? "";

		FilteredPlanType = new List<string>();
		FilteredPlanType = FilteredPlanTypeGenerate;

		model.QCOrderDetails.InspectionPlanType = model.SalesOrderDetails.PlanType;

		//GET INSPECTION PLAN PARAMETERS
		model.ParameterList = _qcOrder.GetParameters(inspectionPlan?.InspectionPlanCode ?? "", inspectionPlan?.Version ?? "") ?? new List<ParameterDetail>();

		foreach (var param in model.ParameterList)
		{
			param.MinValue = GetActualValue(param.MinValue);
			param.MaxValue = GetActualValue(param.MaxValue);

			param.ParameterNo = (model.ParameterList.ToList().IndexOf(param) + 1);
		}
		//	#endregion
		//}
		//else
		//{
		//CHECK IF THE GENERATED SAMPLE SIZE IS NOT SAME AS FETCHED SAMPLE SIZE
		//if (model.QCOrderDetails.SampleSize != model.SalesOrderDetails.SampleSize)
		//	{
		//		if (await _jSRuntime.InvokeAsync<bool>("confirm", "Current sample size is different from saved sample size. Would you like to update the QC Order?", $"Update ({model.SalesOrderDetails.SampleSize} Sample Size)", $"Retain ({model.QCOrderDetails.SampleSize} Sample Size)"))
		//		{
		//			//CHANGE SAMPLE SIZE
		//			model.QCOrderDetails.SampleSize = model.SalesOrderDetails.SampleSize;

		//			model.QCOrderDetails.SampleDetails = new Sample();

		//			model.QCOrderDetails.SampleDetails.SampleDetailList = new List<QCOrderViewModel.SampleDetail>();
		//			for (int count = 1; count <= Convert.ToInt32(model.SalesOrderDetails.SampleSize); count++)
		//			{
		//				model.QCOrderDetails.SampleDetails.SampleDetailList.Add(new QCOrderViewModel.SampleDetail { SampleNo = count, Status = "Pending", NoOfFailed = 0, NoOfPassed = 0, QABy = "QA1" });
		//			}

		//			model.QCOrderDetails.SampleDetails.TotalNoSamples = model.QCOrderDetails.SampleSize;
		//			model.QCOrderDetails.SampleDetails.Open = model.QCOrderDetails.SampleDetails.SampleDetailList.Where(x => x.Status == "Pending").Count();

		//			//Inspection Plan
		//			var inspectionPlan = model.InspectionPlanList.Where(x => x.ItemCode == model.QCOrderDetails.ItemCode && x.PlanType == model.QCOrderDetails.InspectionPlanType).FirstOrDefault();
		//			model.QCOrderDetails.InspectionPlanCode = inspectionPlan?.InspectionPlanCode ?? "";
		//			model.QCOrderDetails.InspectionPlanName = inspectionPlan?.InspectionPlanName ?? "";
		//			model.QCOrderDetails.SamplePassTolerancePercentage = inspectionPlan?.SamplePassTolerancePercentage ?? "";
		//			model.QCOrderDetails.OverallPassTolerancePercentage = inspectionPlan?.OverallPassTolerancePercentage ?? "";


		//			//GET INSPECTION PLAN PARAMETERS
		//			model.ParameterList = _qcOrder.GetParameters(inspectionPlan?.InspectionPlanCode ?? "", inspectionPlan?.Version ?? "") ?? new List<ParameterDetail>();

		//			foreach (var param in model.ParameterList)
		//			{
		//				param.MinValue = GetActualValue(param.MinValue);
		//				param.MaxValue = GetActualValue(param.MaxValue);

		//				param.ParameterNo = (model.ParameterList.ToList().IndexOf(param) + 1);
		//			}
		//		}
		//	}
		//}

		model.SalesOrderDetails = new QCOrderViewModel.SalesOrderDetail();

		FilteredPlanTypeGenerate = new List<string>();
		FilteredPlanTypeGenerate = PlanType;

		model.SalesOrderDetails.PlanType = FilteredPlanTypeGenerate.FirstOrDefault();

		await _js.InvokeVoidAsync("setDocNum", string.Empty, 0, FilteredPlanTypeGenerate);

		StateHasChanged();

		await Task.Delay(1000);
	}

	void CellRender(DataGridCellRenderEventArgs<QCOrderViewModel.ParameterDetail> args)
	{
		if (args.Column.Title == "Result")
		{
			if (args.Data.Result != "For QA")
			{
				args.Attributes.Add("style", $"background-color: {(args.Data.Result == "Passed" ? "var(--rz-success)" : "var(--rz-danger)")}; border-radius: 25px;");
			}
			else
			{
				args.Attributes.Add("style", $"background-color: white; ");
			}
		}
	}

	void CellRenderSampleList(DataGridCellRenderEventArgs<QCOrderViewModel.SampleDetail> args)
	{
		if (args.Column.Title == "Status")
		{
			if (args.Data.Status != "Pending")
			{
				args.Attributes.Add("style", $"background-color: {(args.Data.Status == "Passed" ? "var(--rz-success)" : "var(--rz-danger)")}; border-radius: 25px;");
			}
		}
	}

	public string GetActualValue(string value)
	{
		string newValue = value;

		switch (value)
		{
			case "Y":
				newValue = "Yes/No";
				break;
			case "P":
				newValue = "Passed/Failed";
				break;
		}

		return newValue;
	}

	//public async Task SelectSampleDetail(QCOrderViewModel.SampleDetail sampleDetail)
	public async Task SelectSampleDetail(int sampleDetail)
	{
		try
		{
			bool checker = await _js.InvokeAsync<bool>("checkInspectionPlan");
			if (!checker)
			{
				await _jSRuntime.InvokeVoidAsync("ShowResult", "Info", "Please input Inspection Plan");
				return;
			}

			sampleNo = sampleDetail;

			foreach (var parameter in model.ParameterList)
			{
				parameter.ActualValue = "";
				parameter.Remarks = "";
			}

			var sampleDetails = model.QCOrderDetails.SampleDetails.SampleDetailList.Where(x => x.SampleNo == sampleNo).FirstOrDefault();
			if (sampleDetails.ParameterDetailList.Count > 0)
			{
				foreach (var parameter in model.ParameterList)
				{
					var sample = sampleDetails.ParameterDetailList.Where(x => x.ParameterNo == parameter.ParameterNo).FirstOrDefault();

					parameter.ActualValue = sample?.ActualValue ?? "";
					parameter.Remarks = sample?.Remarks ?? "";
				}
			}

			StateHasChanged();

			await _js.InvokeVoidAsync("nextStep");
		}
		catch (Exception)
		{

			throw;
		}
	}

	[JSInvokable("SubmitParameter")]
	public async Task<object> SubmitParameter()
	{
		bool PreviousPage = false;

		var sampleDetail = model.QCOrderDetails.SampleDetails.SampleDetailList.Where(x => x.SampleNo == sampleNo).FirstOrDefault();
		sampleDetail.ParameterDetailList = new List<ParameterDetail>();

		foreach (var parameter in model.ParameterList)
		{
			sampleDetail.ParameterDetailList.Add(new ParameterDetail
			{
				Parameter = parameter.Parameter,
				Active = parameter.Active,
				ParameterNo = parameter.ParameterNo,
				ActualValue = parameter.ActualValue,
				MaxValue = parameter.MaxValue,
				MinValue = parameter.MinValue,
				ParameterType = parameter.ParameterType,
				Remarks = parameter.Remarks,
				TargetValue = parameter.TargetValue,
				UoM = parameter.UoM,
				Visible = parameter.Visible,
				Weight = parameter.Weight
			});
		}

		sampleDetail.NoOfPassed = sampleDetail.ParameterDetailList.Where(x => x.Result == "Passed").Count();
		sampleDetail.NoOfFailed = sampleDetail.ParameterDetailList.Where(x => x.Result == "Failed").Count();

		var samples = model.QCOrderDetails.SampleDetails;

		if (sampleDetail.ParameterDetailList.Where(x => x.ActualValue == "").Count() <= 0)
		{
			//Check if passed or failed
			//float samplePercentage = Convert.ToSingle(model.QCOrderDetails.SamplePassTolerancePercentage) / 100;
			float parameterWeight = model.ParameterList.Where(x => x.Result == "Passed").ToList().Sum(x => x.Weight);
			sampleDetail.Status = Convert.ToSingle(model.QCOrderDetails.SamplePassTolerancePercentage) <= parameterWeight ? "Passed" : "Failed";

			//Update Samples
			samples.Open = samples.SampleDetailList.Where(x => x.Status == "Pending").Count();
			samples.TotalNoOfPassed = samples.SampleDetailList.Where(x => x.Status == "Passed").Count();
			samples.TotalNoOfFailed = samples.SampleDetailList.Where(x => x.Status == "Failed").Count();
		}

		//Update Header Status
		if (samples.Open == 0 && samples.TotalNoSamples != 0)
		{
			float headerPercentage = Convert.ToSingle(model.QCOrderDetails.OverallPassTolerancePercentage) / 100;
			float sampleListPercentage = Convert.ToSingle(samples.TotalNoOfPassed) / Convert.ToSingle(samples.TotalNoSamples);
			model.QCOrderDetails.Status = headerPercentage <= sampleListPercentage ? "Passed" : "Failed";
		}

		if (sampleNo == model.QCOrderDetails.SampleDetails.SampleDetailList.Count())
		{
			PreviousPage = true;
		}
		else
		{
			sampleNo++;

			foreach (var parameter in model.ParameterList)
			{
				parameter.ActualValue = "";
				parameter.Remarks = "";
			}

			var sampleDetails = model.QCOrderDetails.SampleDetails.SampleDetailList.Where(x => x.SampleNo == sampleNo).FirstOrDefault();
			if (sampleDetails.ParameterDetailList.Count > 0)
			{
				foreach (var parameter in model.ParameterList)
				{
					var sample = sampleDetails.ParameterDetailList.Where(x => x.ParameterNo == parameter.ParameterNo).FirstOrDefault();

					parameter.ActualValue = sample?.ActualValue ?? "";
					parameter.Remarks = sample?.Remarks ?? "";
				}
			}
		}

		StateHasChanged();
		await Task.Delay(1000);
		return new { Success = true, PreviousPage = PreviousPage };
	}

	public async Task InspectionPlanChange(ChangeEventArgs args)
	{
		model.QCOrderDetails.InspectionPlanType = args.Value.ToString();

		//Inspection Plan
		var inspectionPlan = model.InspectionPlanList.Where(x => x.ItemCode == model.QCOrderDetails.ItemCode && x.PlanType == model.QCOrderDetails.InspectionPlanType).FirstOrDefault();
		model.QCOrderDetails.InspectionPlanCode = inspectionPlan?.InspectionPlanCode ?? "";
		model.QCOrderDetails.InspectionPlanName = inspectionPlan?.InspectionPlanName ?? "";
		model.QCOrderDetails.SamplePassTolerancePercentage = inspectionPlan?.SamplePassTolerancePercentage ?? "";
		model.QCOrderDetails.OverallPassTolerancePercentage = inspectionPlan?.OverallPassTolerancePercentage ?? "";

		//GET SAMPLE DETAILS
		model.QCOrderDetails.SampleDetails = new Sample();
		model.QCOrderDetails.SampleDetails.SampleDetailList = new List<QCOrderViewModel.SampleDetail>();
		for (int count = 1; count <= Convert.ToInt32(model.SalesOrderDetails.SampleSize); count++)
		{
			model.QCOrderDetails.SampleDetails.SampleDetailList.Add(new QCOrderViewModel.SampleDetail { SampleNo = count, Status = "Pending", NoOfFailed = 0, NoOfPassed = 0, QABy = "QA1" });
		}

		//GET INSPECTION PLAN PARAMETERS
		model.ParameterList = _qcOrder.GetParameters(inspectionPlan?.InspectionPlanCode ?? "", inspectionPlan?.Version ?? "") ?? new List<ParameterDetail>();

		foreach (var param in model.ParameterList)
		{
			param.MinValue = GetActualValue(param.MinValue);
			param.MaxValue = GetActualValue(param.MaxValue);

			param.ParameterNo = (model.ParameterList.ToList().IndexOf(param) + 1);
		}
	}

	[JSInvokable("SaveQCOrder")]
	public async Task SaveQCOrder()
	{
		if(model.QCOrderDetails.SampleDetails.Open > 0)
		{
            await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", "Please perform quality control on all samples.");
        }

		try
		{
			if (model.QCOrderDetails.QCOrderNo != "")
			{
				//UPDATING
				if (await _qcOrder.PatchQCOrder(model.QCOrderDetails))
				{
					if ((model.QCOrderDetails.InspectionPlanType.ToLower().Contains("dosimetry") || model.QCOrderDetails.InspectionPlanType.ToLower().Contains("dosimeter") || model.QCOrderDetails.InspectionPlanType.ToLower().Contains("dose")) && model.QCOrderDetails.Status.ToLower() == "passed")
					{
						////Update Sales Order Status and Batch Status in SAP to "Irradiated - In Storage - For COI Approval"
						//await _qcOrder.UpdateSOStatus(model.QCOrderDetails.DocNo);

						//UPDATE COI
						COI = await _qcOrder.PostCOI("Update", model.QCOrderDetails);

						HeaderQCOrderStatus = model.QCOrderDetails.Status;
						HeaderQCPlanType = model.QCOrderDetails.InspectionPlanType;

						if (await _jSRuntime.InvokeAsync<bool>("confirm", "QC Order Saved. View COI?", "View", "Return"))
						{
							await Print();
						}
					}

					_hubConnection.UpdateSalesOrder(model.QCOrderDetails.DocNo);

					await _jSRuntime.InvokeVoidAsync("ShowResult", "Success", "Updated Succesfully");
					await Task.Delay(1000);
					_navManager.NavigateTo("/QCOrder", true);

					return; //End of Function
				}
			}
			else
			{
				//SAVING
				(bool res, string QCNumber) = await _qcOrder.SaveQCOrder(model.QCOrderDetails);
				if (res)
				{
					if ((model.QCOrderDetails.InspectionPlanType.ToLower().Contains("dosimetry") || model.QCOrderDetails.InspectionPlanType.ToLower().Contains("dosimeter") || model.QCOrderDetails.InspectionPlanType.ToLower().Contains("dose")) && model.QCOrderDetails.Status.ToLower() == "passed")
					{
						////Update Sales Order Status and Batch Status in SAP to "Irradiated - In Storage - For COI Approval"
						//await _qcOrder.UpdateSOStatus(model.QCOrderDetails.DocNo);

						model.QCOrderDetails.QCOrderNo = QCNumber;

						//SAVE COI
						COI = await _qcOrder.PostCOI("Add", model.QCOrderDetails);

						HeaderQCOrderStatus = model.QCOrderDetails.Status;
						HeaderQCPlanType = model.QCOrderDetails.InspectionPlanType;

						if (await _jSRuntime.InvokeAsync<bool>("confirm", "QC Order Saved. View COI?", "View", "Return"))
						{
							await Print();
						}
					}

					_hubConnection.UpdateSalesOrder(model.QCOrderDetails.DocNo);

					await _jSRuntime.InvokeVoidAsync("ShowResult", "Success", "Saved Succesfully");
					await Task.Delay(1000);
					_navManager.NavigateTo("/QCOrder", true);
					return; //End of Function
				}
			}
			await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", "Saving Failed. Please contact your administrator.");
		}
		catch (Exception ex)
		{
			await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", ex.Message);
		}
	}

	public async Task SelectQCOrder(string QCOrderNo)
	{
		model.QCOrderDetails = await _qcOrder.SelectQCOrder(QCOrderNo);

		//Inspection Plan
		var inspectionPlan = model.InspectionPlanList.Where(x => x.ItemCode == model.QCOrderDetails.ItemCode && x.PlanType == model.QCOrderDetails.InspectionPlanType).FirstOrDefault();

		//GET INSPECTION PLAN PARAMETERS
		model.ParameterList = _qcOrder.GetParameters(inspectionPlan?.InspectionPlanCode ?? "", inspectionPlan?.Version ?? "") ?? new List<ParameterDetail>();

		foreach (var param in model.ParameterList)
		{
			param.MinValue = GetActualValue(param.MinValue);
			param.MaxValue = GetActualValue(param.MaxValue);

			param.ParameterNo = (model.ParameterList.ToList().IndexOf(param) + 1);
		}

		//Set FilteredPlanType depending on SO
		var inspectionPlanList = model.InspectionPlanList.Where(x => x.ItemCode == model.QCOrderDetails.ItemCode).DistinctBy(x => x.PlanType)
			.Select(x => new
			{
				PlanType = x.PlanType
			})
			.ToList();

		FilteredPlanType = new List<string>();

		foreach (var plan in inspectionPlanList)
		{
			FilteredPlanType.Add(plan.PlanType);
		}

		HeaderQCOrderNo = model.QCOrderDetails.QCOrderNo;
		HeaderQCOrderStatus = model.QCOrderDetails.Status;
		HeaderQCPlanType = model.QCOrderDetails.InspectionPlanType;
		CanCOI = model.SalesOrderList.Where(x => x.SONo == model.QCOrderDetails.DocNo && x.U_SOStatus.ToLower().Contains("irradiated")).Any();

		if (HeaderQCPlanType.ToLower().Contains("dosimetry") && HeaderQCOrderStatus == "Passed")
		{
			COI = _qcOrder.GetCertificateOfIrradiation(HeaderQCOrderNo);
		}

		await _jSRuntime.InvokeVoidAsync("HideModal");
	}

	public async Task<bool> SelectQCOrder(string ItemCode, string PlanType)
	{
		var qcOrderDetails = await _qcOrder.SelectQCOrder(ItemCode, PlanType);

		if (qcOrderDetails != null)
		{
			model.QCOrderDetails = await _qcOrder.SelectQCOrder(ItemCode, PlanType);

			//Inspection Plan
			var inspectionPlan = model.InspectionPlanList.Where(x => x.ItemCode == model.QCOrderDetails.ItemCode && x.PlanType == model.QCOrderDetails.InspectionPlanType).FirstOrDefault();

			//GET INSPECTION PLAN PARAMETERS
			model.ParameterList = _qcOrder.GetParameters(inspectionPlan?.InspectionPlanCode ?? "", inspectionPlan?.Version ?? "") ?? new List<ParameterDetail>();

			foreach (var param in model.ParameterList)
			{
				param.MinValue = GetActualValue(param.MinValue);
				param.MaxValue = GetActualValue(param.MaxValue);

				param.ParameterNo = (model.ParameterList.ToList().IndexOf(param) + 1);
			}

			//Set FilteredPlanType depending on SO
			var inspectionPlanList = model.InspectionPlanList.Where(x => x.ItemCode == model.QCOrderDetails.ItemCode).DistinctBy(x => x.PlanType)
				.Select(x => new
				{
					PlanType = x.PlanType
				})
				.ToList();

			FilteredPlanType = new List<string>();

			foreach (var plan in inspectionPlanList)
			{
				FilteredPlanType.Add(plan.PlanType);
			}

			HeaderQCOrderNo = model.QCOrderDetails.QCOrderNo;
			HeaderQCOrderStatus = model.QCOrderDetails.Status;
			HeaderQCPlanType = model.QCOrderDetails.InspectionPlanType;
			CanCOI = model.SalesOrderList.Where(x => x.SONo == model.QCOrderDetails.DocNo && x.U_SOStatus.ToLower().Contains("irradiated")).Any();

			if (HeaderQCPlanType.ToLower().Contains("dosimetry") && HeaderQCOrderStatus == "Passed")
			{
				COI = _qcOrder.GetCertificateOfIrradiation(HeaderQCOrderNo);
			}

			return true;
		}

		return false;
	}

	public async Task ViewCOI()
	{
		_navManager.NavigateTo($"/CertificateOfIrradiationDetails/{model.QCOrderDetails.QCOrderNo}");
	}

	public async Task Print()
	{
		try
		{
			List<string> Headers = new List<string>
			{
				$@"""CertificateNo""",
				$@"""DocNo""",
				$@"""CustomerName""",
				$@"""CustomerPONo""",
				$@"""ItemName""",
				$@"""ManufacturingLotNo""",
				$@"""TotalNoBoxes""",
				$@"""FacilityName""",
				$@"""IrradiationDate""",
				$@"""RequiredDose""",
				$@"""ActualDose""",
				$@"""Signature""",
				$@"""Name""",
				$@"""JobTitle""",
				$@"""IssuedDate"""
			};

			//Formulate string value for RequiredDose
			string RequiredDose = "";
			if (Convert.ToDouble(COI.MinValue) != Convert.ToDouble(COI.MaxValue))
			{
				RequiredDose = $@"""{COI.MinValue} to {COI.MaxValue} kGy""";
			}
			else
			{
				RequiredDose = $@"""{COI.MinValue} kGy""";
			}

			string FacilityName = COI.FacilityName == "" ? "ISI Tanay E-beam Irradiation Facility" : COI.FacilityName;

			List<string> Arguements = new List<string>
			{
				$@"""{COI.CertificateOfIrradiationNumber}""",
				$@"""{COI.DocNo}""",
				$@"""{COI.CustomerName}""",
				$@"""{COI.CustomerPONo}""",
				$@"""{COI.ItemName}""",
				$@"""{COI.ManufacturingLotNo}""",
				$@"""{model.QCOrderDetails.Quantity}""",
				//$@"""{COI.TotalNoOfBoxes}""",
				$@"""{FacilityName}""",
				$@"""{COI.IrradiationDate.ToString("MM-dd-yyyy")}""",
				RequiredDose,
				$@"""{COI.ActualValue}""",
				$@"""{COI.ApproverName}""",
				$@"""{COI.ApproverName}""",
				$@"""{COI.ApproverJobTitle}""",
				$@"""{COI.ApproverIssuedDate?.ToString("MM-dd-yyyy")}"""
			};

			string Header = string.Join(", ", Headers);
			string args = $"nextLine{string.Join(", ", Arguements)}";
			string FilePath = ($"{_environment.WebRootPath}/PRINT_LAYOUT/CertificateOfIrradiation.rpt").Replace("\\", "/");
			string Database = ($"{_environment.WebRootPath}/PRINT_LAYOUT/CertificateOfIrradiation.txt").Replace("\\", "/");

			string endpoint = _configuration["PrinterAPI"]?.ToString() ?? "";
			string url = $"{endpoint}/api/Print?Header={Header}&args={args}&PrinterName={string.Empty}&FilePath={FilePath}&Database={Database}";

			// JavaScript to open the report in a new window
			string script = "var newWindow = window.open(`" + url + "`, '_blank');";
			script += "newWindow.focus();";

			// Execute the JavaScript
			await _jSRuntime.InvokeVoidAsync("eval", script);
		}
		catch (Exception ex)
		{
			await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", ex.Message);
		}
	}

	void SearchQCOrders(string SearchValue)
	{
		if (SearchValue != "")
		{
			SearchValue = SearchValue.ToLower();
			filteredQCOrders = model.QCOrderList.Where(
				x =>
					x.QCOrderNo.ToLower().Contains(SearchValue) ||
					x.InspectionPlanType.ToLower().Contains(SearchValue) ||
					x.CustomerName.ToLower().Contains(SearchValue) ||
					x.ItemName.ToLower().Contains(SearchValue)
			).ToList();
		}
		else
		{
			filteredQCOrders = model.QCOrderList;
		}
	}

	void SearchSOGenerate(string SearchValue)
	{
		if (SearchValue != "")
		{
			SearchValue = SearchValue.ToLower();
			filteredSOGenerate = model.SalesOrderList.Where(
				x =>
					x.SONo.ToString().ToLower().Contains(SearchValue) ||
					x.CustomerName.ToLower().Contains(SearchValue) ||
					x.ItemName.ToLower().Contains(SearchValue)
			).ToList();
		}
		else
		{
			filteredSOGenerate = model.SalesOrderList;
		}
	}

	void SearchSO(string SearchValue)
	{
		if (SearchValue != "")
		{
			SearchValue = SearchValue.ToLower();
			filteredSO = model.SalesOrderList.Where(
				x =>
					x.SONo.ToString().ToLower().Contains(SearchValue) ||
					x.CustomerName.ToLower().Contains(SearchValue) ||
					x.ItemName.ToLower().Contains(SearchValue)
			).ToList();
		}
		else
		{
			filteredSO = model.SalesOrderList;
		}
	}
	void SearchDosimetryType(string SearchValue)
	{
		if (SearchValue != "")
		{
			SearchValue = SearchValue.ToLower();
			filteredDosimetry = model.DosimetryList.Where(
				x =>
					x.Code.ToString().ToLower().Contains(SearchValue) ||
					x.Name.ToLower().Contains(SearchValue) 
			).ToList();
		}
		else
		{
			filteredDosimetry = model.DosimetryList;
		}
	}
	//void OnCellContextMenu(DataGridCellMouseEventArgs<QCOrderViewModel.SampleDetail> args)
	//{
	//	string eme = "";

	//	selectedSamples = new List<QCOrderViewModel.SampleDetail>() { args.Data };

	//	ContextMenuService.Open(args,
	//		new List<ContextMenuItem> {
	//			new ContextMenuItem(){ Text = "Context menu item 1", Value = 1, Icon = "home" },
	//			new ContextMenuItem(){ Text = "Context menu item 2", Value = 2, Icon = "search" }
	//		},
	//	(e) => {
	//		//eme = $"Menu item with Value={e.Value} clicked. Column: {args.Column.Property}, EmployeeID: {args}";
	//		}
	//	);
	//}
}
