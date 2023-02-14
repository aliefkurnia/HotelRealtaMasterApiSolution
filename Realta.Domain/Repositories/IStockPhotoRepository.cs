using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IStockPhotoRepository
    {
        IEnumerable<StockPhoto> FindAllStockPhoto();
        Task<IEnumerable<StockPhoto>> FindAllStockPhotoAsync();
        StockPhoto FindStockPhotoById(int id);
        void Insert(StockPhoto stockPhoto);
        void Edit(StockPhoto stockPhoto);
        void Remove(StockPhoto stockPhoto);
    }
}
