using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IStockDetailRepository
    {
        Task<IEnumerable<StockDetail>> FindAllStockDetailByStockId(int stockId);
        StockDetail FindStockDetailById(int id);
        void SwitchStatus(StockDetail stockDetail);
        void GenerateBarcodePO(PurchaseOrderDetail purchaseOrderDetail);

    }
}
