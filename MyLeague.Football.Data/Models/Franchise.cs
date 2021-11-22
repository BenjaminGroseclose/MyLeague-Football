using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyLeague.Football.Data.Models
{
    public class Franchise
    {
        /// <summary>
        /// Used to create an inital franchise
        /// </summary>
        public Franchise(int id,
                         string region,
                         string name,
                         string abbrevation,
                         string logo,
                         string primaryColor,
                         string secondaryColor,
                         Conference conference,
                         Division division)
        {
            this.Id = id;
            this.Region = region;
            this.Name = name;
            this.Abbrevation = abbrevation;
            this.Logo = logo;
            this.PrimaryColor = primaryColor;
            this.SecondaryColor = secondaryColor;
            this.Conference = conference;
            this.Division = division;
        }

        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the franchise's region. Example: Detroit
        /// </summary>
        public string Region { get; private set; }

        /// <summary>
        /// Gets or sets the franchies's name. Example: Lions
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the franchise's abbreviation. Example DET
        /// </summary>
        public string Abbrevation { get; private set; }

        /// <summary>
        /// Gets the franchise's svg logo
        /// </summary>
        public string Logo { get; private set; }

        /// <summary>
        /// Gets or sets if the franchise is controlled by the computer or not.
        /// If false can be assumed that is user's team.
        /// </summary>
        public bool IsComputer { get; set; }

        /// <summary>
        /// Gets the franchise's primary color
        /// </summary>
        public string PrimaryColor { get; private set; }

        /// <summary>
        /// Gets the franchise's secondary color
        /// </summary>
        public string SecondaryColor { get; private set; }

        public Conference Conference { get; private set; }

        public Division Division { get; private set; }

        public IEnumerable<Player> Players { get; private set; }

        /// <summary>
        /// Gets the full name of the franchise
        /// </summary>
        public string FullName() => $"{Region} {Name}";
    }

    public enum Conference
    {
        AFC,
        NFC
    }

    public enum Division
    {
        NORTH, 
        EAST,
        SOUTH,
        WEST
    }
}
