using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreIdentity.Web.ViewModels.Manage
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        [DisplayName("Rol Adı")]
        public string Name { get; set; }
    }
}
