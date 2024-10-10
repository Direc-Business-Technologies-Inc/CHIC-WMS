namespace Application.Models.ControlViewModels
{
	public class MacroSelectTableViewModel
	{
		public class MacroSelectTables
		{
			public SelectTableParameterModel SelectTableParameter { get; set; } = new SelectTableParameterModel();
			public string Id { get; set; } = "";
		}
		public class SelectTableColumnList
		{
			public string Key { get; set; } = "";
			public string Value { get; set; } = "";
		}

		public class SelectTableParameterModel
		{
			public string InputName { get; set; } = "";
			public string TableName { get; set; } = "";
			public string Id { get => InputName.ToLower().Replace(" ", "-") ?? ""; }
			public string TableId { get => TableName.ToLower().Replace(" ", "-") ?? ""; }
			public List<SelectTableColumnList> ParamTableColumns { get; set; } = new List<SelectTableColumnList>();
		}

	}
}
