using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Models.Static;

namespace API.Models.User;

[Table("UserPokemon")]
public class UserPokemon
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }


    [ForeignKey("Team")]
    public int TeamId { get; set; }
    public Team Team { get; set; } = null!;

    public int TeamSlot { get; set; }


    [ForeignKey("Pokemon")]
    public int? PokemonId { get; set; }
    public Pokemon? Pokemon { get; set; } = null!;

    public string? Nickname { get; set; }

    public int Level { get; set; } = 100;

    [ForeignKey("Gender")]
    public int GenderId { get; set; } = 4;
    public Gender Gender { get; set; } = null!;

    public bool Shiny { get; set; }

    [ForeignKey("TeraPkmnType")]
    public int TeraPkmnTypeId { get; set; } = 1;
    public PkmnType TeraPkmnType { get; set; } = null!;


    [ForeignKey("Item")]
    public int? ItemId { get; set; }
    public Item? Item { get; set; }

    [ForeignKey("Ability")]
    public int? AbilityId { get; set; }
    public Ability? Ability { get; set; } = null!;


    [ForeignKey("Move1")]
    public int? Move1Id { get; set; }
    public Move? Move1 { get; set; }

    [ForeignKey("Move2")]
    public int? Move2Id { get; set; }
    public Move? Move2 { get; set; }

    [ForeignKey("Move3")]
    public int? Move3Id { get; set; }
    public Move? Move3 { get; set; }

    [ForeignKey("Move4")]
    public int? Move4Id { get; set; }
    public Move? Move4 { get; set; }


    [ForeignKey("Nature")]
    public int NatureId { get; set; } = 1;
    public Nature Nature { get; set; } = null!;


    public int HPEV { get; set; }
    public int AttackEV { get; set; }
    public int DefenseEV { get; set; }
    public int SpecialAttackEV { get; set; }
    public int SpecialDefenseEV { get; set; }
    public int SpeedEV { get; set; }


    public int HPIV { get; set; } = 31;
    public int AttackIV { get; set; } = 31;
    public int DefenseIV { get; set; } = 31;
    public int SpecialAttackIV { get; set; } = 31;
    public int SpecialDefenseIV { get; set; } = 31;
    public int SpeedIV { get; set; } = 31;
}   