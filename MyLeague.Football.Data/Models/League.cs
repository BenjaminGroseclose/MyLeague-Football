using System;
using System.ComponentModel.DataAnnotations;

namespace MyLeague.Football.Data.Models
{
    public class League
    {
        [Key]
        public int Id { get; set; }
        public DateTime LeagueDate { get; set; }

        public int CurrentSeason { get; set; }

        public int CurrentWeek { get; set; }

        public Franchise ChoosenFranchise { get; set; }

        public string CoachFirstName { get; set; }

        public string CoachLastName { get; set; }

        // TODO: Add other settings
    }
}
