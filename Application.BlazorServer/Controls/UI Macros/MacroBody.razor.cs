namespace Application.BlazorServer.Controls.UI_Macros
{
	public partial class MacroBody
	{
		[Parameter]
		public RenderFragment ChildContent { get; set; } = builder => builder.AddContent(0, "");

	}
}
