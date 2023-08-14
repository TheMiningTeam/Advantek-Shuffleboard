using System.ComponentModel.DataAnnotations.Schema;

namespace Shuffleboard.Models
{
    public class BestPlayerDaily
    {
        public int Id { get; set; }
        public DateTime LastUpdate { get; set; }

        [ForeignKey("BPDPlayerId")]
        public int PlayerId { get; set; }
    }
}
