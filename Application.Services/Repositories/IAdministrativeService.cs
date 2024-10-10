namespace Application.Services.Repositories;

public interface IAdministrativeService
{
	bool SaveUser(UsersTableViewModel model);
	UserViewModel InitializeUserManagement();
	UserViewModel InitializeUserGroup();
	UserViewModel InitializeUserGroupSetup(string UserGrpId);
	bool PostUserGroup(UserViewModel userGroup, string CreateUserId);
	List<ModuleViewModel> GetModules();
	UserLogins GetUserLogin(string UserName);
}