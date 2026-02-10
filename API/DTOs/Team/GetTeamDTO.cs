using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using API.DTOs.UserPokemon;

namespace API.DTOs.Team;

public class GetTeamDTO
{
    public int Id { get; set; }
    public string TeamName { get; set; } = "";

    [MaxLength(6, ErrorMessage = "A team can not have more than 6 pokemon")]
    [JsonPropertyName("pokemon")]
    public List<GetUserPokemonDTO> UserPokemon { get; set; } = [];
}