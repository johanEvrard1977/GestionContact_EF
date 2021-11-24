using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContact.Models
{
    public class UserRoleApi : IdentityUserRole<string>
    {
        public virtual RoleApi Role { get; set; }
        public virtual UserApi User { get; set; }
    }
}
