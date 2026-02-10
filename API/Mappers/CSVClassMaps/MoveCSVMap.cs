using API.Models.Static;
using CsvHelper.Configuration;

namespace API.Mappers.CSVClassMaps
{
    public class MoveCSVMap: ClassMap<Move>
    {
        public MoveCSVMap()
        {
            Map(m => m.Id).Index(0).Name("id");
            Map(m => m.Identifier).Index(1).Name("identifier");
            Map(m => m.Power).Index(2).Name("power");
            Map(m => m.PP).Index(3).Name("pp");
            Map(m => m.Accuracy).Index(4).Name("accuracy");
            Map(m => m.Priority).Index(5).Name("priority");
            Map(m => m.PkmnTypeId).Index(6).Name("type_id");
            Map(m => m.DamageClassId).Index(7).Name("damage_class_id");
            Map(m => m.MoveEffectId).Index(8).Name("effect_id");
            Map(m => m.MoveEffectChance).Index(9).Name("effect_chance");
        }
    }
}