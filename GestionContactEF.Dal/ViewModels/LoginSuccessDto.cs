using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionContactEF.Dal.ViewModels
{
    public class LoginSuccessDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
