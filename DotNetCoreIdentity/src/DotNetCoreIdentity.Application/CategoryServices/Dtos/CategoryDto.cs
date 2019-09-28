using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreIdentity.Application.CategoryServices.Dtos
{
    public class CategoryDto : EntityDto
    {
        public string Name { get; set; }
        public string UrlName { get; set; }
    }
}
