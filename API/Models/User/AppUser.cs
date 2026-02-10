using Microsoft.AspNetCore.Identity;

namespace API.Models.User;

public class AppUser : IdentityUser
{
    public List<Team> Teams { get; } = [];
}