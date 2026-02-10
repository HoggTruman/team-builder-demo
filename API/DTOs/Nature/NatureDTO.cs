namespace API.DTOs.Nature;

public class NatureDTO
{
    public int Id { get; set; }
    public string Identifier { get; set; } = "";

    public double AttackMultiplier { get; set; }
    public double DefenseMultiplier { get; set; }
    public double SpecialAttackMultiplier { get; set; }
    public double SpecialDefenseMultiplier { get; set; }
    public double SpeedMultiplier { get; set; }
}