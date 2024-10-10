using DataManager.Models.Users;

namespace DataManager.Models.Configurations;
[Table("MODL")]
public class Modules
{
    [Key]
    [Column(TypeName = "VARCHAR(20)")]
    public string ModuleId { get; set; } = null!;
    public int LineNum { get; set; }
    [Column(TypeName = "VARCHAR(100)")]

    public string Name { get; set; } = null!;

    [Column(TypeName = "VARCHAR(254)")]
    public string Description { get; set; } = null!;

    [Column(TypeName = "VARCHAR(50)")]
    public string Icon { get; set; } = null!;

    [Column(TypeName = "VARCHAR(50)")]
    public string WebLink { get; set; } = null!;

    [Column(TypeName = "VARCHAR(50)")]
    public string IconGroup { get; set; } = null!;

    [Column(TypeName = "VARCHAR(50)")]
    public string GroupName { get; set; } = null!;

    [Column(TypeName = "VARCHAR(50)")]
    public string IconSubGroup { get; set; } = null!;

    [Column(TypeName = "VARCHAR(50)")]
    public string SubGroupName { get; set; } = null!;

    [Column(TypeName = "DATETIME2(7)")]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    [Column(TypeName = "VARCHAR(32)")]
    public string CreatedUserId { get; set; } = null!;

    [Column(TypeName = "DATETIME2(7)")]
    public DateTime? UpdatedDate { get; set; }

    [Column(TypeName = "VARCHAR(32)")]
    public string? UpdatedUserId { get; set; }
    public bool Active { get; set; }

    [NotMapped]
    public ICollection<UserModules> UserModules { get; set; }
}
