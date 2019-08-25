using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreIdentity.Domain.Identity
{
    public class ApplicationUser :IdentityUser
    {
        public int NationalIdNumber { get; set; }
    }
}
