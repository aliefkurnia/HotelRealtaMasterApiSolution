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
        private Lazy<ICartRepository> _cartRepository;
        private IStockRepository _stockRepository;
        private IStockDetailRepository _stockDetailRepository;
        private Lazy<IStockPhotoRepository> _stockPhotoRepository;
        private IVendorProductRepository _vendorProductRepository;

        public RepositoryManager(AdoDbContext adoContext)
        {
            _adoContext = adoContext;
            _purchaseOrderRepository = new Lazy<IPurchaseOrderRepository>(() => new PurchaseOrderRepository(adoContext));
            _cartRepository = new Lazy<ICartRepository>(() => new CartRepository(adoContext));
            _stockPhotoRepository = new Lazy<IStockPhotoRepository>(() => new StockPhotoRepository(adoContext));
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

    }
}
