using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreIdentity.Domain.PostTypes
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public string UrlName { get; set; }
    }
}
