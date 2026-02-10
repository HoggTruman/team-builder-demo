using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Static
{
    [Table("Ability")]
    public class Ability
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Identifier { get; set; } = "";
        public string FlavorText { get; set; } = "";

        public List<Pokemon> Pokemon { get; } = [];
    }
}