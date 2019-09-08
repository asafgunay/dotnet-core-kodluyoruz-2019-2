using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreIdentity.Web.ViewModels.Manage
{
    public class AssignRoleViewModel
    {
        public string UserId { get; set; }
        [DisplayName("Eklemek istediğiniz rolü seçin")]
        public string RoleId { get; set; }
        public List<SelectListItem> RoleList { get; set; }
    }
}
