namespace Application.BlazorServer.Shared;

public partial class LoginLayout
{
    [Inject]
    IJSRuntime _jsRuntime { get; set; }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        //await _jsRuntime.InvokeVoidAsync("loadMain");
        //await _jsRuntime.InvokeVoidAsync("loadPageAuth");
        //await _jsRuntime.InvokeVoidAsync("loadAuthTwoSteps");
        //await _jsRuntime.InvokeVoidAsync("loadUiModals");
        //await _jsRuntime.InvokeVoidAsync("loadUiToasts");
    }
}