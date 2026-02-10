using API.DTOs.Account;
using API.Interfaces;
using API.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }




    [HttpPost("login")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(AuthorizedUserDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.FindByNameAsync(loginDTO.UserName);

        if (user != null)
        {
            var loginResult = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

            if (loginResult.Succeeded)
            {
                return Ok(
                    new AuthorizedUserDTO
                    {
                        UserName = loginDTO.UserName,
                        Token = _tokenService.CreateToken(user)
                    }
                );
            }
        }

        return Unauthorized("Incorrect username/password");
    }




    [HttpPost("register")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(AuthorizedUserDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);


        var newAppUser = new AppUser()
        {
            UserName = registerDTO.UserName,
        };

        var userCreationResult = await _userManager.CreateAsync(newAppUser, registerDTO.Password);

        if (userCreationResult.Succeeded)
        {
            var roleResult = await _userManager.AddToRoleAsync(newAppUser, "User");

            if (roleResult.Succeeded)
            {
                return Ok(
                    new AuthorizedUserDTO
                    {
                        UserName = newAppUser.UserName,
                        Token = _tokenService.CreateToken(newAppUser)
                    }
                );
            }
            else
            {
                return StatusCode(500, roleResult.Errors);
            }
        }
        else 
        {
            return StatusCode(500, userCreationResult.Errors);
        } 
    }
}