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
    public class PagedList<T> : List<T>
    {
        public MetaData MetaData { get; set; }
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);

        }
    }
<<<<<<< HEAD

=======
>>>>>>> a3d71f2f678ee5f4482d2fca01414a760c6bcf83
}
