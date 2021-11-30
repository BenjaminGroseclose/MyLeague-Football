using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLeague.Football.Data.Models
{
    public class League
    {
        [Key]
        public int Id { get; set; }
        public DateTime LeagueDate { get; set; }
        public int ChoosenFranchiseId { get; set; }

        [ForeignKey("ChoosenFranchiseId")]
        public Franchise ChoosenFranchise { get; set; }

        public string CoachFirstName { get; set; }

        public string CoachLastName { get; set; }

        public IEnumerable<ScheduleWeek> Schedule { get; set; }

        // TODO: Add other settings
    }
}
