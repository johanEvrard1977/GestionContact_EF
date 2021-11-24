using System;
using System.ComponentModel.DataAnnotations;

namespace GestionContact.Models
{
    public class ContactApi
    {
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
        public string Email { get; set; }
        [Required]
        public string Telephone { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? Date_De_Naissance { get; set; }
        public string UserId { get; set; }
        public virtual UserApi User { get; set; }

        public int NombreDeSeancesAuthorisee { get; set; }
        public int AdresseId { get; set; }
        public virtual AdresseApi Adresse { get; set; }
        public int? ImageId { get; set; }
        public virtual ImageApi Image { get; set; }
        public string ImageUri { get; set; }
        public string MimeType { get; set; }
        public byte[] File { get; set; }

        //infos concernant l'adresse
        public string Rue { get; set; }
        public string Ville { get; set; }
        public int Numero { get; set; }
        public int CP { get; set; }
        public int? Boite { get; set; }

    }
}
