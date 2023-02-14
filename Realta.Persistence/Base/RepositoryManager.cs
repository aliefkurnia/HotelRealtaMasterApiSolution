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
        private AdoDbContext _adoContext;
        private IVendorRepository _vendorRepository;
        private IVendorProductRepository _vendorProductRepository;

        public RepositoryManager(AdoDbContext adoContext)
        {
            _adoContext = adoContext;
        }

        public IVendorRepository VendorRepository 
        { get 
            { 
                if (_vendorRepository == null)
                {
                    _vendorRepository = new VendorRepository(_adoContext);
                }
                return _vendorRepository;
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
