using System.ComponentModel.DataAnnotations;

namespace MyLeague.Football.Data.Models
{
    public class FranchiseRecord
    {
        public FranchiseRecord() { }

        public FranchiseRecord(int franchiseId, int season, League league)
        {
            this.FranchiseId = franchiseId;
            this.Season = season;
            this.Wins = 0;
            this.Loses = 0;
            this.Ties = 0;
            this.LeagueId = league.Id;
            this.League = league;
        }

        [Key]
        public int Id { get; set; }
        public int FranchiseId { get; set; }
        public int Season { get; set; }
        public int Wins { get; set; }
        public int Loses { get; set; }
        public int Ties { get; set; }
        public int LeagueId { get; set; }
        public League League { get; set; }
    }

    public enum RecordOptions
    {
        WIN,
        LOSS,
        TIE
    }
}
