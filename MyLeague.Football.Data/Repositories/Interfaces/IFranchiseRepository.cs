using MyLeague.Football.Data.Models;
using System.Collections.Generic;

namespace MyLeague.Football.Data.Repositories.Interfaces
{
    public interface IFranchiseRepository
    {
        IEnumerable<Franchise> GetAll(bool removeByeWeek = true);

        Franchise GetById(int id);

        Franchise SetAsUser(int id);

        void UpdateDraftPicks(int franchiseId, DraftPick draftPick);
    }
}