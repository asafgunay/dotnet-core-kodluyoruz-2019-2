using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartEFCore.Models.Shared
{
    // generic base class
    public class Entity<T> : CommonEntity
    {
        // [Key]
        public T Id { get; set; }
        // Generic olarak belirtmek demek onun tip degiskenligine sahip olmasi demek
    }
    // default base class
    public class Entity : CommonEntity
    {
        // [Key]
        public int Id { get; set; }
    }

    // non generic base class fields
    public class CommonEntity
    {
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }
        public string HiddenValue { get; set; }
    }
}
