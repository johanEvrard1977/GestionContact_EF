using GestionContactEF.Core.Interfaces;
using GestionContactEF.Dal.DBContext;
using GestionContactEF.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContactEF.Core.Repositories
{
    public class ImageRepository : Repository<int, Image>, IImageRepository
    {
        private readonly Context _context;
        public ImageRepository(Context context) : base(context)
        {
            _context = context;
        }
    }
}
