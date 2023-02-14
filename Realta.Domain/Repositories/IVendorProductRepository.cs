using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IVendorProductRepository
    {
        IEnumerable<VendorProduct> FindAllVendorProduct();
        Task<IEnumerable<VendorProduct>> FindAllVendorProductAsync();
        VendorProduct FindVendorProductById(int id);
        void Insert(VendorProduct venPro);
        void Edit(VendorProduct venPro);
        void Remove(VendorProduct venPro);
    }
}
