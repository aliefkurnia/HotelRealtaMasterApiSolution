using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Realta.Contract.Models;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;
using Realta.Services.Abstraction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public StocksController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var stocks = await _repositoryManager.StockRepository.FindAllStocksAsync();
            return Ok(stocks.ToList());
        }

        [HttpGet("pageList")]
        public async Task<IActionResult> GetStockPaging([FromQuery] StocksParameters stocksParameters)
        {

            var stocks = await _repositoryManager.StockRepository.GetAllStockPaging(stocksParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(stocks.MetaData));

            return Ok(stocks.ToList());
        }

        // GET api/<StocksController>/5
        [HttpGet("{id}", Name = "GetStock")]
        public IActionResult FindStocksById(int id)
        {
            var stock = _repositoryManager.StockRepository.FindStocksById(id);
            if (stock == null)
            {
                _logger.LogError("Stock object sent from client is null");
                return BadRequest("Stock object is null");
            }

            var stockDto = new StocksDto
            {
                StockId = stock.StockId,
                StockName = stock.StockName,
                StockDescription = stock.StockDesc,
                StockQuantity= stock.StockQty,
                StockReorderPoint= stock.StockReorderPoint,
                StockUsed= stock.StockUsed,
                StockScrap = stock.StockScrap,
                StockSize = stock.StockSize,
                StockColor = stock.StockColor,
                StockModifiedDate = stock.StockModifiedDate
            };

            return Ok(stockDto);
        }

        // POST api/<StocksController>
        [HttpPost]
        public IActionResult Post([FromBody] StocksDto stocksDto)
        {
            //1. prevent regionDto from is null
            if (stocksDto == null)
            {
                _logger.LogError("StockDto object sent from client is null");
                return BadRequest("StockDto object is null");
            }

            var stock = new Stocks 
            { 
                StockName = stocksDto.StockName,
                StockDesc = stocksDto.StockDescription,
                StockReorderPoint = stocksDto.StockReorderPoint,
                StockQty = stocksDto.StockQuantity,
                StockUsed = stocksDto.StockUsed,
                StockScrap = stocksDto.StockScrap,
                StockSize = stocksDto.StockSize,
                StockColor = stocksDto.StockColor,
                StockModifiedDate = DateTime.Now
            };

            // post 
            _repositoryManager.StockRepository.Insert(stock);
            stocksDto.StockId = stock.StockId;

            //forward
            return CreatedAtRoute("GetStock", new { id = stock.StockId }, new
            {
                Status = "Success",
                Message = "Data has been create",
                Data = stocksDto
            });
        }

        // PUT api/<StocksController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] StocksDto stocksDto)
        {
            if (stocksDto == null)
            {
                _logger.LogError("StockDto object sent from client is null");
                return BadRequest("StockDto object is null");
            }

            var stock = new Stocks {
                StockId = id,
                StockName = stocksDto.StockName,
                StockDesc = stocksDto.StockDescription,
                StockReorderPoint = stocksDto.StockReorderPoint,
                StockQty = stocksDto.StockQuantity,
                StockUsed = stocksDto.StockUsed,
                StockScrap = stocksDto.StockScrap,
                StockSize = stocksDto.StockSize,
                StockColor = stocksDto.StockColor,
                StockModifiedDate = DateTime.Now
            };

            _repositoryManager.StockRepository.Edit(stock);
            stocksDto.StockId = id;

            return CreatedAtRoute("GetStock", new { id = id }, new
            {
                Status = "Success",
                Message = $"Data from id : {stocksDto.StockId} has been update",
                Data = stocksDto
            });
        }

        // DELETE api/<StocksController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Id object sent from client is null");
                return BadRequest("Id object is null");
            }

            // find region by id
            var stock = _repositoryManager.StockRepository.FindStocksById(id.Value);
            if (stock == null)
            {
                _logger.LogError($"Stocks with id {id} not found");
                return NotFound();
            }

            _repositoryManager.StockRepository.Remove(stock);
            return Ok(new
            {
                Status = "Success",
                Message = "Data Has Been Remove"
            });
        }
    }
}
