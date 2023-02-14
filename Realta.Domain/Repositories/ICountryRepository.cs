using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface ICountryRepository
    {
        IEnumerable<Country> FindAllCountry();
        Task<IEnumerable<Country>> FindAllCountryAsync();
        Country FindCountryById(int id);
        void Insert(Country Country);
        void Edit(Country Country);
        void Remove(Country Country);
    }
}
