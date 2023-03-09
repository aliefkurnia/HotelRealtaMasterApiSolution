using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realta.Domain.RequestFeatures;

namespace Realta.Domain.Repositories
{
    public interface IPriceItemsRepository
    {
        IEnumerable<PriceItems> FindAllPriceItems();
        Task<IEnumerable<PriceItems>> FindAllPriceItemsAsync();
        PriceItems FindPriceItemsById(int id);
        IEnumerable<PriceItems> FindPriceItemsByName(string name);
        void Insert(PriceItems priceItems);
        void Edit(PriceItems priceItems);
        void Remove(PriceItems priceItems);
        int GetIdSequence();
        Task<IEnumerable<PriceItems>> GetPriceItemsPaging(PriceItemsParameters priceItemsParameters);
        Task<PagedList<PriceItems>> GetPriceItemsPageList(PriceItemsParameters priceItemsParameters);

    }
}
