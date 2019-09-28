using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DotNetCoreIdentity.Application
{
   
    public class EntityDto<T> : CommonEntityDto
    {
        public T Id { get; set; }
    }
    // default base dto
    public class EntityDto : CommonEntityDto
    {
        public int Id { get; set; }
    }

    public class CommonEntityDto
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
