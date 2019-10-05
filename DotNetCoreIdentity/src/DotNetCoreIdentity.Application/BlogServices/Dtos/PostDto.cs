using DotNetCoreIdentity.Application.CategoryServices.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DotNetCoreIdentity.Application.BlogServices.Dtos
{
    public class PostDto : EntityDto<Guid>
    {
        [Display(Name ="Başlık")]
        public string Title { get; set; }
        [Display(Name ="İçerik")]
        public string Content { get; set; }
        [Display(Name ="SEO Dostu Url")]
        public string UrlName { get; set; }
        [Display(Name ="Resim")]
        public string ImageUrl { get; set; }
        [Display(Name ="Kategori")]
        public int? CategoryId { get; set; }
        public CategoryDto Category { get; set; }
       
        public string PlainContent { get; set; }
    }
}
