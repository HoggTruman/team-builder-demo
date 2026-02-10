using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs.UserPokemon;

public class CreateUserPokemonDTO : IValidatableObject
{
    [DefaultValue(1)]
    [Range(1, 6, ErrorMessage = "{0} must be between {1} and {2}")]
    public int TeamSlot { get; set; } = 1;

    [DefaultValue(null)]
    public int? PokemonId { get; set; }

    [DefaultValue(null)]
    public string? Nickname { get; set; }

    [DefaultValue(100)]
    [Range(1, 100, ErrorMessage = "{0} must be between {1} and {2}")]
    public int Level { get; set; } = 100;

    [DefaultValue(4)]
    public int GenderId { get; set; } = 4;

    [DefaultValue(false)]
    public bool Shiny { get; set; }

    [DefaultValue(1)]
    public int TeraPkmnTypeId { get; set; } = 1;

    [DefaultValue(null)]
    public int? ItemId { get; set; }

    [DefaultValue(null)]
    public int? AbilityId { get; set; }



    [DefaultValue(null)]
    public int? Move1Id { get; set; }

    [DefaultValue(null)]
    public int? Move2Id { get; set; }

    [DefaultValue(null)]
    public int? Move3Id { get; set; }

    [DefaultValue(null)]
    public int? Move4Id { get; set; }



    [DefaultValue(1)]
    public int NatureId { get; set; } = 1;




    [DefaultValue(0)]
    [Range(0, 252, ErrorMessage = "{0} must be between {1} and {2}")]
    [JsonPropertyName("hpEV")]
    public int HPEV { get; set; }

    [DefaultValue(0)]
    [Range(0, 252, ErrorMessage = "{0} must be between {1} and {2}")]
    public int AttackEV { get; set; }

    [DefaultValue(0)]
    [Range(0, 252, ErrorMessage = "{0} must be between {1} and {2}")]
    public int DefenseEV { get; set; }

    [DefaultValue(0)]
    [Range(0, 252, ErrorMessage = "{0} must be between {1} and {2}")]
    public int SpecialAttackEV { get; set; }

    [DefaultValue(0)]
    [Range(0, 252, ErrorMessage = "{0} must be between {1} and {2}")]
    public int SpecialDefenseEV { get; set; }

    [DefaultValue(0)]
    [Range(0, 252, ErrorMessage = "{0} must be between {1} and {2}")]
    public int SpeedEV { get; set; }




    [DefaultValue(31)]
    [Range(0, 31, ErrorMessage = "{0} must be between {1} and {2}")]
    [JsonPropertyName("hpIV")]
    public int HPIV { get; set; } = 31;

    [DefaultValue(31)]
    [Range(0, 31, ErrorMessage = "{0} must be between {1} and {2}")]
    public int AttackIV { get; set; } = 31;

    [DefaultValue(31)]
    [Range(0, 31, ErrorMessage = "{0} must be between {1} and {2}")]
    public int DefenseIV { get; set; } = 31;

    [DefaultValue(31)]
    [Range(0, 31, ErrorMessage = "{0} must be between {1} and {2}")]
    public int SpecialAttackIV { get; set; } = 31;

    [DefaultValue(31)]
    [Range(0, 31, ErrorMessage = "{0} must be between {1} and {2}")]
    public int SpecialDefenseIV { get; set; } = 31;

    [DefaultValue(31)]
    [Range(0, 31, ErrorMessage = "{0} must be between {1} and {2}")]
    public int SpeedIV { get; set; } = 31;


    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        const int MAX_EV_TOTAL = 510;

        int evSum = HPEV + AttackEV + DefenseEV + SpecialAttackEV + SpecialDefenseEV + SpeedEV;

        if (evSum > MAX_EV_TOTAL)
        {
            yield return new ValidationResult(
                "The total of a pokemon's EVs can not exceed 510"
            );
        }



        var moveIdList = new List<int?> {Move1Id, Move2Id, Move3Id, Move4Id};
        moveIdList.RemoveAll(x => x == null);
        var moveIdSet = new HashSet<int?>(moveIdList);

        if (moveIdList.Count != moveIdSet.Count)
        {
            yield return new ValidationResult(
                "Specified pokemon moves must be unique"
            );
        }
        
    }
}