using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Static;

[Table("Move")]
public class Move
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public string Identifier { get; set; } = "";
    public int? Power { get; set; }

    [Column("pp")]
    public int? PP { get; set; }
    public int? Accuracy { get; set; }
    public int Priority { get; set; }

    [ForeignKey("PkmnType")]
    public int PkmnTypeId { get; set; }
    public PkmnType PkmnType { get; set; } = default!;

    [ForeignKey("DamageClass")]
    public int DamageClassId { get; set; }
    public DamageClass DamageClass { get; set; } = default!;

    [ForeignKey("MoveEffect")]
    public int? MoveEffectId { get; set; }
    public MoveEffect? MoveEffect { get; set; }

    public int? MoveEffectChance { get; set; }


    public List<Pokemon> Pokemon { get; } = [];
}
