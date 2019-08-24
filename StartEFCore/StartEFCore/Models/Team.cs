using StartEFCore.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StartEFCore.Models
{
    public class Team : Entity<int>
    {

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
