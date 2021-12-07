using MyLeague.Football.Core;
using MyLeague.Football.Data.API;
using MyLeague.Football.Data.API.Models;
using MyLeague.Football.Data.Models;
using MyLeague.Football.Data.Repositories.Interfaces;
using MyLeague.Football.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeague.Football.Services.Implementations
{
    public class LeagueService : ILeagueService
    {
        private readonly IFranchiseRepository franchiseRepository;
        private readonly ILeagueRepository leagueRepository;
        private readonly ISportsDataAPI sportsDataAPI;
        private readonly IScheduleRepository scheduleRepository;
        private readonly IRecordsRepository recordsRepository;

        public LeagueService(IFranchiseRepository franchiseRepository,
                             ILeagueRepository leagueRepository,
                             ISportsDataAPI sportsDataAPI,
                             IScheduleRepository scheduleRepository,
                             IRecordsRepository recordsRepository)
        {
            this.franchiseRepository = franchiseRepository;
            this.leagueRepository = leagueRepository;
            this.sportsDataAPI = sportsDataAPI;
            this.scheduleRepository = scheduleRepository;
            this.recordsRepository = recordsRepository;
        }

        public async Task CreateLeague(string coachFirstName, string coachLastName, Franchise franchise)
        {
            try
            {
                var sportsDataSchedule = await this.sportsDataAPI.GetScheduleBySeason(2021);

                var schedule = this.MapToScheduleWeek(sportsDataSchedule, 2021);

                this.franchiseRepository.SetAsUser(franchise.Id);

                this.scheduleRepository.SaveSchedule(schedule);

                var league = this.leagueRepository.CreateLeague(new League (1, franchise, new DateTime(2021, 8, 16), coachFirstName, coachLastName, 2021, 1));

                this.recordsRepository.CreateDefaultRecords(2021, league);

                return;
            }
            catch (Exception ex)
            {
                // TODO: Handle exceptions
                if (ex != null)
                {

                }

                throw;
            }

        }

        private IEnumerable<WeekSchedule> MapToScheduleWeek(IEnumerable<SportsDataSchedule> sportsDataSchedule, int season)
        {
            IEnumerable<Franchise> franchises = this.franchiseRepository.GetAll(false);
            List<WeekSchedule> weeks = new List<WeekSchedule>();

            foreach (var sportsDataGame in sportsDataSchedule)
            {
                try
                {
                    var homeTeam = franchises.First(x => x.Abbrevation == sportsDataGame.HomeTeam);
                    var awayTeam = franchises.First(x => x.Abbrevation == sportsDataGame.AwayTeam);

                    DateTime? dateOfGame;

                    if (Constants.BYE_ABBREVATION.Equals(homeTeam.Abbrevation) || Constants.BYE_ABBREVATION.Equals(awayTeam.Abbrevation))
                    {
                        dateOfGame = null;
                    }
                    else
                    {
                        dateOfGame = DateTime.Parse(sportsDataGame.Date);
                    }

                    var week = new WeekSchedule(homeTeam, awayTeam, season, sportsDataGame.Week, dateOfGame);
                    weeks.Add(week);
                }
                catch (Exception ex)
                {
                    if (ex != null)
                    {

                    }

                    throw;
                }
            }

            return weeks;
        }

        // TODO: Program Schedule instead of read from JSON
        /* There was an attempt to auto generate schedule but due to time could not continue
        public IEnumerable<ScheduleWeek> GenerateSchedule(IEnumerable<Franchise> franchises, int season)
        {
            List<ScheduleWeek> schedule = new List<ScheduleWeek>();

            try
            {
                Random rand = new Random();

                foreach (var franchise in franchises)
                {
                    Console.WriteLine("-----------------------");
                    Console.WriteLine($"{franchise.FullName}: {franchise.Id}");
                    Console.WriteLine("-----------------------");
                    // 18 Weeks
                    List<int> availableWeeks = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
                    IEnumerable<ScheduleWeek> scheduleWeeks = schedule.Where(x => x.HomeTeamId == franchise.Id || x.AwayTeamId == franchise.Id);

                    availableWeeks.RemoveAll(x => scheduleWeeks.Select(x => x.Week).Contains(x));

                    IEnumerable<Franchise> divisionalOpponents = franchises.Where(x => x.Conference == franchise.Conference && x.Division == franchise.Division && x.Id != franchise.Id);

                    // Bye week
                    IEnumerable<int> possibleByeWeeks = availableWeeks.Where(x => x > 6 && x < 14);

                    int byeWeek = 0;
                    
                    if (possibleByeWeeks.Any())
                    {
                        byeWeek = possibleByeWeeks.ElementAt(rand.Next(possibleByeWeeks.Count() - 1));
                    }
                    else
                    {
                        byeWeek = availableWeeks.ElementAt(rand.Next(availableWeeks.Count - 1));
                    }
                    

                    Console.WriteLine($"Bye Week: {byeWeek}");

                    availableWeeks.Remove(byeWeek);
                    schedule.Add(new ScheduleWeek(franchise, null, season, byeWeek));

                    // Schedule divisional games
                    for (int i = 0; i < divisionalOpponents.Count(); i++)
                    {
                        Franchise divisionalOpponent = divisionalOpponents.ElementAt(i);
                        if (schedule.Where(x => x.HomeTeamId == divisionalOpponent.Id && x.AwayTeamId == franchise.Id).Any())
                        {
                            Console.WriteLine($"Skipping adding division games cause it is already scheduled vs: {divisionalOpponent.FullName}");
                            continue;
                        }

                        
                        var determinedHomeWeek = this.DeterminePlayableWeek(rand, divisionalOpponent, ref schedule, availableWeeks, season);
                        int homeGameWeek = 0;
                        int awayGameWeek = 0;


                        if (determinedHomeWeek.canSchedule)
                        {
                            homeGameWeek = determinedHomeWeek.week;
                            availableWeeks.Remove(homeGameWeek);
                            this.ScheduleGame(franchise, divisionalOpponent, season, homeGameWeek, ref schedule, availableWeeks);
                        }

                        var determinedAwayWeek = this.DeterminePlayableWeek(rand, divisionalOpponent, ref schedule, availableWeeks, season);

                        if (determinedAwayWeek.canSchedule)
                        {
                            awayGameWeek = determinedAwayWeek.week;
                            availableWeeks.Remove(awayGameWeek);
                            this.ScheduleGame(divisionalOpponent, franchise, season, awayGameWeek, ref schedule, availableWeeks);
                        }

                        Console.WriteLine($"Added divisional Home and Away game vs {divisionalOpponent.FullName} in weeks home:{homeGameWeek}, away: {awayGameWeek}");
                    }

                    int startingSeason = 2021;
                    int rotation = season - startingSeason;

                    // Step 3: Schedule same conference opponent
                    Division sameConferenceDivisoin = this.GetSameConferenceDivisionByRotation(franchise.Division, rotation);
                    IEnumerable<Franchise> sameConferenceDivisionOpponents = franchises.Where(x => x.Conference == franchise.Conference &&
                                                                                              x.Division == sameConferenceDivisoin);
                    
                    for (int i = 0; i < sameConferenceDivisionOpponents.Count(); i++)
                    {
                        Franchise sameConferenceDivisionOpponent = sameConferenceDivisionOpponents.ElementAt(i);
                        if (schedule.Where(x => x.HomeTeamId == sameConferenceDivisionOpponent.Id && x.AwayTeamId == franchise.Id).Any() ||
                            schedule.Where(x => x.AwayTeamId == sameConferenceDivisionOpponent.Id && x.HomeTeamId == franchise.Id).Any())
                        {
                            Console.WriteLine($"Skipping adding same conference game cause it is already scheduled vs: {sameConferenceDivisionOpponent.FullName}");
                            continue;
                        }

                        if (i % 2 == 0)
                        {
                            var determinedWeek = this.DeterminePlayableWeek(rand, sameConferenceDivisionOpponent, ref schedule, availableWeeks, season);

                            if (determinedWeek.canSchedule)
                            {
                                int homeGameWeek = determinedWeek.week;
                                availableWeeks.Remove(homeGameWeek);
                                this.ScheduleGame(franchise, sameConferenceDivisionOpponent, season, homeGameWeek, ref schedule, availableWeeks);
                                Console.WriteLine($"Added same conference Home game vs {sameConferenceDivisionOpponent.FullName} in week: {homeGameWeek}");
                            }
                        }
                        else
                        {
                            var determinedWeek = this.DeterminePlayableWeek(rand, sameConferenceDivisionOpponent, ref schedule, availableWeeks, season);

                            if (determinedWeek.canSchedule)
                            {
                                int awayGameWeek = determinedWeek.week;
                                availableWeeks.Remove(awayGameWeek);
                                this.ScheduleGame(sameConferenceDivisionOpponent, franchise, season, awayGameWeek, ref schedule, availableWeeks);
                                Console.WriteLine($"Added same conference Away game vs {sameConferenceDivisionOpponent.FullName} in week: {awayGameWeek}");
                            }
                        }
                    }

                    // Step 4: Schedule non conference same divison games
                    Division nonConferenceDivision = this.GetNonConferenceDivisionByRotation(franchise.Division, rotation);
                    IEnumerable<Franchise> nonConferenceDivisionOpponents = franchises.Where(x => x.Conference != franchise.Conference &&
                                                                              x.Division == nonConferenceDivision);

                    for (int i = 0; i < nonConferenceDivisionOpponents.Count(); i++)
                    {
                        Franchise nonConferenceDivisionOpponent = nonConferenceDivisionOpponents.ElementAt(i);
                        if (schedule.Where(x => x.HomeTeamId == nonConferenceDivisionOpponent.Id && x.AwayTeamId == franchise.Id).Any() ||
                            schedule.Where(x => x.AwayTeamId == nonConferenceDivisionOpponent.Id && x.HomeTeamId == franchise.Id).Any())
                        {
                            Console.WriteLine($"Skipping adding non conference game cause it is already scheduled vs: {nonConferenceDivisionOpponent.FullName}");
                            continue;
                        }

                        if (i % 2 == 0)
                        {
                            var determinedWeek =  this.DeterminePlayableWeek(rand, nonConferenceDivisionOpponent, ref schedule, availableWeeks, season);

                            if (determinedWeek.canSchedule)
                            {
                                int homeGameWeek = determinedWeek.week;
                                availableWeeks.Remove(homeGameWeek);
                                this.ScheduleGame(franchise, nonConferenceDivisionOpponent, season, homeGameWeek, ref schedule, availableWeeks);
                                Console.WriteLine($"Added non conference Home game vs {nonConferenceDivisionOpponent.FullName} in week: {homeGameWeek}");
                            }
                        }
                        else
                        {
                            var determinedWeek = this.DeterminePlayableWeek(rand, nonConferenceDivisionOpponent, ref schedule, availableWeeks, season);

                            if (determinedWeek.canSchedule)
                            {
                                int awayGameWeek = determinedWeek.week;
                                availableWeeks.Remove(awayGameWeek);
                                this.ScheduleGame(nonConferenceDivisionOpponent, franchise, season, awayGameWeek, ref schedule, availableWeeks);
                                Console.WriteLine($"Added non conference Away game vs {nonConferenceDivisionOpponent.FullName} in week: {awayGameWeek}");
                            }
                        }
                    }

                    // Step 5: Schedule the rest of the games till we get to 18
                    List<int> alreadyScheduleOpponents = schedule.Where(x => x.HomeTeamId == franchise.Id).Select(x => x.AwayTeamId).ToList();
                    alreadyScheduleOpponents.AddRange(schedule.Where(x => x.AwayTeamId == franchise.Id).Select(x => x.HomeTeamId));

                    List<Franchise> sameConferencesFranchises = franchises.Where(x => x.Conference == franchise.Conference &&
                                                                                        !alreadyScheduleOpponents.Contains(x.Id) &&
                                                                                        x.Id != franchise.Id).ToList();
                    List<Franchise> nonConferencesFranchises = franchises.Where(x => x.Conference != franchise.Conference &&
                                                                                       !alreadyScheduleOpponents.Contains(x.Id) &&
                                                                                        x.Id != franchise.Id).ToList();

                    // Checking if any weeks remain, if non continue to next franchise
                    if (!availableWeeks.Any())
                    {
                        var finalSchedule1 = this.GetTeamsSchedule(schedule, franchise);
                        Console.WriteLine($"Exit 1 - Franchise: {franchise.FullName} total game: {finalSchedule1.Count()} schedule:");

                        //string scheduleJson1 = JsonSerializer.Serialize(this.GetTeamsSchedule(schedule, franchise));

                        //Console.WriteLine(scheduleJson1);
                        continue;
                    }

                    var sameConferenceRandomDetermination1 = this.DetermineValidOpponentBasedOnAvailableSchedule(rand, sameConferencesFranchises, ref schedule, availableWeeks, season);

                    if (sameConferenceRandomDetermination1.canSchedule)
                    {
                        availableWeeks.Remove(sameConferenceRandomDetermination1.week);
                        this.ScheduleGame(sameConferenceRandomDetermination1.opponent, franchise, season, sameConferenceRandomDetermination1.week, ref schedule, availableWeeks);
                        Console.WriteLine($"Add random same conference home game vs {sameConferenceRandomDetermination1.opponent.FullName} in week {sameConferenceRandomDetermination1.week}");
                    }

                    if (!availableWeeks.Any())
                    {
                        var finalSchedule2 = this.GetTeamsSchedule(schedule, franchise);
                        Console.WriteLine($"Exit 2 - Franchise: {franchise.FullName} total game: {finalSchedule2.Count()} schedule:");

                        //string scheduleJson2 = JsonSerializer.Serialize(this.GetTeamsSchedule(schedule, franchise));

                        //Console.WriteLine(scheduleJson2);
                        continue;
                    }

                    sameConferencesFranchises.Remove(sameConferenceRandomDetermination1.opponent);
                    var sameConferenceRandomDetermination2 = this.DetermineValidOpponentBasedOnAvailableSchedule(rand, sameConferencesFranchises, ref schedule, availableWeeks, season);

                    if (sameConferenceRandomDetermination2.canSchedule)
                    {
                        availableWeeks.Remove(sameConferenceRandomDetermination2.week);
                        this.ScheduleGame(sameConferenceRandomDetermination2.opponent, franchise, season, sameConferenceRandomDetermination2.week, ref schedule, availableWeeks);
                        Console.WriteLine($"Add random same conference away game vs {sameConferenceRandomDetermination2.opponent.FullName} in week {sameConferenceRandomDetermination2.week}");
                    }

                    if (!availableWeeks.Any())
                    {
                        var finalSchedule3 = this.GetTeamsSchedule(schedule, franchise);
                        Console.WriteLine($"Exit 3 - Franchise: {franchise.FullName} total game: {finalSchedule3.Count()} schedule:");

                        //string scheduleJson3 = JsonSerializer.Serialize(this.GetTeamsSchedule(schedule, franchise));

                        //Console.WriteLine(scheduleJson3);
                        continue;
                    }

                    // Should only be one week left
                    var nonConferenceRandomDetermination = this.DetermineValidOpponentBasedOnAvailableSchedule(rand, nonConferencesFranchises, ref schedule, availableWeeks, season);

                    if (nonConferenceRandomDetermination.canSchedule)
                    {
                        availableWeeks.Remove(nonConferenceRandomDetermination.week);
                        nonConferencesFranchises.Remove(nonConferenceRandomDetermination.opponent);

                        if (rand.Next(1) == 1)
                        {
                            // Home game
                            this.ScheduleGame(franchise, nonConferenceRandomDetermination.opponent, season, nonConferenceRandomDetermination.week, ref schedule, availableWeeks);
                            Console.WriteLine($"Add random non conference home game vs {nonConferenceRandomDetermination.opponent.FullName} in week {nonConferenceRandomDetermination.week}");
                        }
                        else
                        {
                            // Away game
                            this.ScheduleGame(franchise, nonConferenceRandomDetermination.opponent, season, nonConferenceRandomDetermination.week, ref schedule, availableWeeks);
                            Console.WriteLine($"Add random non conference away game vs {nonConferenceRandomDetermination.opponent.FullName} in week {nonConferenceRandomDetermination.week}");
                        }
                    }

                    // Check to make sure we have 18 games scheduled
                    var finalSchedule = this.GetTeamsSchedule(schedule, franchise);

                    if (finalSchedule.Count() != 18)
                    {
                        // Missing games
                        List<int> allWeeks = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };

                        allWeeks.RemoveAll(x => finalSchedule.Select(x => x.Week).Contains(x));


                        List<Franchise> sameConfTeams = franchises.Where(x => x.Conference == franchise.Conference &&
                                                                                            !alreadyScheduleOpponents.Contains(x.Id) &&
                                                                                            x.Id != franchise.Id).ToList();
                        List<Franchise> nonConfTeams = franchises.Where(x => x.Conference != franchise.Conference &&
                                                                                           !alreadyScheduleOpponents.Contains(x.Id) &&
                                                                                            x.Id != franchise.Id).ToList();

                        List<Franchise> allFranchisesPossible = new List<Franchise>();
                        allFranchisesPossible.AddRange(sameConfTeams);
                        allFranchisesPossible.AddRange(nonConfTeams);

                        foreach (var week in allWeeks)
                        {
                            var scheduleWithAnyone = this.DetermineValidOpponentBasedOnAvailableSchedule(rand, allFranchisesPossible, ref schedule, new List<int> { week }, season);

                            if (scheduleWithAnyone.canSchedule)
                            {
                                allFranchisesPossible.Remove(scheduleWithAnyone.opponent);

                                if (rand.Next(1) == 1)
                                {
                                    // Home game
                                    this.ScheduleGame(franchise, scheduleWithAnyone.opponent, season, scheduleWithAnyone.week, ref schedule, availableWeeks);
                                }
                                else
                                {
                                    // Away game
                                    this.ScheduleGame(scheduleWithAnyone.opponent, franchise, season, scheduleWithAnyone.week, ref schedule, availableWeeks);
                                }
                            }
                        }
                    }
                }

                List<ScheduleWeek> weeksToRemove = new List<ScheduleWeek>();

                // Validate Schedule
                foreach (Franchise franchise in franchises)
                {
                    List<ScheduleWeek> finalFranchiseSchedule = this.GetTeamsSchedule(schedule, franchise);

                    if (finalFranchiseSchedule.Count() == 18)
                    {
                        continue;
                    }

                    if (finalFranchiseSchedule.Count() < 18)
                    {
                        List<int> alreadyScheduleOpponents = schedule.Where(x => x.HomeTeamId == franchise.Id).Select(x => x.AwayTeamId).ToList();
                        alreadyScheduleOpponents.AddRange(schedule.Where(x => x.AwayTeamId == franchise.Id).Select(x => x.HomeTeamId));

                        List<Franchise> sameConferencesFranchises = franchises.Where(x => x.Conference == franchise.Conference &&
                                                                                            !alreadyScheduleOpponents.Contains(x.Id) &&
                                                                                            x.Id != franchise.Id).ToList();
                        List<Franchise> nonConferencesFranchises = franchises.Where(x => x.Conference != franchise.Conference &&
                                                                                           !alreadyScheduleOpponents.Contains(x.Id) &&
                                                                                            x.Id != franchise.Id).ToList();

                        List<Franchise> allFranchisesPossible = new List<Franchise>();
                        allFranchisesPossible.AddRange(sameConferencesFranchises);
                        allFranchisesPossible.AddRange(nonConferencesFranchises);

                        // Add games
                        List<int> allWeeks = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
                        allWeeks.RemoveAll(x => finalFranchiseSchedule.Select(y => y.Week).Contains(x));

                        foreach (int week in allWeeks)
                        { 
                            List<int> availableWeek = new List<int> { week };

                            var scheduleWithAnyone = this.DetermineValidOpponentBasedOnAvailableSchedule(rand, allFranchisesPossible, ref schedule, availableWeek, season);

                            if (scheduleWithAnyone.canSchedule)
                            {
                                // Schedule a game
                                if (rand.Next(1) == 1)
                                {
                                    // Home game
                                    this.ScheduleGame(franchise, scheduleWithAnyone.opponent, season, scheduleWithAnyone.week, ref schedule, availableWeek);
                                }
                                else
                                {
                                    // Away game
                                    this.ScheduleGame(scheduleWithAnyone.opponent, franchise, season, scheduleWithAnyone.week, ref schedule, availableWeek);
                                }
                            }
                            else
                            {
                                var teamScheduledThisWeek = schedule.Where(x => x.Week == week).Select(x => x.HomeTeam).ToList();
                                teamScheduledThisWeek.AddRange(schedule.Where(x => x.Week == week).Select(x => x.AwayTeam));

                                Franchise opponent = franchises.Where(x => !teamScheduledThisWeek.Contains(x)).FirstOrDefault();

                                if (opponent == null)
                                {

                                }
                            }
                        }
                    }
                    else
                    {
                        // Remove games
                        var groupedSchedule = finalFranchiseSchedule.GroupBy(x => x.Week);

                        // Check for weeks outside of expected 1 - 18 and remove them
                        var wrongWeeks = finalFranchiseSchedule.Where(x => x.Week < 1 || x.Week > 18);

                        foreach (var wrongWeek in wrongWeeks)
                        {
                            weeksToRemove.Add(wrongWeek);
                            finalFranchiseSchedule.Remove(wrongWeek);
                        }

                        // Check if there is still an issue
                        finalFranchiseSchedule = this.GetTeamsSchedule(schedule, franchise);

                        if (finalFranchiseSchedule.Count() == 18)
                        {
                            break;
                        }

                        throw new Exception("Why won't you go away");
                    }
                }

                foreach (var removingWeek in weeksToRemove)
                {
                    schedule.Remove(removingWeek);
                }

                return schedule;
            }
            catch (SchedulingException ex)
            {
                errorCount++;

                Console.WriteLine($"Failed to generate schedule, trying again attempt count: {errorCount}");

                if (errorCount > 4)
                {
                    throw;
                }

                // Try to generate it again
                return this.GenerateSchedule(franchises, season);
            }
            catch (Exception ex)
            {
                if (ex != null)
                {

                }

                throw;
            }
        }

        private (bool canSchedule, int week, Franchise opponent) DetermineValidOpponentBasedOnAvailableSchedule(Random rand, IEnumerable<Franchise> franchises, ref List<ScheduleWeek> schedule, List<int> availableWeeks, int season)
        {
            if (!availableWeeks.Any())
            {
                throw new Exception("Something went wrong");
            }

            List<Franchise> validOpponents = new List<Franchise>();

            // Try to find a valid opponent with the given weeks
            foreach (Franchise franchise in franchises)
            {
                IEnumerable<int> weeksFranchiseTaken = schedule.Where(x => x.HomeTeamId == franchise.Id || x.AwayTeamId == franchise.Id).Select(x => x.Week);
                 List<int> allWeeks = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };

                allWeeks.RemoveAll(x => weeksFranchiseTaken.Contains(x));

                foreach (int week in allWeeks)
                {
                    if (availableWeeks.Contains(week))
                    {
                        validOpponents.Add(franchise);
                        break;
                    }
                }
            }

            // Try to move a game to make room for this one
            if (!validOpponents.Any())
            {
                Franchise opponentFixed = null;
                int weekScheduled;
                // Try to fix the schedules
                foreach (Franchise franchise in franchises)
                {
                    try
                    {
                        var fixedSchedule = this.FixSchedule(rand, franchise, schedule, availableWeeks, season);

                        if (fixedSchedule.canReschedule)
                        {
                            opponentFixed = fixedSchedule.opponent;
                            weekScheduled = fixedSchedule.week;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    catch (SchedulingException)
                    {
                        continue;
                    }
                }

                if (opponentFixed == null)
                {
                    Console.WriteLine("Unable to determine valid opponent, not going to schedule");

                    return (false, -1, null);
                }

                var determinedWeekFixed = this.DeterminePlayableWeek(rand, opponentFixed, ref schedule, availableWeeks, season);

                if (determinedWeekFixed.canSchedule)
                {
                    return (true, determinedWeekFixed.week, opponentFixed);
                }
                else
                {
                    return (false, -1, null);
                }

            }

            Franchise opponent = validOpponents.ElementAt(rand.Next(validOpponents.Count));

            if (opponent == null)
            {
                throw new Exception();
            }

            var determinedWeek = this.DeterminePlayableWeek(rand, opponent, ref schedule, availableWeeks, season);

            if (determinedWeek.canSchedule)
            {
                return (true, determinedWeek.week, opponent);
            }
            else
            {
                return (false, -1, null);
            }
        }

        private (bool canSchedule, int week) DeterminePlayableWeek(Random rand, Franchise opponent, ref List<ScheduleWeek> schedule, List<int> availableWeeks, int season)
        {
            IEnumerable<int> opponentTakenWeeks = schedule.Where(x => x.HomeTeamId == opponent.Id || x.AwayTeamId == opponent.Id).Select(x => x.Week);
            List<int> oppenentOpenWeeks = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };

            oppenentOpenWeeks.RemoveAll(x => opponentTakenWeeks.Contains(x));

            List<int> possibleWeek = new List<int>();

            foreach (int week in availableWeeks)
            {
                if (oppenentOpenWeeks.Contains(week))
                {
                    possibleWeek.Add(week);
                }
            }
                
            if (!possibleWeek.Any())
            {
                var fixedSchedule = FixSchedule(rand, opponent, schedule, availableWeeks, season);

                if (fixedSchedule.canReschedule)
                {
                    return (true, fixedSchedule.week);
                }
                else
                {
                    return (false, -1);
                }
            }

            int choosenWeek = possibleWeek.ElementAt(rand.Next(possibleWeek.Count - 1));

            return (true, choosenWeek);
        }

        private (bool canReschedule, int week, Franchise opponent) FixSchedule(Random rand, Franchise opponent, List<ScheduleWeek> schedule, List<int> availableWeeks, int season)
        {
            // Move an opponent game so we can schedule the game
            var opponentSchedule = this.GetTeamsSchedule(schedule, opponent);

            // Try to use teams by week
            ScheduleWeek opponentByeWeek = opponentSchedule.FirstOrDefault(x => x.AwayTeamId == -1);

            if (opponentByeWeek != null)
            {
                availableWeeks.Remove(opponentByeWeek.Week);
                schedule.Select(x => x.HomeTeamId);
                List<int> allWeeks = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };

                allWeeks.RemoveAll(x => opponentSchedule.Select(x => x.Week).Contains(x));

                // If no weeks are available then can not use bye week
                if (allWeeks.Any())
                {
                    IEnumerable<int> possibleByeWeeks = allWeeks.Where(x => x > 6 && x < 14);

                    // Removing to add new bye week
                    schedule.Remove(opponentByeWeek);

                    if (possibleByeWeeks.Any())
                    {
                        schedule.Add(new ScheduleWeek(opponent, null, season, possibleByeWeeks.ElementAt(rand.Next(possibleByeWeeks.Count() - 1))));
                    }
                    else
                    {
                        schedule.Add(new ScheduleWeek(opponent, null, season, allWeeks.ElementAt(rand.Next(allWeeks.Count() - 1))));
                    }

                    return (true, opponentByeWeek.Week, opponent);
                }
            }

            if (!availableWeeks.Any())
            {
                return (false, -1, null);
            }

            // int reschedulingWeek = availableWeeks.ElementAt(rand.Next(availableWeeks.Count - 1));

            foreach(int possibleWeek in availableWeeks)
            {
                if (!opponentSchedule.Select(x => x.Week).Contains(possibleWeek))
                {
                    return (true, possibleWeek, opponent);
                }

                ScheduleWeek opponentRescheduleWeek = opponentSchedule.First(x => x.Week == possibleWeek);
                Franchise oppenentRescheduleOpponent;
                bool isOpponentHomeInReschedule;

                // Remove old game to add new game

                if (opponentRescheduleWeek.HomeTeamId == opponent.Id)
                {
                    isOpponentHomeInReschedule = true;
                    oppenentRescheduleOpponent = opponentRescheduleWeek.AwayTeam;
                }
                else
                {
                    isOpponentHomeInReschedule = false;
                    oppenentRescheduleOpponent = opponentRescheduleWeek.HomeTeam;
                }

                var oppenentRescheduleOpponentSchedule = this.GetTeamsSchedule(schedule, oppenentRescheduleOpponent);
                List<int> possibleRescheduleWeeks = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };

                possibleRescheduleWeeks.RemoveAll(x => opponentSchedule.Select(y => y.Week).Contains(x));
                possibleRescheduleWeeks.RemoveAll(x => oppenentRescheduleOpponentSchedule.Select(y => y.Week).Contains(x));

                if (!possibleRescheduleWeeks.Any())
                {
                    continue;

                    // Attempted recusion
                    possibleRescheduleWeeks = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };

                    possibleRescheduleWeeks.RemoveAll(x => opponentSchedule.Select(y => y.Week).Contains(x));

                    var fixedOpponentsReschedule = this.FixSchedule(rand, oppenentRescheduleOpponent, schedule, possibleRescheduleWeeks, season);

                    if (!fixedOpponentsReschedule.canReschedule)
                    {
                        continue;
                    }

                    return (true, possibleWeek, opponent);
                }

                // Only remove after you have found a week.
                schedule.Remove(opponentRescheduleWeek);

                int rescheduleWeek = possibleRescheduleWeeks.ElementAt(rand.Next(possibleRescheduleWeeks.Count - 1));
                if (isOpponentHomeInReschedule)
                {
                    this.ScheduleGame(opponent, oppenentRescheduleOpponent, season, rescheduleWeek, ref schedule, availableWeeks);
                }
                else
                {
                    this.ScheduleGame(oppenentRescheduleOpponent, opponent, season, rescheduleWeek, ref schedule, availableWeeks);
                }

                return (true, possibleWeek, opponent);
            }

            return (false, -1, null);
        }

        private Division GetSameConferenceDivisionByRotation(Division franchiseDivision, int rotation)
        {
            switch(rotation % 3)
            {
                case 0:
                    switch (franchiseDivision)
                    {
                        case Division.NORTH: return Division.SOUTH;
                        case Division.SOUTH: return Division.NORTH;
                        case Division.EAST: return Division.WEST;
                        case Division.WEST: return Division.EAST;
                        default: throw new ArgumentOutOfRangeException(nameof(franchiseDivision));
                    }
                case 1:
                    switch (franchiseDivision)
                    {
                        case Division.NORTH: return Division.EAST;
                        case Division.SOUTH: return Division.WEST;
                        case Division.EAST: return Division.NORTH;
                        case Division.WEST: return Division.SOUTH;
                        default: throw new ArgumentOutOfRangeException(nameof(franchiseDivision));
                    }
                case 2:
                    switch (franchiseDivision)
                    {
                        case Division.NORTH: return Division.WEST;
                        case Division.SOUTH: return Division.EAST;
                        case Division.EAST: return Division.SOUTH;
                        case Division.WEST: return Division.NORTH;
                        default: throw new ArgumentOutOfRangeException(nameof(franchiseDivision));
                    }
                default: throw new ArgumentOutOfRangeException(nameof(rotation));
            }
        }

        private Division GetNonConferenceDivisionByRotation(Division franchiseDivision, int rotation)
        {
            switch (rotation % 3)
            {
                case 0:
                    switch (franchiseDivision)
                    {
                        case Division.NORTH: return Division.EAST;
                        case Division.SOUTH: return Division.WEST;
                        case Division.EAST: return Division.NORTH;
                        case Division.WEST: return Division.SOUTH;
                        default: throw new ArgumentOutOfRangeException(nameof(franchiseDivision));
                    }
                case 1:
                    switch (franchiseDivision)
                    {
                        case Division.NORTH: return Division.WEST;
                        case Division.SOUTH: return Division.EAST;
                        case Division.EAST: return Division.SOUTH;
                        case Division.WEST: return Division.NORTH;
                        default: throw new ArgumentOutOfRangeException(nameof(franchiseDivision));
                    }
                case 2:
                    switch (franchiseDivision)
                    {
                        case Division.NORTH: return Division.SOUTH;
                        case Division.SOUTH: return Division.NORTH;
                        case Division.EAST: return Division.WEST;
                        case Division.WEST: return Division.EAST;
                        default: throw new ArgumentOutOfRangeException(nameof(franchiseDivision));
                    }
                default: throw new ArgumentOutOfRangeException(nameof(rotation));
            }
        }

        private List<ScheduleWeek> GetTeamsSchedule(IEnumerable<ScheduleWeek> schedule, Franchise franchise)
        {
            return schedule.Where(x => x.HomeTeamId == franchise.Id || x.AwayTeamId == franchise.Id).ToList();
        }  


        private void ScheduleGame(Franchise homeTeam, Franchise awayTeam, int season, int week, ref List<ScheduleWeek> schedule, List<int> otherWeeks)
        {
            var homeTeamSchedule = this.GetTeamsSchedule(schedule, homeTeam);
            var awayTeamSchedule = this.GetTeamsSchedule(schedule, awayTeam);

            if (homeTeamSchedule.Select(x => x.Week).Contains(week))
            {
                return;
                if (!otherWeeks.Any())
                {
                    return;
                }

                var fixedSchedule = this.FixSchedule(new Random(), homeTeam, schedule, otherWeeks, season);
                if (fixedSchedule.canReschedule)
                {
                    schedule.Add(new ScheduleWeek(homeTeam, awayTeam, season, fixedSchedule.week));
                }
            }
            else if (awayTeamSchedule.Select(x => x.Week).Contains(week))
            {
                return;
                if (!otherWeeks.Any())
                {
                    return;
                }

                var fixedSchedule = this.FixSchedule(new Random(), awayTeam, schedule, otherWeeks, season);

                if (fixedSchedule.canReschedule)
                {
                    schedule.Add(new ScheduleWeek(homeTeam, awayTeam, season, fixedSchedule.week));
                }
            }
            else
            {
                schedule.Add(new ScheduleWeek(homeTeam, awayTeam, season, week));
            }
        }
        */
    }
}
