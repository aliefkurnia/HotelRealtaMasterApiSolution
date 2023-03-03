using Microsoft.AspNetCore.Mvc;
using Realta.Contract.Models;
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
                StodStockId = r.StodStockId,
                StodId = r.StodId,
                StodBarcodeNumber = r.StodBarcodeNumber,
                StodStatus = r.StodStatus,
                StodNotes = r.StodNotes,
                StodFaciId = r.StodFaciId,
                StodPoNumber = _repositoryManager.PurchaseOrderRepository.FindById(r.StodPoheId.Value).PoheNumber
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
                StodStockId = stockDetail.StodStockId,
                StodId =stockDetail.StodId,
                StodBarcodeNumber=stockDetail.StodBarcodeNumber,
                StodStatus=stockDetail.StodStatus,
                StodNotes=stockDetail.StodNotes,
                StodFaciId=stockDetail.StodFaciId,
                StodPoNumber=_repositoryManager.PurchaseOrderRepository.FindById(stockDetail.StodPoheId.Value).PoheNumber
            };

            return Ok(stockDetailDto);
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

        [HttpPut("switchStatus/{id}")]
        public IActionResult EditStatus(int id, [FromBody] StockDetailDto stockDetailDto)
        {
            if (stockDetailDto == null)
            {
                _logger.LogError("StockPhotoDto object sent from client is null");
                return BadRequest("StockPotoDto object is null");
            }

            var stockDetail = new StockDetail
            {
                StodId = id,
                StodStockId = stockDetailDto.StodStockId,
                StodStatus = stockDetailDto.StodStatus,
                StodNotes = stockDetailDto.StodNotes,
                StodFaciId = stockDetailDto.StodFaciId
            };

            _repositoryManager.StockDetailRepository.SwitchStatus(stockDetail);
            var stockDetailStatus = _repositoryManager.StockDetailRepository.FindStockDetailById(id);

            return Ok(stockDetailStatus);
        }
    }
}
