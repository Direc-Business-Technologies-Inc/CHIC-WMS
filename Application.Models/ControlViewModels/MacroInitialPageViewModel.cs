namespace Application.Models.ControlViewModels
{
	public class MacroInitialPageViewModel
	{
		public class InitialPageColumnList
		{
			public string Key { get; set; } = "";
			public string Value { get; set; } = "";
		}
		public class InitialPageParameterModel
		{
			public dynamic Breadcrumbs { get; set; } = default!;
			public string PageName { get; set; } = "";
			public string Link { get; set; } = "";
			public List<InitialPageColumnList> ParamTableColumns { get; set; } = new List<InitialPageColumnList>();
			public bool CanAdd { get; set; } = true;
		}
	}
}
