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

        [Required(ErrorMessage = "Takım adı alanı zorunludur")]
        [Display(Name = "Takım Adı")]
        [StringLength(25, ErrorMessage = "{0} maksimum {1} karakter, minimum {2} karakter uzunluğunda olmalı", MinimumLength = 5)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Logo alanı zorunludur")]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Logo")]
        public string LogoUrl { get; set; }
        [Required(ErrorMessage = "Şehir alanı zorunludur")]
        [Display(Name = "Şehir")]
        public string Province { get; set; }
        [Display(Name = "Kuruluş")]
        [Range(1870, 2019, ConvertValueInInvariantCulture = true, ErrorMessage = "{0} yıl aralığı {1} - {2} olmalı")]
        public int? Year { get; set; }

        public virtual ICollection<Player> Players { get; set; }


    }
}
