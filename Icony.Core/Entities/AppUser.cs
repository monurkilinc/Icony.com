using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icony.Core.Entities
{
    public class AppUser : Microsoft.AspNetCore.Identity.IdentityUser
    {
        public string? FullName { get; set; }
    }
}
