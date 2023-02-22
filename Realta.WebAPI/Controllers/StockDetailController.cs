using Microsoft.AspNetCore.Mvc;
using Realta.Contract;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Services.Abstraction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers
{
    [Route("api/stock_detail")]
    [ApiController]
    public class StockDetailController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public StockDetailController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        // GET: api/<StockDetailController>
        [HttpGet]
        public IActionResult Get()
        {
            var stockDetail = _repositoryManager.StockDetailRepository.FindAllStockDetail().ToList();

            var stocksDetailDto = stockDetail.Select(r => new StockDetailDto
            {
                stod_stock_id = r.stod_stock_id,
                stod_id = r.stod_id,
                stod_barcode_number = r.stod_barcode_number,
                stod_status = r.stod_status,
                stod_notes = r.stod_notes,
                stod_faci_id = r.stod_faci_id,
                stod_pohe_id = r.stod_pohe_id
            });

            return Ok(stocksDetailDto);
        }

        // GET api/<StockDetailController>/5
        [HttpGet("{id}", Name = "GetStockDetail")]
        public IActionResult FindStockDetailById(int id)
        {
            var stockDetail = _repositoryManager.StockDetailRepository.FindStockDetailById(id);
            if (stockDetail == null)
            {
                _logger.LogError("Stock Detail object sent from client is null");
                return BadRequest("Stock Detail object is null");
            }

            var stockDetailDto = new StockDetailDto
            {
                stod_stock_id = stockDetail.stod_stock_id,
                stod_id=stockDetail.stod_id,
                stod_barcode_number=stockDetail.stod_barcode_number,
                stod_status=stockDetail.stod_status,
                stod_notes=stockDetail.stod_notes,
                stod_faci_id=stockDetail.stod_faci_id,
                stod_pohe_id=stockDetail.stod_pohe_id
            };

            return Ok(stockDetailDto);
        }

        // POST api/<StockDetailController>
        [HttpPost]
        public IActionResult Post([FromBody] StockDetailDto stockDetailDto)
        {
            //1. prevent regionDto from is null
            if (stockDetailDto == null)
            {
                _logger.LogError("StockDetailDto object sent from client is null");
                return BadRequest("StockDetailDto object is null");
            }

            var stockDetail = new StockDetail
            {
                stod_stock_id = stockDetailDto.stod_stock_id,
                stod_barcode_number = stockDetailDto.stod_barcode_number,
                stod_status = stockDetailDto.stod_status,
                stod_notes = stockDetailDto.stod_notes,
                stod_faci_id = stockDetailDto.stod_faci_id,
                stod_pohe_id = stockDetailDto.stod_pohe_id
            };

            // post 
            _repositoryManager.StockDetailRepository.Insert(stockDetail);
            stockDetailDto.stod_id = stockDetail.stod_id;

            //forward
            return CreatedAtRoute("GetStockDetail", new { id = stockDetail.stod_id }, stockDetailDto);
        }

        // PUT api/<StockDetailController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] StockDetailDto stockDetailDto)
        {
            if (stockDetailDto == null)
            {
                _logger.LogError("StockDetailDto object sent from client is null");
                return BadRequest("StockDetailDto object is null");
            }

            var stockDetail = new StockDetail
            {
                stod_id = id,
                stod_stock_id = stockDetailDto.stod_stock_id,
                stod_barcode_number= stockDetailDto.stod_barcode_number,
                stod_status= stockDetailDto.stod_status,
                stod_notes= stockDetailDto.stod_notes,
                stod_faci_id= stockDetailDto.stod_faci_id,
                stod_pohe_id= stockDetailDto.stod_pohe_id
            };

            // post 
            _repositoryManager.StockDetailRepository.Edit(stockDetail);
            stockDetailDto.stod_id = stockDetail.stod_id;

            //forward
            return CreatedAtRoute("GetStockDetail", new { id = stockDetailDto.stod_id }, stockDetailDto);
        }

        // DELETE api/<StockDetailController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Id object sent from client is null");
                return BadRequest("Id object is null");
            }

            // find region by id
            var stockDetail = _repositoryManager.StockDetailRepository.FindStockDetailById(id.Value);
            if (stockDetail == null)
            {
                _logger.LogError($"Stock Photo with id {id} not found");
                return NotFound();
            }

            _repositoryManager.StockDetailRepository.Remove(stockDetail);
            return Ok("Data has been remove");
        }

        [HttpGet, Route("findAllAsync")]
        public async Task<IActionResult> GetAsync()
        {
            var stockPhotos = await _repositoryManager.StockDetailRepository.FindAllStockDetailAsync();
            return Ok(stockPhotos.ToList());
        }

        [HttpPut, Route("switchStatus")]
        public IActionResult EditStatus(int id, [FromBody] StockDetailDto stockDetailDto)
        {
            if (stockDetailDto == null)
            {
                _logger.LogError("StockPhotoDto object sent from client is null");
                return BadRequest("StockPotoDto object is null");
            }

            var stockDetail = new StockDetail
            {
                stod_id = id,
                stod_stock_id = stockDetailDto.stod_stock_id,
                stod_status = stockDetailDto.stod_status,
                stod_faci_id = stockDetailDto.stod_faci_id
            };

            _repositoryManager.StockDetailRepository.Edit(stockDetail);
            stockDetailDto.stod_id = id;

            return CreatedAtRoute("GetStockDetail", new { id = id }, stockDetailDto);
        }
    }
}
