using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DotNetCoreIdentity.Domain
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
        [Display(Name = "Oluşturma Tarihi")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        [Display(Name = "Son Güncelleme")]
        public DateTime? ModifiedDate { get; set; }
       
        [Display(Name = "Oluşturan Kullanıcı")]
        public string CreatedBy { get; set; }
        public string CreatedById { get; set; }

        [Display(Name = "Düzenleyen Kullanıcı")]
        public string ModifiedBy { get; set; }
        public string ModifiedById { get; set; }
    }
}
