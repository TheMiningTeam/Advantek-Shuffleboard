namespace Shuffleboard.Models
{
    internal class LatestMatch
    {
        public LatestMatch()
        {
        }

        public string RedPlayer { get; set; }
        public int RedScore { get; set; }
        public string BluePlayer { get; set; }
        public int BlueScore { get; set; }
        public string MatchDate { get; set; }
    }
}