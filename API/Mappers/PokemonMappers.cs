using API.Models.Static;
using API.DTOs.Pokemon;


namespace API.Mappers;

public static class PokemonMappers
{
    public static PokemonDTO ToPokemonDTO(this Pokemon pokemonModel)
    {
        return new PokemonDTO
        {
            Id = pokemonModel.Id,
            Identifier = pokemonModel.Identifier,
            SpeciesId = pokemonModel.SpeciesId,

            PokemonPkmnTypes = pokemonModel.PokemonPkmnTypes
                .OrderBy(x => x.Slot)
                .Select(x => x.PkmnType.Identifier)
                .ToList(),

            PokemonGenderIds = pokemonModel.Genders
                .Select(x => x.Id)
                .Order()
                .ToList(),

            PokemonAbilityIds = pokemonModel.PokemonAbilities
                .OrderBy(x => x.Slot)
                .Select(x => x.AbilityId)
                .ToList(),

            PokemonMoveIds = pokemonModel.PokemonMoves
                .Select(x => x.MoveId)
                .ToList(),

            BaseStats = pokemonModel.BaseStats.ToBaseStatsDTO() 
        };
    }
} 
