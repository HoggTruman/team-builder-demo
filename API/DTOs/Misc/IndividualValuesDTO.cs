using System.ComponentModel.DataAnnotations;

namespace API.DTOS.Misc;

public class IndividualValuesDTO
{
    [Range(0, 31, ErrorMessage = "{0} IV must be between {1} and {2}")]
    public int HP { get; set; }

    [Range(0, 31, ErrorMessage = "{0} IV must be between {1} and {2}")]
    public int Attack { get; set; }

    [Range(0, 31, ErrorMessage = "{0} IV must be between {1} and {2}")]
    public int Defense { get; set; }

    [Range(0, 31, ErrorMessage = "{0} IV must be between {1} and {2}")]
    public int SpecialAttack { get; set; }

    [Range(0, 31, ErrorMessage = "{0} IV must be between {1} and {2}")]
    public int SpecialDefense { get; set; }

    [Range(0, 31, ErrorMessage = "{0} IV must be between {1} and {2}")]
    public int Speed { get; set; }
}