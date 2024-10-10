using BlazorStrap.Utilities;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;

namespace Application.BlazorServer.Pages.QCMaintenance;

public partial class QCMaintenance : ComponentBase
{
	[Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;
	[Inject] protected IQCMaintenanceService _qcMaintenance { get; set; }
	[Inject] private NavigationManager _navManager { get; set; }

	private IJSObjectReference _js { get; set; } = default!;

	dynamic Breadcrumbs = new dynamic[]

	{
		"QC Maintenance"
	};

	private int UoMCount = 1;
	private bool isLoading = false;
	private ushort _newRowsCount { get; set; } = 1;
	//private ushort newRowsCount { get => _newRowsCount; set => _newRowsCount = (ushort)(value <= 0 ? 1 : value > 50 ? 50 : value); }
	private ushort newRowsCount { get; set; } = 1;
	private bool Duplicate = false;

	RadzenDataGrid<Parameters>? parameterList = new RadzenDataGrid<Parameters>();

	QCMaintenanceViewModel model = new QCMaintenanceViewModel();

	List<Customers> filteredCustomers = new List<Customers>();
	List<Item> filteredItems = new List<Item>();
	List<Employees> filteredEmployees = new List<Employees>();
	List<InspectionPlans> filteredInspectionplans = new List<InspectionPlans>();

	private string inspectionplanSearchValue { get; set; } = string.Empty;
	private string customerSearchValue { get; set; } = string.Empty;
	private string itemSearchValue { get; set; } = string.Empty;
	private string employeeSearchValue { get; set; } = string.Empty;

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			_js = await _jSRuntime.InvokeAsync<IJSObjectReference>("import", "../scripts/CustomScripts/QCMaintenance.js");

			await _js.InvokeVoidAsync("InitializeValidator");

			//For Calling of backend function from JS
			await _js.InvokeVoidAsync("setdotnetInstance", DotNetObjectReference.Create(this));
		}
	}

	protected override void OnInitialized()
	{
		try
		{
			model = _qcMaintenance.InitializeQCMaintenance();
			model.VersionList.Add(new QCMaintenanceViewModel.Version { VersionNumber = "1" });
			model.QcMaintenance.Version = "1";
			filteredCustomers = model.CustomerList;
			filteredItems = model.ItemList;
			filteredEmployees = model.EmployeeList;
			filteredInspectionplans = model.InspectionPlanList;

			//Change Default Parameters based on Plan Type
			ChangeDefaultParameters();
		}
		catch (Exception)
		{
			throw;
		}
	}

	public void SelectInput(string InputName, string value)
	{
		model.QcMaintenance.GetType().GetProperty(InputName.Replace(" ", "")).SetValue(model.QcMaintenance, value);
		if (InputName == "Inspection Plan Code")
		{
			model.QcMaintenance = _qcMaintenance.GetInspectionPlan(value);
			model.QcMaintenance.Date = Convert.ToDateTime(model.QcMaintenance.Date).ToString("yyyy-MM-dd");
			UoMCount = model.QcMaintenance.ParameterList.Count + 1;
			model.VersionList = _qcMaintenance.GetVersions(value);
			Duplicate = true;

		}
		if (InputName == "Customer Code")
		{
			model.QcMaintenance.GetType().GetProperty("CustomerName").SetValue(model.QcMaintenance, model.CustomerList.Where(x => x.CardCode == value).FirstOrDefault()?.CardName ?? "");
		}

		if (InputName == "Item Code")
		{
			var inspectionPlan = _qcMaintenance.GetInspectionPlan(model.QcMaintenance.ItemCode, model.QcMaintenance.PlanType);
			if (inspectionPlan != null)
			{
				model.QcMaintenance = inspectionPlan;
				model.QcMaintenance.Date = Convert.ToDateTime(model.QcMaintenance.Date).ToString("yyyy-MM-dd");
				UoMCount = model.QcMaintenance.ParameterList.Count + 1;
				model.VersionList = _qcMaintenance.GetVersions(model.QcMaintenance.InspectionPlanCode);
				Duplicate = true;
				_jSRuntime.InvokeVoidAsync("HideModal");
				return;
			}
			model.QcMaintenance.InspectionPlanCode = "";
			model.VersionList = new List<QCMaintenanceViewModel.Version>();
			model.VersionList.Add(new QCMaintenanceViewModel.Version { VersionNumber = "1" });
			model.QcMaintenance.Version = "1";
			Duplicate = false;
			model.QcMaintenance.GetType().GetProperty("ItemName").SetValue(model.QcMaintenance, model.ItemList.Where(x => x.ItemCode == value).FirstOrDefault()?.ItemName ?? "");
		}
		_jSRuntime.InvokeVoidAsync("HideModal");
	}
	public async Task DeleteRow(Parameters data)
	{
		if (model.QcMaintenance.ParameterList.Where(x => x.Selected == true).Any())
		{
			foreach (var param in model.QcMaintenance.ParameterList.Where(x => x.Selected == true).ToList())
			{
				model.QcMaintenance.ParameterList.Remove(param);
			}
		}
		else
		{
			model.QcMaintenance.ParameterList.Remove(data);
		}
		await parameterList.Reload();
		UpdateTotalWeight();
	}

	public async Task SetActive(ChangeEventArgs args, Parameters data)
	{
		data.Active = Convert.ToBoolean(args.Value);
		UpdateTotalWeight();
	}


	public async Task AddParameterRow()
	{
		isLoading = true;

		for (ushort i = 0; i < newRowsCount; i++)
		{
			try
			{
				var parameter = new Parameters();
				parameter.SelectId = $"select-uom-{UoMCount}";
				parameter.Version = model.QcMaintenance.Version;
				parameter.InspectionPlanCode = model.QcMaintenance.InspectionPlanCode;
				parameter.InspectionPlanName = model.QcMaintenance.InspectionPlanName;
				model.QcMaintenance.ParameterList.Add(parameter);

				await parameterList.Reload();

				//StateHasChanged();

				await Task.Delay(100);

				//await _js.InvokeVoidAsync("select2Dropdown", parameter.SelectId);

				UoMCount++;
			}
			catch (Exception)
			{
				isLoading = false;
				throw;
			}
		}

		isLoading = false;
	}

	public async Task ChangeParameterType(ChangeEventArgs args, Parameters data)
	{
		data.ParameterType = args.Value.ToString();

		if (data.ParameterType == "Quantitative")
		{
			data.MinValue = "";
			data.MaxValue = "";
			data.TargetValue = "";
		}
		else if (data.ParameterType == "Qualitative")
		{
			data.MinValue = "Y";
			data.MaxValue = "Y";
			data.TargetValue = "Yes";
		}
	}

	public async Task ChangeMinMaxValue(ChangeEventArgs args, Parameters data, string Type)
	{
		if (Type == "MinValue")
		{
			data.MinValue = args.Value.ToString();
		}
		else if (Type == "MaxValue")
		{
			data.MaxValue = args.Value.ToString();
		}

		if (data.ParameterType == "Qualitative")
		{
			data.MinValue = args.Value.ToString();
			data.MaxValue = args.Value.ToString();

			if (args.Value.ToString() == "Y")
			{
				data.TargetValue = "Yes";
			}
			else if (args.Value.ToString() == "P")
			{
				data.TargetValue = "Passed";
			}
		}
	}

	public async Task ChangeWeightValue(ChangeEventArgs args, Parameters data)
	{
		try
		{
			float newWeight = float.TryParse(args.Value.ToString(), out float newParsedWeight) ? newParsedWeight : 0;

			if (data.Weight != newWeight) // Only update if the weight has changed
			{
				data.Weight = newWeight;
				UpdateTotalWeight();
			}
		}
		catch (Exception)
		{
			throw;
		}
	}
	private void UpdateTotalWeight()
	{
		model.QcMaintenance.TotalWeight = model.QcMaintenance.ParameterList.Where(x => x.Active != false).Sum(x => x.Weight);
	}

	[JSInvokable("ClearData")]
	public async Task ClearData()
	{
		_navManager.NavigateTo("QCMaintenance", true);

		await Task.Delay(1000);
	}

	[JSInvokable("SaveQCMaintenance")]
	public async Task SaveQCMaintenance()
	{
		try
		{
			if (_qcMaintenance.SaveQCMaintenance(model))
			{
				await _jSRuntime.InvokeVoidAsync("ShowResult", "Success", "Saved Succesfully");
				await Task.Delay(1000);
				_navManager.NavigateTo("QCMaintenance", true);
				return; //End of Function
			}
			await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", "Saving Failed. Please contact your administrator.");
		}
		catch (Exception ex)
		{
			await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", ex.Message);
		}
	}

	public void ToggleSelectAllRow(bool args)
	{
		foreach (var param in model.QcMaintenance.ParameterList)
		{
			param.Selected = args;
		}
	}

	public void ToggleRowSelection(bool args, Parameters param)
	{
		model.QcMaintenance.ParameterList.Where(x => x.SelectId == param.SelectId).FirstOrDefault().Selected = args;
	}

	public async Task ChangeVersion(string Version)
	{
		model.QcMaintenance = _qcMaintenance.GetInspectionPlanWithVersion(model.QcMaintenance.InspectionPlanCode, Version);
		UoMCount = model.QcMaintenance.ParameterList.Count + 1;
	}

	public async Task DuplicateInspectionPlan()
	{
		model.VersionList = new List<QCMaintenanceViewModel.Version>
		{
			new QCMaintenanceViewModel.Version { VersionNumber = "1" }
		};
		model.QcMaintenance.Version = "1";
		model.QcMaintenance.InspectionPlanCode = "";
		model.QcMaintenance.InspectionPlanName = "";
		model.QcMaintenance.Date = DateTime.Today.ToString("yyyy-MM-dd");
		model.QcMaintenance.Section = "";
		model.QcMaintenance.Approver = "";
		model.QcMaintenance.ItemCode = "";
		model.QcMaintenance.ItemName = "";

		Duplicate = false;
	}

	public async Task PlanTypeChange(ChangeEventArgs args)
	{
		//If Inspection plan for that item with certain plan type is existing, fetch said data
		var inspectionPlan = _qcMaintenance.GetInspectionPlan(model.QcMaintenance.ItemCode, args.Value.ToString());
		if (inspectionPlan != null)
		{
			model.QcMaintenance = inspectionPlan;
			model.QcMaintenance.Date = Convert.ToDateTime(model.QcMaintenance.Date).ToString("yyyy-MM-dd");
			UoMCount = model.QcMaintenance.ParameterList.Count + 1;
			model.VersionList = _qcMaintenance.GetVersions(model.QcMaintenance.InspectionPlanCode);
			Duplicate = true;
			model.QcMaintenance.ParameterList = inspectionPlan.ParameterList;
			return;
		}

		model.QcMaintenance.InspectionPlanCode = "";
		model.VersionList = new List<QCMaintenanceViewModel.Version>
		{
			new QCMaintenanceViewModel.Version { VersionNumber = "1" }
		};
		model.QcMaintenance.Version = "1";
		Duplicate = false;
		model.QcMaintenance.PlanType = args.Value.ToString();
		model.QcMaintenance.ParameterList = new List<Parameters>();

		//Change Default Parameters based on Plan Type
		ChangeDefaultParameters();
	}

	public void ChangeDefaultParameters()
	{
		//Clear Params
		var defaultParams = model.QcMaintenance.ParameterList.Where(x => x.DefaultParameter == true).ToList();

		foreach(var param in defaultParams) 
		{
			model.QcMaintenance.ParameterList.Remove(param);
		}

		//Add New Params
		var newParams = model.DefaultParameterList.Where(x => x.PlanType == model.QcMaintenance.PlanType).ToList();

		foreach (var param in newParams)
		{
			param.DefaultParameter = true;
			model.QcMaintenance.ParameterList.Add(param);
		}

		//Reorder ParameterList
		model.QcMaintenance.ParameterList = model.QcMaintenance.ParameterList.OrderByDescending(x => x.DefaultParameter).ToList();
	}

	public async Task CheckValue(ChangeEventArgs args, Parameters param, string type)
	{

		switch (type)
		{
			case "Min":
				#region Min
				if (string.IsNullOrEmpty(args.Value.ToString()))
				{
					param.MinValue = args.Value.ToString();
					return;
				}

				//Min-Max Only
				if (!string.IsNullOrEmpty(param.MaxValue) && string.IsNullOrEmpty(param.TargetValue))
				{
					if (!MinMax(args.Value.ToString(), param.MaxValue))
					{
						await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", "Minimum value must be less than or equal to maximum value.");
						param.MinValue = param.MaxValue;
						return;
					}
				}
				//Min-Target Only
				else if (string.IsNullOrEmpty(param.MaxValue) && !string.IsNullOrEmpty(param.TargetValue))
				{
					if (!MinTarget(args.Value.ToString(), param.TargetValue))
					{
						await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", "Minimum value must be more than or equal to target value.");
						param.MinValue = param.TargetValue;
						return;
					}
				}
				//Min-Max-Target
				else if (!string.IsNullOrEmpty(param.MaxValue) && !string.IsNullOrEmpty(param.TargetValue))
				{
					if (!MinMaxTarget(args.Value.ToString(), param.MaxValue, param.TargetValue))
					{
						await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", "Target value must be equal or between the minimum and maximum value.");
						param.TargetValue = (!MinTarget(param.MinValue, args.Value.ToString())) ? param.MinValue : param.MaxValue;
						return;
					}
				}
				param.MinValue = args.Value.ToString();
				break;
			#endregion

			case "Max":
				#region Max
				if (string.IsNullOrEmpty(args.Value.ToString()))
				{
					param.MaxValue = args.Value.ToString();
					return;
				}

				//Min-Max Only
				if (!string.IsNullOrEmpty(param.MinValue) && string.IsNullOrEmpty(param.TargetValue))
				{
					if (!MinMax(param.MinValue, args.Value.ToString()))
					{
						await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", "Maximum value must be more than or equal to minimum value.");
						param.MaxValue = param.MinValue;
						return;
					}
				}
				//Max-Target Only
				else if (string.IsNullOrEmpty(param.MinValue) && !string.IsNullOrEmpty(param.TargetValue))
				{
					if (!MaxTarget(args.Value.ToString(), param.TargetValue))
					{
						await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", "Maximum value must be less than or equal to target value.");
						param.MaxValue = param.TargetValue;
						return;
					}
				}
				//Min-Max-Target
				else if (!string.IsNullOrEmpty(param.MinValue) && !string.IsNullOrEmpty(param.TargetValue))
				{
					if (!MinMaxTarget(param.MinValue, args.Value.ToString(), param.TargetValue))
					{
						await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", "Target value must be equal or between the minimum and maximum value.");
						param.TargetValue = (!MinTarget(param.MinValue, args.Value.ToString())) ? param.MinValue : param.MaxValue;
						return;
					}
				}
				param.MaxValue = args.Value.ToString();
				break;
			#endregion

			case "Target":
				#region Target
				if (string.IsNullOrEmpty(args.Value.ToString()))
				{
					param.TargetValue = args.Value.ToString();
					return;
				}

				//Min-Target Only
				if (!string.IsNullOrEmpty(param.MinValue) && string.IsNullOrEmpty(param.MaxValue))
				{
					if (!MinTarget(param.MinValue, args.Value.ToString()))
					{
						await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", "Minimum value must be less than or equal to target value.");
						param.TargetValue = param.MinValue;
						return;
					}
				}
				//Max-Target Only
				else if (string.IsNullOrEmpty(param.MinValue) && !string.IsNullOrEmpty(param.MaxValue))
				{
					if (!MaxTarget(param.MaxValue, args.Value.ToString()))
					{
						await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", "Maximum value must be less than or equal to target value.");
						param.TargetValue = param.MaxValue;
						return;
					}
				}
				//Min-Max-Target
				else if (!string.IsNullOrEmpty(param.MinValue) && !string.IsNullOrEmpty(param.MaxValue))
				{
					if (!MinMaxTarget(param.MinValue, param.MaxValue, args.Value.ToString()))
					{
						await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", "Target value must be equal or between the minimum and maximum value.");
						param.TargetValue = (!MinTarget(param.MinValue, args.Value.ToString())) ? param.MinValue : param.MaxValue;
						return;
					}
				}
				param.TargetValue = args.Value.ToString();
				break;
			#endregion

			default: break;
		}

		static bool MinMax(string min, string max)
		{
			double minVal = Convert.ToDouble(min);
			double maxVal = Convert.ToDouble(max);

			if (maxVal < 0 && minVal < 0)
			{
				maxVal = Math.Abs(maxVal);
				minVal = Math.Abs(minVal);
			}

			return minVal <= maxVal;
		}

		static bool MaxTarget(string max, string target)
		{
			double maxVal = Convert.ToDouble(max);
			double targetVal = Convert.ToDouble(target);

			if (maxVal < 0 && targetVal < 0)
			{
				maxVal = Math.Abs(maxVal);
				targetVal = Math.Abs(targetVal);
			}

			return maxVal >= targetVal;
		}

		static bool MinTarget(string min, string target)
		{
			double minVal = Convert.ToDouble(min);
			double targetVal = Convert.ToDouble(target);

			if (minVal < 0 && targetVal < 0)
			{
				minVal = Math.Abs(minVal);
				targetVal = Math.Abs(targetVal);
			}

			return minVal <= targetVal;
		}

		static bool MinMaxTarget(string min, string max, string target)
		{
			double minVal = Convert.ToDouble(min);
			double maxVal = Convert.ToDouble(max);
			double targetVal = Convert.ToDouble(target);

			if(minVal < 0 && maxVal < 0 && targetVal < 0)
			{
				minVal = Math.Abs(minVal);
				maxVal = Math.Abs(maxVal);
				targetVal = Math.Abs(targetVal);
			}

			return (minVal <= targetVal && targetVal <= maxVal);
		}
	}

	void SearchEmployees(string SearchValue)
	{
		if (SearchValue != "")
		{
			SearchValue = SearchValue.ToLower();
			filteredEmployees = model.EmployeeList.Where(
				x =>
					x.EmployeeName.ToLower().Contains(SearchValue) ||
					x.EmployeeCode.ToLower().Contains(SearchValue)
			).ToList();
		}
		else
		{
			filteredEmployees = model.EmployeeList;
		}
	}

	void SearchItems(string SearchValue)
	{
		if (SearchValue != "")
		{
			SearchValue = SearchValue.ToLower();
			filteredItems = model.ItemList.Where(
				x =>
					x.ItemName.ToLower().Contains(SearchValue) ||
					x.ItemCode.ToLower().Contains(SearchValue)
			).ToList();
		}
		else
		{
			filteredItems = model.ItemList;
		}
	}

	void SearchCustomers(string SearchValue)
	{
		if (SearchValue != "")
		{
			SearchValue = SearchValue.ToLower();
			filteredCustomers = model.CustomerList.Where(
				x =>
					x.CardName.ToLower().Contains(SearchValue) ||
					x.CardCode.ToLower().Contains(SearchValue)
			).ToList();
		}
		else
		{
			filteredCustomers = model.CustomerList;
		}
	}

	void SearchInspectionPlans(string SearchValue)
	{
		if (SearchValue != "")
		{
			SearchValue = SearchValue.ToLower();
			filteredInspectionplans = model.InspectionPlanList.Where(
				x =>
					x.InspectionPlanCode.ToLower().Contains(SearchValue) ||
					x.InspectionPlanName.ToLower().Contains(SearchValue) ||
					x.CustomerName.ToLower().Contains(SearchValue) ||
					x.ItemName.ToLower().Contains(SearchValue)
			).ToList();
		}
		else
		{
			filteredInspectionplans = model.InspectionPlanList;
		}
	}

	void LimitNumberToHundredSample(string val)
	{
		if(val == "")
		{
			val = "0";
		}

		if(Convert.ToInt64(val) > 100)
		{
			val = "100";
		}

		model.QcMaintenance.SamplePassTolerancePercentage = val;
		StateHasChanged();

		_jSRuntime.InvokeVoidAsync("eval", $"$('#sample-tolerance').val({val})");
	}

	void LimitNumberToHundredOverall(string val)
	{
		if (val == "")
		{
			val = "0";
		}

		if (Convert.ToInt64(val) > 100)
		{
			val = "100";
		}

		model.QcMaintenance.OverallPassTolerancePercentage = val;
		StateHasChanged();

		_jSRuntime.InvokeVoidAsync("eval", $"$('#overall-tolerance').val({val})");
	}
}
