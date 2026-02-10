using API.Models.Static;
namespace API.Interfaces.Repository;

public interface IPokemonRepository
{
    List<Pokemon> GetAll();
    Pokemon? GetById(int id);
}