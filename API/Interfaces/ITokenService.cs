using API.Models.User;

namespace API.Interfaces;

public interface ITokenService
{
    public string CreateToken(AppUser appUser);
}