using API.Models.Static;
using CsvHelper.Configuration;

namespace API.Mappers.CSVClassMaps
{
    public class AbilityCSVMap : ClassMap<Ability>
    {
        public AbilityCSVMap()
        {
            Map(m => m.Id).Index(0).Name("id");
            Map(m => m.Identifier).Index(1).Name("identifier");
            Map(m => m.FlavorText).Index(1).Name("flavor_text");
        }
    }
}

