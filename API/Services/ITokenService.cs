using API.Models.User;

namespace API.Services;

public interface ITokenService
{
    public string CreateToken(AppUser appUser);
}