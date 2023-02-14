using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IProvincesRepository
    {
        IEnumerable<Provinces> FindAllProvinces();
        Task<IEnumerable<Provinces>> FindAllProvincesAsync();
        Provinces FindProvincesById(int id);
        void Insert(Provinces provinces);
        void Edit(Provinces provinces);
        void Remove(Provinces provinces);
    }
}
