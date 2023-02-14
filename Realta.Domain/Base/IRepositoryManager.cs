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
        IVendorRepository VendorRepository { get; }
        IPurchaseOrderHeaderRepository PurchaseOrderHeaderRepository { get; }
        IPurchaseOrderDetailRepository PurchaseOrderDetailRepository { get; }
    }
}
