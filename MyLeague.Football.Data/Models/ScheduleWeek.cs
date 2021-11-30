using System;
using System.ComponentModel.DataAnnotations;

namespace MyLeague.Football.Data.Models
{
    public class ScheduleWeek
    {
        public ScheduleWeek() { }
        public ScheduleWeek(Franchise homeTeam, Franchise awayTeam, int season, int week, DateTime? dateOfGame)
        {
            this.HomeTeamId = homeTeam.Id;
            this.HomeTeam = homeTeam;
            this.AwayTeamId = awayTeam.Id;
            this.AwayTeam = awayTeam;
            this.Season = season;
            this.Week = week;
            this.DateOfGame = dateOfGame;

            // Set to -1 to represent the game not being play yet
            this.HomeTeamScore = -1;
            this.AwayTeamScore = -1;
        }

        [Key]
        public int Id { get; set; }
        public int HomeTeamId { get; set; }
        public Franchise HomeTeam { get; set; }

        public int AwayTeamId { get; set; }
        public Franchise AwayTeam { get; set; }
        public DateTime? DateOfGame { get; set; }

        /// <summary>
        /// Year of that season
        /// </summary>
        public int Season { get; set; }

        /// <summary>
        /// The week of that season 1 - 17
        /// </summary>
        public int Week { get; set; }

        public int HomeTeamScore { get; private set; }

        public int AwayTeamScore { get; private set; }

        public void ScoreGame(int homeTeam, int awayTeam)
        {
            this.HomeTeamScore = homeTeam;
            this.AwayTeamScore = awayTeam;
        }

        public Franchise GetWinner()
        {
            if (this.HomeTeamScore > this.AwayTeamScore)
            {
                return this.HomeTeam;
            }
            else
            {
                return this.AwayTeam;
            }
        }

        public Franchise GetLoser()
        {
            if (this.HomeTeamScore < this.AwayTeamScore)
            {
                return this.HomeTeam;
            }
            else
            {
                return this.AwayTeam;
            }
        }
    }
}
