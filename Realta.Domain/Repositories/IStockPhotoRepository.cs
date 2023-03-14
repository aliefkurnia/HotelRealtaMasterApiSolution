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
        StockPhoto FindStockPhotoById(int id);
        Task<IEnumerable<StockPhoto>> GetAllPhotoByStockId(int stockId);
        void InsertUploadPhoto(StockPhoto stockPhoto);
        void Remove(StockPhoto stockPhoto);
    }
}
