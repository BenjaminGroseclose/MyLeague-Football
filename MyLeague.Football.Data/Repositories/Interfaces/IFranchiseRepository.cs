using MyLeague.Football.Data.Models;
using System.Collections.Generic;

namespace MyLeague.Football.Data.Repositories.Interfaces
{
    public interface IFranchiseRepository
    {
        IEnumerable<Franchise> GetAll(bool includePlayer, bool removeByeWeek);

        Franchise GetById();

        Franchise SetAsPlayer(int id);
    }
}