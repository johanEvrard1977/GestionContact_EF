using GestionContactEF.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContactEF.Core.Interfaces
{
    public interface IImageRepository : IRepository<int, Image>
    {
    }
}
