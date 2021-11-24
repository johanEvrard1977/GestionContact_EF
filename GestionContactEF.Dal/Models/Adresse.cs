using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContactEF.Dal.Models
{
    public class Adresse
    {
        public int Id { get; set; }
        public string Rue { get; set; }
        public int Numero { get; set; }
        public int CP { get; set; }
        public int? Boite { get; set; }
        public string Ville { get; set; }
        public IEnumerable<Contact> Contacts { get; set; }
    }
}
