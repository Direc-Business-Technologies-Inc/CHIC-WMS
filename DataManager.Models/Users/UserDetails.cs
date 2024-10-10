namespace DataManager.Models.Users;
[Table("USRD")]
public class UserDetails
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("UserLogins")]
    public string UserId { get; set; } /* FK Scalar */
    public UserLogins UserLogins { get; set; }
    [Column(TypeName = "VARCHAR(100)")]
    public string LastName { get; set; }
    [Column(TypeName = "VARCHAR(100)")]
    public string FirstName { get; set; }
    [Column(TypeName = "VARCHAR(100)")]
    public string? MiddleName { get; set; }
    public bool IsActive { get; set; }
    [Column(TypeName = "VARCHAR(100)")]
    public string? Company { get; set; }
    [Column(TypeName = "VARCHAR(50)")]
    public string? Department { get; set; }
    [Column(TypeName = "VARCHAR(100)")]
    public string Email { get; set; }
    public bool IsEmailConfirmed { get; set; }
    [Column(TypeName = "VARCHAR(50)")]
    public string? Phone { get; set; }
    public bool IsPhoneConfirmed { get; set; }
    public byte? Background { get; set; }
    public byte? DisplayPicture { get; set; }
    [Column(TypeName = "DATETIME2(7)")]
    public DateTime CreatedDate { get; set; }
    [Column(TypeName = "VARCHAR(32)")]
    public string CreatedUserId { get; set; }
    [Column(TypeName = "DATETIME2(7)")]
    public DateTime? UpdatedDate { get; set; }
    [Column(TypeName = "VARCHAR(32)")]
    public string? UpdatedUserId { get; set; }

}
