using API.Models.Static;
using CsvHelper.Configuration;

namespace API.Mappers.CSVClassMaps
{
    public class PokemonMoveCSVMap : ClassMap<PokemonMove>
    {
        public PokemonMoveCSVMap()
        {
            Map(m => m.PokemonId).Index(0).Name("pokemon_id");
            Map(m => m.MoveId).Index(1).Name("move_id");
        }
    }
}