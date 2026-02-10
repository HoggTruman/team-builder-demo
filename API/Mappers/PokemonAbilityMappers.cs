using API.Models.Static;
using API.DTOs.PokemonAbility;


namespace API.Mappers;

public static class PokemonAbilityMappers
{
    public static PokemonAbilityDTO ToPokemonAbilityDTO(this PokemonAbility pokemonAbilityModel)
    {
        return new PokemonAbilityDTO
        {
            Identifier = pokemonAbilityModel.Ability.Identifier,
            FlavorText = pokemonAbilityModel.Ability.FlavorText,              
        };
    }
} 
