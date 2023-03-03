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
        IServiceTaskRepository ServiceTaskRepository { get; }
        IPriceItemsRepository PriceItemsRepository { get; }
        IPolicyRepository PolicyRepository { get; }
        ICategory_GroupRepository CategoryGroupRepository { get; }
        IPriceItemsPhotoRepository PriceItemsPhotoRepository { get; }
    }
}
