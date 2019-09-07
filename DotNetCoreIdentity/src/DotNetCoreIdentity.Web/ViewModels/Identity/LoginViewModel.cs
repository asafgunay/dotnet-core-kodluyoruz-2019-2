using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreIdentity.Web.ViewModels.Identity
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        [StringLength(20, ErrorMessage = "{0} alanı en fazla {1} karakter olabilir. En az {2} karakter olabilir.", MinimumLength = 6)]
        public string Password { get; set; }
        [Display(Name ="Beni hatırla")]
        public bool RememberMe { get; set; } = false;
    }
}
