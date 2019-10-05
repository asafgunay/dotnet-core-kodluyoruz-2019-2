using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DotNetCoreIdentity.Application.CategoryServices.Dtos
{
    public class CategoryDto : EntityDto
    {
        [Display(Name="Kategori Adı")]
        public string Name { get; set; }
        [Display(Name ="Seo Dostu Url")]
        public string UrlName { get; set; }
    }
}
