using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Account;

public class AuthorizedUserDTO
{
    [Required]
    public required string UserName { get; set; }
    [Required]
    public required string Token { get; set; }
}