using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLeague.Football.Data.Models
{
    public class BaseDataModel
    {
        /// <summary>
        /// Tables primary key
        /// </summary>
        [Key]        
        public int Id { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> of when row was created
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> of when row was last updated
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated { get; set; }
    }
}
