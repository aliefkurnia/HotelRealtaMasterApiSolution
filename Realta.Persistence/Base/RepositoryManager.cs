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
        private readonly AdoDbContext _adoContext;
        private IVendorRepository _vendorRepository;
        private Lazy<IPurchaseOrderRepository> _purchaseOrderRepository;
        private Lazy<ICartRepository> _cartRepository;
        private IStockRepository _stockRepository;
        private IStockDetailRepository _stockDetailRepository;
        private Lazy<IStockPhotoRepository> _stockPhotoRepository;
        private IVendorProductRepository _vendorProductRepository;
        private IRegionsRepository _regionsRepository;
        private ICountryRepository _countryRepository;
        private IProvincesRepository _provincesRepository;
        private IAddressRepository _addressRepository;
        private IMembersRepository _membersRepository;
        private IServiceTaskRepository _service_TaskRepository;
        private IPriceItemsRepository _price_itemsRepository;
        private IPolicyRepository _policyRepository;
        private ICategoryGroupRepository _category_GroupRepository;
        private Lazy<IPriceItemsPhotoRepository> _priceItemsPhotoRepository;
        private Lazy<ICategoryGroupPhotoRepository> _categoryGroupPhotoRepository;

        public RepositoryManager(AdoDbContext adoContext)
        {
            _adoContext = adoContext;
            _purchaseOrderRepository = new Lazy<IPurchaseOrderRepository>(() => new PurchaseOrderRepository(adoContext));
            _cartRepository = new Lazy<ICartRepository>(() => new CartRepository(adoContext));
<<<<<<< HEAD
            _priceItemsPhotoRepository = new Lazy<IPriceItemsPhotoRepository>(() => new PriceItemsPhotoRepository (adoContext));
            _categoryGroupPhotoRepository = new Lazy<ICategoryGroupPhotoRepository>(() => new CategoryGroupPhotoRepository(adoContext));
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
=======
            _stockPhotoRepository = new Lazy<IStockPhotoRepository>(() => new StockPhotoRepository(adoContext));
>>>>>>> 2bcfc209a1187bc4a3c11681c798f81f3d140aac
        }

        public IVendorRepository VendorRepository
        {
            get
            {
                if (_vendorRepository == null)
                {
                    _vendorRepository = new VendorRepository(_adoContext);
                }
                return _vendorRepository;
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

        public IPurchaseOrderRepository PurchaseOrderRepository => _purchaseOrderRepository.Value;
        public ICartRepository CartRepository => _cartRepository.Value;
        public IStockPhotoRepository StockPhotoRepository => _stockPhotoRepository.Value;

        public IStockRepository StockRepository
        {
            get
            {
                if (_stockRepository == null)
                {
                    _stockRepository = new StocksRepository(_adoContext);
                }
                return _stockRepository;
            }
        }

        public IStockDetailRepository StockDetailRepository
        {
            get
            {
                if (_stockDetailRepository == null)
                {
                    _stockDetailRepository = new StockDetailRepository(_adoContext);
                }
                return _stockDetailRepository;
            }
        }

        public IVendorProductRepository VendorProductRepository
        {
            get
            {
                if (_vendorProductRepository == null)
                {
                    _vendorProductRepository = new VendorProductRepository(_adoContext);
                }
                return _vendorProductRepository;
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


        public ICategoryGroupRepository CategoryGroupRepository
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
        public ICategoryGroupPhotoRepository CategoryGroupPhotoRepository => _categoryGroupPhotoRepository.Value;

    }
}
