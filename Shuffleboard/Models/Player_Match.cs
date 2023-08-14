#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shuffleboard.Models
{
    public class Player_Match
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("MatchId")]
        public int MatchId { get; set; }

        [ForeignKey("PlayerId")]
        public int PlayerId { get; set; }

        public int Score { get; set; }
        public bool Won { get; set; }

        [StringLength(4)]
        public string Color { get; set; }
    }
}
