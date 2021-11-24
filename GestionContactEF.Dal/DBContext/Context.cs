using GestionContactEF.Dal.Models;
using GestionContactEF.Dal.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContactEF.Dal.DBContext
{
    public class Context : IdentityDbContext<User>
    {
        // membres masqués par IdentityDbContext<User>
        //public DbSet<User> Users { get; set; }
        //public DbSet<Role> Roles { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Adresse> Adresses { get; set; }
        public DbSet<Image> Images { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasKey("Id");
            builder.Entity<Contact>().HasKey("Id");
            builder.Entity<Image>().HasKey("Id");

            builder.Entity<User>(b =>
            {
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            builder.Entity<Contact>(u =>
            {
                u.HasOne(u => u.Image)
                .WithOne(u => u.Contact)
                .HasForeignKey<Contact>(u => u.ImageId);
            });
            builder.Entity<User>(b =>
            {
                b.HasMany(e => e.Contacts)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });
            builder.Entity<Adresse>(b =>
            {
                b.HasMany(e => e.Contacts)
                    .WithOne(e => e.Adresse)
                    .HasForeignKey(ur => ur.AdresseId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            builder.Entity<User>().Property(u => u.FirstName).IsRequired().HasMaxLength(75);
            builder.Entity<User>().Property(u => u.LastName).IsRequired().HasMaxLength(75);
            builder.Entity<Contact>().Property(u => u.Email).IsRequired().HasMaxLength(255);
            builder.Entity<Contact>().Property(u => u.Nom).IsRequired().HasMaxLength(75);
            builder.Entity<Contact>().Property(u => u.Prenom).IsRequired().HasMaxLength(75);
            builder.Entity<Adresse>().Property(u => u.Rue).IsRequired().HasMaxLength(150);
            builder.Entity<Adresse>().Property(u => u.Numero).IsRequired();
            builder.Entity<Adresse>().Property(u => u.Ville).IsRequired().HasMaxLength(75);
            builder.Entity<Adresse>().Property(u => u.CP).IsRequired();
            builder.Entity<Adresse>().Property(u => u.Boite);
            builder.Entity<Image>().Property(u => u.File).IsRequired();
            builder.Entity<Image>().Property(u => u.MimeType).IsRequired();



            //This line allows a unique relation (a user with a email could not be repeated)
            builder.Entity<User>()
                .HasIndex(uw => uw.Email).IsUnique();
            builder.Entity<Contact>()
                .HasIndex(uw => uw.Email).IsUnique();

            base.OnModelCreating(builder);

            
        }
    }
}