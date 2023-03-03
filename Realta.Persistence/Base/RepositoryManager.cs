using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Persistence.Repositories;
using Realta.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Persistence.Base
{
    public class RepositoryManager : IRepositoryManager
    {
        private AdoDbContext _adoContext;
        private IRegionsRepository _regionsRepository;
        private ICountryRepository _countryRepository;
        private IProvincesRepository _provincesRepository;
        private IAddressRepository _addressRepository;
        private IMembersRepository _membersRepository;
        private IServiceTaskRepository _service_TaskRepository;
        private IPriceItemsRepository _price_itemsRepository;
        private IPolicyRepository _policyRepository;
        private ICategory_GroupRepository _category_GroupRepository;
        private Lazy<IPriceItemsPhotoRepository> _priceItemsPhotoRepository;
        public RepositoryManager(AdoDbContext adoContext)
        {
            _adoContext = adoContext;
            _priceItemsPhotoRepository = new Lazy<IPriceItemsPhotoRepository>(() => new PriceItemsPhotoRepository (adoContext));
        }



        public IRegionsRepository RegionRepository
        {
            get
            {
                if (_regionsRepository == null)
                {
                    _regionsRepository = new RegionsRepository(_adoContext);
                }
                return _regionsRepository;
            }
        }

        public ICountryRepository CountryRepository
        {
            get
            {
                if (_countryRepository == null)
                {
                    _countryRepository = new CountryRepository(_adoContext);
                }
                return _countryRepository;
            }
        }

        public IProvincesRepository ProvincesRepository
        {
            get
            {
                if (_provincesRepository == null)
                {
                    _provincesRepository = new ProvincesRepository(_adoContext);
                }
                return _provincesRepository;
            }
        }

        public IAddressRepository AddressRepository
        {
            get
            {
                if (_addressRepository == null)
                {
                    _addressRepository = new AddressRepository(_adoContext);
                }
                return _addressRepository;
            }
        }

        public IMembersRepository MembersRepository
        {
            get
            {
                if (_membersRepository == null)
                {
                    _membersRepository = new MembersRepository(_adoContext);
                }
                return _membersRepository;
            }
        }

        public IServiceTaskRepository ServiceTaskRepository
        {
            get
            {
                if (_service_TaskRepository == null)
                {
                    _service_TaskRepository = new ServiceTaskRepository(_adoContext);
                }
                return _service_TaskRepository;
            }
        }


        public IPriceItemsRepository PriceItemsRepository
        {
            get
            {
                if (_price_itemsRepository == null)
                {
                    _price_itemsRepository = new PriceItemsRepository(_adoContext);
                }
                return _price_itemsRepository;
            }
        }


        public IPolicyRepository PolicyRepository
        {
            get
            {
                if (_policyRepository == null)
                {
                    _policyRepository = new PolicyRepository(_adoContext);
                }
                return _policyRepository;
            }
        }


        public ICategory_GroupRepository CategoryGroupRepository
        {
            get
            {
                if (_category_GroupRepository == null)
                {
                    _category_GroupRepository = new CategoryGroupRepository(_adoContext);
                }
                return _category_GroupRepository;
            }
        }

        public IPriceItemsPhotoRepository PriceItemsPhotoRepository => _priceItemsPhotoRepository.Value;
    }
}
