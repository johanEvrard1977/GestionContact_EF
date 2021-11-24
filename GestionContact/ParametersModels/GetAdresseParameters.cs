using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionContact.ParametersModels
{
    public class GetAdresseParameters
    {
        public int Numero { get; set; }
        public int CP { get; set; }
        public int Boite { get; set; }
        public string Ville { get; set; }
        public string Rue { get; set; }
    }
}
