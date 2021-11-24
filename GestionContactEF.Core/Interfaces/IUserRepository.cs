using GestionContactEF.Core.Paging;
using GestionContactEF.Dal.Models;
using GestionContactEF.Dal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContactEF.Core.Interfaces
{
    public interface IUserRepository : IRepository<string, User>
    {
        Task<User> GetOne(string id, bool lazyLoading);

        Task<IEnumerable<User>> Get(bool lazyloading, string lastName,
            string firstName);
        Task<bool> UserMailExists(string mail);

        Task<bool> AlreadyExists(string id);
        Task<PagedList<User>> GetUsers(Parameters Parameters);
    }
}
