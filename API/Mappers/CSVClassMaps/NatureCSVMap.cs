using API.Models.Static;
using CsvHelper.Configuration;

namespace API.Mappers.CSVClassMaps
{
    public class NatureCSVMap : ClassMap<Nature>
    {
        public NatureCSVMap()
        {
            Map(m => m.Id).Index(0).Name("id");
            Map(m => m.Identifier).Index(1).Name("identifier");
            Map(m => m.AttackMultiplier).Index(2).Name("attack_multiplier");
            Map(m => m.DefenseMultiplier).Index(3).Name("defense_multiplier");
            Map(m => m.SpecialAttackMultiplier).Index(4).Name("special_attack_multiplier");
            Map(m => m.SpecialDefenseMultiplier).Index(5).Name("special_defense_multiplier");
            Map(m => m.SpeedMultiplier).Index(6).Name("speed_multiplier");
        }
    }
}