using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Static;

[Table("PokemonPkmnType")]
public class PokemonPkmnType
{
    [ForeignKey("Pokemon")]
    public int PokemonId { get; set; }
    public Pokemon Pokemon { get; set; } = default!;

    [ForeignKey("PkmnType")]
    public int PkmnTypeId { get; set; }
    public PkmnType PkmnType { get; set; } = default!;
    
    public int Slot { get; set; }

}
