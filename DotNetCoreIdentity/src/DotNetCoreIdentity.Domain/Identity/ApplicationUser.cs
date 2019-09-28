using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreIdentity.Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public long? NationalIdNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
