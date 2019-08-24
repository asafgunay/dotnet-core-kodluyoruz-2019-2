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
        [Display(Name ="Takım Adı")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name ="Logo")]
        public string LogoUrl { get; set; }
        [Required]
        [Display(Name ="Şehir")]
        public string Province { get; set; }
        [Display(Name ="Kuruluş")]
        public int? Year { get; set; }

        public virtual ICollection<Player> Players { get; set; }


    }
}
