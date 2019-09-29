using DotNetCoreIdentity.Domain.PostTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DotNetCoreIdentity.Domain.BlogEntries
{
    public class Post : Entity<Guid>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string UrlName { get; set; }
        public string ImageUrl { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public virtual int? CategoryId { get; set; }
    }
}
