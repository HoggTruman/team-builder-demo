using API.Models.Static;

namespace API.Interfaces.Repository;

public interface IGenderRepository
{
    List<Gender> GetAll();
    Gender? GetById(int id);
}