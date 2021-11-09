using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeague.Football.Data.Models
{
    public class Player : BaseDataModel
    {
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
        public DateTime DateDrafted { get; set; }
        
        public int? FranchiseId { get; set; }

        [ForeignKey("FranchiseId")]
        public Franchise Franchise { get; set; }

        public int JerseyNumber { get; set; }

        public Contract Contract { get; set; }

        public PlayerAttributes PlayerAttributes { get; set; }

        public string Experience(DateTime currentDate)
        {
            int years = currentDate.Year - DateDrafted.Year;

            if (years == 0)
            {
                return "R";
            }
            else
            {
                return years.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the player's age
        /// </summary>
        public int Age(DateTime currentDate)
        {
            return currentDate.Year - this.DateOfBirth.Year;
        }

        public string FullName() => $"{FirstName}, {LastName}";
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
