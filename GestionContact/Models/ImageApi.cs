using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionContact.Models
{
    public class ImageApi
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "File is required")]
        public byte[] File { get; set; }
        [Required(ErrorMessage = "MimeType is required")]
        public string MimeType { get; set; }
        public string ImageUri { get; set; }
    }
}
