using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IAddressRepository
    {
        IEnumerable<Address> FindAllAddress();
        Task<IEnumerable<Address>> FindAllAddressAsync();
        Address FindAddressById(int id);
        void Insert(Address address);
        void Edit(Address address);
        void Remove(Address address);
        Task<PagedList<Address>> GetAddressPageList(AddressParameter addressParameter);
    }
}
