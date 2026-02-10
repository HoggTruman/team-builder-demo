using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Static;

[Table("Pokemon")]
public class Pokemon
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public required string Identifier { get; set; }
    public int SpeciesId { get; set; }


    public List<PkmnType> PkmnTypes { get; } = [];
    public List<PokemonPkmnType> PokemonPkmnTypes { get; } = [];

    public List<Gender> Genders { get; } = [];

    public List<Ability> Abilities { get; } = [];
    public List<PokemonAbility> PokemonAbilities { get; } = [];

    public List<Move> Moves { get; } = [];
    public List<PokemonMove> PokemonMoves { get; } = [];

    public BaseStats BaseStats { get; set; } = default!;
}
