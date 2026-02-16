using API.Models.Static;
using API.DTOs.Move;


namespace API.Mappers;

public static class MoveMappers
{
    public static GetMoveDTO ToMoveDTO(this Move moveModel)
    {
        return new GetMoveDTO
        {
            Id = moveModel.Id,
            Identifier = moveModel.Identifier,
            Power = moveModel.Power,
            PP = moveModel.PP,
            Accuracy = moveModel.Accuracy, 
            Priority = moveModel.Priority,

            PkmnType  = moveModel.PkmnType.Identifier,

            DamageClass  = moveModel.DamageClass.Identifier,
            MoveEffect = moveModel.MoveEffect != null? moveModel.MoveEffect.Description: "",
            MoveEffectChance = moveModel.MoveEffectChance
        };
    }
} 
