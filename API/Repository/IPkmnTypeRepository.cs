using API.Models.Static;

namespace API.Repository;

public interface IPkmnTypeRepository
{
    List<PkmnType> GetAll();
    PkmnType? GetById(int id);
}