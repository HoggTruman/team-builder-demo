using API.DTOs.Team;
using API.Models.User;

namespace API.Interfaces.Repository;

public interface ITeamRepository
{
    List<Team> GetTeams(string userId);

    Team? GetTeamById(int id, string userId);

    Team CreateTeam(CreateUpdateTeamDTO createTeamDTO, string userId);

    Team? UpdateTeamById(int id, CreateUpdateTeamDTO updateTeamDTO, string userId);

    Team? DeleteTeamById(int id, string userId);

    List<Team> CreateTeams(List<CreateTeamsDTO> teamDTOs, string userId);
}