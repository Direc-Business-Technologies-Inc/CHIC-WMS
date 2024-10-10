namespace Application.BlazorServer.Pages.Dashboard;

public partial class Dashboard : ComponentBase
{
    [Inject] IDashboardService _dashboardService { get; set; }
    [Inject] private NavigationManager _navManager { get; set; }
    dynamic Breadcrumbs = new dynamic[]
	{
		"Dashboard",
		"Home Page"
	};

	DashboardViewModel model = new DashboardViewModel();

	//InitialPageParameterModel DashboardPage = new InitialPageParameterModel();

    string searchValue = "";
    List<DashboardViewModel.SalesOrders> tableData = new List<DashboardViewModel.SalesOrders>();

    protected override void OnInitialized()
	{
        model = _dashboardService.InitializeDashboard().Result;

        tableData = model.SalesOrderList;

        //model.SalesOrderList = new List<DashboardViewModel.SalesOrders>
        //{
        //new DashboardViewModel.SalesOrders { CustomerName = "Farquaad", ItemCode = "ITSH-1234", ItemName = "QIEH", ProductType = "ITSH", SONo = "SO-11932" },
        //new DashboardViewModel.SalesOrders { CustomerName = "Shrek", ItemCode = "ITSH-1234", ItemName = "Anana", ProductType = "ITSH", SONo = "SO-11933" },
        //new DashboardViewModel.SalesOrders { CustomerName = "Fiona", ItemCode = "ITSH-1234", ItemName = "OAJD", ProductType = "ITSH", SONo = "SO-11934" },
        //new DashboardViewModel.SalesOrders { CustomerName = "Donkey", ItemCode = "ITSH-1234", ItemName = "Pinana", ProductType = "ITSH", SONo = "SO-11935" },
        //};

        //DashboardPage.ParamTableColumns = model.SalesOrderColumnList;
        //DashboardPage.Breadcrumbs = Breadcrumbs;
        //DashboardPage.Link = "/Dashboard/SalesOrderDetails";
        //DashboardPage.PageName = "FinishedGoods";
        //DashboardPage.CanAdd = false;

	}

    public async Task SearchSalesOrder(string value)
    {
        if (value != "")
        {
            tableData = model.SalesOrderList.Where(x => x.SONo.ToLower().Contains(value.ToLower())
            || x.CustomerName.ToLower().Contains(value.ToLower())
            || x.ProductType.ToLower().Contains(value.ToLower())
            || x.ItemCode.ToLower().Contains(value.ToLower())
            || x.ItemName.ToLower().Contains(value.ToLower())).ToList();
        }
        else
        {
            tableData = model.SalesOrderList;
        }

    }

    public async Task SelectSalesOrder(string SONo)
    {
        _navManager.NavigateTo($"/Dashboard/SalesOrderDetails/{SONo}");
    }
}
