namespace DataManager.Models.Users;

[Table("USRL")]
public class UserLogins
{
    [Key]
    [Column(TypeName = "VARCHAR(32)")]
    public string UserId { get; set; }
    [Column(TypeName = "NVARCHAR(50)")]
    public string Username { get; set; }
    [Column(TypeName = "NVARCHAR(MAX)")]
    public string Password { get; set; }
    public bool IsSuperUser { get; set; }
    public bool IsActive { get; set; }
    public bool IsLocked { get; set; }
    [ForeignKey("UserGroups")]
    public int UserGroupId { get; set; } /* FK Scalar */
    public UserGroups UserGroup { get; set; }
    [Column(TypeName = "DATETIME2(7)")]
    public DateTime? LastLogin { get; set; }
    [Column(TypeName = "NVARCHAR(MAX)")]
    public string? LastPassword { get; set; }
    [Column(TypeName = "DATETIME2(7)")]
    public DateTime? LastPasswordSet { get; set; }
    public int FailedAttemptCount { get; set; }
    [Column(TypeName = "DATETIME2(7)")]
    public DateTime? FailedAttempt { get;set; }
    public bool IsLockedoutEnabled { get; set; }
    public bool IsTwoFactorEnabled { get; set; }
    [Column(TypeName = "DATETIME2(7)")]
    public DateTime? ResetAttempt { get; set; }
    [Column(TypeName = "DATETIME2(7)")]
    public DateTime CreatedDate { get; set; }
    [Column(TypeName = "VARCHAR(32)")]
    public string CreatedUserId { get; set; }
    [Column(TypeName = "DATETIME2(7)")]
    public DateTime? UpdatedDate { get; set; }
    [Column(TypeName = "VARCHAR(32)")]
    public string? UpdatedUserId { get; set; }
    [NotMapped]
    public UserDetails UserDetails { get; set; }
}
