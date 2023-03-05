using Realta.Domain.Base;
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
        private IStockRepository _stockRepository;
        private IStockDetailRepository _stockDetailRepository;
        private IStockPhotoRepository _stockPhotoRepository;
        private IVendorProductRepository _vendorProductRepository;

        public RepositoryManager(AdoDbContext adoContext)
        {
            _adoContext = adoContext;
            _purchaseOrderRepository = new Lazy<IPurchaseOrderRepository>(() => new PurchaseOrderRepository(adoContext));
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

        public IPurchaseOrderRepository PurchaseOrderRepository => _purchaseOrderRepository.Value;   

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
        public IStockPhotoRepository StockPhotoRepository
        {
            get
            {
                if (_stockPhotoRepository == null)
                {
                    _stockPhotoRepository = new StockPhotoRepository(_adoContext);
                }
                return _stockPhotoRepository;
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
    }
}
