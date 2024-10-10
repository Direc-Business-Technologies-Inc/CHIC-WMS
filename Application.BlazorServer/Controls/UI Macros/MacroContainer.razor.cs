namespace Application.BlazorServer.Controls.UI_Macros
{
	public partial class MacroContainer
	{
		[Parameter]
		public RenderFragment ChildContent { get; set; } = builder => builder.AddContent(0, "");

		[Parameter]
		public int Columns { get; set; } = 1;
	}
}
