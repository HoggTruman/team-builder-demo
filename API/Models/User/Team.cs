using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.User
{
    [Table("Team")]
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("AppUser")]
        public string AppUserId { get; set; } = "";
        public AppUser AppUser { get; set; } = default!;

        public string TeamName { get; set; } = "";

        public List<UserPokemon> UserPokemon { get; set; } = [];
    }
}