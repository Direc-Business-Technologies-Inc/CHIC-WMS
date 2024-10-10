namespace Application.BlazorServer.Pages.CertificateOfIrradiationApproval;

public partial class CertificateOfIrradiationApproval : ComponentBase
{
    [Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;
    [Inject] private NavigationManager _navManager { get; set; }
    [Inject] private ICertificateOfIrradiationService _certificateOfIrradiation { get; set; }
    private IJSObjectReference _js { get; set; } = default!;
    private string _tabName { get; set; } = "For Approval";
    dynamic Breadcrumbs = new dynamic[]
    {
        "Certificate of Irradiation"
    };

    CertificateOfIrradiationViewModel model = new CertificateOfIrradiationViewModel();
    List<CertificateOfIrradiationViewModel.COISalesOrderDetails> tableData = new List<CertificateOfIrradiationViewModel.COISalesOrderDetails>();
    string searchValue = "";

    private DateTime startDate { get; set; } = DateTime.Today.AddDays(-5);
    private DateTime endDate { get; set; } = DateTime.Today;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _js = await _jSRuntime.InvokeAsync<IJSObjectReference>("import", "../scripts/CustomScripts/CertificateOfIrradiationApproval.js");

			//For Calling of backend function from JS
			await _js.InvokeVoidAsync("initializeDateRange", DotNetObjectReference.Create(this));
			await _js.InvokeVoidAsync("setdotnetInstance", DotNetObjectReference.Create(this));
        }
    }

    protected override void OnInitialized()
    {
        try
        {
			model = _certificateOfIrradiation.InitializeCertificateOfIrradiation();

			//model.SalesOrderList = new List<CertificateOfIrradiationViewModel.SalesOrderDetails> {
                //new CertificateOfIrradiationViewModel.SalesOrderDetails{ CustomerName = "Fiona", IrradiationDate = DateTime.Now, ItemName = "Robber Shoes", SONo = "1", Status = "Approved" },
                //new CertificateOfIrradiationViewModel.SalesOrderDetails{ CustomerName = "Donkey", IrradiationDate = DateTime.Now, ItemName = "Milky Makapuso", SONo = "1", Status = "For Approval" },
                //new CertificateOfIrradiationViewModel.SalesOrderDetails{ CustomerName = "Farquaad", IrradiationDate = DateTime.Now, ItemName = "Milky Makapuno", SONo = "1", Status = "Rejected" }
   //         };

            tableData = model.COISalesOrderList.Where(x => x.IrradiationDate >= startDate && x.IrradiationDate <= endDate).ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task SelectSchedule(string SONo)
    {
        _navManager.NavigateTo($"/CertificateOfIrradiationDetails/{SONo}");
    }

    public async Task SearchIrradiation(string value)
    {
        if (value != "")
        {
            tableData = model.COISalesOrderList.Where(x => (x.IrradiationDate >= startDate && x.IrradiationDate <= endDate) 
            && (x.DocNo.ToString().ToLower().Contains(value.ToLower())
            || x.CustomerName.ToLower().Contains(value.ToLower())
            || x.ItemName.ToLower().Contains(value.ToLower()))).ToList();
        }
        else
        {
            tableData = model.COISalesOrderList.Where(x => x.IrradiationDate >= startDate && x.IrradiationDate <= endDate).ToList();
		}

    }

	[JSInvokable("FilterSchedule")]
	public async Task FilterSchedule(string start, string end)
	{
        startDate = Convert.ToDateTime(start);
		endDate = Convert.ToDateTime(end);
		if (searchValue != "")
		{
			tableData = model.COISalesOrderList.Where(x => (x.IrradiationDate >= startDate && x.IrradiationDate <= endDate)
			&& (x.DocNo.ToString().ToLower().Contains(searchValue.ToLower())
			|| x.CustomerName.ToLower().Contains(searchValue.ToLower())
			|| x.ItemName.ToLower().Contains(searchValue.ToLower()))).ToList();
		}
		else
		{
			tableData = model.COISalesOrderList.Where(x => x.IrradiationDate >= startDate && x.IrradiationDate <= endDate).ToList();
		}

		StateHasChanged();
	}
}
