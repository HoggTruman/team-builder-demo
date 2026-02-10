using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using API.DTOS.Misc;

namespace API.DTOs.UserPokemon;

public class GetUserPokemonDTO
{
    public int Id { get; set; }

    [Range(1, 6, ErrorMessage = "{0} must be between {1} and {2}")]
    public int TeamSlot { get; set; }

    public int? PokemonId { get; set; }

    public string? Nickname { get; set; }

    [Range(1, 100, ErrorMessage = "{0} must be between {1} and {2}")]
    public int Level { get; set; } = 100;

    public int GenderId { get; set; } = 4;

    public bool Shiny { get; set; }

    public int TeraPkmnTypeId { get; set; } = 1;

    public int? ItemId { get; set; }

    public int? AbilityId { get; set; }

    public int? Move1Id { get; set; }
    public int? Move2Id { get; set; }
    public int? Move3Id { get; set; }
    public int? Move4Id { get; set; }

    public int NatureId { get; set; } = 1;


    [JsonPropertyName("hpEV")]
    public int HPEV { get; set; }
    public int AttackEV { get; set; }
    public int DefenseEV { get; set; }
    public int SpecialAttackEV { get; set; }
    public int SpecialDefenseEV { get; set; }
    public int SpeedEV { get; set; }


    [JsonPropertyName("hpIV")]
    public int HPIV { get; set; } = 31;
    public int AttackIV { get; set; } = 31;
    public int DefenseIV { get; set; } = 31;
    public int SpecialAttackIV { get; set; } = 31;
    public int SpecialDefenseIV { get; set; } = 31;
    public int SpeedIV { get; set; } = 31;
}