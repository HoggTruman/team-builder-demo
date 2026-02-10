using System.Text.Json.Serialization;

namespace API.DTOs.BaseStats
{
    public class BaseStatsDTO
    {
        [JsonPropertyName("hp")]
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }
    }
}