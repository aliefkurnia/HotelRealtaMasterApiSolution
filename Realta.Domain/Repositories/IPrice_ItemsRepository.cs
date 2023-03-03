using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IPrice_ItemsRepository
    {
        IEnumerable<PriceItems> FindAllPriceItems();
        Task<IEnumerable<PriceItems>> FindAllPriceItemsAsync();
        PriceItems FindPriceItemsById(int id);
        IEnumerable<PriceItems> FindPriceItemsByName(string name);
        void Insert(PriceItems priceItems);
        void Edit(PriceItems priceItems);
        void Remove(PriceItems priceItems);
    }
}
