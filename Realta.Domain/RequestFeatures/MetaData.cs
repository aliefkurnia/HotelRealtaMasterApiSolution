<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.RequestFeatures
=======
﻿namespace Realta.Domain.RequestFeatures
>>>>>>> a3d71f2f678ee5f4482d2fca01414a760c6bcf83
{
    public class MetaData
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
<<<<<<< HEAD
=======

>>>>>>> a3d71f2f678ee5f4482d2fca01414a760c6bcf83
}
