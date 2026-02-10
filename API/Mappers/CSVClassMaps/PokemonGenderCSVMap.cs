using API.Models.Static;
using CsvHelper.Configuration;

namespace API.Mappers.CSVClassMaps
{
    public class PokemonGenderCSVMap : ClassMap<PokemonGender>
    {
        public PokemonGenderCSVMap()
        {
            Map(m => m.PokemonId).Index(0).Name("PokemonId");
            Map(m => m.GenderId).Index(1).Name("GenderId");
        }
    }
}