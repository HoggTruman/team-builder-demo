using API.Models.Static;

namespace API.Repository;

public interface INatureRepository
{
    List<Nature> GetAll();
    Nature? GetById(int id);
}