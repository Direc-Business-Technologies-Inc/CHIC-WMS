using Application.BlazorServer.Security;
using System.Diagnostics.Metrics;
using System.Security.Claims;
namespace Application.BlazorServer.Shared;

public partial class MainLayout
{
	[Inject] IJSRuntime _jsRuntime { get; set; }

	[Inject] NavigationManager _navigationManager { get; set; }
	[Inject] AuthenticationService _authenticationService { get; set; }

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		string currentUri = _navigationManager.Uri.ToString();
		string baseUri = _navigationManager.BaseUri.ToString();

		string uri = currentUri.Replace(baseUri.Remove(baseUri.Length - 1, 1), "");

		//balik sa login kapag hindi authorized or session expired
		if (NotAuthorized())
		{
			//Exclude Login and Dashboard Notification
			if (!(uri == "/" || uri == "/LoginAccount" || uri == "/DashboardNotification"))
			{
				_navigationManager.NavigateTo("");
				return;
			}
		}

		await _jsRuntime.InvokeVoidAsync("loadMain");
		//await _jsRuntime.InvokeVoidAsync("loadDashboardAnalytics");
		//await _jsRuntime.InvokeVoidAsync("loadFormWizardIcons");


		await _jsRuntime.InvokeVoidAsync("ActivateNavlinks", uri);

		if (!firstRender)
		{
			string RemovePopOver = @"
						const popoverTriggerList = document.querySelectorAll('.popover');

						if (popoverTriggerList.length > 0) {
						  popoverTriggerList.forEach(element => {
							element.remove();
						  });
						}
					";

			await _jsRuntime.InvokeVoidAsync("eval", RemovePopOver);


			string AdjustCalendarView = @"
					$("".rz-datepicker-title"").each(function () {
						$(this).addClass(""d-flex"");
						$(this).addClass(""align-items-center"");
						$(this).addClass(""justify-content-center"");
					});

					$("".rz-datepicker-next"").each(function () {
						$(this).addClass(""d-flex"");
						$(this).addClass(""align-items-center"");
					});

					$("".rz-datepicker-prev"").each(function () {
						$(this).addClass(""d-flex"");
						$(this).addClass(""align-items-center"");
					});";

			await _jsRuntime.InvokeVoidAsync("eval", AdjustCalendarView);

			string AddRegexToDates = @"
					// Corrected query selector
					const inputDateRangeRegexElement = document.querySelector('input[type=""text""][name=""daterange""]');

					if (inputDateRangeRegexElement) {
						inputDateRangeRegexElement.addEventListener('input', function(event) {
							const regex = /^(\d{2}\/\d{2}\/\d{4}) - (\d{2}\/\d{2}\/\d{4})$/;
							const inputValue = event.target.value;

							if (!regex.test(inputValue)) {
								// Input value does not match the regex pattern
								console.log('Invalid date range format!');
								// Prevent default behavior to block the user input
								event.preventDefault();
							} else {
								// Input value matches the regex pattern
								console.log('Valid date range format:', inputValue);
								// You can add further actions here for valid input if needed
							}
						});
					} else {
						console.log('Input element not found.');
					}
					";

			await _jsRuntime.InvokeVoidAsync("eval", AddRegexToDates);
		}
	}

	public bool NotAuthorized()
	{
		return _authenticationService.GetModuleAuthentications().Result == null;
	}
}