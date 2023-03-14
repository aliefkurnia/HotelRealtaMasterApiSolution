using Realta.Domain.Dto;
using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Realta.Domain.Repositories
{
    public interface IVendorRepository
    {
        IEnumerable<Vendor> FindAllVendor();
        Task<IEnumerable<Vendor>> FindAllVendorAsync();
        Task<IEnumerable<Vendor>> GetVendorPaging (VendorParameters vendorParameters);
        Task<PagedList<Vendor>> GetVendorPage (VendorParameters vendorParameters);
        Vendor FindVendorById(int id);
        void Insert(Vendor vendor);
        void Edit(Vendor vendor);
        void Remove(Vendor vendor);
    }
}
