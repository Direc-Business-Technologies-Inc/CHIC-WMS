using Microsoft.AspNetCore.Components;

namespace Application.Models.ControlViewModels
{
	public class MacroTabsViewModel
	{
		public class TabsParameterModel
		{
			public RenderFragment[] RenderFragments { get; set; } = Array.Empty<RenderFragment>();
			public string[] TabLabels { get; set; } = Array.Empty<string>();
		}
	}
}
