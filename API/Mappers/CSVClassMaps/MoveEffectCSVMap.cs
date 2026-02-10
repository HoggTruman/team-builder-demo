using API.Models.Static;
using CsvHelper.Configuration;

namespace API.Mappers.CSVClassMaps
{
    public class MoveEffectCSVMap : ClassMap<MoveEffect>
    {
        public MoveEffectCSVMap()
        {
            Map(m => m.Id).Index(0).Name("move_effect_id");
            Map(m => m.Description).Index(1).Name("short_effect");
        }
    }
}

