using API.Models.Static;
using CsvHelper.Configuration;

namespace API.Mappers.CSVClassMaps
{
    public class PokemonAbilityCSVMap : ClassMap<PokemonAbility>
    {
        public PokemonAbilityCSVMap()
        {
            Map(m => m.PokemonId).Index(0).Name("pokemon_id");
            Map(m => m.AbilityId).Index(1).Name("ability_id");
            Map(m => m.Slot).Index(2).Name("slot");
        }
    }
}

