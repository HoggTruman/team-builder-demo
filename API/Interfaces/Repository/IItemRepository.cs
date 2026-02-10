using API.Models.Static;

namespace API.Interfaces.Repository;

public interface IItemRepository
{
    List<Item> GetAll();
    Item? GetById(int id);
}