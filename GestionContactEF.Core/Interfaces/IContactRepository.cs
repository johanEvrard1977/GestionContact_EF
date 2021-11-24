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
    public interface IContactRepository : IRepository<int, Contact>
    {
        Task<Contact> GetOne(int id, string email);

        Task<IEnumerable<Contact>> Get(string lastName,
            string firstName);
        Task<bool> AlreadyExists(int id);
        Task<PagedList<Contact>> GetContacts(Parameters Parameters);
    }
}