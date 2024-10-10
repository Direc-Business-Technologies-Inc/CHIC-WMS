namespace Application.BlazorServer.Controls.UI_Macros
{
	public partial class MacroSelectModal
	{
		[Parameter]
		public string InputName { get; set; } = "";
		[Parameter]
		public string Size { get; set; } = "";
		[Parameter]
		public string Title { get; set; } = "";
		[Parameter]
		public RenderFragment ChildContent { get; set; } = builder => builder.AddContent(0, "");

		public string Id { get => InputName.ToLower().Replace(" ", "-"); }
	}
}
