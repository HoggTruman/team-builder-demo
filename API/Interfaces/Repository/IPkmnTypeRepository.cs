using API.Models.Static;

namespace API.Interfaces.Repository;

public interface IPkmnTypeRepository
{
    List<PkmnType> GetAll();
    PkmnType? GetById(int id);
}