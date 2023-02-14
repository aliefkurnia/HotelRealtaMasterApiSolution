using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IStockRepository
    {
        IEnumerable<Stocks> FindAllStocks();
        Task<IEnumerable<Stocks>> FindAllStocksAsync();
        Stocks FindStocksById(int id);
        void Insert(Stocks stocks);
        void Edit(Stocks stocks);
        void Remove(Stocks stocks);
    }
}
