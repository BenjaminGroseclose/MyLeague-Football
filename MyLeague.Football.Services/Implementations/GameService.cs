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
            int gameDifference = 0;

            int homeTeamOverall = this.EvaluateRoster(homeTeam);
            int awayTeamOverall = this.EvaluateRoster(awayTeam);

            // Home team gets 3 points
            gameDifference = gameDifference + 3;

            gameDifference = gameDifference + (homeTeamOverall - awayTeamOverall);


            /* game different
             * 0 = tie
             * > 0 = home team win
             * < 0 = away team win
             */

            if (gameDifference == 0)
            {
                // Tie
            }
            else if (gameDifference > 0)
            {
                // Home team win
            }
            else
            {
                // Away team win
            }
        }

        private (int awayTeamScore, int homeTeamScore) ScoreVariantions(int gameDifference)
        {
            var variations = this.DefaultGameVariantions();
        }

        /// <summary>
        /// Returns the overall of the team counting "starters" only.
        /// </summary>
        private int EvaluateRoster(Franchise franchise)
        {
            List<Player> starters = new List<Player>();

            var positions = Enum.GetValues(typeof(Position)).Cast<Position>().ToList();

            foreach(var position in positions)
            {
                switch (position)
                {
                    case Position.QB:
                        starters.Add(franchise.Players.Where(x => x.Position == Position.QB).OrderBy(x => x.PlayerAttributes.Overall).First());
                        break;
                    case Position.RB:
                        starters.Add(franchise.Players.Where(x => x.Position == Position.RB).OrderBy(x => x.PlayerAttributes.Overall).First());
                        break;
                    case Position.FB:
                        starters.Add(franchise.Players.Where(x => x.Position == Position.FB).OrderBy(x => x.PlayerAttributes.Overall).First());
                        break;
                    case Position.LT:
                        starters.Add(franchise.Players.Where(x => x.Position == Position.LT).OrderBy(x => x.PlayerAttributes.Overall).First());
                        break;
                    case Position.LG:
                        starters.Add(franchise.Players.Where(x => x.Position == Position.LG).OrderBy(x => x.PlayerAttributes.Overall).First());
                        break;
                    case Position.C:
                        starters.Add(franchise.Players.Where(x => x.Position == Position.C).OrderBy(x => x.PlayerAttributes.Overall).First());
                        break;
                    case Position.RG:
                        starters.Add(franchise.Players.Where(x => x.Position == Position.RG).OrderBy(x => x.PlayerAttributes.Overall).First());
                        break;
                    case Position.RT:
                        starters.Add(franchise.Players.Where(x => x.Position == Position.RT).OrderBy(x => x.PlayerAttributes.Overall).First());
                        break;
                    case Position.WR:
                        starters.AddRange(franchise.Players.Where(x => x.Position == Position.WR).OrderBy(x => x.PlayerAttributes.Overall).Take(3));
                        break;
                    case Position.TE:
                        starters.AddRange(franchise.Players.Where(x => x.Position == Position.TE).OrderBy(x => x.PlayerAttributes.Overall).Take(2));
                        break;
                    case Position.RE:
                        starters.Add(franchise.Players.Where(x => x.Position == Position.RE).OrderBy(x => x.PlayerAttributes.Overall).First());
                        break;
                    case Position.DT:
                        starters.AddRange(franchise.Players.Where(x => x.Position == Position.DT).OrderBy(x => x.PlayerAttributes.Overall).Take(2));
                        break;
                    case Position.LE:
                        starters.Add(franchise.Players.Where(x => x.Position == Position.LE).OrderBy(x => x.PlayerAttributes.Overall).First());
                        break;
                    case Position.ROLB:
                        starters.Add(franchise.Players.Where(x => x.Position == Position.ROLB).OrderBy(x => x.PlayerAttributes.Overall).First());
                        break;
                    case Position.MLB:
                        starters.Add(franchise.Players.Where(x => x.Position == Position.MLB).OrderBy(x => x.PlayerAttributes.Overall).First());
                        break;
                    case Position.LOLB:
                        starters.Add(franchise.Players.Where(x => x.Position == Position.LOLB).OrderBy(x => x.PlayerAttributes.Overall).First());
                        break;
                    case Position.CB:
                        starters.AddRange(franchise.Players.Where(x => x.Position == Position.CB).OrderBy(x => x.PlayerAttributes.Overall).Take(3));
                        break;
                    case Position.FS:
                        starters.Add(franchise.Players.Where(x => x.Position == Position.FS).OrderBy(x => x.PlayerAttributes.Overall).First());
                        break;
                    case Position.SS:
                        starters.Add(franchise.Players.Where(x => x.Position == Position.SS).OrderBy(x => x.PlayerAttributes.Overall).First());
                        break;
                }
            }

            int startersCount = starters.Count();

            return starters.Sum(x => x.PlayerAttributes.Overall) / startersCount;
        }
    }
}
