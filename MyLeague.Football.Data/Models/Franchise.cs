using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeague.Football.Data.Models
{
    public class Franchise : BaseDataModel
    {
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
        /// Gets or 
        /// </summary>
        public string Logo { get; private set; }

        public bool IsComputer { get; set; }

        /// <summary>
        /// Gets the full name of the franchise
        /// </summary>
        public string FullName() => $"{Region} {Name}";
    }
}
