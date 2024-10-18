
namespace Application.BlazorServer.SweetAlert2;
public static class SweetAlert2Helper
{
	public static async Task PromptAsync(this SweetAlertService swal, Func<Task> confirmCallback,
		Func<Task>? cancelCallback = null)
	{
		var result = await swal.FireAsync(SwalPromptOptions);
		if (result.IsConfirmed)
			await confirmCallback();
		else if (cancelCallback is { } callback)
			await callback();
	}


	public static async Task<bool> PromptAsync(this SweetAlertService swal)
	{
		var result = await swal.FireAsync(SwalPromptOptions);
		return result.IsConfirmed;
	}


	public static async Task<bool> PromptUnsavedChangesAsync(this SweetAlertService swal)
	{
		var result = await swal.FireAsync(SwalPromptUnsavedChangesOptions);
		return result.IsConfirmed;
	}


	public static readonly SweetAlertOptions SwalPromptOptions = new()
	{
		Title = "Are you sure you want to proceed?",
		Icon = SweetAlertIcon.Warning,
		ShowConfirmButton = true,
		ShowCancelButton = true,
		CustomClass = new()
		{
			ConfirmButton = "btn btn-primary me-1",
			CancelButton = "btn btn-label-secondary"
		},
		ButtonsStyling = false
	};

	public static readonly SweetAlertOptions SwalPromptUnsavedChangesOptions = new()
	{
		Title = "You may have unsaved changes!",
		Text = "Are you sure you want to discard all changes you may have made?",
		Icon = SweetAlertIcon.Warning,
		ShowConfirmButton = true,
		ShowCancelButton = true,
		CustomClass = new()
		{
			ConfirmButton = "btn btn-primary me-1",
			CancelButton = "btn btn-label-secondary"
		},
		ButtonsStyling = false
	};
}
