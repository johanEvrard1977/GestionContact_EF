using GestionContactEF.Dal.DBContext;
using GestionContactEF.Dal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContactEF.Dal.Seed
{
    public class SeedDb
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<Context>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            User user = new User();
            User recruteur = new User();
            User candidat = new User();
            context.Database.EnsureCreated();

            if (!roleManager.RoleExistsAsync("SuperAdmin").Result)
            {
                IdentityRole recruteurRole = new IdentityRole("SuperAdmin");
                IdentityResult res = roleManager.CreateAsync(recruteurRole).Result;
            }
            if (!roleManager.RoleExistsAsync("Recruteur").Result)
            {
                IdentityRole recruteurRole = new IdentityRole("Recruteur");
                IdentityResult res = roleManager.CreateAsync(recruteurRole).Result;
            }
            if (!roleManager.RoleExistsAsync("Candidat").Result)
            {
                IdentityRole candidatRole = new IdentityRole("Candidat");
                IdentityResult res = roleManager.CreateAsync(candidatRole).Result;
            }

            if (!context.Users.Any())
            {

                user.Email = "superAdmin@gmail.com";
                user.SecurityStamp = Guid.NewGuid().ToString();
                user.UserName = "SuperAdmin";
                user.FirstName = "Super";
                user.LastName = "Admin";
                user.EmailConfirmed = true;
                IdentityResult res = userManager.CreateAsync(user, "SuperAdmin007!").Result;
                if (res.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "SuperAdmin").Wait();
                }
                recruteur.Id = "ac95d77e-abc6-4fd0-a234-34097e455a87";
                recruteur.Email = "recruteur@gmail.com";
                recruteur.SecurityStamp = Guid.NewGuid().ToString();
                recruteur.UserName = "Recruteur";
                recruteur.FirstName = "Super";
                recruteur.LastName = "Recruteur";
                recruteur.EmailConfirmed = true;
                res = userManager.CreateAsync(recruteur, "Recruteur007!").Result;
                if (res.Succeeded)
                {
                    userManager.AddToRoleAsync(recruteur, "Recruteur").Wait();
                }

                candidat.Id = "47c504c3-20c7-4838-b758-9998e3776acc";
                candidat.Email = "candidat@gmail.com";
                candidat.SecurityStamp = Guid.NewGuid().ToString();
                candidat.UserName = "Candidat";
                candidat.FirstName = "Super";
                candidat.LastName = "Candidat";
                candidat.EmailConfirmed = true;
                res = userManager.CreateAsync(candidat, "Candidat007!").Result;
                if (res.Succeeded)
                {
                    userManager.AddToRoleAsync(candidat, "Candidat").Wait();
                }
            }



            if (!context.Adresses.Any())
            {
                Adresse adresse = new Adresse
                {
                    Rue = "Rue Ferrer",
                    Ville = "Bruxelles",
                    Numero = 20,
                    Boite = 2,
                    CP = 1000
                };
                context.Adresses.Add(adresse);
                context.Entry(adresse).State = EntityState.Added;
                context.SaveChanges();
            }

            if (!context.Contacts.Any())
            {
                Contact contact = new Contact
                {
                    Nom = "BonBon",
                    Prenom = "toto",
                    Date_De_Naissance = DateTime.Parse(DateTime.Now.Date.ToShortTimeString().ToString()),
                    Email = "toto@gmail.com",
                    Telephone = "0492042565",
                    UserId = context.Users.First().Id,
                    AdresseId = context.Adresses.First().Id,
                    NombreDeSeancesAuthorisee = 10

                };
                context.Contacts.Add(contact);
                context.Entry(contact).State = EntityState.Added;
                context.SaveChanges();
                Contact contact2 = new Contact
                {
                    Nom = "la fouine",
                    Prenom = "fifou",
                    Date_De_Naissance = DateTime.Parse(DateTime.Now.Date.ToShortTimeString().ToString()),
                    Email = "lafouine@gmail.com",
                    Telephone = "0492042565",
                    UserId = context.Users.First().Id,
                    AdresseId = context.Adresses.First().Id,
                    NombreDeSeancesAuthorisee = 10

                };
                context.Contacts.Add(contact2);
                context.Entry(contact2).State = EntityState.Added;
                context.SaveChanges();
            }
        }
    }
}
