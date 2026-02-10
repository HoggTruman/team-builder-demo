using API.Models.Static;
using CsvHelper.Configuration;

namespace API.Mappers.CSVClassMaps
{
    public class PokemonCSVMap : ClassMap<Pokemon>
    {
        public PokemonCSVMap()
        {
            Map(m => m.Id).Index(0).Name("id");
            Map(m => m.Identifier).Index(1).Name("identifier");
            Map(m => m.SpeciesId).Index(2).Name("species_id");
        }
    }
}

