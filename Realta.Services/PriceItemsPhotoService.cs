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
    internal class PriceItemsPhotoService : IPriceItemsPhotoService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IUtilityService _utilityService;

        public PriceItemsPhotoService(IRepositoryManager repositoryManager, IUtilityService utilityService)
        {
            _repositoryManager = repositoryManager;
            _utilityService = utilityService;
        }

        public void InsertPriceItemsAndPriceItemsPhoto(PriceItemsPhotoGroupDto priceItemsPhotoGroupDto, out int pritId)
        {
            //1. insert into table PriceItems
            var priceItems = new PriceItems
            {
                PritName = priceItemsPhotoGroupDto.PriceItemsForCreateDto.PritName,
                PritPrice = priceItemsPhotoGroupDto.PriceItemsForCreateDto.PritPrice,
                PritDescription = priceItemsPhotoGroupDto.PriceItemsForCreateDto.PritDescription,
                PritType = priceItemsPhotoGroupDto.PriceItemsForCreateDto.PritType,
                PritIconUrl = priceItemsPhotoGroupDto.PriceItemsForCreateDto.PritIconUrl,
                PritModifiedDate = priceItemsPhotoGroupDto.PriceItemsForCreateDto.PritModifiedDate,
            };


            //2. insert product to table
            _repositoryManager.PriceItemsRepository.Insert(priceItems);

            //3. if insert product success then get prorductId
            pritId = _repositoryManager.PriceItemsRepository.GetIdSequence();


            //4. extract photos 
            var allPhotos = priceItemsPhotoGroupDto.AllPhotos;


            foreach (var itemPhoto in allPhotos)
            {
                var fileName = _utilityService.UploadSingleFile(itemPhoto);
                var priceItemsPhoto = new PriceItemsPhoto
                {
                    PhotoFilename = fileName,
                    PhotoFileSize = (short?)itemPhoto.Length,
                    PhotoFileType = itemPhoto.ContentType,
                    PhotoOriginalFilename = itemPhoto.FileName,
                    PhotoPrimary = 0,
                    PhotoPriceItemsId = pritId
                };
                _repositoryManager.PriceItemsPhotoRepository.Insert(priceItemsPhoto);
            }




        }
    }
}
