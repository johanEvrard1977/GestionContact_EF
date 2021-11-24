using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionContact.Models
{
    public class AdresseApi
    {
        public int Id { get; set; }
        public string Rue { get; set; }
        public int Numero { get; set; }
        public int CP { get; set; }
        public int? Boite { get; set; }
        public string Ville { get; set; }
        public IEnumerable<ContactApi> Contacts { get; set; }
    }
}
