using API.Models.Static;
using CsvHelper.Configuration;

namespace API.Mappers.CSVClassMaps
{
    public class GenderCSVMap : ClassMap<Gender>
    {
        public GenderCSVMap()
        {
            Map(m => m.Id).Index(0).Name("id");
            Map(m => m.Identifier).Index(1).Name("identifier");
        }
    }
}

