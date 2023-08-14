#nullable disable
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shuffleboard.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }
        public ICollection<Player_Match> MatchId { get; set; }

        [Required(ErrorMessage = "The match cannot end with a stalemate")]
        [DataType(DataType.Date)]
        [DisplayName("Match Start Time")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [DisplayName("Match End Time")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get { return dateCreated ?? DateTime.Now; } set { this.dateCreated = value; } }
        private DateTime? dateCreated = null;
    }
}
