using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Static;

[Table("PokemonAbility")]
public class PokemonAbility
{
    [ForeignKey("Pokemon")]
    public int PokemonId { get; set; }
    public Pokemon Pokemon { get; set; } = default!;

    [ForeignKey("Ability")]
    public int AbilityId { get; set; }
    public Ability Ability { get; set; } = default!;
    
    public int Slot { get; set; }

}