using System.ComponentModel.DataAnnotations;

namespace MyLeague.Football.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public bool DatabaseInitialize { get; set; }
        public bool GameStarted { get; set; }
    }
}
