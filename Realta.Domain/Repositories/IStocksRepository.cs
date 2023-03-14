using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IStockRepository
    {
        Task<IEnumerable<Stocks>> FindAllStocksAsync();
        Task<PagedList<Stocks>> GetAllStockPaging(StocksParameters stocksParameters);
        Stocks FindStocksById(int id);
        void Insert(Stocks stocks);
        void Edit(Stocks stocks);
        void Remove(Stocks stocks);
    }
}
