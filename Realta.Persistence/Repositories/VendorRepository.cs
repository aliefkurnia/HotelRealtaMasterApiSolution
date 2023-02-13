using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Persistence.Base;
using Realta.Persistence.Interface;
using Realta.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Persistence.Repositories
{
    internal class VendorRepository : RepositoryBase<Vendor>, IVendorRepository
    {
        public VendorRepository(AdoDbContext AdoContext) : base (AdoContext) 
        { 
        }

        public void Edit(Vendor vendor)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vendor> FindAllVendor()
        {
            IEnumerator<Vendor> dataSet = FindAll<Vendor>("Select * From purchasing.vendor");

            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;
            }
        }

        public Task<IEnumerable<Vendor>> FindAllVendorAsync()
        {
            throw new NotImplementedException();
        }

        public Vendor FindVendorById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Vendor vendor)
        {
            throw new NotImplementedException();
        }

        public void Remove(Vendor vendor)
        {
            throw new NotImplementedException();
        }
    }
}
