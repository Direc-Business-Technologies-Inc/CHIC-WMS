namespace Application.BlazorServer.Controls.UI_Macros
{
	public partial class MacroBreadcrumbs : ComponentBase
	{
		[Parameter]
		public RenderFragment ChildContent { get; set; } = builder => builder.AddContent(0, "");

		[Parameter]
		public dynamic Breadcrumbs { get; set; } = default!;

		[Inject] 
		protected IJSRuntime JSRuntime { get; set; } = default!;
		protected override void OnAfterRender(bool firstRender)
		{
			if (firstRender)
			{
				string breadcrumbsJson = System.Text.Json.JsonSerializer.Serialize(Breadcrumbs);
				JSRuntime.InvokeVoidAsync("InitializeBreadcrumbs", breadcrumbsJson);
			}
		}
	}
}
