using AutoMapper;
using DataManager.Models.Users;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using static Application.Models.ViewModels.UserViewModel;

namespace Application.BlazorServer.Security;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
	private readonly IMapper Mapper;
	private AuthenticationState authenticationState;

	public CustomAuthenticationStateProvider(IMapper mapper, AuthenticationService service)
    {
		Mapper = mapper;

		authenticationState = new AuthenticationState(service.CurrentUser);

		service.UserChanged += (newUser) =>
		{
			authenticationState = new AuthenticationState(newUser);

			NotifyAuthenticationStateChanged(
				Task.FromResult(new AuthenticationState(newUser)));
		};
	}

	public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
		Task.FromResult(authenticationState);

	public void AuthenticateUser(UserLogins userIdentifier)
	{
		List<UserGroupsViewModel> modules = Mapper.Map<List<UserGroupsViewModel>>(userIdentifier.UserGroup?.UserModules.ToList() ?? new List<UserModules>());

		var identity = new ClaimsIdentity(new[]
		{
			new Claim(ClaimTypes.Name, userIdentifier.UserDetails?.FirstName ?? ""),
			new Claim(ClaimTypes.Role, userIdentifier.UserGroup?.GroupName ?? ""),
			new Claim("ModuleAuthentications"
				, Newtonsoft.Json.JsonConvert.SerializeObject(modules)),
			new Claim("UserId"
				, userIdentifier.UserDetails?.UserId ?? "")
		}, "Custom Authentication");

		var user = new ClaimsPrincipal(identity);

		NotifyAuthenticationStateChanged(
			Task.FromResult(new AuthenticationState(user)));
	}

	public ClaimsIdentity GetClaimsIdentity(UserLogins userIdentifier)
	{
		List<UserGroupsViewModel> modules = Mapper.Map<List<UserGroupsViewModel>>(userIdentifier.UserGroup?.UserModules.ToList() ?? new List<UserModules>());

		var identity = new ClaimsIdentity(new[]
		{
			new Claim(ClaimTypes.Name, userIdentifier.UserDetails?.FirstName ?? ""),
			new Claim(ClaimTypes.Role, userIdentifier.UserGroup?.GroupName ?? ""),
			new Claim("ModuleAuthentications"
				, Newtonsoft.Json.JsonConvert.SerializeObject(modules)),
			new Claim("UserId"
				, userIdentifier.UserDetails?.UserId ?? "")
		}, "Custom Authentication");

		return identity;
	}
}