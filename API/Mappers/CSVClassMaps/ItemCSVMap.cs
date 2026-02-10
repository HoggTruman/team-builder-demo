using API.Models.Static;
using CsvHelper.Configuration;

namespace API.Mappers.CSVClassMaps
{
    public class ItemCSVMap : ClassMap<Item>
    {
        public ItemCSVMap()
        {
            Map(m => m.Id).Index(0).Name("id");
            Map(m => m.Identifier).Index(1).Name("identifier");
            Map(m => m.Effect).Index(2).Name("effect");
        }
    }
}

