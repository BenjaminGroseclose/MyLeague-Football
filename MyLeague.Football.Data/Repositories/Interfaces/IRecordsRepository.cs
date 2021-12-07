using MyLeague.Football.Data.Models;
using System.Collections.Generic;

namespace MyLeague.Football.Data.Repositories.Interfaces
{
    public interface IRecordsRepository
    {
        void CreateDefaultRecords(int season, League league);

        IEnumerable<FranchiseRecord> GetFranchiseRecords(int season);

        void UpdateFranchisesRecord(int season, int franchiseId, RecordOptions recordOptions);
    }
}
