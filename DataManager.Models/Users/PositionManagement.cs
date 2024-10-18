namespace DataManager.Models.Users;


public class PositionManagement
{
	[Key]
	public int Id { get; set; }
	public string PosId { get; set; }
	[Column(Order = 3)]
	public string PosName { get; set; }
	public string PosDesc { get; set; }
	[Column(Order = 5)]
	public string Classification { get; set; }
	public bool isActive { get; set; }

	[Column(TypeName = "DATETIME2(7)")]
	public DateTime CreatedDate { get; set; }
	[Column(TypeName = "VARCHAR(32)")]
	public string CreatedUserId { get; set; }
	[Column(TypeName = "DATETIME2(7)")]
	public DateTime? UpdatedDate { get; set; }
	[Column(TypeName = "VARCHAR(32)")]
	public string? UpdatedUserId { get; set; }
}
