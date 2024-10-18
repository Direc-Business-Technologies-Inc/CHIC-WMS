using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models.ViewModels;
public class UserViewModel
{
	//public List<UsersTableViewModel> UserDetailList { get; set; } = new List<UsersTableViewModel>();
	//public UserDetailsViewModel UserDetail { get; set; } = new UserDetailsViewModel();
	//public class UserDetailsViewModel
	//{
	//	public int Id { get; set; }
	//	public int UserId { get; set; }
	//	public string LastName { get; set; }
	//	public string FirstName { get; set; }
	//	public string MiddleName { get; set; }
	//	public string Company { get; set; }
	//	public string Department { get; set; }
	//	public string Email { get; set; }
	//	public bool IsEmailConfirmed { get; set; }
	//	public string PhoneNo { get; set; }
	//	public bool IsPhoneConfirmed { get; set; }
	//	public byte? Background { get; set; }
	//	public string DisplayPicture { get; set; }
	//	public DateTime CreateDate { get; set; }
	//	public int CreatedByUserId { get; set; }
	//	public DateTime? UpdateDate { get; set; }
	//	public int? UpdatedByUserId { get; set; }
	//}

	public List<UsersTableViewModel> UserTableList { get; set; } = new List<UsersTableViewModel>();
	public List<UsersTableViewModel> UserSelectedTableList { get; set; } = new List<UsersTableViewModel>();
    public UsersTableViewModel UsersTableDetails { get; set; } = new UsersTableViewModel();
    public class UsersTableViewModel
	{
		public string UserId { get; set; } = "";
		public int? UserGrpId { get; set; }
		public string UserName { get; set; } = "";
		public string Password { get; set; } = "";
		public bool SuperUser { get; set; }
		public bool IsActive { get; set; }
		public bool IsLocked { get; set; }
		public string FullName { get; set; } = "";
		public string LastName { get; set; } = "";
		public string FirstName { get; set; } = "";
		public string MiddleName { get; set; } = "";
		public string Company { get; set; } = "";
		public string Department { get; set; } = "";
		public string Email { get; set; } = "";
		public string PhoneNo { get; set; } = "";
	}


	public List<PositionManagementViewModel> PositionManagementList { get; set; } = new List<PositionManagementViewModel>();
	public PositionManagementViewModel PositionDetails { get; set; } = new PositionManagementViewModel();
	public class PositionManagementViewModel
	{
		public string PosManageID { get; set; }
		public string PositionName { get; set; }
		public string Description { get; set; }
		public string Classification { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreateDate { get; set; }
		public string CreatedByUserId { get; set; }
		public DateTime? UpdateDate { get; set; }
		public string UpdatedByUserId { get; set; }

	}
	public List<UsersTableColumnList> UsersColumnList { get; set; } = new List<UsersTableColumnList>
	{
		new UsersTableColumnList { Key = "UserName", Value = "User Name"},
		new UsersTableColumnList { Key = "FullName", Value = "Full Name"},
		new UsersTableColumnList { Key = "Email", Value = "Create Date"},
		new UsersTableColumnList { Key = "SuperUser", Value = "Create Date"},
		new UsersTableColumnList { Key = "IsActive", Value = "Create Date"},
		new UsersTableColumnList { Key = "IsLocked", Value = "User Count"}
	};

	public List<UserGroupsViewModel> UserGroupList { get; set; } = new List<UserGroupsViewModel>();
	public UserGroupsViewModel UserGroup { get; set; } = new UserGroupsViewModel();
	public class UserGroupsViewModel
	{
		public int UserGrpId { get; set; }
		public string Name { get; set; }
		public string ModuleId { get; set; }
		public DateTime CreateDate { get; set; }
		public int CreatedByUserId { get; set; }
		public DateTime? UpdateDate { get; set; }
		public int UserCount { get; set; }
		public int? UpdatedByUserId { get; set; }
		public bool IsActive { get; set; }
		public bool IsReadOnly { get; set; }
		public bool CanCreate { get; set; }
		public bool CanUpdate { get; set; }

	}
	public List<UsersTableColumnList> UserGroupsColumnList { get; set; } = new List<UsersTableColumnList>
	{
		new UsersTableColumnList { Key = "Code", Value = "Code"},
		new UsersTableColumnList { Key = "Name", Value = "Name"},
		new UsersTableColumnList { Key = "CreateDate", Value = "Create Date"},
		new UsersTableColumnList { Key = "UserCount", Value = "User Count"}
	};

	public List<AuthorizationViewModel> AuthList { get; set; } = new List<AuthorizationViewModel>();
	public AuthorizationViewModel Authorization { get; set; } = new AuthorizationViewModel();
	public class AuthorizationViewModel
	{
		public int UserAuthId { get; set; }
		public int UserId { get; set; }
		public DateTime CreateDate { get; set; }
		public int CreatedByUserId { get; set; }
		public DateTime? UpdateDate { get; set; }
		public int? UpdatedByUserId { get; set; }
		public List<AuthorizationAccessViewModel> AuthAccessList { get; set; } = new List<AuthorizationAccessViewModel>();
	}
	//public List<AuthorizationAccessViewModel> AuthAccessList { get; set; } = new List<AuthorizationAccessViewModel>();
	public AuthorizationAccessViewModel AuthorizationAccess { get; set; } = new AuthorizationAccessViewModel();
	public class AuthorizationAccessViewModel
	{
		public int Id { get; set; }
		public int UserAuthId { get; set; }
		public int ModuleId { get; set; }
		public bool IsCanCreate { get; set; }
		public bool IsReadOnly { get; set; }
		public bool IsCanUpdate { get; set; }
		public DateTime CreateDate { get; set; }
		public int CreatedByUserId { get; set; }
		public DateTime? UpdateDate { get; set; }
		public int? UpdatedByUserId { get; set; }
		///other fields
		public string ModuleCode { get; set; }
		public int UserGrpId { get; set; }
		public int UserId { get; set; }
	}


	public List<ModuleViewModel> ModuleList { get; set; } = new List<ModuleViewModel>();
	public ModuleViewModel Module { get; set; } = new ModuleViewModel();
	public class ModuleViewModel
	{
		public string ModuleId { get; set; }
		public int LineNum { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Icon { get; set; }
		public string WebLink { get; set; }
		public string IconGroup { get; set; }
		public string GroupName { get; set; }
		public string IconSubGroup { get; set; }
		public string SubGroupName { get; set; }
		public bool Active { get; set; }
	}
}
