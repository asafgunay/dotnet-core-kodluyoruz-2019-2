using System.ComponentModel.DataAnnotations;

namespace DotNetCoreIdentity.Application.BlogServices.Dtos
{
    public class CreatePostInput
    {
        [Required]
        [Display(Name ="Başlık")]
        public string Title { get; set; }
        [Required]
        [Display(Name ="İçerik")]
        public string Content { get; set; }
        [Required]
        [Display(Name ="Seo Dostu Url")]
        public string UrlName { get; set; }
        [Required]
        [Display(Name ="Kategori")]
        public int? CategoryId { get; set; }
        public string CreatedById { get; set; }
        public string CreatedBy { get; set; }

    }
}
