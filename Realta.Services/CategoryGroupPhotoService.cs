using Realta.Contract.Models;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Services
{
    internal class CategoryGroupPhotoService : ICategoryGroupPhotoService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IUtilityService _utilityService;

        public CategoryGroupPhotoService(IRepositoryManager repositoryManager, IUtilityService utilityService)
        {
            _repositoryManager = repositoryManager;
            _utilityService = utilityService;
        }

        public void InsertCategoryGroupAndCategoryGroupPhoto(CategoryGroupPhotoGroupDto categoryGroupPhotoGroupDto, out int cagroId)
        {
            //1. insert into table CategoryGroup
            var cagro = new CategoryGroup
            {
                CagroName = categoryGroupPhotoGroupDto.CategoryGroupCreateDto.CagroName,
                CagroDescription = categoryGroupPhotoGroupDto.CategoryGroupCreateDto.CagroDescription,
                CagroType = categoryGroupPhotoGroupDto.CategoryGroupCreateDto.CagroType,
                CagroIcon = categoryGroupPhotoGroupDto.CategoryGroupCreateDto.CagroIcon,
                CagroIconUrl = categoryGroupPhotoGroupDto.CategoryGroupCreateDto.CagroIconUrl,
            };


            //2. insert product to table
            _repositoryManager.CategoryGroupRepository.Insert(cagro);

            //3. if insert product success then get cagroId
            cagroId = _repositoryManager.CategoryGroupRepository.GetIdSequence();


            //4. extract photos 
            var allPhotos = categoryGroupPhotoGroupDto.AllPhotos;


            foreach (var itemPhoto in allPhotos)
            {
                var fileName = _utilityService.UploadSingleFile(itemPhoto);
                var categoryGroupPhoto = new CategoryGroupPhoto
                {
                    PhotoFilename = fileName,
                    PhotoFileSize = (short?)itemPhoto.Length,
                    PhotoFileType = itemPhoto.ContentType,
                    PhotoOriginalFilename = itemPhoto.FileName,
                    PhotoPrimary = 1,
                    PhotoCategoryGroupId = cagroId
                };
                _repositoryManager.CategoryGroupPhotoRepository.Insert(categoryGroupPhoto);
            }
        }
    }
}
