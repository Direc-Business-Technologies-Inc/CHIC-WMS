using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Models.ViewModels.UserViewModel;

namespace Application.BlazorServer.Pages.Administration;

public partial class UserAuthorization
{
    [Inject] protected IAdministrativeService _adminService { get; set; }

    
    
    private string value;
    UserViewModel usersvm = new UserViewModel();
    UserViewModel posvm = new UserViewModel();

    private string selectedUserID;
    private string selectedPositionID;
    List<PositionManagementViewModel> positionTable = new List<PositionManagementViewModel>();
    List<UsersTableViewModel> userTable = new List<UsersTableViewModel>();

    protected override void OnInitialized()
    {
        usersvm = _adminService.InitializePositionManagement();
        positionTable = usersvm.PositionManagementList;

        posvm = _adminService.InitializeUserManagement();
        userTable = posvm.UserTableList; 
        // Optionally set a default value
        //selectedPositionID = positionTable.FirstOrDefault()?.PosManageID;
    }

    async Task HandleSelectUser(string value)
    {
        
    }
    void LoadData(LoadDataArgs args)
    {
        //var query = 

        //if (!string.IsNullOrEmpty(args.Filter))
        //{
        //    query = query.Where(c => c.CustomerID.ToLower().Contains(args.Filter.ToLower()) || c.ContactName.ToLower().Contains(args.Filter.ToLower()));
        //}

        //customers = query.ToList();

        //InvokeAsync(StateHasChanged);
    }

}