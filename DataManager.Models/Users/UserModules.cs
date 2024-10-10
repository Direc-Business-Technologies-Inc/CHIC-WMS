using DataManager.Models.Configurations;

namespace DataManager.Models.Users;

[Table("USRM")]
public class UserModules
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("UserGroupId")]
    public int UserGroupId { get; set; }
    public UserGroups UserGroups { get; set; }
    [ForeignKey("ModuleId")]
    public string ModuleId { get; set; }
    public Modules Modules { get; set; }
    public bool IsReadOnly { get; set; }
    public bool CanCreate { get; set; }
    public bool CanUpdate { get; set; }
    [Column(TypeName = "DATETIME2(7)")]
    public DateTime CreatedDate { get; set; }
    [Column(TypeName = "VARCHAR(32)")]
    public string CreatedUserId { get; set; }
    [Column(TypeName = "DATETIME2(7)")]
    public DateTime? UpdatedDate { get; set; }
    [Column(TypeName = "VARCHAR(32)")]
    public string? UpdatedUserId { get; set; }
}
