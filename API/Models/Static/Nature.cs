using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Static;

[Table("Nature")]
public class Nature
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public string Identifier { get; set; } = "";

    public double AttackMultiplier { get; set; }
    public double DefenseMultiplier { get; set; }
    public double SpecialAttackMultiplier { get; set; }
    public double SpecialDefenseMultiplier { get; set; }
    public double SpeedMultiplier { get; set; }
}