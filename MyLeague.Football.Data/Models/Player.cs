using AutoMapper;
using MyLeague.Football.Data.Generators;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLeague.Football.Data.Models
{
    public class Player
    {
        public Player() { }
        public Player(string firstName, string lastName, string college, InitialPlayerModel initialPlayer, IMapper mapper)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DateOfBirth = DateTime.Parse(initialPlayer.DateOfBirth);
            this.Position = this.DeterminePosition(initialPlayer.Position);
            this.College = college;
            this.FranchiseId = initialPlayer.TeamId;
            this.Experience = initialPlayer.Experience;
            this.JerseyNumber = initialPlayer.JerseyNumber;
            this.PlayerAttributes = mapper.Map<PlayerAttributes>(initialPlayer);
        }

        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the player's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the player's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the player's date of birth
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the player's position
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// Gets or sets the player's college
        /// </summary>
        public string College { get; set; }

        /// <summary>
        /// Gets or sets the date the player was drafted
        /// </summary>
        public int Experience { get; set; }
        
        public int? FranchiseId { get; set; }

        [ForeignKey("FranchiseId")]
        public Franchise Franchise { get; set; }

        public int JerseyNumber { get; set; }

        public Contract Contract { get; set; }

        public int PlayerAttributeId { get; set; }

        [ForeignKey("PlayerAttributeId")]
        public PlayerAttributes PlayerAttributes { get; set; }

        /// <summary>
        /// Gets or sets the player's age
        /// </summary>
        public int Age(DateTime currentDate)
        {
            return currentDate.Year - this.DateOfBirth.Year;
        }

        [NotMapped]
        public string FullName => $"{FirstName}, {LastName}";

        private Position DeterminePosition(string position)
        {
            switch(position)
            {
                case "QB": return Position.QB;
                case "RB": return Position.RB;
                case "FB": return Position.FB;
                case "WR": return Position.WR;
                case "TE": return Position.TE;
                case "LT": return Position.LT;
                case "LG": return Position.LG;
                case "C": return Position.C;
                case "RG": return Position.RG;
                case "RT": return Position.RT;
                case "LE": return Position.LE;
                case "DT": return Position.DT;
                case "RE": return Position.RE;
                case "LOLB": return Position.LOLB;
                case "MLB": return Position.MLB;
                case "ROLB": return Position.ROLB;
                case "CB": return Position.CB;
                case "FS": return Position.FS;
                case "SS": return Position.SS;
                case "K": return Position.K;
                case "P": return Position.P;
                default: throw new Exception($"Unable to parse position: {position}");
            }
        }
    }
}

/// <summary>
/// The positions for a <see cref="MyLeague.Football.Data.Models.Player"/>
/// </summary>
public enum Position
{
    QB,
    RB,
    FB,
    WR,
    TE,
    LT,
    LG,
    C,
    RG,
    RT,
    LE,
    DT,
    RE,
    LOLB,
    MLB,
    ROLB,
    CB,
    FS,
    SS,
    P,
    K
}
