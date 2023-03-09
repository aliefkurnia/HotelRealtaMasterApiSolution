using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.RequestFeatures
{
    public abstract class RequestParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;

<<<<<<< HEAD
        // jumlah data yang akan di fetch
        private int _pageSize = 10;
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

=======
        //jumlah data yang akan di fetch dari database
        private int _pageSize = 10;
        public int PageSize { 
            get 
            { 
                return _pageSize; 
            }
            set
            {
                _pageSize = (value>maxPageSize) ? maxPageSize : value;
            }
        }
    }
    public class PurchaseOrderParameters : RequestParameters
    {
        public string? Keyword { get; set; }
        public int? Status { get; set; }
>>>>>>> a3d71f2f678ee5f4482d2fca01414a760c6bcf83
    }
}
