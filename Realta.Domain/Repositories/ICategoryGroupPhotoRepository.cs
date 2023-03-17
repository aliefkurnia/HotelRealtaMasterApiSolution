using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Repositories
{
    public interface ICategoryGroupPhotoRepository
    {
        IEnumerable<CategoryGroupPhoto> FindAllCategoryGroupPhoto();
        Task<IEnumerable<CategoryGroupPhoto>> FindAllCategoryGroupPhotoAsync();
        CategoryGroupPhoto FindOrderById(int id);
        void Insert(CategoryGroupPhoto categoryGroupPhoto);
        void Edit(CategoryGroupPhoto categoryGroupPhoto);
        void Remove(CategoryGroupPhoto categoryGroupPhoto);
    }
}
