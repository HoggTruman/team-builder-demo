using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Account;

public class RegisterDTO
{
    [Required]
    [MinLength(6, ErrorMessage = "Username must be at least 6 characters")]
    [MaxLength(20, ErrorMessage = "Username must not exceed 20 characters")]
    public required string UserName { get; set; }

    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    [MaxLength(20, ErrorMessage = "Password must not exceed 20 characters")]
    public required string Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public required string ConfirmPassword { get; set; }
}