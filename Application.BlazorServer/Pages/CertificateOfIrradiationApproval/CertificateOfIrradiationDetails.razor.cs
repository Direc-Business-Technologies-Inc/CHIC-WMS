using System.Linq;
using System.Reflection.PortableExecutable;
using static Application.Models.ViewModels.FormsAndReportsViewModel.IrradiationSalesOrderDetailsViewModel;

namespace Application.BlazorServer.Pages.CertificateOfIrradiationApproval;

public partial class CertificateOfIrradiationDetails : ComponentBase
{
	[Parameter]
	public string SONo { get; set; } = "";
	[Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;
	[Inject] protected ICertificateOfIrradiationService _qcOrder { get; set; }
	[Inject] private NavigationManager _navManager { get; set; }
	[Inject] private IConfiguration _configuration { get; set; }
	[Inject] private IWebHostEnvironment _environment { get; set; }
	private IJSObjectReference _js { get; set; } = default!;
	//[Inject] ContextMenuService ContextMenuService { get; set; }

	dynamic Breadcrumbs = new dynamic[]
	{
		"Certificate of Irradiation",
		"Certificate of Irradiation Details"
	};

	CertificateOfIrradiationViewModel model = new CertificateOfIrradiationViewModel();

	int sampleNo = 0;

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			_js = await _jSRuntime.InvokeAsync<IJSObjectReference>("import", "../scripts/CustomScripts/CertificateOfIrradiationDetails.js");

			//For Calling of backend function from JS
			await _js.InvokeVoidAsync("setdotnetInstance", DotNetObjectReference.Create(this));

			await _js.InvokeVoidAsync("InitializeStepper");
		}
	}

	protected override async Task OnInitializedAsync()
	{
		try
		{
			//GET INSPECTION PLAN LIST
			model = await _qcOrder.InitializeCertificateOfIrradiationDetails();

			await SelectQCOrder(SONo);

			//string QCOrderNo = model.Where(x => x.)

			//model.QCOrderDetails = await _qcOrder.SelectQCOrder(QCOrderNo);
		}
		catch (Exception)
		{

			throw;
		}
	}

	void CellRenderSampleList(DataGridCellRenderEventArgs<CertificateOfIrradiationViewModel.SampleDetail> args)
	{
		if (args.Column.Title == "Status")
		{
			if (args.Data.Status != "Pending")
			{
				args.Attributes.Add("style", $"background-color: {(args.Data.Status == "Passed" ? "var(--rz-success)" : "var(--rz-danger)")}; border-radius: 25px;");
			}
		}
	}
	void CellRender(DataGridCellRenderEventArgs<CertificateOfIrradiationViewModel.ParameterDetail> args)
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

	public void SelectInput(string InputName, string value)
	{

	}

	public async Task SelectSampleDetail(int sampleDetail)
	{
		try
		{
			//bool checker = await _js.InvokeAsync<bool>("checkInspectionPlan");
			//if (!checker)
			//{
			//	await _jSRuntime.InvokeVoidAsync("ShowResult", "Info", "Please input Inspection Plan");
			//	return;
			//}

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

	public async Task SelectQCOrder(string QCOrderNo)
	{
		model.QCOrderDetails = await _qcOrder.SelectQCOrder(QCOrderNo);

		//Inspection Plan
		var inspectionPlan = model.InspectionPlanList.Where(x => x.ItemCode == model.QCOrderDetails.ItemCode && x.PlanType == model.QCOrderDetails.InspectionPlanType).FirstOrDefault();

		//GET INSPECTION PLAN PARAMETERS
		model.ParameterList = _qcOrder.GetParameters(inspectionPlan?.InspectionPlanCode ?? "", inspectionPlan?.Version ?? "") ?? new List<CertificateOfIrradiationViewModel.ParameterDetail>();

		foreach (var param in model.ParameterList)
		{
			param.MinValue = GetActualValue(param.MinValue);
			param.MaxValue = GetActualValue(param.MaxValue);

			param.ParameterNo = (model.ParameterList.ToList().IndexOf(param) + 1);
		}

		//GET COI DATA
		model.COISalesOrder = await _qcOrder.GetCOIDetails(QCOrderNo);

		await _jSRuntime.InvokeVoidAsync("HideModal");
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

	public async Task UpdateCOI(string Status)
	{
		try
		{
			model.COISalesOrder.Status = Status;
			if (await _qcOrder.UpdateCOI(model.COISalesOrder))
			{
				//Update Sales Order Status and Batch Status in SAP to "Irradiated - In Storage - For COI Approval"
				await _qcOrder.UpdateSOStatus(model.COISalesOrder.DocNo);

				await _jSRuntime.InvokeVoidAsync("ShowResult", "Success", "Saved Succesfully");
				await Task.Delay(1000);
				_navManager.NavigateTo("/CertificateOfIrradiationApproval");
				return; //End of Function
			}
			await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", "Saving Failed. Please contact your administrator.");
		}
		catch (Exception ex)
		{
			await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", ex.Message);
		}
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
				$@"""IssuedDate""",
				$@"""DosimetryFilm"""
			};

			//Formulate string value for RequiredDose
			string RequiredDose = "";
			if (Convert.ToDouble(model.COISalesOrder.MinValue) != Convert.ToDouble(model.COISalesOrder.MaxValue))
			{
				RequiredDose = $@"""{model.COISalesOrder.MinValue} to {model.COISalesOrder.MaxValue} kGy""";
			}
			else
			{
				RequiredDose = $@"""{model.COISalesOrder.MinValue} kGy""";
			}

			string FacilityName = model.COISalesOrder.FacilityName == "" ? "ISI Tanay E-beam Irradiation Facility" : model.COISalesOrder.FacilityName;

			List<string> Arguements = new List<string>
			{
				$@"""{model.COISalesOrder.CertificateOfIrradiationNumber}""",
				$@"""{model.COISalesOrder.DocNo}""",
				$@"""{model.COISalesOrder.CustomerName}""",
				$@"""{model.COISalesOrder.CustomerPONo}""",
				$@"""{model.COISalesOrder.ItemName}""",
				$@"""{model.COISalesOrder.ManufacturingLotNo}""",
				$@"""{model.QCOrderDetails.Quantity}""",
				$@"""{FacilityName}""",
				$@"""{model.COISalesOrder.IrradiationDate.ToString("MM-dd-yyyy")}""",
				RequiredDose,
				$@"""{model.COISalesOrder.ActualValue}""",
				$@"""{model.COISalesOrder.ApproverName}""",
				$@"""{model.COISalesOrder.ApproverName}""",
				$@"""{model.COISalesOrder.ApproverJobTitle}""",
				$@"""{model.COISalesOrder.ApproverIssuedDate?.ToString("MM-dd-yyyy")}""",
				$@"""{model.COISalesOrder.DosimetryFilm}"""
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
}
