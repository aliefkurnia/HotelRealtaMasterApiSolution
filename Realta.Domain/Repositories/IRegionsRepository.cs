using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface IRegionsRepository
    {
        IEnumerable<Regions> FindAllRegions();
        Task<IEnumerable<Regions>> FindAllRegionsAsync();
        Regions FindRegionsById(int id);
        void Insert(Regions regions);
        void Edit(Regions regions);
        void Remove(Regions regions);
    }
}
