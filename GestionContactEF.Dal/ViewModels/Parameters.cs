using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionContactEF.Dal.ViewModels
{
    public class Parameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 4;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;

            }
        }
    }
}
