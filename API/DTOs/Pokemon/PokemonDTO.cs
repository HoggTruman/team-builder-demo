using System.Text.Json.Serialization;
using API.DTOs.PokemonAbility;
using API.DTOs.BaseStats;

namespace API.DTOs.Pokemon
{
    public class PokemonDTO
    {
        public int Id { get; set; }
        public string? Identifier { get; set; }
        public int SpeciesId { get; set; }

        [JsonPropertyName("types")]
        public List<string> PokemonPkmnTypes { get; set; } = [];
        [JsonPropertyName("genders")]
        public List<int> PokemonGenderIds { get; set; } = [];
        [JsonPropertyName("abilities")]
        public List<int> PokemonAbilityIds { get; set; } = [];
        [JsonPropertyName("moves")]
        public List<int> PokemonMoveIds { get; set; } = [];

        public BaseStatsDTO BaseStats { get; set; } = new BaseStatsDTO();

    }
}