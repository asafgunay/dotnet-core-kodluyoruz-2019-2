using DotNetCoreIdentity.Application.CategoryServices.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreIdentity.Application.BlogServices.Dtos
{
    public class PostDto : EntityDto<Guid>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string UrlName { get; set; }
        public string ImageUrl { get; set; }
        public int? CategoryId { get; set; }
        public CategoryDto Category { get; set; }
    }
}
