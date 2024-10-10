using Application.BlazorServer.Security;
using DataManager.Services.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Security.Claims;

namespace Application.BlazorServer.Pages.Security;
public partial class AuthLogin : ComponentBase
{
	[Inject] IJSRuntime? _jsRuntime { get; set; }
	[Inject] NavigationManager? _navManager { get; set; }
	[Inject] IAuthLoginService? _authloginrepo { get; set; }
	[Inject] IAdministrativeService? _adminrepo { get; set; }
	[Inject] AuthenticationStateProvider _authenticationStateProvider { get; set; }
	[Inject] AuthenticationService _authenticationService { get; set; }
	string _userName = string.Empty;
	private bool _Unsuccessful { get; set; } = false;

	LoginViewModel _loginViewModel { get; set; } = new();

	private IJSObjectReference _js { get; set; } = default!;

	string? _resetEmail { get; set; }
    //string? _userName { get; set; }
    //string? _password { get; set; }

    protected override async Task OnInitializedAsync()
    {
        string currentUri = _navManager.Uri.ToString();
        string baseUri = _navManager.BaseUri.ToString();

        string uri = currentUri.Replace(baseUri.Remove(baseUri.Length - 1, 1), "");

        var ModuleAuthentications = _authenticationService.GetModuleAuthentications().Result;

		//If Authorized then pumunta sa base site, automatic punta sa home
        if (ModuleAuthentications != null && uri == "/")
        {
            _navManager.NavigateTo("Home");
            return;
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			_js = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "../scripts/CustomScripts/AuthLogin.js");

			await _js.InvokeVoidAsync("loadPageAuth");
			// var authentication = await _authenticationState;
			// var userName = authentication?.User?.Identity?.Name;
			// if (userName != null) _navManager.NavigateTo("/Home", true);
			//For Calling of backend function from JS
			await _js.InvokeVoidAsync("setdotnetInstance", DotNetObjectReference.Create(this));
		}
	}

	async Task ShowResult(string result, string message)
	{
		await _jsRuntime.InvokeVoidAsync("ShowResult", result, message);
	}


	//async Task Login(LoginViewModel login)
	[JSInvokable("Login")]
	public async Task Login()
	{
		try
		{
			//Authenticate user
			if (await _authloginrepo.LoginResult(_loginViewModel))
			{
				var currentUser = _authenticationService.CurrentUser;

				//Get User Details
				DataManager.Models.Users.UserLogins userIdentifier = _adminrepo.GetUserLogin(_loginViewModel.Username);

				//Authorize user
				var identity = ((CustomAuthenticationStateProvider)_authenticationStateProvider)
					.GetClaimsIdentity(userIdentifier);

				var newUser = new ClaimsPrincipal(identity);

				_authenticationService.CurrentUser = newUser;

				_navManager?.NavigateTo("/Home", true);
			}
			else
			{
				_Unsuccessful = true;
				StateHasChanged();
				//throw new Exception("Login Failed");
			}
		}
		catch (Exception)
		{

			throw;
		}

	}
}