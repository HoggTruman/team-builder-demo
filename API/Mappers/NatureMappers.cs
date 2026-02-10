using API.Models.Static;
using API.DTOs.Nature;


namespace API.Mappers;

public static class NatureMappers
{
    public static NatureDTO ToNatureDTO(this Nature natureModel)
    {
        return new NatureDTO
        {
            Id = natureModel.Id,
            Identifier = natureModel.Identifier,
            AttackMultiplier = natureModel.AttackMultiplier,
            DefenseMultiplier = natureModel.DefenseMultiplier,
            SpecialAttackMultiplier = natureModel.SpecialAttackMultiplier,
            SpecialDefenseMultiplier = natureModel.SpecialDefenseMultiplier,
            SpeedMultiplier = natureModel.SpeedMultiplier           
        };
    }
} 
