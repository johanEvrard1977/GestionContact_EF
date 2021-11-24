using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionContact.ParametersModels
{
    public class GetContactParameters : GetParameters
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
    }
}
