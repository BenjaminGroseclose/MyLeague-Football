using MyLeague.Football.Data.Models;
using System.Collections.Generic;

namespace MyLeague.Football.Data.Repository.Interfaces
{
    public interface IFranchiseRepository
    {
        IEnumerable<Franchise> GetAll(bool includePlayer);

        Franchise GetById();

        Franchise Update(Franchise franchise);
    }
}