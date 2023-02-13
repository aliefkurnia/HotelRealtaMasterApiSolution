using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IVendorRepository
    {
        IEnumerable<Vendor> FindAllVendor();
        Task<IEnumerable<Vendor>> FindAllVendorAsync();
        Vendor FindVendorById(int id);
        void Insert(Vendor vendor);
        void Edit(Vendor vendor);
        void Remove(Vendor vendor);
    }
}
