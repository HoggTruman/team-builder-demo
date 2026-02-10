using API.Models.Static;
using API.DTOs.BaseStats;


namespace API.Mappers;

public static class BaseStatsMappers
{
    public static BaseStatsDTO ToBaseStatsDTO(this BaseStats baseStatsModel)
    {
        return new BaseStatsDTO
        {
            HP = baseStatsModel.HP,
            Attack = baseStatsModel.Attack,
            Defense = baseStatsModel.Defense,
            SpecialAttack = baseStatsModel.SpecialAttack,
            SpecialDefense = baseStatsModel.SpecialDefense,
            Speed = baseStatsModel.Speed           
        };
    }
} 
