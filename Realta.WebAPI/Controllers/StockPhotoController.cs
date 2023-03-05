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

            foreach (var itemPhoto in stockPhotoGroupDto.AllFoto)
            {
                var fileName = _utilityService.UploadSingleFile(itemPhoto);
                var stockPhoto = new StockPhoto
                {
                    SphoPhotoFileName = fileName,
                    SphoPrimary = 0,
                    SphoStockId = stockId
                };
                _repositoryManager.StockPhotoRepository.InsertUploadPhoto(stockPhoto);
            }

            return Ok("Success Upload Foto");

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
            return Ok("Data has been remove");
        }

    }
}
