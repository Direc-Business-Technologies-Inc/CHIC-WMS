namespace Application.BlazorServer.Pages.Administration
{
    public partial class UserGroup : ComponentBase
    {
        [Inject] protected IAdministrativeService _adminService { get; set; }
        [Inject] private NavigationManager _navManager { get; set; }

        dynamic Breadcrumbs = new dynamic[]
        {
            "Administration",
            "Users",
            "User Groups"
        };

        UserViewModel usergroupvm = new UserViewModel();
        //TableParameterModel UserGroups = new TableParameterModel();

        List<UserViewModel.UserGroupsViewModel> groupTable = new List<UserViewModel.UserGroupsViewModel>();

        string searchValue = "";

        protected override void OnInitialized()
        {
            usergroupvm = _adminService.InitializeUserGroup();

            //usergroupvm.UserGroupList = new List<UserViewModel.UserGroupsViewModel>
            //{
            //    new UserViewModel.UserGroupsViewModel { UserGrpId = 1, Name = "OPERATIONS DEPARTMENT", CreateDate = Convert.ToDateTime("4/27/2023"), UserCount = 8},
            //    new UserViewModel.UserGroupsViewModel { UserGrpId = 2, Name = "OPERATIONS DEPARTMENT - GPS", CreateDate = Convert.ToDateTime("4/27/2023"), UserCount = 1},
            //    new UserViewModel.UserGroupsViewModel { UserGrpId = 3, Name = "QUALITY CONTROL DEPARTMENT", CreateDate = Convert.ToDateTime("4/27/2023"), UserCount = 4},
            //    new UserViewModel.UserGroupsViewModel { UserGrpId = 4, Name = "QUALITY CONTROL DEPARTMENT HEAD", CreateDate = Convert.ToDateTime("4/27/2023"), UserCount = 1}
            //};

            groupTable = usergroupvm.UserGroupList;

            //UserGroups.ParamTableColumns = usergroupvm.UserGroupsColumnList;
            //UserGroups.TableName = "User Groups";
            //UserGroups.HasActions = true;
        }

        public async Task SelectGroup(int? Id)
        {
            _navManager.NavigateTo($"/UserGroupSetup/{Id.ToString()}");
        }

        public async Task SearchGroup(string value)
        {
            if (value != "")
            {
                groupTable = usergroupvm.UserGroupList.Where(x => x.UserGrpId.ToString().ToLower().Contains(value.ToLower())
                || x.Name.ToLower().Contains(value.ToLower())).ToList();
            }
            else
            {
                groupTable = usergroupvm.UserGroupList;
            }

        }
    }
}
