using API.Models.Static;

namespace API.Interfaces.Repository;

public interface INatureRepository
{
    List<Nature> GetAll();
    Nature? GetById(int id);
}