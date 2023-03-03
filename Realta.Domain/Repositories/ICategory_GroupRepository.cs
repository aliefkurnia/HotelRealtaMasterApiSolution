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
        IEnumerable<CategoryGroup> FindAllCategoryGroup();
        Task<IEnumerable<CategoryGroup>> FindAllCategoryGroupAsync();
        CategoryGroup FindCategoryGroupById(int id);
        void Insert(CategoryGroup categoryGroup);
        void Edit(CategoryGroup categoryGroup);
        void Remove(CategoryGroup categoryGroup);
    }
}
