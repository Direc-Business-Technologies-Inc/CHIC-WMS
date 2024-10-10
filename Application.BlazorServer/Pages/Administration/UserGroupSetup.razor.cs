using Application.BlazorServer.Security;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using static Application.Models.ViewModels.DashboardViewModel;
using static Application.Models.ViewModels.UserViewModel;

namespace Application.BlazorServer.Pages.Administration;

public partial class UserGroupSetup : ComponentBase
{
	[Parameter]
	public string UserGrpId { get; set; } = "0";

	[Inject] protected IAdministrativeService _adminService { get; set; }
	[Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;
	[Inject] private NavigationManager _navManager { get; set; }
	[Inject] AuthenticationService _authenticationService { get; set; }

	private IJSObjectReference _js { get; set; } = default!;

	dynamic Breadcrumbs = new dynamic[]
	{
		"Administration",
		"Users",
		"User Group Setup"
	};

	UserViewModel usersvm = new UserViewModel();
	UserViewModel.UserGroupsViewModel usergroupsvm = new UserViewModel.UserGroupsViewModel();

	RadzenDataGrid<UserViewModel.ModuleViewModel> UserGroupGrid;
	List<UserViewModel.ModuleViewModel> pageList = new List<UserViewModel.ModuleViewModel>();

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			_js = await _jSRuntime.InvokeAsync<IJSObjectReference>("import", "../scripts/CustomScripts/UserGroupSetup.js");
			await _js.InvokeVoidAsync("InitializeStepper");
		}
	}

	protected override void OnInitialized()
	{
		usersvm = _adminService.InitializeUserGroupSetup(UserGrpId);

		if(UserGrpId == "0")
		{
			usersvm.UserGroup.IsActive = true;
		}
	}

	public async Task PostUserGroup()
	{
		try
		{
			string CreateUserId = _authenticationService.GetUserId().Result;

			if (_adminService.PostUserGroup(usersvm, CreateUserId))
			{
				await _jSRuntime.InvokeVoidAsync("ShowResult", "Success", "Saved Succesfully");
				await Task.Delay(1000);
				_navManager?.NavigateTo("/UserGroup");
			}
			else
			{
				await _jSRuntime.InvokeVoidAsync("ShowResult", "Error", "Saving Failed. Please contact your administrator.");
			}
		}
		catch (Exception ex)
		{
			await _jSRuntime.InvokeVoidAsync("ShowResult", ex.Message);
		}
	}

	private void Select(UsersTableViewModel item)
	{
		usersvm.UserSelectedTableList.Add(item);
	}

	private void Deselect(UsersTableViewModel item)
	{
		usersvm.UserSelectedTableList.Remove(item);
	}

	private void SelectAll()
	{
		usersvm.UserSelectedTableList = new List<UsersTableViewModel>();

		foreach (var user in usersvm.UserTableList)
		{
			usersvm.UserSelectedTableList.Add(user);
		}
	}

	private void DeselectAll()
	{
		usersvm.UserSelectedTableList = new List<UsersTableViewModel>();
	}
	void RowRender(RowRenderEventArgs<UserViewModel.ModuleViewModel> args)
	{
		args.Expandable = usersvm.ModuleList.Where(x => x.GroupName == args.Data.GroupName).Any();
	}

	void RowExpand(UserViewModel.ModuleViewModel module)
	{
		pageList = new List<UserViewModel.ModuleViewModel>();
		pageList = usersvm.ModuleList.Where(x => x.GroupName == module.GroupName).ToList();
	}

	public async Task SelectAuthorization(string args, string moduleId)
	{
		var userGroup = usersvm.UserGroupList.Where(x => x.ModuleId == moduleId).FirstOrDefault();

		if (userGroup == null)
		{
			var newUser = new UserGroupsViewModel();
			newUser.ModuleId = moduleId;
			newUser.IsActive = true;

			switch (args)
			{
				case "Full":
					newUser.CanCreate = true;
					newUser.CanUpdate = true;
					newUser.IsReadOnly = false;
					break;
				case "Create":
					newUser.CanCreate = true;
					newUser.CanUpdate = false;
					newUser.IsReadOnly = false;
					break;
				case "Update":
					newUser.CanCreate = false;
					newUser.CanUpdate = true;
					newUser.IsReadOnly = false;
					break;
				case "Read":
					newUser.CanCreate = false;
					newUser.CanUpdate = false;
					newUser.IsReadOnly = true;
					break;
				default:
					newUser.CanCreate = false;
					newUser.CanUpdate = false;
					newUser.IsReadOnly = false;
					break;
			}
			usersvm.UserGroupList.Add(newUser);

		}
		else
		{
			switch (args)
			{
				case "Full":
					userGroup.CanCreate = true;
					userGroup.CanUpdate = true;
					userGroup.IsReadOnly = false;
					break;
				case "Create":
					userGroup.CanCreate = true;
					userGroup.CanUpdate = false;
					userGroup.IsReadOnly = false;
					break;
				case "Update":
					userGroup.CanCreate = false;
					userGroup.CanUpdate = true;
					userGroup.IsReadOnly = false;
					break;
				case "Read":
					userGroup.CanCreate = false;
					userGroup.CanUpdate = false;
					userGroup.IsReadOnly = true;
					break;
				default:
					userGroup.CanCreate = false;
					userGroup.CanUpdate = false;
					userGroup.IsReadOnly = false;
					break;
			}
		}

	}

	public async Task SelectAuthorizationByGroup(string args, string groupName)
	{
		var moduleGroup = usersvm.ModuleList.Where(x => x.GroupName == groupName).ToList();

		foreach(var module in moduleGroup)
		{
			await SelectAuthorization(args, module.ModuleId);
		}
	}

	public async Task<string> GetAuthorization(ModuleViewModel data)
	{
		var userGroup = usersvm.UserGroupList.Where(x => x.ModuleId == data.ModuleId).FirstOrDefault();

		string permission = "";

		if (userGroup == null)
		{
			permission = "No";
		}
		else
		{
			if (userGroup.CanCreate && userGroup.CanUpdate)
			{
				permission = "Full";
			}
			else if (userGroup.CanCreate)
			{
				permission = "Create";
			}
			else if (userGroup.CanUpdate)
			{
				permission = "Update";
			}
			else if (userGroup.IsReadOnly)
			{
				permission = "Read";
			}
			else
			{
				permission = "No";
			}
		}

		return permission;
	}

	public async Task<string> GetAuthorizationByGroup(ModuleViewModel data)
	{
		List<UserGroupsViewModel> userGroupsViewModels = new List<UserGroupsViewModel>();

		var moduleGroup = usersvm.ModuleList.Where(x => x.GroupName == data.GroupName).ToList();

		int moduleGroupCount = moduleGroup.Count();

		foreach (var Module in moduleGroup)
		{
			UserGroupsViewModel groupsViewModel = new UserGroupsViewModel();
			var userGroup = usersvm.UserGroupList.Where(x => x.ModuleId == Module.ModuleId).FirstOrDefault();
			if (userGroup == null)
			{
				groupsViewModel.CanCreate = false;
				groupsViewModel.CanUpdate = false;
				groupsViewModel.IsReadOnly = false;
			}
			else
			{
				groupsViewModel.CanCreate = userGroup.CanCreate;
				groupsViewModel.CanUpdate = userGroup.CanUpdate;
				groupsViewModel.IsReadOnly = userGroup.IsReadOnly;
			}

			userGroupsViewModels.Add(groupsViewModel);
		}

		string permission = "";

		if (userGroupsViewModels.Where(x => x.CanUpdate == true && x.CanCreate == true).ToList().Count() == moduleGroupCount)
		{
			permission = "Full";
		}
		else if (userGroupsViewModels.Where(x => x.CanCreate == true).ToList().Count() == moduleGroupCount)
		{
			permission = "Create";
		}
		else if (userGroupsViewModels.Where(x => x.CanUpdate == true).ToList().Count() == moduleGroupCount)
		{
			permission = "Update";
		}
		else if (userGroupsViewModels.Where(x => x.IsReadOnly).ToList().Count() == moduleGroupCount)
		{
			permission = "Read";
		}
		else if (userGroupsViewModels.Where(x => x.CanUpdate == false && x.CanCreate == false && x.IsReadOnly == false).ToList().Count() == moduleGroupCount)
		{
			permission = "No";
		}
		else
		{
			permission = "-";
		}

		return permission;
	}

	public async Task PreviousPage()
	{
		_navManager?.NavigateTo("/UserGroup");
	}
}
