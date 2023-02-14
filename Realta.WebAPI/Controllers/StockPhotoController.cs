using Microsoft.AspNetCore.Mvc;
using Realta.Services.Abstraction;
using Realta.Domain.Entities;
using Realta.Domain.Base;
using Realta.Contract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers
{
    [Route("api/stock_photo")]
    [ApiController]
    public class StockPhotoController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public StockPhotoController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        // GET: api/<StockPhotoController>
        [HttpGet]
        public IActionResult Get()
        {
            var stockPhoto = _repositoryManager.StockPhotoRepository.FindAllStockPhoto().ToList();

            var stocksPhotoDto = stockPhoto.Select(r => new StockPhotoDto
            {
                spho_id = r.spho_id,
                spho_thumbnail_filename = r.spho_thumbnail_filename,
                spho_photo_filename = r.spho_photo_filename,
                spho_primary = r.spho_primary,
                spho_url = r.spho_url,  
                spho_stock_id = r.spho_stock_id
            });

            return Ok(stockPhoto);
        }

        // GET api/<StockPhotoController>/5
        [HttpGet("{id}", Name = "GetStockPhoto")]
        public IActionResult FindAllStockPhotoById(int id)
        {
            var stockPhoto = _repositoryManager.StockPhotoRepository.FindStockPhotoById(id);
            if (stockPhoto == null)
            {
                _logger.LogError("Stock Photo object sent from client is null");
                return BadRequest("Stock Photo object is null");
            }

            var stockPhotoDto = new StockPhotoDto
            {
                spho_id=stockPhoto.spho_id,
                spho_thumbnail_filename=stockPhoto.spho_thumbnail_filename,
                spho_photo_filename=stockPhoto.spho_photo_filename,
                spho_primary=stockPhoto.spho_primary,
                spho_url=stockPhoto.spho_url,
                spho_stock_id=stockPhoto.spho_stock_id
            };

            return Ok(stockPhotoDto);
        }
        
        // POST api/<StockPhotoController>
        [HttpPost]
        public IActionResult Post([FromBody] StockPhotoDto stockPhotoDto)
        {
            //1. prevent regionDto from is null
            if (stockPhotoDto == null)
            {
                _logger.LogError("StockPhotoDto object sent from client is null");
                return BadRequest("StockPotoDto object is null");
            }

            var stockPhoto = new StockPhoto
            {
                spho_thumbnail_filename = stockPhotoDto.spho_thumbnail_filename,
                spho_photo_filename = stockPhotoDto.spho_photo_filename,
                spho_primary = stockPhotoDto.spho_primary,
                spho_url = stockPhotoDto.spho_url,
                spho_stock_id = stockPhotoDto.spho_stock_id
            };

            // post 
            _repositoryManager.StockPhotoRepository.Insert(stockPhoto);

            stockPhotoDto.spho_id = stockPhoto.spho_id;

            //forward
            return CreatedAtRoute("GetStockPhoto", new { id = stockPhoto.spho_id }, stockPhotoDto);
        }

        // PUT api/<StockPhotoController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] StockPhotoDto stockPhotoDto)
        {
            if (stockPhotoDto == null)
            {
                _logger.LogError("StockPhotoDto object sent from client is null");
                return BadRequest("StockPotoDto object is null");
            }

            var stockPhoto = new StockPhoto
            {
                spho_id = id,
                spho_thumbnail_filename = stockPhotoDto.spho_thumbnail_filename,
                spho_photo_filename = stockPhotoDto.spho_photo_filename,
                spho_primary = stockPhotoDto.spho_primary,
                spho_url = stockPhotoDto.spho_url,
                spho_stock_id = stockPhotoDto.spho_stock_id
            };

            _repositoryManager.StockPhotoRepository.Edit(stockPhoto);
            stockPhotoDto.spho_id = id;

            return CreatedAtRoute("GetStockPhoto", new { id = id }, stockPhotoDto);
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

        // GET: api/<ProductController>
        [HttpGet, Route("findAllAsync")]
        public async Task<IActionResult> GetAsync()
        {
            var stockPhotos = await _repositoryManager.StockPhotoRepository.FindAllStockPhotoAsync();
            return Ok(stockPhotos.ToList());
        }
    }
}
