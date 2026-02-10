using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Static;

[Table("MoveEffect")]
public class MoveEffect
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public string Description { get; set; } = "";


    public List<Move> Moves { get; } = [];        
}
