using System;
using System.ComponentModel.DataAnnotations;

namespace MyLeague.Football.Data.Models
{
    public class League
    {
        public League(int id, Franchise choosenFranchise, DateTime leagueDate, string coachFirstName, string coachLastName, int season, int week)
        {
            this.Id = id;
            this.ChoosenFranchise = choosenFranchise;
            this.LeagueDate = leagueDate;
            this.CoachFirstName = coachFirstName;
            this.CoachLastName = coachLastName;
            this.CurrentSeason = season;
            this.CurrentWeek = week;
        }

        [Key]
        public int Id { get; set; }
        public DateTime LeagueDate { get; set; }

        public int CurrentSeason { get; set; }

        public int CurrentWeek { get; set; }

        public Franchise ChoosenFranchise { get; private set; }

        public string CoachFirstName { get; private set; }

        public string CoachLastName { get; private set; }

        // TODO: Add other settings

        public void AdvanceWeek()
        {
            this.CurrentWeek++;
            this.LeagueDate.AddDays(7);
            // TODO: Check for season end
        }
    }
}
