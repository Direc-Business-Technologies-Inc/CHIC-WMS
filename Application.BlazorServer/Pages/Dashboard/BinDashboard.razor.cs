namespace Application.BlazorServer.Pages.Dashboard;

public partial class BinDashboard : ComponentBase
{
	[Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;
	[Inject] protected IDashboardService _binDashboard { get; set; }
	private IJSObjectReference _js { get; set; } = default!;
	private bool _RenderSelectInputJs { get; set; } = false;

	dynamic Breadcrumbs = new dynamic[]
	{
			"Dashboard",
			"Bin Dashboard"
	};

	DashboardViewModel.BinDashboardViewModel model = new DashboardViewModel.BinDashboardViewModel();

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			_js = await _jSRuntime.InvokeAsync<IJSObjectReference>("import", "../scripts/CustomScripts/BinDashboard.js");

			//For Calling of backend function from JS
			await _js.InvokeVoidAsync("setdotnetInstance", DotNetObjectReference.Create(this));

			await _js.InvokeVoidAsync("initializeBinDashboard");
		}

		if (_RenderSelectInputJs)
		{
            SelectInputJs();

            _RenderSelectInputJs = false;
        }
	}

	protected override void OnInitialized()
	{
		try
		{
			model = _binDashboard.InitalizeBinDashboard().Result;
		}
		catch (Exception)
		{
			//throw;
		}
	}

	public void SelectInput(string InputName, string value)
	{
		string prevValue = "";
		string prevWarehouseNameValue = "";
		if (InputName == "WarehouseCode")
		{
			prevValue = model.BinMappingDetails.WarehouseCode;
			prevWarehouseNameValue = model.BinMappingDetails.WarehouseName;
			model.BinMappingDetails.WarehouseCode = value;
			model.BinMappingDetails.WarehouseName = model.WarehouseList.Where(x => x.WarehouseCode == value).FirstOrDefault().WarehouseName;
		}
		if (InputName == "Shelf")
		{
			prevValue = model.BinMappingDetails.Shelf;
			model.BinMappingDetails.Shelf = value;
		}
		try
		{
			_binDashboard.GetBinMappingDetails(model);
		}
		catch (Exception ex)
		{
			if (ex.Message == "Context does not exist")
			{
				_jSRuntime.InvokeVoidAsync("ShowResult", "Info", $"Warehouse {model.BinMappingDetails.WarehouseCode} and shelf {model.BinMappingDetails.Shelf} is not mapped");

				if(InputName == "WarehouseCode")
				{
					model.BinMappingDetails.WarehouseCode = prevValue;
					model.BinMappingDetails.WarehouseName = prevWarehouseNameValue;
				}

				if (InputName == "Shelf")
				{
					model.BinMappingDetails.Shelf = prevValue;
				}
			}
		}

		//SelectInputJs();

		_RenderSelectInputJs = true;

        StateHasChanged();
	}

	public async Task SelectInputJs()
	{
		var task = Task.Run(() => _js.InvokeVoidAsync("SetCanvassPins", model.BinMappingDetails.BinMappingPins));
		task.Wait();

		await _js.InvokeVoidAsync("ReloadCanvass");

		await _jSRuntime.InvokeVoidAsync("HideModal");
	}

	[JSInvokable("getCanvassPins")]
	public async Task<List<DashboardViewModel.BinDashboardViewModel.BinMappingPin>> getCanvassPins()
	{
		return model.BinMappingDetails.BinMappingPins.ToList();
	}
}
