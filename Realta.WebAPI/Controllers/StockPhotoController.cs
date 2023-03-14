using Microsoft.AspNetCore.Mvc;
using Realta.Services.Abstraction;
using Realta.Domain.Entities;
using Realta.Domain.Base;
using Realta.Contract.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers
{
    [Route("api/stock_photo")]
    [ApiController]
    public class StockPhotoController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IUtilityService _utilityService;

        public StockPhotoController(IRepositoryManager repositoryManager, ILoggerManager logger, IUtilityService utilityService)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
            _utilityService = utilityService;
        }

        // POST api/<StockPhotoController>
        [HttpPost("{stockId}"), DisableRequestSizeLimit]
        public async Task<IActionResult> Post(int stockId)
        {
            var formColletion = await Request.ReadFormAsync();
            formColletion.TryGetValue("SetPrimary", out var SetPrimary);

            var files = formColletion.Files;

            var allPhotos = new List<IFormFile>();
            foreach (var item in files)
            {
                allPhotos.Add(item);
            }

            var stockPhotoGroupDto = new StockPhotoGroupDto
            {
                AllFoto = allPhotos
            };

            if (stockPhotoGroupDto == null)
            {
                return BadRequest();
            }

            int counter = 0;
            foreach (var itemPhoto in stockPhotoGroupDto.AllFoto)
            {
                var fileName = _utilityService.UploadSingleFile(itemPhoto);
                var stockPhoto = new StockPhoto
                {
                    SphoPhotoFileName = fileName,
                    SphoStockId = stockId
                };

                if (counter == 0)
                {
                    stockPhoto.SphoPrimary = bool.Parse(SetPrimary);
                }
                else
                {
                    stockPhoto.SphoPrimary = false;
                }
                _repositoryManager.StockPhotoRepository.InsertUploadPhoto(stockPhoto);
                counter++;
            }

            return Ok(new
            {
                Status = "Success",
                Message = "Success Upload Foto"
            });

        }

        [HttpGet("{stockId}")]
        public async Task<IActionResult> GetStockPhotoById(int stockId)
        {
            var stockPhoto = await _repositoryManager.StockPhotoRepository.GetAllPhotoByStockId(stockId);

            var stockPhotoDto = stockPhoto.Select(r => new StockPhotoDto
            {
                SphoId = r.SphoId,
                SphoThumbnailFilename = r.SphoThumbnailFilename,
                SphoPhotoFileName = r.SphoPhotoFileName,
                SphoPrimary = r.SphoPrimary,
                SphoUrl = r.SphoUrl,
                StockName = r.StockName
            });

            var respon = new
            {
                Status = "success",
                Data = stockPhotoDto.ToList()
            };

            return Ok(stockPhotoDto.ToList());

        }

        // DELETE api/<StockPhotoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Id object sent from client is null");
                return BadRequest("Id object is null");
            }

            // find region by id
            var stockPhoto = _repositoryManager.StockPhotoRepository.FindStockPhotoById(id.Value);
            if (stockPhoto == null)
            {
                _logger.LogError($"Stock Photo with id {id} not found");
                return NotFound();
            }

            _repositoryManager.StockPhotoRepository.Remove(stockPhoto);
            return Ok(new
            {
                Status = "Success",
                Message = "Data has been remove"
            });
        }

    }
}
