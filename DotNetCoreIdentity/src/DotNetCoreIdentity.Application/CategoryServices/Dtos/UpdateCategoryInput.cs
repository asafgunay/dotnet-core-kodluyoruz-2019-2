using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreIdentity.Application.CategoryServices.Dtos
{
    public class UpdateCategoryInput : CreateCategoryInput
    {
        public int Id { get; set; }
        public string ModifiedById { get; set; }
        public string ModifiedBy { get; set; }

        //public string Name { get; set; }
        //public string UrlName { get; set; }
        //public string CreatedById { get; set; }
    }
}
