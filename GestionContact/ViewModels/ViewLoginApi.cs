using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionContact.ViewModels
{
    public class ViewLoginApi
    {
        [Required]
        [DisplayName(displayName: "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }
    }
}
