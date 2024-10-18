namespace Application.Services.Repositories;

public interface IAdministrativeService
{
	bool SaveUser(UsersTableViewModel model);
	bool SavePosition(PositionManagementViewModel model,string CreateUserId);

	UserViewModel InitializeUserManagement();
	UserViewModel InitializePositionManagement();

	UserViewModel InitializeUserGroup();
	UserViewModel InitializeUserGroupSetup(string UserGrpId);
	bool PostUserGroup(UserViewModel userGroup, string CreateUserId);
	List<ModuleViewModel> GetModules();
	UserLogins GetUserLogin(string UserName);
}