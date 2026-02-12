using API.Models.Static;
namespace API.Repository;

public interface IPokemonRepository
{
    List<Pokemon> GetAll();
    Pokemon? GetById(int id);
}