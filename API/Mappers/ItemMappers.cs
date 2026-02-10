using API.DTOs.Item;
using API.Models.Static;

namespace API.Mappers;

public static class ItemMappers
{
    public static ItemDTO ToItemDTO(this Item itemModel)
    {
        return new ItemDTO
        {
            Id = itemModel.Id,
            Identifier = itemModel.Identifier,
            Effect = itemModel.Effect
        };
    }
}