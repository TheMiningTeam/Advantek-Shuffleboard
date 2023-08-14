#nullable disable
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shuffleboard.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        public ICollection<Player_Match> PlayerId { get; set; }
        public ICollection<BestPlayerDaily> BPDPlayerId { get; set; }

        [DisplayName("Date Created")]
        public DateTime Created { get; set; } = DateTime.Now;

        [DisplayName("Date Modified")]
        public DateTime Modified { get { return dateModified ?? DateTime.Now; } set { this.dateModified = value; } }
        private DateTime? dateModified = null;

        public bool Deleted { get; set; }

        [DisplayName("First Name")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Required(ErrorMessage = "A first name is required")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [Required(ErrorMessage = "A last name is required")]
        public string LastName { get; set; }

        [DisplayName("Nickname")]
        [StringLength(50, ErrorMessage = "Nickname cannot be longer than 50 characters.")]
        [Required(ErrorMessage = "A nickname is required")]
        public string Nick { get; set; }

        [DisplayName("Times Won")]
        [DefaultValue(0)]
        public int Won { get; set; }

        [DisplayName("Times Lost")]
        [DefaultValue(0)]
        public int Lost { get; set; }

        [DisplayName("Total Score")]
        [DefaultValue(0)]
        public int TotalScore { get; set; }

        [DisplayName("Times Chosen Red")]
        [DefaultValue(0)]
        public int RedTeam { get; set; }

        [DisplayName("Times Chosen Blue")]
        [DefaultValue(0)]
        public int BlueTeam { get; set; }

        [DisplayName("Favorite Team")]
        [StringLength(4)]
        public string FavTeam { get; set; } = "NONE";
    }
}
