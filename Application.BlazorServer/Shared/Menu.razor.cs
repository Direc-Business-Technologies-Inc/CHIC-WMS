using Application.BlazorServer.Security;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System;
using System.Security.Claims;
using static Application.Models.ViewModels.UserViewModel;

namespace Application.BlazorServer.Shared;

public partial class Menu : ComponentBase
{
    [Inject] IAdministrativeService _administrativeService { get; set; }
    [Inject] AuthenticationService _authenticationService { get; set; }
    [Inject] NavigationManager _navigationManager { get; set; }
    List<ModuleViewModel> model = new List<ModuleViewModel>();
    List<UserGroupsViewModel> userModel = new List<UserGroupsViewModel>();
    protected override async Task OnInitializedAsync()
    {
        model = _administrativeService.GetModules();

        var ModuleAuthentications = _authenticationService.GetModuleAuthentications().Result;

        string currentUri = _navigationManager.Uri.ToString();
        string baseUri = _navigationManager.BaseUri.ToString();

        string uri = currentUri.Replace(baseUri.Remove(baseUri.Length - 1, 1), "");

        //balik sa login kapag hindi authorized 
        if (ModuleAuthentications == null)
        {
            //Exclude Login and Dashboard Notification
            if (!(uri == "/" || uri == "/LoginAccount" || uri == "/DashboardNotification"))
            {
                _navigationManager.NavigateTo("");
                return;
            }
        }

        if (uri == "/DashboardNotification")
        {
            userModel = new List<UserGroupsViewModel>();
        }
        else
        {
            userModel = JsonConvert.DeserializeObject<List<UserGroupsViewModel>>(ModuleAuthentications);
        }
    }

    public bool CanViewGroup(string moduleGroup)
    {
        var res = userModel.Join(model, UM => new { UM.ModuleId }, M => new { M.ModuleId }, (UM, M) => new { UM, M })
            .Where(UMM => UMM.M.GroupName == moduleGroup && UMM.M.Active == true && (UMM.UM.CanCreate == true || UMM.UM.CanUpdate == true || UMM.UM.IsReadOnly == true)).Any();
        return res;
    }

    public bool CanViewSubGroup(string moduleSubGroup)
    {
        var res = userModel.Join(model, UM => new { UM.ModuleId }, M => new { M.ModuleId }, (UM, M) => new { UM, M })
            .Where(UMM => UMM.M.SubGroupName == moduleSubGroup && UMM.M.Active == true && (UMM.UM.CanCreate == true || UMM.UM.CanUpdate == true || UMM.UM.IsReadOnly == true)).Any();
        return res;
    }

    public bool CanViewModule(string moduleId)
    {
        return userModel.Where(x => x.ModuleId == moduleId && (x.CanCreate == true || x.CanUpdate == true || x.IsReadOnly == true)).Any();
    }
}