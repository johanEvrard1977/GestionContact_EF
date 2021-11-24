using GestionContactEF.Core.Interfaces;
using GestionContactEF.Core.Paging;
using GestionContactEF.Dal.DBContext;
using GestionContactEF.Dal.Models;
using GestionContactEF.Dal.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContactEF.Core.Repositories
{
    public class ContactRepository : Repository<int, Contact>, IContactRepository
    {
        private readonly Context _context;
        private readonly DbSet<Contact> _entities;
        public ContactRepository(Context context) : base(context)
        {
            _context = context;
            _entities = _context.Set<Contact>();
        }


        public async Task<Contact> GetOne(int id, string email)
        {
            if (!(id == 0))
            {
                return await _entities
                    .Include(e => e.User)
                    .ThenInclude(e => e.UserRoles)
                    .ThenInclude(w => w.Role)
                    .Include(w => w.Adresse)
                    .Include(w => w.Image)
                    .Where(w => w.Id == id)
                    .FirstOrDefaultAsync();
            }
            else if (!(email is null))
            {
                return await _entities
                    .Include(e => e.User)
                    .ThenInclude(e => e.UserRoles)
                    .ThenInclude(w => w.Role)
                    .Include(w => w.Adresse)
                    .Include(w => w.Image)
                    .Where(w => w.Email.Equals(email))
                    .FirstOrDefaultAsync();
            }
            return await _entities.FirstAsync();
        }
        public async Task<IEnumerable<Contact>> Get(string lastname, string firstname)
        {
            var request = from contacts in _context.Contacts select contacts;
            if (firstname != null)
            {
                request = request.OrderBy(w => w.Prenom)
                    .Include(w => w.User)
                    .ThenInclude(w => w.UserRoles)
                    .ThenInclude(w => w.Role)
                    .Include(w => w.Adresse)
                    .Include(w => w.Image)
                    .Where(w => w.Prenom.Contains(firstname));
            }
            else if (lastname != null)
            {
                request = request.OrderBy(w => w.Nom)
                    .Include(w => w.User)
                    .ThenInclude(w => w.UserRoles)
                    .ThenInclude(w => w.Role)
                    .Include(w => w.Adresse)
                    .Include(w => w.Image)
                    .Where(w => w.Nom.Contains(lastname));
            }
            else
            {
                request = request.Include(w => w.User)
                    .ThenInclude(w => w.UserRoles)
                    .ThenInclude(w => w.Role)
                    .Include(w => w.Adresse)
                    .Include(w => w.Image)
                    .OrderBy(w => w.Nom);
            }
            return await request.ToListAsync();
        }

        public async Task<PagedList<Contact>> GetContacts(Parameters Parameters)
        {
            var contacts = await _context.Contacts.ToListAsync();
            return PagedList<Contact>
                .ToPagedList(contacts, Parameters.PageNumber, Parameters.PageSize);
        }

        public async Task<bool> AlreadyExists(int id)
        {
            if (await _context.Contacts.AnyAsync(e => e.Id == id))
                return true;
            return false;
        }
    }
}
