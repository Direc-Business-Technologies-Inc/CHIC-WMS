using DataManager.Libraries.Repositories;

namespace Application.Services.Core;

public class AuthLoginService : IAuthLoginService
{
	IAuthDataService _authData;
	IMySqlDataAccess _mySql;
	public AuthLoginService(IAuthDataService authData, IMySqlDataAccess mySql)
	{
		_authData = authData;
		_mySql = mySql;
	}

	public async Task<bool> LoginResult(LoginViewModel login)
	{
		try
		{
			UserLogins user = new()
			{
				Username = login.Username,
				Password = login.Password
			};

			HttpResponseMessage? isSuccess = _authData.Login(user);

			//insert condition here to see if the RunAsync function was successful
			return await Task.FromResult(isSuccess.IsSuccessStatusCode);
		}
		catch (Exception)
		{

			throw;
		}

	}
}
