using Realta.Domain.Dto;
using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IVendorProductRepository
    {
        Task<IEnumerable<VendorProduct>> FindAllVendorProductAsync();
        VendorProduct FindVendorProductById(int vendorId);
        Task<IEnumerable<VendorProduct>> FindVendorProductByVendorId(int vendorId);
        //    Task<IEnumerable<VendorProduct>> GetVendorProductPaging(VendorProductParameters vendorProductParameters);
        VendorProductNested GetVendorProduct(int VendorId);
        void Insert(VendorProduct venPro);
        void Edit(VendorProduct venPro);
        void Remove(VendorProduct venPro);
        bool ValidasiInsert(int stockId, int vendorId);

    }
}
