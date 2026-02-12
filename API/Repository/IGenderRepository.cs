using API.Models.Static;

namespace API.Repository;

public interface IGenderRepository
{
    List<Gender> GetAll();
    Gender? GetById(int id);
}