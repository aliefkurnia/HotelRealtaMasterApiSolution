using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Persistence.Base;
using Realta.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Persistence.Repositories
{
    internal class VendorProductRepository : RepositoryBase<VendorProduct>, IVendorProductRepository
    {
        public VendorProductRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(VendorProduct venPro)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VendorProduct> FindAllVendorProduct()
        {
            IEnumerator<VendorProduct> dataSet = FindAll<VendorProduct>("Select * From purchasing.vendor_product");

            while (dataSet.MoveNext())
            {
                var data = dataSet.Current;
                yield return data;
            }
        }

        public Task<IEnumerable<VendorProduct>> FindAllVendorProductAsync()
        {
            throw new NotImplementedException();
        }

        public VendorProduct FindVendorProductById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(VendorProduct venPro)
        {
            throw new NotImplementedException();
        }

        public void Remove(VendorProduct venPro)
        {
            throw new NotImplementedException();
        }
    }
}
