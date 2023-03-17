using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface ICategoryGroupRepository
    {
        IEnumerable<CategoryGroup> FindAllCategoryGroup();
        Task<IEnumerable<CategoryGroup>> FindAllCategoryGroupAsync();
        CategoryGroup FindCategoryGroupById(int id);
        void Insert(CategoryGroup categoryGroup);
        void Edit(CategoryGroup categoryGroup);
        void Remove(CategoryGroup categoryGroup);
        int GetIdSequence();
        Task<PagedList<CategoryGroup>> GetCategoryGroupPageList(CategoryGroupParameter categoryGroupParameter);

    }
}
