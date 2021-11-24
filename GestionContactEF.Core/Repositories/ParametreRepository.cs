using GestionContactEF.Core.Interfaces;
using GestionContactEF.Dal.DBContext;
using GestionContactEF.Dal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContactEF.Core.Repositories
{
    public class ParametreRepository : Repository<int, Parameters>, IParametreRepository
    {
        private readonly Context _context;
        public ParametreRepository(Context context) : base(context)
        {
            _context = context;
        }
    }
}
