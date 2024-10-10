namespace DataManager.Models.Users;
[Table("USRG")]
public class UserGroups
{
    [Key]
    public int UserGroupId { get; set; }

    [Column(TypeName = "VARCHAR(100)")]
    public string GroupName { get; set; } = null!;

    public bool IsActive { get; set; } = true;
    [Column(TypeName = "DATETIME2(7)")]
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    [Column(TypeName = "VARCHAR(32)")]
    public string CreatedUserId { get; set; } = null!;
    [Column(TypeName = "DATETIME2(7)")]
    public DateTime? UpdatedDate { get; set; }
    [Column(TypeName = "VARCHAR(32)")]
    public string? UpdatedUserId { get; set; }
    [NotMapped]
    public ICollection<UserLogins> UserLogins { get; set; }
	[NotMapped]
	public ICollection<UserModules> UserModules { get; set; }
}
