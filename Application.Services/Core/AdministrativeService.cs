using DataManager.Models.Configurations;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Core;

public class AdministrativeService : IAdministrativeService
{
	IAuthDataService _authDataService;
	private readonly IMapper Mapper;
	
	public AdministrativeService(IAuthDataService authDataService, IMapper mapper)
	{
		_authDataService = authDataService;
		Mapper = mapper;
	}
	public bool SaveUser(UsersTableViewModel model)
	{
		try
		{
			string userId = Guid.NewGuid().ToString("N");

			UserLogins user = new UserLogins();
			user.Username = model.UserName;
			user.Password = Cryption.EncryptPassword(model.UserName, model.Password);
			user.IsSuperUser = model.SuperUser;
			user.IsActive = model.IsActive;
			user.IsLocked = model.IsLocked;
			user.UserGroupId = model.UserGrpId ?? 0;

			//USER DETAILS
			user.UserDetails = new UserDetails();
			user.UserDetails.UserId = user.UserId;
			user.UserDetails.LastName = model.LastName;
			user.UserDetails.FirstName = model.FirstName;
			user.UserDetails.FirstName = model.FirstName;
			user.UserDetails.MiddleName = model.MiddleName;
			user.UserDetails.MiddleName = model.MiddleName;
			user.UserDetails.IsActive = model.IsActive;
			user.UserDetails.Company = model.Company;
			user.UserDetails.Department = model.Department;
			user.UserDetails.Email = model.Email;
			user.UserDetails.Phone = model.PhoneNo;

			if (model.UserId == "")
			{
				//SAVE
				//HEADER
				user.UserId = userId;
				user.CreatedDate = DateTime.Now;
				user.CreatedUserId = userId; //Get identity claims

				//USER DETAILS
				user.UserDetails.CreatedDate = user.CreatedDate;
				user.UserDetails.CreatedUserId = user.CreatedUserId;

				_authDataService.PostUser(user);
			}
			else
			{
				//UPDATE

				user.UpdatedDate = DateTime.Now;
				user.UpdatedUserId = userId; //Get identity claims

				//USER DETAILS
				user.UserDetails.CreatedDate = DateTime.Now;
				user.UserDetails.CreatedUserId = userId;

				_authDataService.UpdateUser(user);
			}
			return true;
		}
		catch (Exception)
		{

			throw;
		}
	}

	public UserViewModel InitializeUserManagement()
	{
		UserViewModel model = new UserViewModel();

		List<UserLogins> users = _authDataService.GetUserLogins();

		List<UserGroups> userGroups = _authDataService.GetUserGroups();

		foreach (var user in users)
		{
			model.UserTableList.Add(new UsersTableViewModel
			{
				UserId = user.UserId,
				UserGrpId = user.UserGroupId,
				UserName = user.Username,
				Password = Cryption.DecryptPassword(user.Username, user.Password),
				FullName = user.UserDetails != null ? FormulateFullName(user.UserDetails) : "",
				FirstName = user.UserDetails != null ? user.UserDetails.FirstName : "",
				MiddleName = user.UserDetails != null ? user.UserDetails.MiddleName : "",
				LastName = user.UserDetails != null ? user.UserDetails.LastName : "",
				PhoneNo = user.UserDetails != null ? user.UserDetails.Phone : "",
				Email = user.UserDetails != null ? user.UserDetails.Email : "",
				Company = user.UserDetails != null ? user.UserDetails.Company : "",
				Department = user.UserDetails != null ? user.UserDetails.Department : "",
				SuperUser = user.IsSuperUser,
				IsActive = user.IsActive,
				IsLocked = user.IsLocked
			});
		}

		foreach (var group in userGroups)
		{
			model.UserGroupList.Add(new UserGroupsViewModel { UserGrpId = group.UserGroupId, Name = group.GroupName });

		}
		return model;
	}

	public UserViewModel InitializeUserGroup()
	{
		UserViewModel model = new UserViewModel();

		List<UserGroups> userGroups = _authDataService.GetUserGroups();

		foreach (var group in userGroups)
		{
			model.UserGroupList.Add(new UserGroupsViewModel { UserGrpId = group.UserGroupId, Name = group.GroupName });

		}

		return model;
	}

	public UserViewModel InitializeUserGroupSetup(string UserGrpId)
	{
		UserViewModel model = new UserViewModel();

		if (UserGrpId != "0")
		{

			//GET USER DETAILS
			UserGroups userGroups = _authDataService.GetUserGroup(UserGrpId);

			model.UserGroup = new UserGroupsViewModel { UserGrpId = userGroups.UserGroupId, Name = userGroups.GroupName, IsActive = userGroups.IsActive };

			foreach (var user in userGroups.UserLogins)
			{
				model.UserSelectedTableList.Add(new UsersTableViewModel
				{
					UserId = user.UserId,
					UserGrpId = user.UserGroupId,
					UserName = user.Username,
					Password = Cryption.DecryptPassword(user.Username, user.Password),
					FullName = user.UserDetails != null ? FormulateFullName(user.UserDetails) : "",
					FirstName = user.UserDetails != null ? user.UserDetails.FirstName : "",
					MiddleName = user.UserDetails != null ? user.UserDetails.MiddleName : "",
					LastName = user.UserDetails != null ? user.UserDetails.LastName : "",
					PhoneNo = user.UserDetails != null ? user.UserDetails.Phone : "",
					Email = user.UserDetails != null ? user.UserDetails.Email : "",
					Company = user.UserDetails != null ? user.UserDetails.Company : "",
					Department = user.UserDetails != null ? user.UserDetails.Department : "",
					SuperUser = user.IsSuperUser,
					IsActive = user.IsActive,
					IsLocked = user.IsLocked
				});
			}

			//GET USER AUTHORIZATION
			//UserGroups userGroupAuthorization = _authDataService.GetUserGroupAuthorizations(UserGrpId);

			foreach (var userGroup in userGroups.UserModules)
			{
				model.UserGroupList.Add(new UserGroupsViewModel { UserGrpId = userGroup.UserGroupId, ModuleId = userGroup.ModuleId, IsReadOnly = userGroup.IsReadOnly, CanCreate = userGroup.CanCreate, CanUpdate = userGroup.CanUpdate });
			}
		}

		//GET USER LIST
		List<UserLogins> users = _authDataService.GetUserLogins();
		if (Convert.ToInt32(UserGrpId) == 1)
		{
			foreach (var user in users.Where(x => x.UserGroupId != 1))
			{
				model.UserTableList.Add(new UsersTableViewModel
				{
					UserId = user.UserId,
					UserGrpId = user.UserGroupId,
					UserName = user.Username,
					Password = Cryption.DecryptPassword(user.Username, user.Password),
					FullName = user.UserDetails != null ? FormulateFullName(user.UserDetails) : "",
					FirstName = user.UserDetails != null ? user.UserDetails.FirstName : "",
					MiddleName = user.UserDetails != null ? user.UserDetails.MiddleName : "",
					LastName = user.UserDetails != null ? user.UserDetails.LastName : "",
					PhoneNo = user.UserDetails != null ? user.UserDetails.Phone : "",
					Email = user.UserDetails != null ? user.UserDetails.Email : "",
					Company = user.UserDetails != null ? user.UserDetails.Company : "",
					Department = user.UserDetails != null ? user.UserDetails.Department : "",
					SuperUser = user.IsSuperUser,
					IsActive = user.IsActive,
					IsLocked = user.IsLocked
				});
			}
		}
		else
		{
			foreach (var user in users.Where(x => x.UserGroupId == Convert.ToInt32(UserGrpId) || x.UserGroupId == 1))
			{
				model.UserTableList.Add(new UsersTableViewModel
				{
					UserId = user.UserId,
					UserGrpId = user.UserGroupId,
					UserName = user.Username,
					Password = Cryption.DecryptPassword(user.Username, user.Password),
					FullName = user.UserDetails != null ? FormulateFullName(user.UserDetails) : "",
					FirstName = user.UserDetails != null ? user.UserDetails.FirstName : "",
					MiddleName = user.UserDetails != null ? user.UserDetails.MiddleName : "",
					LastName = user.UserDetails != null ? user.UserDetails.LastName : "",
					PhoneNo = user.UserDetails != null ? user.UserDetails.Phone : "",
					Email = user.UserDetails != null ? user.UserDetails.Email : "",
					Company = user.UserDetails != null ? user.UserDetails.Company : "",
					Department = user.UserDetails != null ? user.UserDetails.Department : "",
					SuperUser = user.IsSuperUser,
					IsActive = user.IsActive,
					IsLocked = user.IsLocked
				});
			}
		}


		//GET MODULE LIST
		List<Modules> moduleList = _authDataService.GetModuleList();

		foreach (var module in moduleList)
		{
			model.ModuleList.Add(new ModuleViewModel { ModuleId = module.ModuleId, Name = module.Name, Description = module.Description, GroupName = module.GroupName, SubGroupName = module.SubGroupName });
		}

		return model;
	}

	public string FormulateFullName(UserDetails user)
	{
		string firstName = user.FirstName.IsNullOrEmpty() ? "" : user.FirstName;
		string middleName = user.MiddleName.IsNullOrEmpty() ? "" : " " + user.MiddleName;
		string lastName = user.LastName.IsNullOrEmpty() ? "" : " " + user.LastName;

		return firstName + middleName + lastName;
	}

	public bool PostUserGroup(UserViewModel userGroup, string CreateUserId)
	{
		try
		{
			UserGroups userGroups = new UserGroups();

			//UserGroups
			UserGroups user = new UserGroups();
			user.GroupName = userGroup.UserGroup.Name;
			user.IsActive = userGroup.UserGroup.IsActive;
			user.CreatedUserId = CreateUserId;

			if (userGroup.UserGroup.UserGrpId == 0)
			{
				//ADD
				userGroups = _authDataService.PostUserGroup(user);
			}
			else
			{
				user.UserGroupId = userGroup.UserGroup.UserGrpId;
				//UPDATE
				userGroups = _authDataService.UpdateUserGroup(user);
			}

			//Assigning of UserGroups to UserLogin
			List<string> userIds = new List<string>();
			foreach (var userLogin in userGroup.UserSelectedTableList)
			{
				userIds.Add(userLogin.UserId);
			}
			_authDataService.SetUserGroup(userIds, userGroups.UserGroupId);

			//Assigning of UserGroups to UserModules
			List<UserModules> userModules = Mapper.Map<List<UserModules>>(userGroup.UserGroupList);
			userModules.ForEach(
				module =>
				{
					module.UserGroupId = userGroups.UserGroupId;
					module.CreatedDate = DateTime.Now;
					module.CreatedUserId = ""; //To change when the authorization is implemented
				});
			_authDataService.SetUserGroupModules(userModules);
		}
		catch (Exception)
		{

			throw;
		}
		return true;
	}

	public List<ModuleViewModel> GetModules()
	{
		List<Modules> moduleList = _authDataService.GetModuleList();

		return Mapper.Map<List<ModuleViewModel>>(moduleList).OrderBy(x => x.LineNum).ToList();
	}

	public UserLogins GetUserLogin(string UserName)
	{
		UserLogins userLogins = _authDataService.GetUserLogin(UserName);
		return userLogins;
	}
}
