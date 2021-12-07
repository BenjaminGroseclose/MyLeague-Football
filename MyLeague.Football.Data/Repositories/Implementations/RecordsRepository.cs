using MyLeague.Football.Data.Models;
using MyLeague.Football.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLeague.Football.Data.Repositories.Implementations
{
    public class RecordsRepository : IRecordsRepository
    {
        private readonly MyLeagueFootballContext dbContext;

        public RecordsRepository(MyLeagueFootballContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CreateDefaultRecords(int season, League league)
        {
            List<FranchiseRecord> records = new List<FranchiseRecord>();

            for (int i = 1;i <= 32; i++)
            {
                records.Add(new FranchiseRecord(i, season, league));
            }

            this.dbContext.FranchiseRecords.AddRange(records);
            this.dbContext.SaveChanges();
        }

        public IEnumerable<FranchiseRecord> GetFranchiseRecords(int season)
        {
            return this.dbContext.FranchiseRecords.Where(x => x.Season == season);
        }

        public void UpdateFranchisesRecord(int season, int franchiseId, RecordOptions recordOptions)
        {
            var franchiseRecord = this.dbContext.FranchiseRecords.FirstOrDefault(x => x.Season == season && x.FranchiseId == franchiseId);

            if (franchiseRecord == null)
            {
                throw new ArgumentException($"Was not able to find a franchise record in season: {season}, with franchiseId: {franchiseId}");
            }

            switch(recordOptions)
            {
                case RecordOptions.WIN:
                    franchiseRecord.Wins++;
                    break;
                case RecordOptions.LOSS:
                    franchiseRecord.Loses++;
                    break;
                case RecordOptions.TIE:
                    franchiseRecord.Ties++;
                    break;
                default:
                    throw new ArgumentException($"Unexpected record option provided: {recordOptions}");
            }

            this.dbContext.SaveChanges();
        }
    }
}
