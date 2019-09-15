using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreIdentity.Application.CategoryServices.Dtos
{
    public class UpdateCategoryInput : CreateCategoryInput
    {
        public int Id { get; set; }
        public string ModifiedById { get; set; }
    }
}
