using API.Models.Static;
using API.DTOs.Ability;


namespace API.Mappers;

public static class AbilityMappers
{
    public static AbilityDTO ToAbilityDTO(this Ability abilityModel)
    {
        return new AbilityDTO
        {
            Id = abilityModel.Id,
            Identifier = abilityModel.Identifier,
            FlavorText = abilityModel.FlavorText   
        };
    }
} 
