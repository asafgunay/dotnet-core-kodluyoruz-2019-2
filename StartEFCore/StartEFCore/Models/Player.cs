using StartEFCore.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StartEFCore.Models
{
    public class Player : Entity
    {
        [Required]
        [Display(Name ="Ad Soyad")]
        public string LongName { get; set; }
        [Display(Name ="Yaş")]
        public int? Age { get; set; }
        [Required]
        [Display(Name ="Numara")]
        public int Number { get; set; }
        [Required]
        [Display(Name ="Pozisyon")]
        public string Position { get; set; }
        [Display(Name ="Resim")]
        public string ImageUrl { get; set; }
        [Display(Name ="Takım")]
        public virtual int? TeamId { get; set; }
        [ForeignKey("TeamId")]
        [Display(Name ="Takım")]
        public virtual Team Team { get; set; }

    }
}
