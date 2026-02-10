using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Static;

[Table("PokemonGender")]
public class PokemonGender
{
    [ForeignKey("Pokemon")]
    public int PokemonId { get; set; }
    public Pokemon Pokemon { get; set; } = default!;

    [ForeignKey("Gender")]
    public int GenderId { get; set; }
    public Gender Gender { get; set; } = default!;
}
