using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContactEF.Dal.Models
{
    public class Image
    {
        public int Id { get; set; }
        public byte[] File { get; set; }
        public string MimeType { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
