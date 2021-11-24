using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContact.Models
{
    public class UserApi
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "FirstName is required")]
        [MaxLength(75)]
        [MinLength(2)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        [MaxLength(75)]
        [MinLength(2)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        [MaxLength(255)]
        public string Email { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "vous devez spécifier un mot de passe compris entre 8 et 30 caractères" +
            "avec un chiffre compris entre 0 et 9, contenir au moins une minuscule et une majuscule," +
            "contenir un caractere special tel que @,#,$ ou %")]
        //doit contenit au moins un chiffre entre 0 et 9 (?=.*\d)
        //doit contenir au moins une minuscule et une majuscule (?=.*[a-z])  (?=.*[A-Z]) 
        //doit contenir un caractere special tel que @,#,$ ou %
        // doit matcher avec ce qu'il vient d'être définit: .
        // au moins 8 caractères et au plus 30 caractères {8,30}
        [RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{8,30})(?=.*\\d)(?=.*[a - z])(?=.*[A - Z])(?=.*[@#$%].{8, 40})")]
        public string Password { get; set; }
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public virtual IEnumerable<UserRoleApi> UserRoles { get; set; }
        public virtual IEnumerable<ContactApi> Contacts { get; set; }
        
    }
}
