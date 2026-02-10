using API.Models.Static;
using CsvHelper.Configuration;

namespace API.Mappers.CSVClassMaps
{
    public class BaseStatsCSVMap : ClassMap<BaseStats>
    {
        public BaseStatsCSVMap()
        {
            Map(m => m.PokemonId).Index(0).Name("PokemonId");
            Map(m => m.HP).Index(1).Name("HP");
            Map(m => m.Attack).Index(2).Name("Attack");
            Map(m => m.Defense).Index(3).Name("Defense");
            Map(m => m.SpecialAttack).Index(4).Name("SpecialAttack");
            Map(m => m.SpecialDefense).Index(5).Name("SpecialDefense");
            Map(m => m.Speed).Index(6).Name("Speed");
        }
    }
}

