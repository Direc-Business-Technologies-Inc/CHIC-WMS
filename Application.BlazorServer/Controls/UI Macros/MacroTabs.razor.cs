namespace Application.BlazorServer.Controls.UI_Macros
{
	public partial class MacroTabs
	{
        [Parameter]
        public TabsParameterModel TabsParameter { get; set; } = new TabsParameterModel();

        private int activeTabIndex = 0;
	}
}
