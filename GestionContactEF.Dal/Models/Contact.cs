using System;

namespace GestionContactEF.Dal.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public DateTime? Date_De_Naissance { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int NombreDeSeancesAuthorisee { get; set; }
        public int AdresseId { get; set; }
        public virtual Adresse Adresse { get; set; }
        public int? ImageId { get; set; }
        public virtual Image Image { get; set; }
    }
}
