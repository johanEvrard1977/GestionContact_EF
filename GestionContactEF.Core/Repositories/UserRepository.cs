using GestionContactEF.Core.Interfaces;
using GestionContactEF.Core.Paging;
using GestionContactEF.Dal.DBContext;
using GestionContactEF.Dal.Models;
using GestionContactEF.Dal.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionContactEF.Core.Repositories
{
    public class UserRepository : Repository<string, User>, IUserRepository
    {
        private readonly Context _context;
        private readonly DbSet<User> _entities;
        public UserRepository(Context context) : base(context)
        {
            _context = context;
            _entities = _context.Set<User>();
        }


        public async Task<User> GetOne(string id, bool lazyLoading)
        {
            if (lazyLoading)
            {
                return await _entities
                    .Include(e => e.UserRoles)
                    .Where(w => w.Id.Equals(id))
                    .FirstOrDefaultAsync();
            }
            return await _entities.FirstAsync();
        }
        public async Task<IEnumerable<User>> Get(bool lazyLoading,string lastname, string firstname)
        {
            var request = from users in _context.Users select users;
            switch (lazyLoading)
            {
                case true:
                    if (firstname != null)
                    {
                        request = request.Include(w => w.UserRoles)
                            .ThenInclude(w => w.Role)
                            .OrderBy(w => w.FirstName)
                            .Where(w => w.FirstName.Contains(firstname));
                    }
                    else if (lastname != null)
                    {
                        request = request.Include(w => w.UserRoles)
                            .ThenInclude(w => w.Role)
                            .OrderBy(w => w.LastName)
                            .Where(w => w.LastName.Contains(firstname));
                    }
                    else
                    {
                        request = request.Include(w => w.UserRoles)
                            .ThenInclude(w => w.Role)
                            .OrderBy(w => w.LastName);
                    }
                    break;

                case false:
                    if (firstname != null)
                    {
                        request = request.OrderBy(w => w.FirstName)
                            .Where(w => w.FirstName.Contains(firstname));
                    }
                    else if (lastname != null)
                    {
                        request = request.OrderBy(w => w.LastName)
                            .Where(w => w.LastName.Contains(lastname));
                    }
                    else
                    {
                        request = request.OrderBy(w => w.LastName);
                    }
                    break;

            }
            return await request.ToListAsync();
        }


        public async Task<bool> UserMailExists(string mail)
        {
            if (await _context.Users.AnyAsync(e => e.Email == mail))
                return true;
            return false;
        }


        public async Task<bool> AlreadyExists(string id)
        {
            if (await _context.Users.AnyAsync(e => e.Id.Equals(id)))
                return true;
            return false;
        }

        public async Task<PagedList<User>> GetUsers(Parameters Parameters)
        {
            var users = await _context.Users.ToListAsync();
            return PagedList<User>
                .ToPagedList(users, Parameters.PageNumber, Parameters.PageSize);
        }
    }
}
