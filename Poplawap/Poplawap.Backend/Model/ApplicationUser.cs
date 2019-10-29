using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Poplawap.Backend.Model
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Verified { get; set; }
        public string County { get; set; }

        public virtual ICollection<Comments> Comments { get; set; }

        public virtual ICollection<Sales> Sales { get; set; }
    }
}
