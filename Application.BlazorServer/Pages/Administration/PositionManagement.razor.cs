
using Application.BlazorServer.Security;
using System.Linq;
using static Application.Models.ViewModels.UserViewModel;
namespace Application.BlazorServer.Pages.Administration;
public partial class PositionManagement
{
	[Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;
	[Inject] protected IAdministrativeService _adminService { get; set; }
	[Inject] private NavigationManager _navManager { get; set; }
	[Inject] AuthenticationService _authenticationService { get; set; }
	[Inject] SweetAlertService swal { get; set; }
	private IJSObjectReference _js { get; set; } = default!;
	private IJSObjectReference? modal;
	private ElementReference _modalElement;
	bool createupdateHasChanges = false;

	private List<string> classifications;
	private async Task OnBeforeInternalNavigation(Microsoft.AspNetCore.Components.Routing.LocationChangingContext locationChangingContext)
	{
		if (!createupdateHasChanges) return;
		var continueNavigationEvent = await swal.PromptUnsavedChangesAsync();
		if (!continueNavigationEvent) locationChangingContext.PreventNavigation();
	}
	public bool HasChanges { get; set; }
	[Parameter] public EventCallback<bool> HasChangesChanged { get; set; }
	protected event Action HasChangedEventHandler;
	protected virtual async Task InvokeHasChangesChanged()
	{
		createupdateHasChanges = true;
		HasChanges = true;
		HasChangedEventHandler?.Invoke();
		await HasChangesChanged.InvokeAsync(true);
	}

	protected async Task ResetHasChanges() => await HasChangesChanged.InvokeAsync(HasChanges = false);
	private string _userid { get; set; }	
	dynamic Breadcrumbs = new dynamic[]
	{
	"Administration",
	"Users",
	"Position Management"
	};

	UserViewModel usersvm = new UserViewModel();

	List<PositionManagementViewModel> positionTable = new List<PositionManagementViewModel>();

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			_js = await _jSRuntime.InvokeAsync<IJSObjectReference>("import", "../scripts/CustomScripts/PositionManagement.js");

			//For Calling of backend function from JS
			await _js.InvokeVoidAsync("setdotnetInstance", DotNetObjectReference.Create(this));
			await _js.InvokeVoidAsync("InitializeValidator");
			
			//modal = await _js.InvokeAsync<IJSObjectReference>("bootstrap.Modal.getInstance", _modalElement);

		}
	}
	string searchValue = "";
	protected override async void OnInitialized()
	{
		  usersvm = _adminService.InitializePositionManagement();
		 _userid = _authenticationService.GetUserId().Result;


		   classifications = new List<string>
			{
				"System Admin",
				"SuperVisor",
				"Warehouse PPC"
			};
	//usersvm.positionTableList = new List<UsersTableViewModel>
	//{
	//	new UsersTableViewModel { UserId = "1", UserName = "a.bucayan", FullName = "BUCAYAN, ANDREA", Email = "a.bucayan@gmail.com", SuperUser = true, IsActive = true, IsLocked = false },
	//	new UsersTableViewModel { UserId = "2", UserName = "a.dagale", FullName = "DAGALE, ARCEE CRUZ", Email = "a.dagale@gmail.com", SuperUser = true, IsActive = true, IsLocked = false},
	//	new UsersTableViewModel { UserId = "3", UserName = "a.gabito", FullName = "GABITO, AILENE", Email = "a.gabito@gmail.com", SuperUser = true, IsActive = true, IsLocked = false}
	//};

	//usersvm.UserGroupList = new List<UserGroupsViewModel>
	//{
	//	new UserGroupsViewModel { UserGrpId = 1, Code = "ACCTG", Name = "ACCOUNTING DEPARTMENT", CreateDate = Convert.ToDateTime("4/27/2023"), UserCount = 8},
	//	new UserGroupsViewModel { UserGrpId = 2, Code = "OPN - GPS", Name = "OPERATIONS DEPARTMENT - GPS", CreateDate = Convert.ToDateTime("4/27/2023"), UserCount = 1},
	//	new UserGroupsViewModel { UserGrpId = 3,  Code = "QCD", Name = "QUALITY CONTROL DEPARTMENT", CreateDate = Convert.ToDateTime("4/27/2023"), UserCount = 4},
	//	new UserGroupsViewModel { UserGrpId = 4,  Code = "QCD - HEAD", Name = "QUALITY CONTROL DEPARTMENT HEAD", CreateDate = Convert.ToDateTime("4/27/2023"), UserCount = 1}
	//};

	positionTable = usersvm.PositionManagementList;

		//usersvm.UsersTableDetails.UserGrpId = usersvm.UserGroupList.FirstOrDefault().UserGrpId;
	}

	
	public async Task SearchUser(string value)
	{
		if (value != "")
		{
			positionTable = usersvm.PositionManagementList.Where(x => x.PositionName.ToLower().Contains(value.ToLower())|| x.Description.ToLower().Contains(value.ToLower())).ToList();
		}
		else
		{
			positionTable = usersvm.PositionManagementList;
		}

	}
	public async Task SelectPosition(string Id)
	{
		if (Id == "")
		{
			usersvm.PositionDetails = new PositionManagementViewModel();
			usersvm.PositionDetails.IsActive = true;
		}
		else
		{
			var position = usersvm.PositionManagementList.Where(x => x.PosManageID == Id)?.FirstOrDefault() ?? new PositionManagementViewModel();
			usersvm.PositionDetails.PosManageID = position.PosManageID;
			usersvm.PositionDetails.PositionName = position.PositionName;
			usersvm.PositionDetails.Description = position.Description;
			usersvm.PositionDetails.IsActive = position.IsActive;
			usersvm.PositionDetails.Classification = position.Classification;

		}
		await _jSRuntime.InvokeVoidAsync("eval", "$('#modal-createposition').modal('show')");
	}

	[JSInvokable("SavePos")]
	public async Task SavePos()
	{
		try
		{	
			if (_adminService.SavePosition(usersvm.PositionDetails,_userid))
			{
				await _jSRuntime.InvokeVoidAsync("ShowResult", "Success", "Saved Succesfully");
				await Task.Delay(1000);
				_navManager.NavigateTo("PositionManagement", true);
				return; //End of Function
			}
			await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", "Saving Failed. Please contact your administrator.");
		}
		catch (Exception ex)
		{
			await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", ex.Message);
			throw;
		}
	}
	private async Task CloseModal()
	{
		if (createupdateHasChanges && !await swal.PromptUnsavedChangesAsync()) return;
		if (modal is not null)
			await modal.InvokeVoidAsync("hide");

		createupdateHasChanges = false;
	}
	public async Task ChangeClassification(ChangeEventArgs args)
	{
		usersvm.PositionDetails.Classification = args.Value.ToString();
	}
}