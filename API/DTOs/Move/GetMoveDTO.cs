using System.Text.Json.Serialization;

namespace API.DTOs.Move
{
    public class GetMoveDTO
    {
        public int Id { get; set; }
        public string Identifier { get; set; } = "";
        public int? Power { get; set; }
        [JsonPropertyName("pp")]
        public int? PP { get; set; }
        public int? Accuracy { get; set; }
        public int Priority { get; set; }

        [JsonPropertyName("type")]
        public string PkmnType { get; set; } = "";

        public string DamageClass { get; set; } = "";

        public string MoveEffect { get; set; } = "";
        public int? MoveEffectChance { get; set; }
    }
}