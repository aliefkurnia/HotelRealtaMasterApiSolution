using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IPriceItemsPhotoRepository
    {
        IEnumerable<PriceItemsPhoto> FindAllPriceItemsPhoto();
        Task<IEnumerable<PriceItemsPhoto>> FindAllPriceItemsPhotoAsync();
        PriceItemsPhoto FindOrderById(int id);
        void Insert(PriceItemsPhoto priceItemsPhoto);
        void Edit(PriceItemsPhoto priceItemsPhoto);
        void Remove(PriceItemsPhoto priceItemsPhoto);
    }
}
