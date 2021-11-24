using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContact.Models
{
    public class RoleApi
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<UserRoleApi> UserRoles { get; set; }
    }
}
