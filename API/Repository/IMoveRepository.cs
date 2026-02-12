using API.Models.Static;

namespace API.Repository;

public interface IMoveRepository
{
    List<Move> GetAll();
    List<Move>? GetMovesByPokemonId(int id);
}