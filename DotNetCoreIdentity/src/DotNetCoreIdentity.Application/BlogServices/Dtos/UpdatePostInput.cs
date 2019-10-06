using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreIdentity.Application.BlogServices.Dtos
{
    public class UpdatePostInput : CreatePostInput
    {
        public Guid Id { get; set; }
        public string ModifiedById { get; set; }
        public string ModifiedBy { get; set; }

    }
}
