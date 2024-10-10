namespace Application.Models.ControlViewModels
{
	public class MacroInputViewModel
	{
		public class InputParameterModel
		{
			public string Title { get; set; }
			public string InputName { get; set; } = "";
			public bool IsReadOnly { get; set; } = false;
			public bool IsSelectable { get; set; } = false;
			public string Type { get; set; } = "text";
			public string DefaultValue { get; set; } = "";
			public string Id { get => InputName.ToLower().Replace(" ", "-") ?? ""; }
		}
	}
}
