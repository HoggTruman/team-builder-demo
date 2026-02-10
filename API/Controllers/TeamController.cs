using System.Security.Claims;
using API.DTOs.Team;
using API.Interfaces.Repository;
using API.Mappers;
using API.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/team")]
public class TeamController : ControllerBase
{
    private readonly ITeamRepository _repository;
    private readonly UserManager<AppUser> _userManager;

    public TeamController(ITeamRepository repository, UserManager<AppUser> userManager)
    {
        _repository = repository;
        _userManager = userManager;
    }


    [HttpGet("~/api/teams")]
    [Authorize]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(IEnumerable<GetTeamDTO>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetTeams()
    {
        var userName = User.FindFirstValue(ClaimTypes.GivenName)!;
        var appUser = await _userManager.FindByNameAsync(userName);

        if (appUser == null)
            return Unauthorized();

        var teams = _repository.GetTeams(appUser.Id);

        return Ok(teams.Select(x => x.ToGetTeamDTO()));
    }


    [HttpGet("{id:int}")]
    [Authorize]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(GetTeamDTO))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTeamById([FromRoute] int id)
    {
        var userName = User.FindFirstValue(ClaimTypes.GivenName)!;
        var appUser = await _userManager.FindByNameAsync(userName);

        if (appUser == null)
            return Unauthorized();

        var team = _repository.GetTeamById(id, appUser.Id);

        if (team == null)
        {
            return NotFound();
        }

        return Ok(team.ToGetTeamDTO());
    }


    [HttpPost]
    [Authorize]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(GetTeamDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateTeam([FromBody] CreateUpdateTeamDTO createTeamDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userName = User.FindFirstValue(ClaimTypes.GivenName)!;
        var appUser = await _userManager.FindByNameAsync(userName);

        if (appUser == null)
            return Unauthorized();
        
        var team = _repository.CreateTeam(createTeamDTO, appUser.Id);

        return CreatedAtAction(nameof(GetTeamById), new { id = team.Id }, team.ToGetTeamDTO());
    }


    [HttpPut("{id:int}")]
    [Authorize]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(GetTeamDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTeam([FromRoute] int id, [FromBody] CreateUpdateTeamDTO updateTeamDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userName = User.FindFirstValue(ClaimTypes.GivenName)!;
        var appUser = await _userManager.FindByNameAsync(userName);

        if (appUser == null)
            return Unauthorized();

        var team = _repository.UpdateTeamById(id, updateTeamDTO, appUser.Id);

        if (team == null)
            return NotFound();

        return Ok(team.ToGetTeamDTO());
    }


    [HttpDelete("{id:int}")]
    [Authorize]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTeam([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userName = User.FindFirstValue(ClaimTypes.GivenName)!;
        var appUser = await _userManager.FindByNameAsync(userName);

        if (appUser == null)
            return Unauthorized();

        var team = _repository.DeleteTeamById(id, appUser.Id);

        if (team == null)
            return NotFound();

        return NoContent();
    }


    [HttpPost("~/api/teams")]
    [Authorize]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(IEnumerable<GetTeamDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateTeams([FromBody] List<CreateTeamsDTO> teamDTOs)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userName = User.FindFirstValue(ClaimTypes.GivenName)!;
        var appUser = await _userManager.FindByNameAsync(userName);

        if (appUser == null)
            return Unauthorized();

        var teams = _repository.CreateTeams(teamDTOs, appUser.Id);

        return Ok(teams.Select(x => x.ToGetTeamDTO()));
    }
}