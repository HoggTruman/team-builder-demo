using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Static;

[Table("PokemonMove")]
public class PokemonMove
{
    [ForeignKey("Pokemon")]
    public int PokemonId { get; set; }
    public Pokemon Pokemon { get; set; } = default!;

    [ForeignKey("Move")]
    public int MoveId { get; set; }
    public Move Move { get; set; } = default!;        
    
}
