using MyLeague.Football.Data.Models;
using MyLeague.Football.Data.Repositories.Interfaces;
using MyLeague.Football.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeague.Football.Services.Implementations
{
    public class GameService : IGameService
    {
        private readonly ILeagueRepository leagueRepository;
        private readonly IScheduleRepository scheduleRepository;
        private readonly IRecordsRepository recordsRepository;

        public GameService(ILeagueRepository leagueRepository, IScheduleRepository scheduleRepository, IRecordsRepository recordsRepository)
        {
            this.leagueRepository = leagueRepository;
            this.scheduleRepository = scheduleRepository;
            this.recordsRepository = recordsRepository;
        }

        public void AdvanceSeason()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void AdvanceWeek()
        {
            League league = this.leagueRepository.GetLeague(1);

            IEnumerable<WeekSchedule> currentWeekSchedule = this.scheduleRepository.GetScheduleBySeason(league.CurrentSeason).Where(x => x.Week == league.CurrentWeek);

            foreach (var weekSchedule in currentWeekSchedule)
            {
                var game = this.PlayGame(weekSchedule.AwayTeam, weekSchedule.HomeTeam);

                if (game.homeTeamScore > game.awayTeamScore)
                {
                    // Home team wins
                    this.recordsRepository.UpdateFranchisesRecord(league.CurrentSeason, weekSchedule.HomeTeam.Id, RecordOptions.WIN);
                    this.recordsRepository.UpdateFranchisesRecord(league.CurrentSeason, weekSchedule.AwayTeam.Id, RecordOptions.LOSS);
                }
                else if (game.homeTeamScore < game.awayTeamScore)
                {
                    // Away team wins
                    this.recordsRepository.UpdateFranchisesRecord(league.CurrentSeason, weekSchedule.HomeTeam.Id, RecordOptions.LOSS);
                    this.recordsRepository.UpdateFranchisesRecord(league.CurrentSeason, weekSchedule.AwayTeam.Id, RecordOptions.WIN);
                }
                else
                {
                    // Tie
                    this.recordsRepository.UpdateFranchisesRecord(league.CurrentSeason, weekSchedule.HomeTeam.Id, RecordOptions.TIE);
                    this.recordsRepository.UpdateFranchisesRecord(league.CurrentSeason, weekSchedule.AwayTeam.Id, RecordOptions.TIE);
                }


                this.scheduleRepository.SaveScore(weekSchedule.Id, game.awayTeamScore, game.homeTeamScore);
            }

            league.AdvanceWeek();
            this.leagueRepository.UpdateLeague(league.Id, league);
        }

        public (int awayTeamScore, int homeTeamScore) PlayGame(Franchise awayTeam, Franchise homeTeam)
        {
            int homeTeamOverall = this.EvaluateRoster(homeTeam);
            int awayTeamOverall = this.EvaluateRoster(awayTeam);

            return (10, 17);
        }

        /// <summary>
        /// Returns the overall of the team counting "starters" only.
        /// </summary>
        private int EvaluateRoster(Franchise franchise)
        {

        }
    }
}
