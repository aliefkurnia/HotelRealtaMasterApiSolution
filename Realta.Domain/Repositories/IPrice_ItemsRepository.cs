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
        IEnumerable<Price_Items> FindAllPrice_Items();
        Task<IEnumerable<Price_Items>> FindAllPrice_ItemsAsync();
        Price_Items FindPrice_ItemsById(int id);
        void Insert(Price_Items price_Items);
        void Edit(Price_Items price_Items);
        void Remove(Price_Items price_Items);
    }
}
