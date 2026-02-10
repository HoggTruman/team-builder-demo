using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Static;

[Table("PkmnType")]
public class PkmnType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public required string Identifier { get; set; }

    public List<Pokemon> Pokemon { get; } = [];
}
