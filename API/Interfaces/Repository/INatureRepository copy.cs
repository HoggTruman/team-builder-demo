using API.Models.Static;

namespace API.Interfaces.Repository;

public interface IAbilityRepository
{
    List<Ability> GetAll();
    Ability? GetById(int id);
}