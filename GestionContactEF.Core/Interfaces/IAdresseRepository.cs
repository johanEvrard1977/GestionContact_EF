using GestionContactEF.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContactEF.Core.Interfaces
{
    public interface IAdresseRepository : IRepository<int, Adresse>
    {
        Task<Adresse> GetOne(int id, bool lazyLoading);
        Task<Adresse> GetOne(string rue,
            string ville, int CP, int numero, int? boite);

        Task<IEnumerable<Adresse>> Get(bool lazyloading, string rue,
            string ville, int CP, int numero, int? boite);
        Task<bool> AlreadyExists(string rue, string ville, int CP, int numero, int? boite);
    }
}
