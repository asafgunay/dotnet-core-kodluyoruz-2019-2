using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StartEFCore.Data
{
    public class Player : Entity
    {
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [StringLength(30, ErrorMessage = "{0} alanı geçerli maksimum karakter uzunluğu {1}, minimum karakter uzunluğu ise {2}", MinimumLength = 5)]
        [Display(Name = "Ad Soyad")]
        public string LongName { get; set; }
        [Display(Name = "Yaş")]
        [Range(17, 44, ErrorMessage = "{0} sınırı {1} - {2} arasındadır")]
        public int? Age { get; set; }
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [Display(Name = "Numara")]
        [Range(1, 99, ErrorMessage = "{0} seçim aralığı {1} - {2} arasındadır")]
        public int Number { get; set; }
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [Display(Name = "Pozisyon")]
        [StringLength(30, ErrorMessage = "{0} alanı içib maksimum karakter uzunluğu {1}")]
        public string Position { get; set; }
        [Display(Name = "Resim")]
        public string ImageUrl { get; set; }
        [Display(Name = "Takım")]
        public virtual int? TeamId { get; set; }
        [ForeignKey("TeamId")]
        [Display(Name = "Takım")]
        public virtual Team Team { get; set; }
    }
}
