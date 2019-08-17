using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StartEFCore.Models
{
    public class Team
    {
        // [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.ImageUrl)]
        public string LogoUrl { get; set; }
        [Required]
        public string Province { get; set; }
        public int? Year { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
