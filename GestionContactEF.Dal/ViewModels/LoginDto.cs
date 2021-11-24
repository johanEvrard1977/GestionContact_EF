using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionContactEF.Dal.ViewModels
{
    public class LoginDto
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
