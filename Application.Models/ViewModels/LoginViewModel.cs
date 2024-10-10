using System.ComponentModel.DataAnnotations;

namespace Application.Models.ViewModels;

public class LoginViewModel
{
    public string? Username { get; set; }
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    public bool IsActive { get; set; }
    public bool IsLocked { get; set; }
    public bool SuperUser { get; set; }
    public int FailedAttemptCount { get; set; }
    public bool IsLockoutEnabled { get; set; }
    public bool IsTwoFactorEnabled { get; set; }
}
