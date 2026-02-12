using API.Models.Static;

namespace API.Repository;

public interface IItemRepository
{
    List<Item> GetAll();
    Item? GetById(int id);
}