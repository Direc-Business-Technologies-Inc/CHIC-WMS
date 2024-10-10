using DataManager.Models.Users;

namespace DataManager.Services.Repositories
{
    public interface IAuthDataService
    {
        HttpResponseMessage Login(UserLogins login);
        UserLogins PostUser(UserLogins user);
		UserLogins UpdateUser(UserLogins user);
		List<UserLogins> GetUserLogins();
		UserLogins GetUserLogin(string UserName);
		List<UserGroups> GetUserGroups();
		UserGroups GetUserGroup(string Id);
		UserGroups PostUserGroup(UserGroups user);
		UserGroups UpdateUserGroup(UserGroups user);
		bool SetUserGroup(List<string> userIds, int userGrpId);
		bool SetUserGroupModules(List<UserModules> userModules);
		UserGroups GetUserGroupAuthorizations(string Id);
		List<Modules> GetModuleList();
	}
}