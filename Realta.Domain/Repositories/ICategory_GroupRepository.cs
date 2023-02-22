using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface ICategory_GroupRepository
    {
        IEnumerable<Category_Group> FindAllCategory_Group();
        Task<IEnumerable<Category_Group>> FindAllCategory_GroupAsync();
        Category_Group FindCategory_GroupById(int id);
        void Insert(Category_Group category_Group);
        void Edit(Category_Group category_Group);
        void Remove(Category_Group category_Group);
    }
}
