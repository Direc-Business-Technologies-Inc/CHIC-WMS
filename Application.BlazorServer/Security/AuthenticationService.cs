using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Application.BlazorServer.Security;

public class AuthenticationService
{
	public event Action<ClaimsPrincipal>? UserChanged;
	private ClaimsPrincipal? currentUser;
	private DateTime _expirationTime;

	public ClaimsPrincipal CurrentUser
	{
		get
		{
			if (DateTime.UtcNow > _expirationTime)
			{
				return new();
			}
			return currentUser ?? new();
		}
		set
		{
			currentUser = value;

			if (UserChanged is not null)
			{
				UserChanged(currentUser);

				// Set expiration time to 1 hour from now
				_expirationTime = DateTime.UtcNow.AddHours(1);
			}
		}
	}

	public async Task<string> GetModuleAuthentications()
	{
		var auth = CurrentUser.FindFirstValue("ModuleAuthentications");
		return auth;
	}

	public async Task<string> GetUserId()
	{
		var auth = CurrentUser.FindFirstValue("UserId");
		return auth;
	}

	public async Task Logout()
	{
		CurrentUser = new();
	}
}
