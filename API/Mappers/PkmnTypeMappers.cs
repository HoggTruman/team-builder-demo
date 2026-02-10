using API.Models.Static;
using API.DTOs.PkmnType;


namespace API.Mappers;

public static class PkmnTypeMappers
{
    public static PkmnTypeDTO ToPkmnTypeDTO(this PkmnType pkmnTypeModel)
    {
        return new PkmnTypeDTO
        {
            Id = pkmnTypeModel.Id,
            Identifier = pkmnTypeModel.Identifier   
        };
    }
} 
