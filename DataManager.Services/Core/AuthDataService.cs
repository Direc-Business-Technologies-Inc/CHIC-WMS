using Application.Libraries.SAP.SL;

namespace DataManager.Services.Core;

public class AuthDataService : IAuthDataService
{
	readonly Context _context;
	//readonly ILogger _logger;
	readonly AppSettings _appSettings;
	readonly IMySqlDataAccess _mySql;
	public AuthDataService(Context context, AppSettings appSettings, IMySqlDataAccess mySql)
	{
		_context = context;
		//_logger = logger;
		_appSettings = appSettings;
		_mySql = mySql;
	}

	public HttpResponseMessage Login(UserLogins login)
	{
		try
		{
			string? errMessage = "";
			string encryptedPass = Cryption.EncryptPassword(login.Username, login.Password);
			if (login is null)
			{
				errMessage = "No Data Found";
				return new HttpResponseMessage()
				{
					StatusCode = HttpStatusCode.InternalServerError,
					Content = new StringContent(errMessage)
				};
			}

			var s = _context.Database.GetConnectionString();

			List<UserLogins>? userInfo = _context.UserLogin
			.Where(x => x.Username == login.Username)
			.ToList();

			//List<UserLogins>? userInfo = _mySql.GetData<UserLogins, dynamic>(
			//	$"SELECT * FROM USRL WHERE Username = @Username AND Password = @Password",
			//	new { Username = login.Username, Password = encryptedPass },
			//	System.Data.CommandType.Text);

			//if (userInfo is null || !userInfo.Any()) return new HttpResponseMessage()
			if (userInfo is null) return new HttpResponseMessage()
			{
				StatusCode = HttpStatusCode.NotFound,
				Content = new StringContent("Username does not exists.")
			};

			UserLogins? user = userInfo?.FirstOrDefault() ?? new UserLogins();

			if (!user.IsActive) return new HttpResponseMessage()
			{
				StatusCode = HttpStatusCode.Unauthorized,
				Content = new StringContent("Username is inactive.")
			};

			bool userHasValidPassword = Cryption.DecryptPassword(user.Username, user.Password) == login.Password;

			if (!userHasValidPassword)
			{
				int attempt = user.FailedAttemptCount += 1;
				int result = 1;

				if (attempt > _appSettings.Attempt
				&& user.FailedAttempt?.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd")
				&& user.IsLockedoutEnabled)
				{
					_context.UserLogin.Add(new UserLogins
					{
						FailedAttempt = DateTime.Now,
						FailedAttemptCount = attempt
					});
					result = _context.SaveChanges();

					return new HttpResponseMessage()
					{
						StatusCode = HttpStatusCode.TooManyRequests,
						Content = new StringContent($"You {user.Username} have exceed max {_appSettings.Attempt} limit of login attempt.")
					};
				}

				if (result > 0)
				{
					if (attempt == _appSettings.Attempt && user.IsLockedoutEnabled) return new HttpResponseMessage()
					{
						StatusCode = HttpStatusCode.TooManyRequests,
						Content = new StringContent($"You {user.Username} have exceed max {_appSettings.Attempt} limit of login attempt.")
					};

					return new HttpResponseMessage()
					{
						StatusCode = HttpStatusCode.TooManyRequests,
						Content = new StringContent("Incorrect Password.")
					};
				}
				return new HttpResponseMessage()
				{
					StatusCode = HttpStatusCode.Unauthorized,
					Content = new StringContent($"({result}) Updating of attempt result failed. Please check with your administrator.")
				};
			}

			UserLogins uli = _context.UserLogin.Where(x => x.UserId == user.UserId).FirstOrDefault();
			uli.FailedAttemptCount = 0;
			uli.LastLogin = DateTime.Now;
			_context.UserLogin.Entry(uli);

			int successLogin = _context.SaveChanges();

			if (successLogin < 1) return new HttpResponseMessage()
			{
				StatusCode = HttpStatusCode.Unauthorized,
				Content = new StringContent($"({successLogin}) Updating of last login and attempt result failed. Please check with your administrator.")
			};

			return new HttpResponseMessage()
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json")
			};

		}
		catch (Exception)
		{
			throw;
			//errMessage = $"{ex.Message} Please check with your administrator.";
			////_logger.Log(LogLevel.Error, errMessage);
			//return new HttpResponseMessage()
			//{
			//	StatusCode = HttpStatusCode.InternalServerError,
			//	Content = new StringContent(errMessage)
			//};
		}
	}

	public UserLogins PostUser(UserLogins user)
	{
		try
		{
			_context.UserLogin.AddRange(user);
			_context.SaveChanges();
			return user;
		}
		catch (Exception)
		{

			throw;
		}
	}



	public UserLogins UpdateUser(UserLogins user)
	{
		try
		{
			var updateUser = _context.UserLogin.Where(x => x.UserId == user.UserId)
				.Include(x => x.UserDetails)
				.FirstOrDefault();
			updateUser.Username = user.Username;
			updateUser.Password = user.Password;
			updateUser.IsSuperUser = user.IsSuperUser;
			updateUser.IsActive = user.IsActive;
			updateUser.IsLocked = user.IsLocked;
			updateUser.UserGroupId = user.UserGroupId;
			updateUser.UserDetails = user.UserDetails;
			_context.Entry(updateUser).State = EntityState.Modified;
			_context.SaveChanges();
			return user;
		}
		catch (Exception)
		{
			throw;
		}
	}
	


	public List<UserLogins> GetUserLogins()
	{
		try
		{
			List<UserLogins> user = _context.UserLogin
				.Include(x => x.UserDetails)
				.ToList();
			return user;
		}
		catch (Exception)
		{

			throw;
		}
	}

	public UserLogins GetUserLogin(string UserName)
	{
		try
		{
			UserLogins user = _context.UserLogin.Where(x => x.Username == UserName)
				.Include(x => x.UserDetails)
				.Include(x => x.UserGroup)
				.ThenInclude(x => x.UserModules)
				.FirstOrDefault();
			return user;
		}
		catch (Exception)
		{

			throw;
		}
	}

	public List<UserGroups> GetUserGroups()
	{
		try
		{
			List<UserGroups> userGroups = _context.UserGroup.ToList();

			return userGroups;
		}
		catch (Exception)
		{

			throw;
		}
	}
	

	public UserGroups GetUserGroup(string Id)
	{
		try
		{
			UserGroups userGroups = _context.UserGroup.Where(x => x.UserGroupId == Convert.ToInt32(Id))
				.Include(x => x.UserLogins)
				.ThenInclude(x => x.UserDetails)
				.Include(x => x.UserModules)
				.FirstOrDefault();

			return userGroups;
		}
		catch (Exception)
		{

			throw;
		}
	}

	public UserGroups PostUserGroup(UserGroups user)
	{
		try
		{
			user.CreatedDate = DateTime.Now;
			_context.UserGroup.Add(user);
			_context.SaveChanges();
			return user;
		}
		catch (Exception)
		{

			throw;
		}
	}

	public UserGroups UpdateUserGroup(UserGroups user)
	{
		try
		{
			UserGroups userGroup = _context.UserGroup.Where(x => x.UserGroupId == user.UserGroupId).FirstOrDefault();
			userGroup.GroupName = user.GroupName;
			userGroup.IsActive = user.IsActive;
			userGroup.UpdatedDate = DateTime.Now;
			userGroup.UpdatedUserId = user.CreatedUserId; //To change when the authorization is implemented
			_context.Entry(userGroup).State = EntityState.Modified;
			_context.SaveChanges();
			return user;
		}
		catch (Exception)
		{

			throw;
		}
	}

	public bool SetUserGroup(List<string> userIds, int userGrpId)
	{
		try
		{
			//Clear userGrpId
			List<UserLogins> userList = _context.UserLogin.Where(x => x.UserGroupId == userGrpId)?.ToList() ?? new List<UserLogins>();
			foreach (var user in userList)
			{
				user.UserGroupId = 2;
				_context.Entry(user).State = EntityState.Modified;
			}


			//SET UserIds
			foreach (var Id in userIds)
			{
				UserLogins user = _context.UserLogin.Where(x => x.UserId == Id).FirstOrDefault();
				user.UserGroupId = userGrpId;
				_context.Entry(user).State = EntityState.Modified;
			}
			_context.SaveChanges();
		}
		catch (Exception)
		{

			throw;
		}
		return true;
	}

	public bool SetUserGroupModules(List<UserModules> userModules)
	{
		using (var dbTran = _context.Database.BeginTransaction())
		{
			try
			{
				if(userModules.Count() <= 0)
				{
					return true;
				}

				int Id = userModules.FirstOrDefault().UserGroupId;
				List<UserModules> removeUserModules = _context.UserModule.Where(x => x.UserGroupId == Id).ToList();
				//Clear
				_context.UserModule.RemoveRange(removeUserModules);
				_context.SaveChanges();

				//Save
				_context.UserModule.AddRange(userModules);
				_context.SaveChanges();

				dbTran.Commit();
				return true;
			}
			catch (Exception)
			{
				dbTran.Rollback();
				throw;
			}
		}
	}

	public UserGroups GetUserGroupAuthorizations(string Id)
	{
		try
		{
			UserGroups userGroups = _context.UserGroup.Where(x => x.UserGroupId == Convert.ToInt32(Id))
				.Include(x => x.UserModules)
				.FirstOrDefault();

			return userGroups;
		}
		catch (Exception)
		{

			throw;
		}
	}

	public List<Modules> GetModuleList()
	{
		try
		{
			List<Modules> modules = _context.Module.ToList();

			return modules;
		}
		catch (Exception)
		{

			throw;
		}
	}

	public List<PositionManagement> GetPositionList()
	{
		try
		{
			List<PositionManagement> positions = _context.PositionManagement.ToList();

			return positions;
		}
		catch (Exception)
		{

			throw;
		}
	}

	public PositionManagement PostPosition(PositionManagement position)
	{
		try
		{
			PositionManagement positionManagement = _context.PositionManagement.Where(x=>x.PosName == position.PosName).FirstOrDefault();
			if(positionManagement == null)
			{
				_context.PositionManagement.Add(position);
				_context.SaveChanges();
				return position;
			}
			else
			{
				return null;
			}
		}
		catch (Exception)
		{

			throw;
		}
	}
	public PositionManagement UpdatePosition(PositionManagement position)
	{
		try
		{
			var update_position = _context.PositionManagement.Where(x => x.PosId == position.PosId).FirstOrDefault();

			update_position.PosName = position.PosName;
			update_position.PosDesc = position.PosDesc;
			update_position.Classification = position.Classification;
			update_position.isActive = position.isActive;
			update_position.UpdatedUserId = position.UpdatedUserId;
			update_position.UpdatedDate = position.UpdatedDate;

			_context.Entry(update_position).State = EntityState.Modified;
			_context.SaveChanges();
			return position;
		}
		catch (Exception)
		{
			throw;
		}
	}

}