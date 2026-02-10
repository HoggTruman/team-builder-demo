using API.Models.Static;

namespace API.Interfaces.Repository;

public interface IMoveRepository
{
    List<Move> GetAll();
    List<Move>? GetMovesByPokemonId(int id);
}