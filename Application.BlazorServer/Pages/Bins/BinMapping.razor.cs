namespace Application.BlazorServer.Pages.Bins;

public partial class BinMapping : ComponentBase
{
	[Inject] private IJSRuntime _jSRuntime { get; set; }
	[Inject] private IBinServices _BinService { get; set; }
	[Inject] private NavigationManager _navManager { get; set; }
	[Inject] private IWebHostEnvironment _environment { get; set; }
	[Inject] private IFacilityLocationService _facilityLocationService { get; set; }
	private IJSObjectReference _js { get; set; } = default!;

	dynamic Breadcrumbs = new dynamic[]
	{
	"Bins",
	"Bin Mapping"
	};

	List<FacilityLocationViewModel> _facilities { get; set; } = new(); 

	BinMappingViewModel model = new BinMappingViewModel();

	int LevelCount = 0;
	bool isLevelLoading = false;
	string prevWarehouseCode = "";
	string prevShelf = "";
	RadzenDataGrid<Levels>? gridLevel = new RadzenDataGrid<Levels>();
	//RadzenDataGrid<Rows> gridRow = new RadzenDataGrid<Rows>();
	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			_js = await _jSRuntime.InvokeAsync<IJSObjectReference>("import", "../scripts/CustomScripts/BinMapping.js");
			await _js.InvokeVoidAsync("InitializeStepper");
			await _js.InvokeVoidAsync("InitializePinRange");

			//For Calling of backend function from JS
			await _js.InvokeVoidAsync("setdotnetInstance", DotNetObjectReference.Create(this));
		}
	}

	protected override void OnInitialized()
	{
		try
		{
			//Breadcrumbs = model.breadCrumbs;
			//gridLevel = model.LevelList;

			model = _BinService.InitializeBinMapping(model);
			_facilities = _facilityLocationService.GetAll();
		}
		catch (Exception)
		{
			throw;
		}
	}

	public void SelectInput(string InputName, string value)
	{
		_BinService.SelectInput(model, InputName, value);

		if ((InputName == "Level" || InputName == "Aisle") && (model.BinMappingHeader.Aisle != null && model.BinMappingHeader.Level != null))
		{
			string aisle = model.AisleList.Where(x => x.Aisle == model.BinMappingHeader.Aisle).FirstOrDefault().Code;
			string level = model.BinMappingHeader.Level;

			model.RowList = new List<Rows>();
			_BinService.FetchRows(model, aisle);

			_js.InvokeVoidAsync("showPinTable", aisle, level, model.RowList);
		}


		if ((InputName == "WarehouseCode" || InputName == "Shelf") && (model.BinMappingHeader.WarehouseCode != null && model.BinMappingHeader.Shelf != null))
		{
			_BinService.FetchColumn(model);

			if (prevWarehouseCode != model.BinMappingHeader.WarehouseCode || prevShelf != model.BinMappingHeader.Shelf)
			{
				model.BinMappingHeader.ImageUrl = "assets/img/no_image.png";
				model.BinMappingHeader.BinMappingPins = new List<BinMappingPins>();
				_js.InvokeVoidAsync("SetImageUrlFromBackend", model.BinMappingHeader.ImageUrl);
				_js.InvokeVoidAsync("SetCanvassPins", model.BinMappingHeader.BinMappingPins);
			}

			if (_BinService.GetBinMapping(model))
			{
				model.BinMappingHeader.ImageUrl = $"FILE_UPLOAD/{model.BinMappingHeader.FileName}";

				_js.InvokeVoidAsync("SetImageUrlFromBackend", model.BinMappingHeader.ImageUrl);
				_js.InvokeVoidAsync("SetCanvassPins", model.BinMappingHeader.BinMappingPins);
			}
		}

		if (InputName == "WarehouseCode")
		{
			if (prevWarehouseCode != model.BinMappingHeader.WarehouseCode)
			{
				//Get Shelf List Data
				_BinService.GetShelf(model, value);
				model.BinMappingHeader.Shelf = "";
				prevShelf = "";
			}
			prevWarehouseCode = value;
		}

		if (InputName == "Shelf")
		{
			prevShelf = value;
		}

		_jSRuntime.InvokeVoidAsync("HideModal");
	}
	[JSInvokable("SetImageUrl")]
	public async Task SetImageUrl(string Url, string FileName)
	{
		model.BinMappingHeader.ImageUrl = Url;
		model.BinMappingHeader.FileName = FileName;
		await Task.Delay(1000);
	}

	[JSInvokable("AdjustPinList")]
	public async Task AdjustPinList(string Method, BinMappingPins Data, float IntrinsicWidth, float RenderedWidth)
	{
		ICollection<BinMappingPins> pins = _BinService.AdjustPinList(model, Method, Data, IntrinsicWidth, RenderedWidth);
		await Task.Delay(1000);
		//return pins;
	}

	//[JSInvokable("FetchColumn")]
	//public async Task FetchColumn()
	//{
	//Remove all column data before adding new
	//model.LevelList = new List<Levels>();
	//try
	//{

	//	await gridLevel.Reload();
	//	//await gridRow.Reload();

	//}
	//catch (Exception)
	//{

	//	throw;
	//}
	//model.LevelList = new List<Levels>();
	//_BinService.FetchColumn(model);

	//model.LevelList.ForEach(x => { gridLevel.InsertRow(x); });
	//model.RowList.ForEach(x => { gridRow.InsertRow(x); });
	//return rownum;
	//await Task.Delay(1000);
	//}

	[JSInvokable("SaveBinMapping")]
	public async Task SaveBinMapping()
	{
		try
		{
			if (_BinService.SaveBinMapping(model))
			{
				string FilePath = $"{_environment.WebRootPath}/FILE_UPLOAD/{model.BinMappingHeader.FileName}";
				if (_BinService.UploadImage(model.BinMappingHeader.ImageUrl, FilePath))
				{
					await _jSRuntime.InvokeVoidAsync("ShowResult", "Success", "Saved Succesfully");
					await Task.Delay(1000);
					_navManager.NavigateTo("BinMapping", true);
				}
				return;
			}
			await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", "Saving Failed. Please contact your administrator.");
			await Task.Delay(1000);
		}
		catch (Exception ex)
		{
			await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", ex.Message);
			await Task.Delay(1000);
		}
	}

	public void SelectFacility(FacilityLocationViewModel data)
	{
		model.BinMappingHeader.FacilityCode = data.Code;
		_jSRuntime.InvokeVoidAsync("HideModal");
	}

	//async void LoadLevelData(LoadDataArgs args)
	//{
	//	isLevelLoading = true;

	//	var query = model.LevelList;

	//	if (!string.IsNullOrEmpty(args.Filter))
	//	{
	//		// Filter via the Where method
	//		//query = query.Where(args.Filter);
	//	}

	//	if (!string.IsNullOrEmpty(args.OrderBy))
	//	{
	//		string[] OrderByArray = args.OrderBy.Split(' ');

	//		// Sort via the OrderBy method
	//		if (OrderByArray[1] == "asc")
	//		{
	//			query.OrderBy(x => x.Level);
	//		}
	//		else
	//		{
	//			query.OrderBy(x => x.Level);
	//			query.Reverse();
	//		}
	//	}

	//	// Important!!! Make sure the Count property of RadzenDataGrid is set.
	//	LevelCount = query.Count();

	//	// Perform paging via Skip and Take.
	//	//customers = query.Skip(args.Skip.Value).Take(args.Top.Value).ToList();

	//	// Add StateHasChanged(); for DataGrid to update if your LoadData method is async.
	//	StateHasChanged();

	//	isLevelLoading = false;
	//}
}
