using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realta.Contract.Models;

namespace Realta.Services.Abstraction
{
    public interface ICategoryGroupPhotoService
    {
        public void InsertCategoryGroupAndCategoryGroupPhoto(CategoryGroupPhotoGroupDto categoryGroupPhotoGroupDto, out int cagroId);
    }
}
