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
        void Insert(StockDetail stockDetail);
        void Edit(StockDetail stockDetail);
        void SwitchStatus(StockDetail stockDetail);
        void Remove(StockDetail stockDetail);
    }
}
