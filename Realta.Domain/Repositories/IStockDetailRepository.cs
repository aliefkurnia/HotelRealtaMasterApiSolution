using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IStockDetailRepository
    {
        IEnumerable<StockDetail> FindAllStockDetail();
        Task<IEnumerable<StockDetail>> FindAllStockDetailAsync();
        StockDetail FindStockDetailById(int id);
        void SwitchStatus(StockDetail stockDetail);
        void Remove(StockDetail stockDetail);
    }
}
