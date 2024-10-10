using static Application.Models.ViewModels.UserViewModel;

namespace Application.BlazorServer.Pages.Administration;

public partial class UserManagement : ComponentBase
{
    [Inject] protected IJSRuntime _jSRuntime { get; set; } = default!;
    [Inject] protected IAdministrativeService _adminService { get; set; }
    [Inject] private NavigationManager _navManager { get; set; }
    private IJSObjectReference _js { get; set; } = default!;

    dynamic Breadcrumbs = new dynamic[]
    {
        "Administration",
        "Users",
        "User Management"
    };

    UserViewModel usersvm = new UserViewModel();

    List<UsersTableViewModel> userTable = new List<UsersTableViewModel>();

    string searchValue = "";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _js = await _jSRuntime.InvokeAsync<IJSObjectReference>("import", "../scripts/CustomScripts/UserManagement.js");

            //For Calling of backend function from JS
            await _js.InvokeVoidAsync("setdotnetInstance", DotNetObjectReference.Create(this));

            await _js.InvokeVoidAsync("InitializeValidator");
        }
    }

    protected override void OnInitialized()
    {
        usersvm = _adminService.InitializeUserManagement();

        //usersvm.UserTableList = new List<UsersTableViewModel>
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

        userTable = usersvm.UserTableList;

        usersvm.UsersTableDetails.UserGrpId = usersvm.UserGroupList.FirstOrDefault().UserGrpId;
    }

    public async Task SelectUser(string Id)
    {
        if (Id == "")
        {
            usersvm.UsersTableDetails = new UsersTableViewModel();
			usersvm.UsersTableDetails.UserGrpId = usersvm.UserGroupList.FirstOrDefault().UserGrpId;
            usersvm.UsersTableDetails.IsActive = true;
		}
        else
        {
            var user = usersvm.UserTableList.Where(x => x.UserId == Id)?.FirstOrDefault() ?? new UsersTableViewModel();

            usersvm.UsersTableDetails.UserId = user.UserId;
            usersvm.UsersTableDetails.UserGrpId = user.UserGrpId;
            usersvm.UsersTableDetails.UserName = user.UserName;
            usersvm.UsersTableDetails.Password = user.Password;
            usersvm.UsersTableDetails.SuperUser = user.SuperUser;
            usersvm.UsersTableDetails.IsActive = user.IsActive;
            usersvm.UsersTableDetails.IsLocked = user.IsLocked;
            usersvm.UsersTableDetails.FullName = user.FullName;
            usersvm.UsersTableDetails.LastName = user.LastName;
            usersvm.UsersTableDetails.FirstName = user.FirstName;
            usersvm.UsersTableDetails.MiddleName = user.MiddleName;
            usersvm.UsersTableDetails.Company = user.Company;
            usersvm.UsersTableDetails.Department = user.Department;
            usersvm.UsersTableDetails.Email = user.Email;
            usersvm.UsersTableDetails.PhoneNo = user.PhoneNo;

        }
        await _jSRuntime.InvokeVoidAsync("eval", "$('#modal-lg-createuser').modal('show')");
    }

    public async Task SearchUser(string value)
    {
        if (value != "")
        {
            userTable = usersvm.UserTableList.Where(x => x.UserName.ToLower().Contains(value.ToLower())
            || x.FullName.ToLower().Contains(value.ToLower())
            || x.Email.ToLower().Contains(value.ToLower())).ToList();
        }
        else
        {
            userTable = usersvm.UserTableList;
        }

    }

    [JSInvokable("SaveUser")]
    public async Task SaveUser()
    {
        try
        {
            if (_adminService.SaveUser(usersvm.UsersTableDetails))
            {
                await _jSRuntime.InvokeVoidAsync("ShowResult", "Success", "Saved Succesfully");
                await Task.Delay(1000);
                _navManager.NavigateTo("UserManagement", true);
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

    public async Task ChangeUserGroup(ChangeEventArgs args)
    {
        usersvm.UsersTableDetails.UserGrpId = Convert.ToInt32(args.Value);
    }
}
