using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Models.ControlViewModels.MacroSelectTableViewModel;

namespace Application.Models.ControlViewModels
{
	public class MacroTableViewModel
	{
		public class MacroTables
		{
			public TableParameterModel TableParameter { get; set; } = new TableParameterModel();
		}
		public class UsersTableColumnList
		{
			public string Key { get; set; } = "";
			public string Value { get; set; } = "";
		}
		public class TableParameterModel
		{
			public string InputName { get; set; } = "";
			public string TableName { get; set; } = "";
			public string Id { get => InputName.ToLower().Replace(" ", "-").Replace("/", "").Replace(".", "") ?? ""; }
			public string TableId { get => TableName.ToLower().Replace(" ", "-").Replace("/", "").Replace(".", "") ?? ""; }
			public List<UsersTableColumnList> ParamTableColumns { get; set; } = new List<UsersTableColumnList>();
			public bool HasActions { get; set; } = false;
		}
	}
}
