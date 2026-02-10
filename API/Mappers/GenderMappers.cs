using API.DTOs.Gender;
using API.Models.Static;

namespace API.Mappers;

public static class GenderMappers
{
    public static GenderDTO ToGenderDTO(this Gender genderModel)
    {
        return new GenderDTO
        {
            Id = genderModel.Id,
            Identifier = genderModel.Identifier,
        };
    }
}