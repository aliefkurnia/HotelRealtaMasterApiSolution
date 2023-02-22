using Realta.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Base
{
    public interface IRepositoryManager
    {
        IRegionsRepository RegionRepository { get; }
        ICountryRepository CountryRepository { get; }
        IProvincesRepository ProvincesRepository { get; }
        IAddressRepository AddressRepository { get; }
        IMembersRepository MembersRepository { get; }
        IService_TaskRepository service_TaskRepository { get; }
        IPrice_ItemsRepository price_itemsRepository { get; }
        IPolicyRepository policyRepository { get; }
        ICategory_GroupRepository category_groupRepository { get; }
    }
}
