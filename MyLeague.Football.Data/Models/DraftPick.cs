using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLeague.Football.Data.Models
{
    public class DraftPick
    {
        public DraftPick() { }
        public DraftPick(int id, int season, int round, int ownerId)
        {
            this.Id = id;
            this.Season = season;
            this.Round = round;
            this.OwnerId = ownerId;
            this.OriginalFranchiseId = ownerId;
        }

        [Key]
        public int Id { get; set; }
        public int Season { get; set; }
        public int Round { get; set; }

        public int OwnerId { get; set; }
        public Franchise Owner { get; set; }

        public int OriginalFranchiseId { get; set; }
        public Franchise OriginalFranchise { get; set; }
    }
}
